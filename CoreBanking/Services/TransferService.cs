using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.DTOs;
using CoreBanking.Entites;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Services
{
    public class TransferService : ITransferService
    {
        private readonly BankContext _bankContext;
        private readonly IReferenceSequenceService _referenceSequenceService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IAuditService _auditService;
        public TransferService(BankContext bankContext, IReferenceSequenceService referenceSequenceService, IAccountService accountService, ITransactionService transactionService, IAuditService auditService)
        {
            _bankContext = bankContext;
            _referenceSequenceService = referenceSequenceService;
            _accountService = accountService;
            _transactionService = transactionService;
            _auditService = auditService;
        }
        public async Task<string> GenerateReferenceAsync()
        {
            var today = DateTime.UtcNow.Date;

            var sequence = await _referenceSequenceService.GetByDateAsync(today);

            if (sequence == null)
            {
                sequence = new ReferenceSequence
                {
                    Date = today,
                    LastNumber = 1
                };
                await _referenceSequenceService.AddAsync(sequence);
            }
            else
            {
                sequence.LastNumber++;
                await _referenceSequenceService.UpdateAsync(sequence);
            }

            return $"TRF-{today:yyyyMMdd}-{sequence.LastNumber:D6}";
        }

        public async Task<GenericResponse> TransferAsync(TransferRequestDto dto, Guid customerId)
        {
            var response = new GenericResponse();
            
            var source = await _accountService.GetByAccountNumberAsync(dto.SourceAccountNumber);
            var destination = await _accountService.GetByAccountNumberAsync(dto.DestinationAccountNumber);

            if (source == null || destination == null)
            {
                response.success = false;
                response.message = "Invalid account(s)";
                return response;
            }
                
            // Ownership check: customer can only debit their own accounts
            if (source.CustomerId != customerId)
            {
                response.success = false;
                response.message = "You do not own the source account";
                return response;
            }

            if (source.Balance < dto.Amount)
            {
                response.success = false;
                response.message = "Insufficient funds";
                return response;
            }

            await using var transaction = await _bankContext.Database.BeginTransactionAsync();
            var reference = await GenerateReferenceAsync();
            try
            {
                var transfer = new Transfer
                {
                    
                    Reference = reference,
                    FromAccountId = source.Id,
                    ToAccountId = destination.Id,
                    Amount = dto.Amount,
                    CreatedAt = DateTime.UtcNow
                };
                var newTransfer = await _bankContext.Transfers.AddAsync(transfer);
                await _bankContext.SaveChangesAsync();
                //var transferEntity = 

                var ledgers = new[]
                {
                    new Transaction
                    {
                       
                        AccountId = source.Id,
                        TransferId = newTransfer.Entity.Id,
                        Amount = -dto.Amount,
                        TransactionType = "Debit",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Transaction
                    {
                        
                        AccountId = destination.Id,
                        TransferId = newTransfer.Entity.Id,
                        Amount = dto.Amount,
                        TransactionType = "Credit",
                        CreatedAt = DateTime.UtcNow
                    }
                };
                await _transactionService.AddRangeAsync(ledgers);

                // Update balances
                source.Balance -= dto.Amount;
                destination.Balance += dto.Amount;

                await _accountService.UpdateAsync(source);
                await _accountService.UpdateAsync(destination);
                newTransfer.Entity.Status = "Successful";

                await _auditService.LogAsync(customerId, "FUND TRANSFER", "Transfer", newTransfer.Entity.Id, $"Transferred {dto.Amount} from {source.AccountNumber} to {destination.AccountNumber}");
                // Commit
                await _bankContext.SaveChangesAsync();
                await transaction.CommitAsync();

                response.success = true;
                response.data = new TransferResponseDto
                {
                    Amount = dto.Amount,
                    Reference = reference,
                    SourceAccount = source.AccountNumber,
                    DestinationAccount = destination.AccountNumber,
                };

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}

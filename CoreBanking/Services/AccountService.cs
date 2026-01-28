using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.DTOs;
using CoreBanking.Entites;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITransactionService _transactionService;
        private readonly BankContext _bankContext;
        public AccountService(BankContext bankContext, ITransactionService transactionService)
        {
            _bankContext = bankContext;
            _transactionService = transactionService;
        }
        public async Task<GenericResponse> CreateAccount(AccountCreationRequest request, Guid customerId)
        {
            var response = new GenericResponse();

            var customer = _bankContext.Customers.Where(c=>c.Id == customerId).FirstOrDefault();
            if(customer == null)
            {
                response.success = false;
                response.message = "Customer not found";
                response.status = 400;
                response.data = null;
                return response;
            }

            var newAccount = new Account
            {
                CustomerId = customerId,
                AccountNumber = customer.PhoneNumber[1..],
                Balance = request.amount,
                AccountType = request.accountType,
                CreatedAt = DateTime.UtcNow,
                Status = "Active"
            };
            var addedAccount  = await _bankContext.Accounts.AddAsync(newAccount);
            await _bankContext.SaveChangesAsync();

            if (request.amount > 0)
            {
                var transaction = new Transaction
                {

                    AccountId = addedAccount.Entity.Id,
                    //TransferId = newTransfer.Entity.Id,
                    Amount = request.amount,
                    TransactionType = "Credit",
                    CreatedAt = DateTime.UtcNow
                };

                await _bankContext.Transactions.AddAsync(transaction);
                await _bankContext.SaveChangesAsync();
            }

            response.success = true;
            response.message = "Account created sucessfully";
            response.status = 201;
            response.data = addedAccount.Entity;

            return response;
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            var account = await _bankContext.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
            return account;
        }

        public async Task<GenericResponse> GetStatementAsync(string accountNumber, DateTime from, DateTime to, Guid customerId)
        {
            var response = new GenericResponse();
            var account = await GetByAccountNumberAsync(accountNumber);

            if (account == null || account.CustomerId != customerId)
            {
                response.success = false;
                response.message = "UnAuthorized";
                response.data = null;

                return response;
            }

            var openingBalance = await _transactionService.GetBalanceBeforeAsync(account.Id, from);

            var entries = await _transactionService.GetEntriesAsync(account.Id, from, to);

            decimal runningBalance = openingBalance;

            var lines = entries
            .OrderBy(e => e.CreatedAt)
            .Select(e =>
            {
                runningBalance += e.Amount;

                return new StatementLineDto
                {
                    Date = e.CreatedAt,
                    Reference = e.Transfer?.Reference,
                    Debit = e.Amount < 0 ? Math.Abs(e.Amount) : 0,
                    Credit = e.Amount > 0 ? e.Amount : 0,
                    Balance = runningBalance
                };
            })
            .ToList();

            response.success = true;
            response.message = "Statement retrieved successfully";
            response.data = new AccountStatementDto
            {
                AccountNumber = account.AccountNumber,
                OpeningBalance = openingBalance,
                ClosingBalance = runningBalance,
                Transactions = lines
            };

            return response;
        }

        public async Task UpdateAsync(Account account)
        {
            _bankContext.Accounts.Update(account);
            await _bankContext.SaveChangesAsync();
        }
    }
}

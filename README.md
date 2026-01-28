# 🏦 Core Banking Transaction & Reporting System

## Overview

This project is a **Core Banking Transaction and Reporting System** built with **C# / ASP.NET Core / Entity Framework Core**, designed to demonstrate **real-world banking backend concepts** such as double-entry accounting, transactional safety, idempotent transfers, audit logging, and statement generation.

The system models how modern banking platforms process money movement while ensuring **data integrity, auditability, and security**.

---

## ✨ Key Features

### ✅ Account Management
- Secure account creation for authenticated customers
- System-generated **unique 10-digit account numbers**
- Account status management (Active, Suspended, etc.)

### 💸 Funds Transfer Engine
- Atomic money transfers using database transactions
- Double-entry accounting (Debit & Credit ledger entries)
- Idempotent transfers using unique references
- Balance validation (prevents overdrafts)
- Ownership validation (customers can only debit their own accounts)

### 📒 Ledger & Accounting
- Immutable ledger entries as the source of truth
- Separation of business transactions from accounting records
- Ledger-driven balance calculations

### 📄 Bank Statements & Transaction History
- Statement generation for any date range
- Opening balance, running balance, and closing balance
- Debit / Credit breakdown per transaction
- Statements derived from ledger entries (not balances)

### 🧾 Audit Logging
- Append-only audit log
- Captures:
  - Who performed the action
  - What action occurred
  - Which entity was affected
  - Timestamp 
- Designed for compliance and traceability

### 🔐 Security
- JWT-based authentication
- Customer identity enforced at service level
- Authorization checks for sensitive operations

---

## 🧠 Key Design Decisions

### 🔁 Double-Entry Accounting
Every transfer creates:
- One **Debit** ledger entry
- One **Credit** ledger entry

This ensures accounting accuracy and full traceability.

---

### 🔒 Transaction Safety
All transfers execute inside a **single database transaction**, covering:
- Transfer record creation
- Ledger entries
- Balance updates
- Reference generation

If any step fails, the entire operation is rolled back.

---

### 🆔 Transfer Reference Generation
Transfers use **server-generated, human-readable references**:

References are:
- Unique
- Sequential per day
- Generated inside the transaction to avoid race conditions

---

### ♻️ Idempotency
- Unique database constraint on transfer reference
- Prevents duplicate processing during retries
- Database acts as the final authority on uniqueness

---

### 📊 Statement Generation Strategy
- Statements are generated from **ledger entries**
- Balances are projections, not the source of truth
- Guarantees correctness even under concurrency

---

## 🛠 Technologies Used

- **C# / .NET**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Fluent API (EF Core)**
- **Repository & Unit of Work Pattern**

---

## 🔌 API Endpoints (Sample)

### Create Account

**POST** `/api/accounts`
#### Request Body
```json
{
    
    "accountType": "Savings",
    "amount": "10000"
}

### Transfer Funds

**POST** `/api/transfers`
Authorization: Bearer {token}
```json

    
    "SourceAccountNumber": "9098765432",
    "DestinationAccountNumber": "9041107824",
    "amount": "1000"
}

### Get Account Statement

**GET** `/api/accounts/{accountNumber}/statement?from=2026-01-01&to=2026-01-31`
Authorization: Bearer {token}

## What This Project Demonstrates

- Real-world banking domain knowledge

- Safe handling of financial transactions

- Strong understanding of data integrity and concurrency

- Clean separation of concerns

- Enterprise-grade backend design patterns

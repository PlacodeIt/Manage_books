# 📚 Manage Books – C# Library Management Series

## 🧩 Overview
This repository contains a learning series of C# console applications that progressively build a **Library Management System**.  
Each folder represents a different development stage — from basic object-oriented design to a complete database-connected implementation.

---

## 🏗️ Project Structure

| Folder | Description |
|--------|--------------|
| **Book2plus** | OOP-based version using in-memory data structures (`Dictionary`, `HashSet`) and enums. |
| **Book2edition_db** *(or Book23)* | Advanced version using **SQL Server (ADO.NET)** for persistent storage. |

---

## 🧠 Summary of Each Version

### 🔹 **Book2plus – In-Memory (OOP) Edition**
- Manages **books (digital/paper)** and **subscribers** entirely in memory.  
- Features:
  - Add, borrow, return books.
  - Manage subscribers (up to 3 loans).
  - Search books by title prefix (Regex).
  - View books by genre or subscriber’s borrowed list.
- Demonstrates:
  - **OOP principles:** inheritance, encapsulation, polymorphism.
  - **Collections:** `Dictionary`, `HashSet`, `List`.
  - **Enums:** menu control using an `Operation` enum.
- Ideal for showcasing clean, modular console-based design.

---

### 🔹 **Book2edition_db – Database Edition**
- Extends the same system with a **SQL Server backend** using ADO.NET.  
- Features:
  - Persistent data stored in `books` and `subscribers` tables.
  - Borrow/return operations update the database dynamically.
  - CRUD operations for both entities with parameterized SQL commands.
  - Menu divided into: *Manage Library*, *Borrow/Return*, *Information*.
- Demonstrates:
  - **Database integration** via `SqlConnection`, `SqlCommand`, and `SqlDataAdapter`.
  - **Data persistence**, CRUD logic, and table seeding.
  - Understanding of SQL schema design and C# data handling.

---

## ⚙️ Technologies Used
- **C# (.NET)** – Core programming language  
- **ADO.NET** – Database connectivity (in the DB version)  
- **SQL Server Express** – Local database  
- **Collections:** `Dictionary`, `HashSet`, `List`  
- **Regex** – Book search by title prefix

---

# 📚 Student Management System (Multi-Tenant)

This project is a backend application built using **ASP.NET Core** with **Clean Architecture** principles.
It manages student data with a **multi-tenant approach**, allowing data separation between different tenants.

---

## 🚀 Features

* 🧑‍🎓 Student CRUD (Create, Read, Update, Delete)
* 🏢 Multi-tenant support
* 🗄️ Database migration using Entity Framework Core
* 🧱 Clean Architecture (Domain, Application, Infrastructure, Web)
* 🔐 Scalable and maintainable structure

---

## 🏗️ Architecture

This project follows **Clean Architecture**:

* **Domain** → Entities & business rules
* **Application** → Use cases & interfaces
* **Infrastructure** → Database & external services
* **Web** → API / UI layer

---

## 🛠️ Tech Stack

* ASP.NET Core
* Entity Framework Core
* SQL Server
* C#

---

## ⚙️ Getting Started

### 1. Clone repository

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
```

---

### 2. Setup Database

Update connection string in:

```bash
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 3. Run Migration

```bash
dotnet ef database update
```

---

### 4. Run Application

```bash
dotnet run
```

---

## 🔄 Migration Workflow

Every time you change the model:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

## 🧠 Key Learnings

* Understanding how Entity Framework Core handles **design-time vs runtime**
* Implementing **DbContextFactory** for migrations
* Structuring scalable backend using Clean Architecture
* Managing database schema with migrations

---

## 📌 Notes

* This project is part of my learning journey as a backend developer
* Focused on improving database design, architecture, and performance

---

## 📬 Contact

If you have any feedback or suggestions, feel free to connect!

---

⭐ Don't forget to give this repo a star if you find it useful!

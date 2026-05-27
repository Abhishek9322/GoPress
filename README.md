# GoPress 
GoPress is an Online Cloth Pressing and Delivery Management System built using ASP.NET Core Clean Architecture with CQRS Pattern and JWT Authentication.

The system allows customers to request cloth pressing services, shop owners to manage orders, and delivery boys to handle pickup and delivery operations.
--------------
# Features
##  Authentication & Authorization
- JWT Authentication
- Role-Based Authorization
- Secure Login & Registration
- Cookie-Based JWT Storage
- Password Hashing using BCrypt

---

#  User Roles

### Customer
- Register/Login
- Manage Profile
- Add Address
- Place Pressing Orders
- Track Orders

### Delivery Boy
- Register/Login
- Upload License Details
- Manage Delivery Status

### Shop Owner
- Register/Login
- Manage Shop
- Accept Orders
- Process Pressing Requests

### Admin
- Approve Shop Owners
- Approve Delivery Boys
- Manage Users
- Full Dashboard Access

---

#  Architecture

This project follows:

- Clean Architecture
- Repository Pattern
- CQRS Pattern
- MediatR
- Entity Framework Core
- SQL Server

---

# Project Structure

```bash
GoPress
│
├── GoPress.API
├── GoPress.Application
├── GoPress.Domain
├── GoPress.Infrastructure
└── GoPress.MVC

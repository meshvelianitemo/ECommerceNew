# ECommerceNew — Full-Stack E-Commerce Platform

A fully featured, production-ready **full-stack e-commerce platform** in a single monorepo, consisting of an **ASP.NET Core 8 REST API** backend and a **Next.js (TypeScript)** frontend. The backend follows **Clean Architecture** and the **CQRS pattern**, while the frontend delivers a modern, type-safe shopping experience.

---

## 📦 Monorepo Structure

```
ECommerceNew/
├── ECommerceNew_ServerSide/   → ASP.NET Core 8 REST API (backend)
└── ECommerceNew_ClientSide/   → Next.js + TypeScript (frontend)
```

---

## 🏗️ Backend Architecture Overview

The backend solution is split into four projects following Clean Architecture principles:

```
ECommerceNew.sln
├── ECommerceNew.Api           → Presentation layer (Controllers, Program.cs)
├── ECommerceNew.Application   → Business logic (CQRS Commands/Queries, DTOs, Validators)
├── ECommerceNew.Domain        → Core entities and domain models
└── ECommerceNew.Infrastructure → Data access, repositories, external services
```

### Design Patterns Used

- **CQRS** (Command Query Responsibility Segregation) via MediatR
- **Repository Pattern** for data access abstraction
- **Global Exception Handling Middleware** for consistent error responses
- **Result Pattern** (`Result<T>` / `Error`) instead of exceptions for business failures
- **FluentValidation** for request validation on every command/query

---

## 🚀 Features

### 🔐 Authentication & Authorization

- User registration and login with **JWT Bearer tokens**
- Email verification flow
- Password recovery (code-based) and password reset
- Role-based access control (Admin / Customer)

### 🛍️ Products

- Full product CRUD (Admin only)
- Product image upload and deletion via **AWS S3** with **pre-signed URLs** for secure access
- Product category support with parent/child hierarchy
- Paginated and filtered product listing

### 🛒 Shopping Cart

- Add, remove, and view cart items per user
- Quantity management

### ❤️ Wishlist

- Add/remove products from wishlist
- View paginated wishlist with item details

### ⭐ Reviews

- Create, update, and delete product reviews
- One review per user per product (unique constraint enforced at DB level)
- Get reviews by product or by individual review ID

### 📦 Orders

- Order creation and management
- Order repository with full CRUD support

### 💳 Payments

- **PayPal** integration via `PayPalServerSDK`
- Payment service abstraction allowing future provider swaps

### 📧 Email Service

- SMTP-based email delivery via **MailKit / NETCore.MailKit**
- Used for email verification and password recovery flows

### 📁 File Storage

- **AWS S3** integration for product image storage
- Pre-signed URL generation for secure, time-limited image access
- Image upload and deletion support

### 📋 Logging

- Structured logging with **Serilog**
- Sinks: Console, Debug, File, and **Seq** for centralized log management

---

## 🧰 Tech Stack

### Backend

| Layer          | Technology                         |
| -------------- | ---------------------------------- |
| Framework      | ASP.NET Core 8                     |
| Language       | C# (.NET 8)                        |
| Database       | Microsoft SQL Server               |
| ORM            | Entity Framework Core 8            |
| Messaging      | MediatR (CQRS)                     |
| Validation     | FluentValidation                   |
| Authentication | JWT Bearer                         |
| File Storage   | AWS S3 (AWSSDK.S3)                 |
| Payments       | PayPal (PayPalServerSDK)           |
| Email          | MailKit / NETCore.MailKit          |
| Logging        | Serilog (Console, File, Seq sinks) |
| API Docs       | Swagger / Swashbuckle              |
| Resilience     | Polly                              |
| Config         | DotNetEnv (`.env` file support)    |

### Frontend

| Layer             | Technology                       |
| ----------------- | -------------------------------- |
| Framework         | Next.js                          |
| Language          | TypeScript                       |
| API Communication | REST (consuming the backend API) |

---

## 📁 Backend Project Structure

```
ECommerceNew_ServerSide/
├── ECommerceNew.Application/
│   ├── Abstractions/              → Repository & service interfaces
│   ├── Auth/
│   │   ├── Commands/              → Register, Login, VerifyEmail, PasswordReset...
│   │   └── Queries/               → GetUsers
│   ├── ProductCQRS/
│   │   ├── Commands/              → CreateProduct, UpdateProduct, DeleteProduct,
│   │   │                            AddToCart, RemoveFromCart, AddToWishlist...
│   │   ├── Queries/               → GetAllProducts, GetProductById, GetCartItems,
│   │   │                            GetWishList, GetImagePreSignedUrl
│   │   └── DTOs/
│   ├── Reviews/
│   │   ├── Commands/              → CreateReview, UpdateReview, DeleteReview
│   │   ├── Queries/               → GetReview, GetReviewsForProduct
│   │   └── DTOs/
│   └── Results/
│       ├── Errors/                → Error, Result<T>
│       └── Exceptions/            → GlobalExceptionHandlingMiddleware, ValidationException
│
├── ECommerceNew.Domain/
│   └── Entities/
│       ├── UserSide/              → User, Role, EmailVerification
│       ├── ProductSide/           → Product, ProductCategory, ProductImage,
│       │                            Cart, CartItem, WishListItem, Review
│       ├── Commerce/              → Order
│       └── StoreInfo/             → StoreInfo
│
├── ECommerceNew.Infrastructure/
│   ├── EfCore/                    → DbContext
│   ├── Repositories/              → Implementations of all repository interfaces
│   ├── Migrations/                → EF Core migration history
│   └── Enums/                     → UserRolesEnum, VerifyEmailError
│
└── ECommerceNew.Api/
    └── Controllers/
        ├── Auth/                  → AuthenticationController
        ├── Products/              → ProductsController, AdminProductsController
        ├── Cart/                  → CartController
        ├── Wishlist/              → WishlistController
        ├── Reviews/               → ReviewsController
        ├── Orders/                → OrdersController
        └── Users/                 → AdminController
```

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/) (for the frontend)
- SQL Server instance
- AWS account with an S3 bucket configured
<!-- - PayPal developer account (for payments) -->
- SMTP credentials (for email)

### 1. Clone the repository

```bash
git clone https://github.com/meshvelianitemo/ECommerceNew.git
cd ECommerceNew
```

---

### 🖥️ Backend Setup

#### 2. Configure environment variables

Create a `.env` file inside `ECommerceNew_ServerSide/ECommerceNew.Api/`:

```env
# Database
CONNECTION_STRING=Server=your_server;Database=ECommerceDb;User Id=...;Password=...;

# JWT
JWT_SECRET=your_jwt_secret_key
JWT_ISSUER=your_issuer
JWT_AUDIENCE=your_audience

# AWS S3
AWS_ACCESS_KEY=your_access_key
AWS_SECRET_KEY=your_secret_key
AWS_BUCKET_NAME=your_bucket_name
AWS_REGION=your_region

# Email (SMTP)
SMTP_HOST=smtp.example.com
SMTP_PORT=587
SMTP_USER=your_email@example.com
SMTP_PASS=your_password

# PayPal
PAYPAL_CLIENT_ID=your_client_id
PAYPAL_CLIENT_SECRET=your_client_secret
```

#### 3. Apply database migrations

```bash
cd ECommerceNew_ServerSide
dotnet ef database update --project ECommerceNew.Infrastructure --startup-project ECommerceNew.Api
```

#### 4. Run the backend

```bash
cd ECommerceNew.Api
dotnet run
```

The API will be available at `https://localhost:7xxx` and Swagger UI at `/swagger`.

---

### 🌐 Frontend Setup

#### 5. Install dependencies

```bash
cd ECommerceNew_ClientSide
npm install
```

#### 6. Run the frontend

```bash
npm run dev
```

The frontend will be available at `http://localhost:3000`.

---

## 📬 API Endpoints (Overview)

| Domain         | Base Route            | Access        |
| -------------- | --------------------- | ------------- |
| Authentication | `/api/auth`           | Public        |
| Products       | `/api/products`       | Public        |
| Admin Products | `/api/admin/products` | Admin only    |
| Cart           | `/api/cart`           | Authenticated |
| Wishlist       | `/api/wishlist`       | Authenticated |
| Reviews        | `/api/reviews`        | Authenticated |
| Orders         | `/api/orders`         | Authenticated |
| Admin Users    | `/api/admin/users`    | Admin only    |

Full interactive documentation is available via **Swagger** at `/swagger` when running locally.

---

## 🔒 Authorization

| Level                          | Who                                               |
| ------------------------------ | ------------------------------------------------- |
| Public                         | Anyone — browse products, register, login         |
| `[Authorize]`                  | Logged-in users — cart, wishlist, orders, reviews |
| `[Authorize(Roles = "Admin")]` | Admins — product management, user management      |

---

## 📄 License

This project is licensed under the MIT License.

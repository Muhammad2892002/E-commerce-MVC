# E-Commerce

An ASP.NET Core MVC E-Commerce application developed as part of a training program. The project demonstrates modern ASP.NET Core MVC development practices, including authentication, product management, image handling, and a session-based shopping cart.

## 🚀 Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* AutoMapper
* Bootstrap 5
* JavaScript
* Session
* Memory Cache
* Newtonsoft.Json

## ✨ Features

* Authentication & Authorization (Admin & Customer)
* Product Management
* Category Management
* Product Image Upload & Management
* Session-Based Shopping Cart
* Responsive User Interface

## 📌 Sprint Progress

### ✅ Sprint 2 – Task 1: Product Image Management (Completed)

* Created a file service for image management.
* Uploaded product images to `wwwroot/uploads/products`.
* Generated unique file names using `GUID`.
* Validated image type (`jpg`, `jpeg`, `png`, `webp`).
* Enforced a maximum file size of **2 MB**.
* Saved image paths in the database.
* Replaced old images when updating products.
* Automatically deleted images when products were removed.
* Displayed a placeholder image when no product image was available.

### ✅ Sprint 2 – Task 2: Session Shopping Cart (Completed)

* Implemented a `CartItem` model.
* Built a session-based shopping cart service.
* Stored cart data as JSON in the user's session.
* Added products to the cart.
* Increased and decreased product quantities.
* Removed products from the cart.
* Cleared the shopping cart.
* Displayed product images, quantities, unit prices, and order totals.

### 🚧 Sprint 2 – Task 3: Product Browsing Optimization (Not Started)

This task is planned for a future update and will include:

* Server-side pagination
* Product search
* Product sorting
* Category caching using `IMemoryCache`
* Search, sorting, and pagination working together
* Cache invalidation after category CRUD operations

## 🛠 Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio 2022

### Installation

1. Clone the repository:

```bash
git clone https://github.com/Muhammad2892002/E-commerce.git
```

2. Navigate to the project directory:

```bash
cd E-commerce
```

3. Update the connection string in `appsettings.json`.

4. Apply the database migrations:

```bash
Update-Database
```

5. Run the application.

## 📅 Upcoming Features

* Product Pagination
* Product Search
* Product Sorting
* Memory Cache for Categories
* Performance Improvements

Images :
Display products to customer
<img width="950" height="456" alt="image" src="https://github.com/user-attachments/assets/70de88c7-b38e-4e76-b557-cb21a49b9604" />
<img width="950" height="284" alt="image" src="https://github.com/user-attachments/assets/4058b0a4-5b1b-4918-aef5-ae4f1e4d8e95" />

Customer cart
<img width="953" height="408" alt="image" src="https://github.com/user-attachments/assets/4b970589-4069-4425-a703-e90eaa5d8b92" />


## 👨‍💻 Author

**Muhammad Hadidi**





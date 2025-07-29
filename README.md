# 🧱 Shared Library Solution for Microservices

This **Shared Library Solution** is designed to centralize and reuse essential components across multiple .NET microservices in a clean and consistent way.

It provides ready-to-use functionality for:

- 🌍 Global exception handling
- 📜 Structured logging with Serilog
- ⚙️ Common middleware
- 🛠️ Service registration extensions
- 📦 Response wrapping
- 🧪 Shared base classes and interfaces

This Shared Library Must Be Used With Another Project To Add Its Functionality To the Project
---

## 📌 Features

- ✅ Global error handling middleware with detailed problem responses
- 🧾 Standardized logging using Serilog
- 🧰 Extension methods for clean dependency injection registration
- 💬 Common response wrapper (`Response<T>`)
- 🧪 Base interfaces like `IRepository`, `IUnitOfWork`, etc.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- Compatible .NET microservices to consume this shared project

### Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Nasser1A1/SharedLibrarySolution.git
   ```

2. **Reference the shared project** from your microservices (e.g., Product, Order, Auth):
   In your `.csproj`:
   ```xml
   <ProjectReference Include="..\SharedLibrarySolution\eCommerce.SharedLib\eCommerce.SharedLib.csproj" />
   ```

3. **Register shared services** inside `Program.cs` of your consuming microservice:
   ```csharp
   builder.Services.AddSharedServices<ApplicationDbContext>(builder.Configuration, "appsettings.json");
   ```

---

## 📂 Project Structure

```
SharedLibrarySolution/
└── eCommerce.SharedLib/
    ├── Middleware/
    │   └── ExceptionMiddleware.cs
    ├── Extensions/
    │   └── ServiceRegistration.cs
    ├── Logging/
    │   └── SerilogConfig.cs
    ├── ResponseT/
    │   └── Response.cs
    ├── Interfaces/
    │   └── IRepository.cs
    └── eCommerce.SharedLib.csproj
```

---

## 🧩 Integration Points

- 🔌 Plug this library into your API services for:
  - Logging
  - Exception handling
  - Clean dependency injection
  - Shared interfaces and base services

---

## 🛡️ Usage Example

```csharp
// Register shared services in Program.cs
builder.Services.AddSharedServices<AppDbContext>(builder.Configuration, "FileName");

// Use global exception handler
app.UseMiddleware<ExceptionMiddleware>();
```

---

## 🤝 Contribution

If you'd like to improve or add features to this shared library, feel free to open a pull request or issue.

---

## 📄 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

Developed by [Mahmoud Mady](https://github.com/MahmoudMady)

Forked or maintained by [Nasser1A1](https://github.com/Nasser1A1)

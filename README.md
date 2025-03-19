# 📌 EventZ-BE - Cấu Trúc Thư Mục Dự Án

# Kiến trúc Microservices với CQRS và MediatR

## Tổng quan

Dự án này sử dụng kiến trúc **Microservices**, kết hợp với **CQRS (Command Query Responsibility Segregation)** và **MediatR** để tối ưu hóa xử lý yêu cầu. Mục tiêu là tách biệt các thao tác đọc và ghi, giúp hệ thống dễ mở rộng và bảo trì.

## Kiến trúc

### 1. Microservices

Mỗi microservice đảm nhận một chức năng riêng biệt và hoạt động độc lập. Các service giao tiếp qua message broker của RabbitMQ.

### 2. CQRS (Command Query Responsibility Segregation)

- **Command Side**: Xử lý các thao tác ghi (Create, Update, Delete).
- **Query Side**: Xử lý các thao tác đọc dữ liệu.

### 3. MediatR

- Được sử dụng để giảm sự phụ thuộc giữa Controller và Business Logic.
- Các yêu cầu (command/query) được xử lý bởi các handler tương ứng.

---

## Cấu trúc thư mục
## Các framework

## Identity API: Asp.Versioning.Http, Asp.Versioning.Mvc,Asp.Versioning.Mvc.ApiExplorer, Carter, MassTransit, MassTransit.RabbitMQ, MediatR,MediatR.Extensions.Microsoft.Dependency, Newtonsoft.Json

## Identity.Application: MassTransit, BCrypt.Net-Next, Dapper, MailKit, MediatR,Microsoft.AspNetCore.Identity.UI, MimeKit, Newtonsoft.Json

## Identity.Domain: Not FrameWork

## Identity.Infrastructure: BCrypt.Net-Next, Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.Data.SqlClient, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.SqlServer

## Identity.Presentation: Asp.Versioning.Http, Carter

```
📦 Root
 ├── 📂 Identity
 │   ├── 📂 Identity.API            # Middlewares, DI.
 |   |   ├── 📂 DependencyConfig    # Cấu hình Dependency Injection
 |   |   ├── 📂 Middlewares         # Xử lý lỗi
 │   ├── 📂 Identity.Application    # Xử lý nghiệp vụ (CQRS, MediatR Handlers)
 |   |   ├── 📂 DTO                 # Chứa DTO
 |   |   ├── 📂 Features            # Chứa handlers xử lý logic
 |   |   ├── 📂 Interfaces          # Chứa interfaces
 │   ├── 📂 Identity.Domain         # Định nghĩa Entities, Aggregates, Domain Events
 |   |   ├── 📂 Entities            # Chứa entity
 |   |   ├── 📂 Shares              # Chứa các thành phần dùng chung
 │   ├── 📂 Identity.Infrastructure # Cơ sở dữ liệu, Repositories, Dịch vụ bên ngoài
 |   |   ├── 📂 Context             # Chứa context DB
 |   |   ├── 📂 Migrations          # Chứa migrations
 |   |   ├── 📂 Queries             # Chứa class xử lý get dữ liệu từ database
 |   |   ├── 📂 Repository          # Chứa repository
 │   ├── 📂 Identity.Presentation   # Controllers
 |   |   ├── 📂 V1                  # Chứa controller API V1
 ├── 📂 Service khác
 │   ├── 📂 Tương tự Identity
 │
 ├── 📂 CORE
 │   ├── 📂 Base.API                # Chứa phần custom BaseAPIController
 │   ├── 📂 Base.Common             # Chứa phần code dùng chung
 │
 ├── 📂 API Gateway                 # Điều phối yêu cầu API (Ocelot)
 ├── 📂 EventBus                    # Giao tiếp dựa trên sự kiện (RabbitMQ)
 ├── 📂 Docker                      # Cấu hình Docker-Compose
```

---

## Luồng xử lý yêu cầu

### **1. Xử lý Command (Yêu cầu ghi dữ liệu)**

1. **Controller** nhận yêu cầu (POST, PUT, DELETE).
2. Gửi **command** (`MediatR.Send(command)`).
3. **Command Handler** xử lý yêu cầu.
4. **Domain Model** cập nhật trạng thái.
5. **Repository** ghi dữ liệu vào database.
6. **Domain Events** có thể được phát đi nếu cần (qua Event Bus).
7. Trả kết quả về client.

### **2. Xử lý Query (Yêu cầu đọc dữ liệu)**

1. **Controller** nhận yêu cầu (GET).
2. Gửi **query** (`MediatR.Send(query)`).
3. **Query Handler** truy vấn dữ liệu từ database.
4. Trả kết quả về client.

---

## Hướng dẫn chạy Migration và khởi động dự án

### **1. Chạy Migration (Entity Framework Core)**

Trước khi chạy dự án, bạn cần đảm bảo database đã được cập nhật với các migrations.

- **Cách chạy Migration**
  Mở terminal (hoặc Package Manager Console trong Visual Studio) và chạy lệnh sau trong từng service có database:

```bash
dotnet ef database update -p Identity.Infrastructure -s Identity.API
```

> **Giải thích:**
>
> - `-p Identity.Infrastructure`: Chỉ định dự án chứa DbContext.
> - `-s Identity.API`: Chỉ định dự án startup (API).
> - `database update`: Áp dụng migration vào database.

Lặp lại bước này cho các service khác.

### **2. Chạy dự án**

Chạy lệnh sau để khởi động từng service

```bash
dotnet run --project Identity.API
```

Chạy lệnh sau để khởi động từng service

### **3. Chạy rabbitMQ**

Vào thư mục docker chạy lệnh sau

```bash
docker-compose -f docker-compose.RabbitMQ.yaml up
```

## Công nghệ sử dụng

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MediatR** cho CQRS
- **RabbitMQ** cho giao tiếp sự kiện
- **Docker** để triển khai
- **Ocelot** làm API Gateway
- **JWT** cho xác thực

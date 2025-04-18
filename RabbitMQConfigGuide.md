
# Hướng Dẫn Cấu Hình RabbitMQ bằng Docker

## Bước 1: Mở Source Code trong Visual Studio Code

1. Mở Visual Studio Code.
2. Vào **File** > **Open Folder** và chọn thư mục chứa mã nguồn của bạn, sau đó mở thư mục **docker**.

## Bước 2: Chạy RabbitMQ bằng Docker Compose

1. Mở terminal trong Visual Studio Code.
2. Điều hướng tới thư mục chứa file `docker-compose.RabbitMQ.yaml`.
3. Chạy lệnh sau để khởi động RabbitMQ:
   ```bash
   docker-compose -f docker-compose.RabbitMQ.yaml up
   ```

## Bước 3: Chạy RabbitMQ Container (Nếu chưa có)

Nếu bạn chưa có container RabbitMQ, chạy lệnh sau để tạo một container RabbitMQ mới với tên `zealous_goldberg` và expose các cổng cần thiết:

```bash
docker run -d --name zealous_goldberg -p 5672:5672 -p 15672:15672 rabbitmq:3-management-alpine
```

## Bước 4: Kiểm Tra Container đang Chạy

Sử dụng lệnh sau để kiểm tra xem container RabbitMQ có đang chạy hay không:

```bash
docker ps
```

## Bước 5: Truy Cập vào Container RabbitMQ

Để vào trong container RabbitMQ và thực hiện các thao tác quản trị, sử dụng lệnh sau:

```bash
docker exec -it zealous_goldberg sh
```

## Bước 6: Thêm Virtual Host

Sau khi vào container, bạn cần thêm một Virtual Host mới:

```bash
rabbitmqctl add_vhost eventz
```

## Bước 7: Cấp Quyền cho User

Tiếp theo, cấp quyền cho user `guest` trên Virtual Host `eventz`:

```bash
rabbitmqctl set_permissions -p eventz guest ".*" ".*" ".*"
```

## Bước 8: Cấu Hình Quyền Truy Cập

Để cho phép user `guest` truy cập từ các địa chỉ khác ngoài localhost, tạo hoặc sửa file cấu hình sau:

1. Tạo file cấu hình `rabbitmq.conf` với nội dung:
   ```bash
   echo "loopback_users.guest = false" > /etc/rabbitmq/rabbitmq.conf
   ```

## Bước 9: Khởi Động Lại RabbitMQ

Cuối cùng, bạn cần khởi động lại container RabbitMQ để áp dụng các thay đổi cấu hình:

```bash
docker restart zealous_goldberg
```

---

### Ghi Chú

- **Cổng mặc định RabbitMQ**: 
  - Cổng 5672 cho giao thức AMQP.
  - Cổng 15672 cho giao diện web quản trị RabbitMQ.
  
- **Giao diện web quản trị**: Sau khi hoàn thành các bước trên, bạn có thể truy cập giao diện quản trị RabbitMQ tại địa chỉ `http://localhost:15672`. Username và password mặc định là `guest`.

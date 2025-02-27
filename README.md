XÂY DỰNG WEBSITE HỆ THỐNG QUẢN LÝ SỰ KIỆN VÀ BÁN VÉ
  1. Giới thiệu
    Hệ thống quản lý sự kiện và bán vé là một nền tảng giúp người dùng dễ dàng tạo, quản lý và tham gia các sự kiện trực tuyến. Hệ thống cung cấp các tính năng quan trọng như:
    Đăng ký và đăng nhập tài khoản.
    Tìm kiếm và xem chi tiết sự kiện.
    Mua vé trực tuyến và thanh toán online.
    Quản lý tổ chức, sự kiện và thành viên.
    Thống kê doanh thu và tình trạng bán vé.
    Hệ thống được xây dựng trên nền tảng ReactJS (frontend), ASP.NET (backend), và SQL Server (cơ sở dữ liệu). Dữ liệu được lưu trữ an toàn trên Azure Blob Storage, đồng thời sử dụng JSON Web Token (JWT) để bảo mật.
  2. Cấu trúc thư mục
    Event-Ticketing-Website/
    │── client/      # Thư mục chứa mã nguồn frontend (ReactJS)
    │── server/      # Thư mục chứa mã nguồn backend (ASP.NET)
    │── README.md    # Tài liệu hướng dẫn dự án
  3. Công nghệ sử dụng
    Frontend: React TypeScript, Material UI, Tailwind CSS
    Backend: ASP.NET Core, Entity Framework
    Cơ sở dữ liệu: SQL Server
    Realtime Messaging: SignalR
    Bản đồ sự kiện: Mapbox
    Lưu trữ: Azure Blob Storage
    Xác thực: JSON Web Token (JWT)
  4. Cài đặt và chạy dự án
    4.1. Yêu cầu hệ thống
      Node.js >= 16.0.0
      .NET SDK >= 6.0
      SQL Server
      Azure Storage Account
    4.2. Clone dự án
     git clone https://github.com/phatnguyentandev/Event-Ticketing-Website.git
     cd Event-Ticketing-Website
    4.3. Cấu hình biến môi trường
      Tạo file .env ở thư mục client và thêm các biến:
      REACT_APP_API_URL=http://localhost:5000/api
      REACT_APP_MAPBOX_TOKEN=your_mapbox_access_token
      Tạo file appsettings.json trong thư mục server và thay đổi thông tin như sau:
      {
        "ConnectionStrings": {
          "DefaultSQLConnection": "Server=your_server;Database=your_db;Trusted_Connection=True;TrustServerCertificate=True"
        },
        "Azure": {
          "StorageAccount": "your_azure_storage_connection_string"
        },
        "Jwt": {
          "Secret": "your_secret_key"
        }
      }
    4.4. Cài đặt và chạy Backend
     cd server
     dotnet restore
     dotnet run
    Mặc định backend chạy tại http://localhost:5000.
    4.5. Cài đặt và chạy Frontend
     cd client
     npm install
     npm start
    Mặc định frontend chạy tại http://localhost:3000.
  5. Hướng dẫn sử dụng
    5.1. Đối với người dùng
      Đăng ký tài khoản.
      Tìm kiếm và xem thông tin sự kiện.
      Mua vé và thanh toán online.
      Xem danh sách vé đã mua và thông tin hóa đơn.
    5.2. Đối với người tổ chức sự kiện
    Tạo và quản lý thông tin sự kiện.
      Quản lý số lượng vé, trạng thái vé và giá vé.
      Theo dõi số lượng vé bán ra và doanh thu.
      Quản lý thành viên trong tổ chức.
    5.3. Đối với quản trị viên hệ thống
      Quản lý danh sách tài khoản người dùng.
      Theo dõi và quản lý các tổ chức sự kiện.
      Xem thống kê doanh thu hệ thống.

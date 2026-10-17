using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MyRazorApp.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        // Danh sách nhân viên gửi sang giao diện Razor
        public List<EmployeeItem> EmployeesList { get; set; } = new();

        public void OnGet()
        {
            // Khởi tạo dữ liệu giả lập cho danh sách nhân viên
            EmployeesList = new List<EmployeeItem>
            {
                new() {
                    Id = "NV-1001",
                    FullName = "Phạm Thị D",
                    Email = "phamthid@aholahotel.vn",
                    Department = "Lễ tân",
                    Position = "Trưởng bộ phận",
                    Phone = "0901 234 567",
                    Ext = "101",
                    JoinDate = "15/03/2022",
                    Status = "Đang làm việc",
                    AvatarUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=100&auto=format&fit=crop&q=80"
                },
                new() {
                    Id = "NV-1002",
                    FullName = "Hoàng Văn E",
                    Email = "hoangvane@aholahotel.vn",
                    Department = "Buồng phòng",
                    Position = "Trưởng bộ phận",
                    Phone = "0912 345 678",
                    Ext = "102",
                    JoinDate = "10/08/2021",
                    Status = "Đang làm việc",
                    AvatarUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&auto=format&fit=crop&q=80"
                },
                new() {
                    Id = "NV-1003",
                    FullName = "Vũ Minh F",
                    Email = "vuminhf@aholahotel.vn",
                    Department = "F&B (Ẩm thực)",
                    Position = "Quản lý Nhà hàng",
                    Phone = "0923 456 789",
                    Ext = "103",
                    JoinDate = "01/12/2023",
                    Status = "Đang nghỉ phép",
                    AvatarUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=100&auto=format&fit=crop&q=80"
                },
                new() {
                    Id = "NV-1004",
                    FullName = "Đỗ Văn G",
                    Email = "dovang@aholahotel.vn",
                    Department = "Kỹ thuật",
                    Position = "Kỹ sư trưởng",
                    Phone = "0934 567 890",
                    Ext = "104",
                    JoinDate = "05/05/2020",
                    Status = "Đang làm việc",
                    AvatarUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=100&auto=format&fit=crop&q=80"
                }
            };
        }
    }

    // Model dữ liệu từng nhân viên
    public class EmployeeItem
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Ext { get; set; } = string.Empty;
        public string JoinDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
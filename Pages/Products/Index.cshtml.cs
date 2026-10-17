using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBooking.Pages.Products;

public class IndexModel : PageModel
{
    private static readonly List<HotelViewModel> Hotels = new()
    {
        new() { Id = "reverie-saigon", Name = "The Reverie Saigon Hotel", Location = "Quận 1, Hồ Chí Minh", ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=1200&q=85", Rating = "4.9", Reviews = 1420, Price = 4500000, Badge = "Sang trọng 5 sao", Description = "Khách sạn sang trọng ngay trung tâm Quận 1, nổi bật với thiết kế tinh tế, hồ bơi và nhà hàng cao cấp.", Amenities = "Wi-Fi miễn phí, Hồ bơi, Spa, Nhà hàng, Phòng tập" },
        new() { Id = "intercontinental-danang", Name = "InterContinental Danang Sun Peninsula", Location = "Thọ Quang, Sơn Trà, Đà Nẵng", ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=1200&q=85", Rating = "4.8", Reviews = 980, Price = 6200000, Badge = "Resort nghỉ dưỡng", Description = "Khu nghỉ dưỡng bên bán đảo Sơn Trà với bãi biển riêng và tầm nhìn hướng biển tuyệt đẹp.", Amenities = "Wi-Fi miễn phí, Bãi biển riêng, Hồ bơi, Spa, Đưa đón sân bay" },
        new() { Id = "colline-dalat", Name = "Colline Hotel Dalat", Location = "Trung tâm Đà Lạt, Lâm Đồng", ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=1200&q=85", Rating = "4.6", Reviews = 2310, Price = 1350000, Badge = "Bán chạy nhất", Description = "Khách sạn hiện đại nằm ngay trung tâm Đà Lạt, thuận tiện khám phá chợ đêm và quảng trường.", Amenities = "Wi-Fi miễn phí, Nhà hàng, Quầy bar, Lễ tân 24/7" },
        new() { Id = "capella-hanoi", Name = "Capella Hanoi", Location = "Hoàn Kiếm, Hà Nội", ImageUrl = "https://images.unsplash.com/photo-1596394516093-501ba68a0ba6?auto=format&fit=crop&w=1200&q=85", Rating = "4.9", Reviews = 650, Price = 5800000, Badge = "Độc quyền", Description = "Khách sạn boutique đầy nghệ thuật ở khu phố cổ Hà Nội, kết hợp nét cổ điển và dịch vụ cao cấp.", Amenities = "Wi-Fi miễn phí, Hồ bơi trong nhà, Spa, Nhà hàng, Phòng tập" }
    };

    private static readonly List<DestinationViewModel> Destinations = new()
    {
        new() { Name = "Đà Lạt", Hotels = "12.450 khách sạn", Img = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?auto=format&fit=crop&w=500&q=80" },
        new() { Name = "Tokyo", Hotels = "8.320 khách sạn", Img = "https://images.unsplash.com/photo-1540959733332-eab4deabeeaf?auto=format&fit=crop&w=500&q=80" },
        new() { Name = "Bangkok", Hotels = "15.100 khách sạn", Img = "https://images.unsplash.com/photo-1583417319070-4a69db38a482?auto=format&fit=crop&w=500&q=80" },
        new() { Name = "Đà Nẵng", Hotels = "9.800 khách sạn", Img = "https://images.unsplash.com/photo-1559592413-7cec4d0cae2b?auto=format&fit=crop&w=500&q=80" },
        new() { Name = "Seoul", Hotels = "11.200 khách sạn", Img = "https://images.unsplash.com/photo-1508009603885-50cf7c579365?auto=format&fit=crop&w=500&q=80" },
        new() { Name = "Singapore", Hotels = "4.900 khách sạn", Img = "https://images.unsplash.com/photo-1563920614-2632b70490b4?auto=format&fit=crop&w=500&q=80" }
    };

    [BindProperty(SupportsGet = true)] public string? Destination { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? CheckIn { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? CheckOut { get; set; }
    [BindProperty(SupportsGet = true)] public int Adults { get; set; } = 2;
    [BindProperty(SupportsGet = true)] public int Rooms { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public string? Hotel { get; set; }
    [BindProperty(SupportsGet = true)] public string? Handler { get; set; }
    [BindProperty] public string GuestName { get; set; } = "";
    [BindProperty] public string GuestEmail { get; set; } = "";

    public List<HotelViewModel> HotelList { get; private set; } = new();
    public List<DestinationViewModel> DestinationList => Destinations;
    public HotelViewModel? SelectedHotel { get; private set; }
    public DestinationViewModel? SelectedDestination { get; private set; }
    public bool IsSearch { get; private set; }
    public bool BookingComplete { get; private set; }
    public string? BookingMessage { get; private set; }
    public int Nights => CheckIn.HasValue && CheckOut.HasValue ? Math.Max(1, (CheckOut.Value.Date - CheckIn.Value.Date).Days) : 1;
    public int TotalPrice => (SelectedHotel?.Price ?? 0) * Nights * Math.Max(1, Rooms);

    public void OnGet()
    {
        HotelList = Hotels;
        if (!string.IsNullOrWhiteSpace(Hotel))
        {
            SelectedHotel = Hotels.FirstOrDefault(h => h.Id == Hotel);
            if (SelectedHotel is null) Response.StatusCode = 404;
            return;
        }

        IsSearch = !string.IsNullOrWhiteSpace(Destination) || CheckIn.HasValue || CheckOut.HasValue;
        if (IsSearch && !string.IsNullOrWhiteSpace(Destination))
        {
            var query = Destination.Trim();
            SelectedDestination = Destinations.FirstOrDefault(d => d.Name.Equals(query, StringComparison.OrdinalIgnoreCase));
            HotelList = Hotels.Where(h => h.Location.Contains(query, StringComparison.OrdinalIgnoreCase) || h.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    public IActionResult OnPostBook()
    {
        SelectedHotel = Hotels.FirstOrDefault(h => h.Id == Hotel);
        if (SelectedHotel is null) return NotFound();
        if (CheckIn is null || CheckOut is null || CheckIn.Value.Date < DateTime.Today || CheckOut.Value.Date <= CheckIn.Value.Date)
        {
            ModelState.AddModelError(nameof(CheckOut), "Vui lòng chọn ngày nhận và trả phòng hợp lệ.");
        }
        if (Adults < 1 || Adults > 10 || Rooms < 1 || Rooms > 5)
            ModelState.AddModelError(nameof(Adults), "Số khách hoặc số phòng không hợp lệ.");
        if (string.IsNullOrWhiteSpace(GuestName) || string.IsNullOrWhiteSpace(GuestEmail))
            ModelState.AddModelError(nameof(GuestName), "Vui lòng nhập tên và email liên hệ.");

        HotelList = Hotels;
        if (!ModelState.IsValid) return Page();
        BookingComplete = true;
        BookingMessage = $"Đã ghi nhận yêu cầu đặt {SelectedHotel.Name} cho {Adults} khách, {Rooms} phòng. Đây là xác nhận demo; chưa phát sinh thanh toán.";
        return Page();
    }
}

public class HotelViewModel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string Rating { get; set; } = "";
    public int Reviews { get; set; }
    public int Price { get; set; }
    public string Badge { get; set; } = "";
    public string Description { get; set; } = "";
    public string Amenities { get; set; } = "";
}

public class DestinationViewModel
{
    public string Name { get; set; } = "";
    public string Hotels { get; set; } = "";
    public string Img { get; set; } = "";
}

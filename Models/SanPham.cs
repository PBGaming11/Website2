using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class SanPham
    {
        [Key] //khoá chính tự động thêm và là cách dễ nhất để gọi
        public int IdSanPham { get; set; }
        [Required] // yêu cầu phải có
        public string TenSanPham { get; set; }
        public string ImgDaiDien { get; set; }
        public double GiaSanpham { get; set; }
        public double GiamGia { get; set; }
        public string? MoTaSanPham { get; set; } //? cho phép null
        [Required]
        public int DanhmucId { get; set; }

        [ForeignKey("DanhmucId")] //đây là khoá ngoại liên kết với DanhmucId
        public DanhMuc danhmuc { get; set; } // đây là chỗ chữa dữ liệu từ model DanhMuc
    }
}

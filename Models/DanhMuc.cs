using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class DanhMuc
    {
        [Key] // khoá để chỉ đến nó tự động và động nhất
        public int Id { get; set; }
        [Required] //yêu cầu bắt buộc
        public string? Namedanhmuc { get; set; }
        public DateTime Description { get; set; } = DateTime.Now;
        public string imgDanhMuc { get; set; }
    }
}

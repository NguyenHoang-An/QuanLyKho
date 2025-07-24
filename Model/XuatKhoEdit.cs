using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    [Table("tbl_XNK_Xuat_Kho")]
    public class XuatKhoEdit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Số phiếu không được để trống")]
        [Column("So_Phieu_Xuat_Kho")]
        public string So_Phieu_Xuat_Kho { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kho không được để trống")]
        [Column("Kho_ID")]
        public int? Kho_ID { get; set; }

        [Required(ErrorMessage = "Ngày xuất kho không được để trống")]
        [Column("Ngay_Xuat_Kho")]
        public DateTime? Ngay_Xuat_Kho { get; set; }

        [Column("Ghi_Chu")]
        public string? Ghi_Chu { get; set; }
    }
}
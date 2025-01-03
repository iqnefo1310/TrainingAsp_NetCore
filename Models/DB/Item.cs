using System;
using System.ComponentModel.DataAnnotations;

namespace PERT_2.Models.DB
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama item wajib diisi.")]
        [StringLength(100, ErrorMessage = "Nama item maksimal 100 karakter.")]
        public string NamaItem { get; set; }

        [Required(ErrorMessage = "Quantity wajib diisi.")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "Tanggal kedaluwarsa wajib diisi.")]
        [DataType(DataType.Date, ErrorMessage = "Format tanggal tidak valid.")]
        public DateTime TglExpire { get; set; }

        [StringLength(100, ErrorMessage = "Nama supplier maksimal 100 karakter.")]
        public string? Supplier { get; set; }

        [StringLength(100, ErrorMessage = "Alamat supplier maksimal 100 karakter.")]
        public string? AlamatSupplier { get; set; }
    }
}

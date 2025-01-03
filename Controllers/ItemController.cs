using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PERT_2.Models.DB;
using PERT_2.Services;

namespace PERT_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly ItemServices _itemsSevices;

        public ItemController(ItemServices itemServices)
        {
            _itemsSevices = itemServices;
        }

        // GET: api/item
        [HttpGet("Mendapatkan Semua Data")]
        public IActionResult Get()
        {
            try
            {
                var itemList = _itemsSevices.GetItemsList();
                return Ok(new { status = "success", data = itemList });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "error", message = ex.Message });
            }
        }

        // POST: api/item
        [HttpPost("Menambahkan Data")]
        public IActionResult AddItem(
            [FromQuery] string namaItem,
            [FromQuery] int? qty,
            [FromQuery] DateTime? tglExpire,
            [FromQuery] string supplier,
            [FromQuery] string? alamatSupplier)
        {
            // Validasi untuk namaItem
            if (string.IsNullOrEmpty(namaItem))
                return BadRequest("Nama item wajib diisi.");

            // Validasi untuk qty
            if (!qty.HasValue || qty <= 0)
                return BadRequest("Qty harus angka positif dan wajib diisi.");

            // Validasi untuk tglExpire
            if (!tglExpire.HasValue || tglExpire <= DateTime.Now)
                return BadRequest("Tanggal expire harus tanggal valid dan lebih dari hari ini.");

            alamatSupplier = string.IsNullOrEmpty(alamatSupplier) ? "none" : alamatSupplier;
            // Alamat supplier opsional
            var newItem = new Item
            {
                NamaItem = namaItem,
                Qty = qty.Value,
                TglExpire = tglExpire.Value, // Dipastikan tidak null
                Supplier = supplier,
                AlamatSupplier = alamatSupplier // Nullable, bisa null
            };

            var isAdded = _itemsSevices.CreateItems(newItem);
            if (isAdded)
            {
                return Ok("Item added successfully.");
            }
            return BadRequest("Failed to add item.");
        }
        // PUT: api/item/{id}
        [HttpPut("Masukan {id} Untuk Mengedit Data")]
        public IActionResult UpdateItem(
            int id,
            [FromQuery] string? namaItem,
            [FromQuery] int? qty,
            [FromQuery] DateTime? tglExpire,
            [FromQuery] string? supplier,
            [FromQuery] string? alamatSupplier)
        {
            // Periksa apakah item dengan ID yang diberikan ada
            var existingItem = _itemsSevices.GetItemById(id);
            if (existingItem == null)
                return NotFound(new { status = "error", message = $"Item with ID {id} not found." });

            try
            {
                // Update hanya jika nilai baru diberikan
                if (!string.IsNullOrEmpty(namaItem))
                {
                    existingItem.NamaItem = namaItem;
                }

                if (qty.HasValue && qty > 0)
                {
                    existingItem.Qty = qty.Value;
                }

                if (tglExpire.HasValue && tglExpire > DateTime.Now)
                {
                    existingItem.TglExpire = tglExpire.Value;
                }

                if (!string.IsNullOrEmpty(supplier))
                {
                    existingItem.Supplier = supplier;
                }

                // Set "none" jika alamatSupplier kosong
                existingItem.AlamatSupplier = string.IsNullOrEmpty(alamatSupplier) ? "none" : alamatSupplier;

                // Simpan perubahan melalui service
                var isUpdated = _itemsSevices.UpdateItem(id, existingItem);
                if (isUpdated)
                {
                    return Ok(new { status = "success", message = "Item updated successfully." });
                }
                return BadRequest(new { status = "error", message = "Failed to update item." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "error", message = ex.Message });
            }
        }

        // DELETE: api/item/{id}
        [HttpDelete("Masukan {id} Untuk Menghapus Data")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                var isDeleted = _itemsSevices.DeleteItem(id);
                if (isDeleted)
                {
                    return Ok(new { status = "success", message = $"Item with ID {id} deleted successfully." });
                }
                return NotFound(new { status = "error", message = $"Item with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "error", message = ex.Message });
            }
        }
    }
}

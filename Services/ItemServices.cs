using PERT_2.Models;
using PERT_2.Models.DB;

namespace PERT_2.Services
{
    public class ItemServices
    {
        private readonly ApplicationContext _context;

        public ItemServices(ApplicationContext context)
        {
            _context = context;
        }

        // Get all items
        public List<Item> GetItemsList()
        {
            return _context.Items.ToList();
        }

        // Create a new item
        public bool CreateItems(Item item)
        {
            try
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error creating item: {ex.Message}");
                return false;
            }
        }

        // Get item by ID
        public Item? GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.Id == id);
        }

        // Update an existing item
        public bool UpdateItem(int id, Item updatedItem)
        {
            try
            {
                var existingItem = _context.Items.FirstOrDefault(i => i.Id == id);
                if (existingItem == null)
                {
                    return false; // Item not found
                }

                // Update fields
                existingItem.NamaItem = updatedItem.NamaItem;
                existingItem.Qty = updatedItem.Qty;
                existingItem.TglExpire = updatedItem.TglExpire;
                existingItem.Supplier = updatedItem.Supplier;
                existingItem.AlamatSupplier = updatedItem.AlamatSupplier;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error updating item: {ex.Message}");
                return false;
            }
        }

        // Delete an item by ID
        public bool DeleteItem(int id)
        {
            try
            {
                var item = _context.Items.FirstOrDefault(i => i.Id == id);
                if (item == null)
                {
                    return false; // Item not found
                }

                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error deleting item: {ex.Message}");
                return false;
            }
        }
    }
}

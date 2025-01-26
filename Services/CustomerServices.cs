using PERT_2.Models;
using PERT_2.Models.DB;
using PERT_2.Models.DTO;

namespace PERT_2.Services
{
    public class CustomerServices
    {
        private readonly ApplicationContext _context;
        public CustomerServices(ApplicationContext context)
        {
            _context = context;
        }

      /*  public List<Customer> GetListCustommer()
        {
            var datas = _context.Customers.ToList();
            return datas;
        }
*/
        public Customer GetListCustommerById(int idreq)
        {
            try
            {
                var datasCus = _context.Customers.Where(x => x.Id == idreq).FirstOrDefault();
                if (datasCus == null)
                {
                    return null;
                }
                return datasCus;

            }
            catch (Exception)
            {

                throw;
            }
        }

/*        public bool CreateCustomer(Customer customer)
        {
            try
            {

                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
*/
        public bool UpdateCustomer(int Id, CustomerRequestDTO customer)
        {
            try
            {
                var customerOld = _context.Customers.Where(x => x.Id == Id).FirstOrDefault();
                if (customerOld != null)
                {
                    customerOld.Name = customer.Name;
                    customerOld.Address = customer.Address;
                    customerOld.City = customer.City;
                    customerOld.phoneNumber = customer.phoneNumber;
                    customerOld.updatedDate = DateTime.Now;

                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                var customerDataDel = _context.Customers.FirstOrDefault(x => x.Id == id);
                if (customerDataDel != null)
                {
                    _context.Customers.Remove(customerDataDel);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //========================================//
        public List<CustomerDTO> GetlistCustomer()
        {
            var data = _context.Customers.Select(x => new CustomerDTO
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                phoneNumber = x.phoneNumber,
                createdDate = x.createdDate != null ? x.createdDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                updatedDate = x.updatedDate != null ? x.updatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",

            }).ToList();

            return data;
        }

        // Mendapatkan satu data pelanggan berdasarkan ID
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id); // Mengembalikan satu pelanggan dengan ID tertentu
        }

        public bool CreateCustomer(CustomerRequestDTO customer)
        {
            try
            {
                var insertDataCustomer = new Customer
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    City = customer.City,
                    phoneNumber = customer.phoneNumber,
                    createdDate = DateTime.Now,
                    updatedDate = DateTime.Now,
                };
                _context.Customers.Add(insertDataCustomer);
                _context.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

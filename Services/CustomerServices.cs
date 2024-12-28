using PERT_2.Models;
using PERT_2.Models.DB;

namespace PERT_2.Services
{
    public class CustomerServices
    {
        private readonly ApplicationContext _context;
        public CustomerServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<Customer> GetListCustommer()
        {
            var datas = _context.Customers.ToList();
            return datas;
        }

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

        public bool CreateCustomer(Customer customer)
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

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                var customerOld = _context.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
                if (customerOld != null)
                {
                    customerOld.Name = customer.Name;
                    customerOld.Address = customer.Address;
                    customerOld.City = customer.City;
                    customerOld.phoneNumber = customer.phoneNumber;

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
    }
}

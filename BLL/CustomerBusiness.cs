using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _res;
        public CustomerBusiness(ICustomerRepository res)
        {
            _res = res;
        }

        public bool CreateCustomer(CustomerModel model)
        {
            return _res.CreateCustomer(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public CustomerModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Update(CustomerModel model)
        {
            return _res.Update(model);
        }
        public List<CustomerModel> Search(int pageIndex, int pageSize, out long total, string customer_name, string customer_email)
        {
            return _res.Search(pageIndex, pageSize, out total, customer_name, customer_email);
        }
    }
}

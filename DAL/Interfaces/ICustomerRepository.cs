using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface ICustomerRepository
    {
        bool CreateCustomer(CustomerModel model);
        CustomerModel GetDatabyID(string id);
        bool Update(CustomerModel model);
        bool Delete(string id);
        List<CustomerModel> Search(int pageIndex, int pageSize, out long total, string customer_name, string customer_email);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.ViewModel
{
    public class CustomerEditViewModel
    {
        private ICustomersRepository CustomersRepository { get; set; }

        public Customer Customer { get; set; }

        public CustomerEditViewModel()
        {
            this.CustomersRepository = new CustomersRepository();
            this.Customer = this.CustomersRepository.GetCustomerAsync(new Guid("11DA4696-CEA3-4A6D-8E83-013F1C479618")).Result;
        }

        public ICommand SaveCommand { get; set; }

        public void Execute()
        {

        }
    }
}

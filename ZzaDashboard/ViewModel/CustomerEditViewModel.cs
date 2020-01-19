using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            this.CustomersRepository = new CustomersRepository();
            this.Customer = this.CustomersRepository.GetCustomerAsync(new Guid("11DA4696-CEA3-4A6D-8E83-013F1C479618")).Result;
            this.SaveCommand = new RelayCommand(this.SaveExecute, this.SaveCanExecute);
        }

        public ICommand SaveCommand { get; set; }

        public async void SaveExecute(object obj)
        {
            this.Customer = await this.CustomersRepository.UpdateCustomerAsync(this.Customer);
        }

        public bool SaveCanExecute(object obj)
        {
            return !(String.IsNullOrEmpty(this.Customer.FirstName) ||
                String.IsNullOrEmpty(this.Customer.LastName) ||
                String.IsNullOrEmpty(this.Customer.Phone));
        }
    }
}

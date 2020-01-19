using MyCustomersProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;

namespace MyCustomersProject.ViewModel
{
    public class MyCustomersViewModel
    {
        public ObservableCollection<Customer> Customers { get; set; }

        private ICustomersRepository MyCustomersRepository { get; set; }

        public MyCustomersViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
            this.MyCustomersRepository = new CustomersRepository();
            this.GetCommand = new RelayCommand(this.GetCommandExecute);
            this.Customers = new ObservableCollection<Customer>();
        }

        public ICommand GetCommand { get; set; }

        public async void GetCommandExecute(object obj)
        {
            var tempCustomers = await this.MyCustomersRepository.GetCustomersAsync();
            foreach (var customer in tempCustomers)
                this.Customers.Add(customer);
        }
    }
}

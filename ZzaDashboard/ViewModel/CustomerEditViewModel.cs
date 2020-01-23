using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.ViewModel
{
    public class CustomerEditViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return this.customers;
            }
            set
            {
                if (this.customers == value)
                    return;

                this.customers = value;
                this.OnPropertyChanged(nameof(this.Customers));
            }
        }

        //public ObservableCollection<Customer> Customers { get; set; }

        private ICustomersRepository CustomersRepository { get; set; }

        private Customer customer;

        public Customer Customer
        {
            get
            {
                return this.customer;
            }
            set
            {
                if (this.customer == value)
                    return;
                
                this.customer = value;
                this.OnPropertyChanged(nameof(this.Customer));
            }
        }

        public CustomerEditViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
            //this.Customers = new ObservableCollection<Customer>()
            //{
            //    new Customer{FirstName = "Tetyana", LastName = "Ryzhik", Phone = "123"},
            //    new Customer{FirstName = "Andrew", LastName = "Fedorchenko", Phone = "321"},
            //    new Customer{FirstName = "Vlad", LastName = "Radchenko", Phone = "567"},
            //};
            this.CustomersRepository = new CustomersRepository();
            this.Customer = this.CustomersRepository.GetCustomerAsync(new Guid("11DA4696-CEA3-4A6D-8E83-013F1C479618")).Result;
            this.SaveCommand = new RelayCommand(this.SaveExecute, this.SaveCanExecute);
            this.Customers = new ObservableCollection<Customer>();
            //this.InitializeCustomers();
        }

        //public async void InitializeCustomers(object obj)
        //{

        //    this.Customers = await CustomersRepository.GetCustomersAsync();
        //}

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged is null)
                return;

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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

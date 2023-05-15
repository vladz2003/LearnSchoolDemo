using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearnSchool
{
    /// <summary>
    /// Логика взаимодействия для SchoolService.xaml
    /// </summary>
    public partial class SchoolService : Window
    {
        private LearnSchoolContext _db;
        private List<Service> _services;
        public SchoolService()
        {
            InitializeComponent();
            _db = LearnSchoolContext.GetContext();
            _services = _db.Services.ToList();
            ComboBoxServiceCost.ItemsSource = new List<string>
            {
                "Все",
                "По возрастанию",
                "По убыванию"
            };
            ComboBoxServiceDiscount.ItemsSource = new List<string>
            {
                "Все",
                "0% - 5%",
                "5% - 15%",
                "15% - 30%",
                "30% - 70%",
                "70% - 100%"
            };
            ComboBoxServiceCost.SelectedIndex = 0;
            ComboBoxServiceDiscount.SelectedIndex = 0;
        }

        private void GetServices()
        {
            var services = _services;

            services = SortServicesDiscount(services);
            services = SortServicesName(services);
            services = SortServicesPrice(services);
            SetCount(services);

            listViewServices.ItemsSource = services;
        }
        private void ComboBoxServiceDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServices();
        }
        private void ComboBoxServiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServices();        
        }
        private void TextBoxSearchServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetServices();
        }

        private void ButtonDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = (sender as Button).DataContext as Service;

            if (selectedService.ClientServices.Count > 0)
            {
                MessageBox.Show("Удаление услуги невозможно, так как есть записи на нее", "Ошибка");
                return;
            }

            if (MessageBox.Show(
                "Вы действительно желаете удалить данную услугу ?",
                "Внимание",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.Remove(selectedService);
                _services.Remove(selectedService);
                _db.SaveChanges();

                GetServices();
            }
        }

        

        private List<Service> SortServicesPrice(List<Service> services)
        {
            switch (ComboBoxServiceCost.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        services = services.OrderBy(s => s.ServiceCostWithDiscount).ToList();
                        break;
                    }
                case 2:
                    {
                        services = services.OrderByDescending(s => s.ServiceCostWithDiscount).ToList();
                        break;
                    }
            }

            return services;
        }

        private List<Service> SortServicesDiscount(List<Service> services)
        {
            switch (ComboBoxServiceDiscount.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 0 && s.ServiceDiscount < 5).ToList();
                        break;
                    }
                case 2:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 5 && s.ServiceDiscount < 15).ToList();
                        break;
                    }
                case 3:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 15 && s.ServiceDiscount < 30).ToList();
                        break;
                    }
                case 4:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 30 && s.ServiceDiscount < 70).ToList();
                        break;
                    }
                case 5:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 70 && s.ServiceDiscount < 100).ToList();
                        break;
                    }
            }

            return services;
        }

        private List<Service> SortServicesName(List<Service> services)
        {
            return services.Where(
                s => s.ServiceName
                .ToLower()
                .Contains(
                    TextBoxSearchServices
                    .Text
                    .ToLower()))
                .ToList();
        }

        private void SetCount(List<Service> services)
        {
            TextBlockCountServices.Text = services.Count.ToString() + " из " + _services.Count.ToString();
        }

        private void ButtonEditService_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow((sender as Button).DataContext as Service);
            addEditServiceWindow.Show();
            this.Close();
        }

        private void ButtonGoToClientServiceWindow_Click(object sender, RoutedEventArgs e)
        {
            if (listViewServices.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберете услугу для записи нажамием на карточку");
                return;
            }
            if (listViewServices.SelectedItems.Count > 1)
            {
                MessageBox.Show("Одновременно можно записывать только на одну услугу");
                return;
            }

            var selectedService = listViewServices.SelectedItems.Cast<Service>().FirstOrDefault();

            ClientServiceWindow clientServiceWindow = new ClientServiceWindow(selectedService);
            clientServiceWindow.Show();
            this.Close();
        }

        private void ButtonGoToIncomingServicesWindow_Click(object sender, RoutedEventArgs e)
        {
            IncomingServiceWindow incomingServicesWindow = new IncomingServiceWindow();
            incomingServicesWindow.Show();
            this.Close();
        }

        private void ButtonAddService_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow(new Service());
            addEditServiceWindow.Show();
            this.Close();
        }
    }
}

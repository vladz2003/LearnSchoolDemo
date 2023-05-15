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
    /// Логика взаимодействия для GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {
        private LearnSchoolContext _db;
        private List<Service> _services;
        public GuestWindow()
        {
            InitializeComponent();
            _db = LearnSchoolContext.GetContext();
            _services = _db.Services.ToList();
            ComboBoxServiceCostSort.ItemsSource = new List<string>
            {
                "Все",
                "По возрастанию",
                "По убыванию"
            };
            ComboBoxServiceDiscountFilter.ItemsSource = new List<string>
            {
                "Все",
                "0% - 5%",
                "5% - 15%",
                "15% - 30%",
                "30% - 70%",
                "70% - 100%"
            };
            ComboBoxServiceCostSort.SelectedIndex = 0;
            ComboBoxServiceDiscountFilter.SelectedIndex = 0;
        }

        private void ComboBoxServiceDiscountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServices();
        }

        private void ComboBoxServiceCostSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServices();
        }

        private void TextBoxSearchServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetServices();
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

        private List<Service> SortServicesPrice(List<Service> services)
        {
            switch (ComboBoxServiceCostSort.SelectedIndex)
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
            switch (ComboBoxServiceDiscountFilter.SelectedIndex)
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
    }
}

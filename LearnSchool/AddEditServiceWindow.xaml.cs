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
    /// Логика взаимодействия для AddEditServiceWindow.xaml
    /// </summary>
    public partial class AddEditServiceWindow : Window
    {
        private List<Service> _services;
        private LearnSchoolContext _context;
        private Service _service;

        public AddEditServiceWindow(Service service)
        {
            InitializeComponent();
            _service = service;
            DataContext = _service;
            _context = LearnSchoolContext.GetContext();
            _services = _context.Services.ToList();
            if (_service.ServiceId == 0)
            {
                TextBoxServiceId.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Вы действительно хотите вернуться ? Несохраненные данные будут потеряны.",
                "Внимание",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SchoolService serviceWindow = new SchoolService();
                serviceWindow.Show();
                this.Close();
            }
           
        }

        private void ButtonSaveService_Click(object sender, RoutedEventArgs e)
        {
            if (_service.ServiceDuration / 60 >= 4)
            {
                MessageBox.Show("Длительность услуги не может превышать 4 часа");
                return;
            }
            if (_service.ServiceDuration < 0)
            {
                MessageBox.Show("Длительность услуги не может быть отрицательной");
                return;
            }
            if (_services.Find(s => s.ServiceName == _service.ServiceName) != null && _service.ServiceId == 0)
            {
                MessageBox.Show("Услуга с таким названием уже существует в базе");
                return;
            }
            if (_service.ServiceId == 0)
            {
                _context.Services.Add(_service);
            }
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

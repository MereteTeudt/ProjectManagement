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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentAPI.EF;

namespace FluentAPI.GUI
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl
    {
        protected Model model;
        private Employee selectedEmployee;
        public EmployeeUserControl()
        {
            InitializeComponent();
            model = new Model();
            dataGridEmployees.ItemsSource = model.Employees.ToList();
            this.gridEmployee.DataContext = selectedEmployee;
        }

        private void DataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridEmployees.SelectedItem as Employee;
            textBoxEmployeeName.Text = selectedEmployee.Name;
        }

        private void Button_Click_Save_Employee(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Name = textBoxEmployeeName.Text;
            model.Employees.Add(employee);
            model.SaveChanges();
        }

        private void Button_Click_Save_Contact_Info(object sender, RoutedEventArgs e)
        {

        }
    }
}

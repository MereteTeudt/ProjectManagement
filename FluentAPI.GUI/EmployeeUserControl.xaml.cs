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
            UpdateDataGrid();
            this.gridEmployee.DataContext = selectedEmployee;
        }

        private void DataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridEmployees.SelectedItem as Employee;
            textBoxEmployeeFirstName.Text = selectedEmployee?.FirstName;
            textBoxEmployeeLastName.Text = selectedEmployee?.LastName;
            datePickerBirthDate.SelectedDate = selectedEmployee?.BirthDate;
            textBoxEmployeeCPR.Text = "" + selectedEmployee?.CPR;
            datePickerHiringDate.SelectedDate = selectedEmployee?.HiringDate;
            textBoxEmployeeSalary.Text = "" + selectedEmployee?.Pay;

            textBoxEmail.Text = selectedEmployee?.ContactInfo?.Email;
            textBoxPhone.Text = selectedEmployee?.ContactInfo?.Phone;
        }

        private void Button_Click_Save_Employee(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.FirstName = textBoxEmployeeFirstName.Text;
            employee.LastName = textBoxEmployeeLastName.Text;
            employee.BirthDate = datePickerBirthDate.SelectedDate.Value;
            employee.CPR = int.Parse(textBoxEmployeeCPR.Text);
            employee.HiringDate = datePickerHiringDate.SelectedDate.Value;
            employee.Pay = decimal.Parse(textBoxEmployeeSalary.Text);
            model.Employees.Add(employee);
            model.SaveChanges();
            UpdateDataGrid();
        }

        private void Button_Click_Save_Contact_Info(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.ContactInfo.Email = textBoxEmail.Text;
            employee.ContactInfo.Phone = textBoxPhone.Text;
        }

        private void UpdateDataGrid()
        {
            dataGridEmployees.ItemsSource = model.Employees.ToList();
        }
    }
}

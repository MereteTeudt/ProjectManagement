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
            if(selectedEmployee != null)
            {
                buttonUpdateEmployee.IsEnabled = true;
                buttonSaveEmployee.IsEnabled = false;
            }
            textBoxEmployeeFirstName.Text = selectedEmployee?.FirstName;
            textBoxEmployeeLastName.Text = selectedEmployee?.LastName;
            datePickerBirthDate.SelectedDate = selectedEmployee?.BirthDate;
            textBoxEmployeeCPR.Text = "" + selectedEmployee?.CPR;
            datePickerHiringDate.SelectedDate = selectedEmployee?.HiringDate;
            textBoxEmployeeSalary.Text = "" + selectedEmployee?.Pay;

            textBoxEmail.Text = selectedEmployee?.ContactInfo?.Email;
            textBoxPhone.Text = selectedEmployee?.ContactInfo?.Phone;
        }
        private void DataGrid_Employees_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridEmployees.SelectedItem != null)
            {
                if (e.Key == Key.Escape)
                {
                    dataGridEmployees.SelectedItem = selectedEmployee = null;
                    buttonSaveEmployee.IsEnabled = true;
                    buttonUpdateEmployee.IsEnabled = false;
                    textBoxEmployeeFirstName.Text = String.Empty;
                    textBoxEmployeeLastName.Focus();
                }
            }
        }

        private void ButtonSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            if(!string.IsNullOrWhiteSpace(textBoxEmployeeFirstName.Text))
            {
                employee.FirstName = textBoxEmployeeFirstName.Text;
            }
            employee.LastName = textBoxEmployeeLastName.Text;
            employee.BirthDate = datePickerBirthDate.SelectedDate.Value;
            employee.CPR = int.Parse(textBoxEmployeeCPR.Text);
            employee.HiringDate = datePickerHiringDate.SelectedDate.Value;
            employee.Pay = decimal.Parse(textBoxEmployeeSalary.Text);

            employee.ContactInfo.Email = textBoxEmail.Text;
            employee.ContactInfo.Phone = textBoxPhone.Text;

            model.Employees.Add(employee);
            model.SaveChanges();
            UpdateDataGrid();
        }

        private void ButtonUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            selectedEmployee.FirstName = textBoxEmployeeFirstName.Text;
            selectedEmployee.LastName = textBoxEmployeeLastName.Text;
            selectedEmployee.BirthDate = datePickerBirthDate.SelectedDate.Value;
            selectedEmployee.CPR = int.Parse(textBoxEmployeeCPR.Text);
            selectedEmployee.HiringDate = datePickerHiringDate.SelectedDate.Value;
            selectedEmployee.Pay = decimal.Parse(textBoxEmployeeSalary.Text);

            if(selectedEmployee.ContactInfo == null)
            {
                ContactInfo contactInfo = new ContactInfo();
                selectedEmployee.ContactInfo = contactInfo;
            }

            selectedEmployee.ContactInfo.Email = textBoxEmail.Text;
            selectedEmployee.ContactInfo.Phone = textBoxPhone.Text;

            model.SaveChanges();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            dataGridEmployees.ItemsSource = model.Employees.ToList();
        }

    }
}

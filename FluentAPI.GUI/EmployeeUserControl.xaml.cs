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
            //SetDataToDefault();
        }

        private void DataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridEmployees.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                buttonUpdateEmployee.IsEnabled = true;
                buttonSaveEmployee.IsEnabled = false;

                textBoxEmployeeFirstName.Text = selectedEmployee?.FirstName;
                textBoxEmployeeLastName.Text = selectedEmployee?.LastName;
                datePickerBirthDate.SelectedDate = selectedEmployee?.BirthDate;
                textBoxEmployeeCPR.Text = "" + selectedEmployee?.CPR;
                datePickerHiringDate.SelectedDate = selectedEmployee?.HiringDate;
                textBoxEmployeeSalary.Text = "" + selectedEmployee?.Pay;

                textBoxEmail.Text = selectedEmployee?.ContactInfo?.Email;
                textBoxPhone.Text = selectedEmployee?.ContactInfo?.Phone;
            }
            else
            {
                SetDataToDefault();
            }
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
                    textBoxEmployeeFirstName.Focus();
                }
            }
        }

        private void ButtonSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.ContactInfo = new ContactInfo();
            UpdateOrSaveEmployee(employee);

            model.Employees.Add(employee);
            model.SaveChanges();
            UpdateDataGrid();
        }

        private void ButtonUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {

            if (selectedEmployee.ContactInfo == null)
            {
                ContactInfo contactInfo = new ContactInfo();
                selectedEmployee.ContactInfo = contactInfo;
            }
            UpdateOrSaveEmployee(selectedEmployee);
            model.SaveChanges();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            dataGridEmployees.ItemsSource = model.Employees.ToList();
        }

        private void SetDataToDefault()
        {

            textBoxEmployeeFirstName.Text = "";
            textBoxEmployeeLastName.Text = "";
            textBoxEmployeeCPR.Text = "00000000";
            textBoxEmployeeSalary.Text = "0.0";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "000000";

            datePickerHiringDate.SelectedDate = DateTime.Today;
            datePickerBirthDate.SelectedDate = new DateTime(1950, 1, 1);
        }

        private void UpdateOrSaveEmployee(Employee employee)
        {
            try
            {
                employee.FirstName = textBoxEmployeeFirstName.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.LastName = textBoxEmployeeLastName.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.BirthDate = datePickerBirthDate.SelectedDate.Value;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.CPR = int.Parse(textBoxEmployeeCPR.Text);
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.HiringDate = datePickerHiringDate.SelectedDate.Value;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.Pay = decimal.Parse(textBoxEmployeeSalary.Text);
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }


            try
            {
                employee.ContactInfo.Email = textBoxEmail.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                employee.ContactInfo.Phone = textBoxPhone.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}

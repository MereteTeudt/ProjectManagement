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
        private TeamUserControl teamUserControl = new TeamUserControl();
        private ProjectUserControl projectUserControl = new ProjectUserControl();
        public EmployeeUserControl()
        {
            InitializeComponent();
            model = new Model();
            UpdateDataGrid();
            this.gridEmployee.DataContext = selectedEmployee;
            SetDataToDefault();
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

            try
            {
                model.Employees.Add(employee);
                model.SaveChanges();
                UpdateDataGrid();
                teamUserControl.UpdateAllEmployeesDateGrid();
                projectUserControl.UpdateAllTeamsDataGrid();

            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

        }

        private void ButtonUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {

            if (selectedEmployee.ContactInfo == null)
            {
                ContactInfo contactInfo = new ContactInfo();
                selectedEmployee.ContactInfo = contactInfo;
            }
            UpdateOrSaveEmployee(selectedEmployee);

            try
            {
                model.SaveChanges();
                UpdateDataGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
        }

        private void UpdateDataGrid()
        {
            dataGridEmployees.ItemsSource = model.Employees.ToList();
        }

        private void SetDataToDefault()
        {

            textBoxEmployeeFirstName.Text = "";
            textBoxEmployeeLastName.Text = "";
            textBoxEmployeeCPR.Text = "1111111111";
            textBoxEmployeeSalary.Text = "0.0";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "000000";

            datePickerHiringDate.SelectedDate = DateTime.Today;
            datePickerBirthDate.SelectedDate = new DateTime(1950, 1, 1);
        }

        private void UpdateOrSaveEmployee(Employee employee)
        {
            int parsedCPR;
            decimal parsedPay;

            if (!Validator.IsValidName(textBoxEmployeeFirstName.Text))
            {
                MessageBox.Show("Ugyldigt navn. Et navn kan kun bestå af bogstaver og feltet må ikke være blankt.");
            }
            else if (!Validator.IsValidName(textBoxEmployeeLastName.Text))
            {
                MessageBox.Show("Ugyldigt navn. Et navn kan kun bestå af bogstaver og feltet må ikke være blankt.");
            }
            else if (!Validator.IsValidBirthDate(datePickerBirthDate.SelectedDate.Value))
            {
                MessageBox.Show("Ugyldig alder. Ansatte kan ikke være over 70 år.");
            }
            else if (!Validator.IsValidHiringDate(datePickerHiringDate.SelectedDate.Value, datePickerBirthDate.SelectedDate.Value))
            {
                MessageBox.Show("Ugyldig hyringsdato. Hyringsdato skal være senere end den ansattes fødselsdato og firmaets stiftelse.");
            }
            else if (!int.TryParse(textBoxEmployeeCPR.Text, out parsedCPR))
            {
                MessageBox.Show("Ugyldigt CPR nummer. Et CPR nummer kan kun bestå af tal.");
            }
            else if (!Validator.IsValidCPR(parsedCPR))
            {
                MessageBox.Show("Ugyldigt CPR nummer. Et CPR nummer skal være præcis 8 cifre langt.");
            }
            else if (!decimal.TryParse(textBoxEmployeeSalary.Text, out parsedPay))
            {
                MessageBox.Show("Ugyldigt beløb. Beløbet kan kun bestå af tal.");
            }
            else if (!Validator.IsValidAmount(parsedPay))
            {
                MessageBox.Show("Ugyldigt beløb. Beløbet kan ikke være mindre end nul.");
            }
            else
            {
                try
                {
                    employee.FirstName = textBoxEmployeeFirstName.Text;

                    employee.LastName = textBoxEmployeeLastName.Text;

                    employee.BirthDate = datePickerBirthDate.SelectedDate.Value;

                    employee.CPR = parsedCPR;

                    employee.HiringDate = datePickerHiringDate.SelectedDate.Value;

                    employee.Pay = parsedPay;
                
                    employee.ContactInfo.Email = textBoxEmail.Text;

                    employee.ContactInfo.Phone = textBoxPhone.Text;
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }
            }

        }
    }
}

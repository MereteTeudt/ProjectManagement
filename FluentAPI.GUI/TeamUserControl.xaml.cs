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
    /// Interaction logic for TeamUserControl.xaml
    /// </summary>
    public partial class TeamUserControl : UserControl
    {
        protected Model model;
        private Team selectedTeam;
        private Employee selectedEmployee;

        public TeamUserControl()
        {
            InitializeComponent();
            try
            {
                model = new Model();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
            UpdateTeamsComboBox();
            UpdateAllEmployeesDateGrid();
        }

        private void UpdateTeamsComboBox()
        {
            try
            {
                comboBoxTeams.ItemsSource = model.Teams.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
        }

        /// <summary>
        /// Update methods
        /// </summary>

        private void UpdateMembersDataGrid()
        {
            selectedTeam = comboBoxTeams.SelectedItem as Team;
            dataGridMembers.ItemsSource = selectedTeam.Employees.ToList();
        }
        /// <summary>
        /// Updates the datagrid so it only contains employees who are not already in the team
        /// </summary>
        public void UpdateAllEmployeesDateGrid()
        {
            if(selectedTeam != null)
            {
                List<Employee> employeesNotInTeam = new List<Employee>();
                try
                {
                    employeesNotInTeam = model.Employees.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }

                foreach (Employee e in selectedTeam.Employees)
                {
                    employeesNotInTeam.Remove(e);
                }


                dataGridAllEmployees.ItemsSource = employeesNotInTeam;
            }
            else
            {
                try
                {
                    dataGridAllEmployees.ItemsSource = model.Employees.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }
            }

        }

        private void UpdateTeamData()
        {
            if (comboBoxTeams.SelectedItem != null)
            {
                selectedTeam = comboBoxTeams.SelectedItem as Team;
                textBoxTeamName.Text = selectedTeam.Name;
                textBoxTeamDescription.Text = selectedTeam.Description;
                datePickerStartDate.SelectedDate = selectedTeam.StartDate;
                datePickerEndDate.SelectedDate = selectedTeam.EndDate;
                textBoxExpenses.Text = CalculateTeamExpenses(selectedTeam).ToString();
            }
            else
            {
                textBoxTeamName.Text = "";
                textBoxTeamDescription.Text = "";
                datePickerStartDate.SelectedDate = DateTime.Now;
                datePickerEndDate.SelectedDate = DateTime.Now.AddDays(7);
                textBoxExpenses.Text = "";
            }
        }
        private void comboBoxTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateTeamData();
            dataGridMembers.ItemsSource = selectedTeam.Employees.ToList();
            UpdateAllEmployeesDateGrid();
            checkBoxNewTeam.IsChecked = false;
            buttonSave.IsEnabled = false;
            buttonUpdate.IsEnabled = true;

        }

        private void DataGridMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (comboBoxTeams.SelectedIndex > -1)
            {
                buttonAdd.IsEnabled = false;
                buttonRemove.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Vælg et hold for at fjerne en ansat.");
            }
        }
        private void dataGridAllEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxTeams.SelectedIndex > -1)
            {
                buttonAdd.IsEnabled = true;
                buttonRemove.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Vælg et hold for at tilføje en ansat.");
            }

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeams.SelectedIndex > -1)
            {
                selectedEmployee = dataGridAllEmployees.SelectedItem as Employee;
                selectedTeam.Employees.Add(selectedEmployee);
            }
            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
            UpdateMembersDataGrid();
            UpdateAllEmployeesDateGrid();
            UpdateTeamData();
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeams.SelectedIndex > -1)
            {
                selectedEmployee = dataGridMembers.SelectedItem as Employee;
                selectedTeam.Employees.Remove(selectedEmployee);
            }
            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
            UpdateMembersDataGrid();
            UpdateAllEmployeesDateGrid();
            UpdateTeamData();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            selectedTeam = comboBoxTeams.SelectedItem as Team;

            UpdateOrSaveTeam(selectedTeam);
            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            UpdateTeamsComboBox();

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team();

            UpdateOrSaveTeam(team);
            try
            {
                model.Teams.Add(team);
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
            UpdateTeamsComboBox();
        }

        private void UpdateOrSaveTeam(Team team)
        {
            if (!Validator.IsValidName(textBoxTeamName.Text))
            {
                MessageBox.Show("Ugyldigt navn. Et navn kan kun bestå af bogstaver og feltet må ikke være blankt.");
            }
            else if(!Validator.IsValidDescription(textBoxTeamDescription.Text))
            {
                MessageBox.Show("Ugyldig beskrivelse.Beskrivelse må maks indeholde 1000 karakterer og feltet må ikke være tomt");
            }
            else if (!Validator.IsValidStartDate(datePickerStartDate.SelectedDate.Value))
            {
                MessageBox.Show("Ugyldig dato. Holdet kan ikke startes før firmaets stiftelsesdato(1950)");
            }
            else if (!Validator.IsValidEndDate(datePickerEndDate.SelectedDate.Value, datePickerStartDate.SelectedDate.Value))
            {
                MessageBox.Show("Ugyldig dato. Slutdato kan ikke være før Startdato.");
            }
            else
            {
                try
                {
                    team.Name = textBoxTeamName.Text;

                    team.Description = textBoxTeamDescription.Text;

                    team.StartDate = datePickerStartDate.SelectedDate.Value;

                    team.EndDate = datePickerEndDate.SelectedDate.Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }
            }
        }

        private void checkBoxNewTeam_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxTeams.SelectedItem = null;
            buttonAdd.IsEnabled = false;
            buttonRemove.IsEnabled = false;
            dataGridMembers.ItemsSource = null;
            buttonSave.IsEnabled = true;
            buttonUpdate.IsEnabled = false;
        }

        /// <summary>
        /// Calculates and return the total expense for the team
        /// </summary>
        /// <param name="selectedTeam"></param>
        /// <returns></returns>
        public static decimal CalculateTeamExpenses(Team selectedTeam)
        {
            decimal totalPayExpense = 0;
            int durationInMonths = 0;

            durationInMonths = selectedTeam.Duration.Days / 30;
            totalPayExpense = durationInMonths * selectedTeam.Calculate();

            return totalPayExpense;
        }
    }
}

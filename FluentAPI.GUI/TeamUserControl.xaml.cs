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
            model = new Model();
            UpdateTeam();
            dataGridAllEmployees.ItemsSource = model.Employees.ToList();
        }

        private void UpdateTeam()
        {
            comboBoxTeams.ItemsSource = model.Teams.ToList();
        }

        private void UpdateMembersDataGrid()
        {
            selectedTeam = comboBoxTeams.SelectedItem as Team;
            dataGridMembers.ItemsSource = selectedTeam.Employees.ToList();
        }

        private void comboBoxTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTeamData();
            dataGridMembers.ItemsSource = selectedTeam.Employees.ToList();
        }

        public static decimal CalculateTeamExpenses(Team selectedTeam)
        {
            decimal monthlyPayExpense = 0;
            decimal totalPayExpense = 0;

            int monthsInt = ((selectedTeam.ExpectedEndDate.Year - selectedTeam.StartDate.Year) * 12) + selectedTeam.ExpectedEndDate.Month - selectedTeam.StartDate.Month;

            foreach(Employee emp in selectedTeam.Employees.ToList())
            {
                monthlyPayExpense += emp.Pay;
            }

            totalPayExpense = monthsInt * monthlyPayExpense;

            return totalPayExpense;
        }

        private void dataGridAllEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = true;
            buttonRemove.IsEnabled = false;
        }

        private void DataGridMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = false;
            buttonRemove.IsEnabled = true;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeams.SelectedIndex > -1)
            {
                selectedEmployee = dataGridAllEmployees.SelectedItem as Employee;
                selectedTeam.Employees.Add(selectedEmployee);
            }
            model.SaveChanges();
            UpdateMembersDataGrid();
            UpdateTeamData();
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeams.SelectedIndex > -1)
            {
                selectedEmployee = dataGridMembers.SelectedItem as Employee;
                selectedTeam.Employees.Remove(selectedEmployee);
            }
            model.SaveChanges();
            UpdateMembersDataGrid();
            UpdateTeamData();
        }

        private void UpdateTeamData()
        {
            selectedTeam = comboBoxTeams.SelectedItem as Team;
            textBoxTeamInfo.Text = selectedTeam?.Description;
            datePickerStartDate.SelectedDate = selectedTeam?.StartDate;
            datePickerEndDate.SelectedDate = selectedTeam?.ExpectedEndDate;

            textBoxExpenses.Text = CalculateTeamExpenses(selectedTeam).ToString();
        }
    }
}

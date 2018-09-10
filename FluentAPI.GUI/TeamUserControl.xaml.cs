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
        private List<Team> teams;
        private Team selectedTeam;
        private Employee selectedEmployee;

        public TeamUserControl()
        {
            InitializeComponent();
            model = new Model();
            UpdateTeam();
            dataGridMembers.ItemsSource = model.Employees.ToList();

        }

        private void DataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridMembers.SelectedItem as Employee;
            textBoxMember.Text = selectedEmployee.FirstName + selectedEmployee.LastName;
        }

        private void UpdateTeam()
        {
            comboBoxTeams.ItemsSource = TeamsToString();
        }

        private List<string> TeamsToString()
        {
            List<string> teamString = new List<string>();

            teams = model.Teams.ToList();
            foreach(Team t in teams)
            {
                teamString.Add(t.Name);
            }

            return teamString;
        }
    }
}

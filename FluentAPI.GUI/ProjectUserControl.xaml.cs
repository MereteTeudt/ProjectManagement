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
    /// Interaction logic for ProjectUserControl.xaml
    /// </summary>
    public partial class ProjectUserControl : UserControl
    {
        private Model model;
        private Project selectedProject;
        private Team selectedTeam;

        public ProjectUserControl()
        {
            InitializeComponent();
            model = new Model();
            UpdateProject();
            dataGridAllTeams.ItemsSource = model.Teams.ToList();
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxProjects.SelectedIndex > -1)
            {
                selectedTeam = dataGridAllTeams.SelectedItem as Team;
                selectedProject.Teams.Add(selectedTeam);
            }
            model.SaveChanges();
            UpdateAffiliatedTeamsDataGrid();
            UpdateProjectData();

        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxProjects.SelectedIndex > -1)
            {
                selectedTeam = dataGridAllTeams.SelectedItem as Team;
                selectedProject.Teams.Remove(selectedTeam);
            }
            model.SaveChanges();
            UpdateAffiliatedTeamsDataGrid();
            UpdateProjectData();
        }

        private void UpdateProject()
        {
            comboBoxProjects.ItemsSource = model.Projects.ToList();
        }
        private void UpdateAffiliatedTeamsDataGrid()
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;
            dataGridAffiliatedTeams.ItemsSource = selectedProject.Teams.ToList();
        }
        private void UpdateProjectData()
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;
            textBoxProjectInfo.Text = selectedProject?.Description;
            datePickerStartDate.SelectedDate = selectedProject.StartDate;
            datePickerEndDate.SelectedDate = selectedProject.EndDate;
            textBoxBudget.Text = selectedProject.Budget.ToString();

            textBoxExpenses.Text = CalculateProjectExpenses().ToString();

            textBoxExpensesAllProjects.Text = CalculateAllProjectsExpenses().ToString();
        }

        private void comboBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProjectData();
            dataGridAffiliatedTeams.ItemsSource = selectedProject.Teams.ToList();
        }

        private void dataGridAffiliatedTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = false;
            buttonRemove.IsEnabled = true;
        }

        private void dataGridAllTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonAdd.IsEnabled = true;
            buttonRemove.IsEnabled = false;
        }

        private decimal CalculateProjectExpenses()
        {
            decimal projectExpenses = 0;

            foreach(Team t in selectedProject.Teams.ToList())
            {
                projectExpenses += TeamUserControl.CalculateTeamExpenses(t);
            }

            return projectExpenses;
        }
        private decimal CalculateAllProjectsExpenses()
        {
            decimal allProjectsExpenses = 0;

            foreach(Project p in model.Projects.ToList())
            {
                allProjectsExpenses += CalculateProjectExpenses();
            }

            return allProjectsExpenses;
        }
    }
}

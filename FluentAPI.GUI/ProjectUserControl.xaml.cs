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
            UpdateProjectsComboBox();
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

        private void UpdateProjectsComboBox()
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

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;

            UpdateOrSaveProject(selectedProject);
            model.SaveChanges();
            UpdateProjectsComboBox();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project();

            UpdateOrSaveProject(project);
            model.Projects.Add(project);
            model.SaveChanges();
            UpdateProjectsComboBox();
        }

        private void UpdateOrSaveProject(Project project)
        {
            try
            {
                project.Name = textBoxProjectName.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
               project.Description = textBoxProjectInfo.Text;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                project.StartDate = datePickerStartDate.SelectedDate.Value;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                project.EndDate = datePickerEndDate.SelectedDate.Value;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}

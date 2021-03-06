﻿using System;
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
        private OverviewUserControl overviewUserControl = new OverviewUserControl();

        public ProjectUserControl()
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

            UpdateProjectsComboBox();
            UpdateAllTeamsDataGrid();
        }

        private void UpdateProjectsComboBox()
        {
            try
            {
                comboBoxProjects.ItemsSource = model.Projects.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }
        }

        private void UpdateAffiliatedTeamsDataGrid()
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;
            dataGridAffiliatedTeams.ItemsSource = selectedProject.Teams.ToList();
        }

        /// <summary>
        /// Updates the datagrid so it only contains teams who are not already in the selected project
        /// </summary>
        public void UpdateAllTeamsDataGrid()
        {
            if(selectedProject != null)
            {
                List<Team> teamsNotInProject = new List<Team>();
                try
                {
                    teamsNotInProject = model.Teams.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }

                foreach (Team t in selectedProject.Teams)
                {
                    teamsNotInProject.Remove(t);
                }

                dataGridAllTeams.ItemsSource = teamsNotInProject;
            }
            else
            {
                try
                {
                    dataGridAllTeams.ItemsSource = model.Teams.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }
            }
        }

        private void UpdateProjectData()
        {
            if (comboBoxProjects.SelectedItem != null)
            {
                selectedProject = comboBoxProjects.SelectedItem as Project;
                textBoxProjectName.Text = selectedProject.Name;
                textBoxProjectDescription.Text = selectedProject.Description;
                datePickerStartDate.SelectedDate = selectedProject.StartDate;
                datePickerEndDate.SelectedDate = selectedProject.EndDate;
                textBoxBudget.Text = selectedProject.Budget.ToString();
            }
            else
            {
                textBoxProjectName.Text = "";
                textBoxProjectDescription.Text = "";
                datePickerStartDate.SelectedDate = DateTime.Now;
                datePickerEndDate.SelectedDate = DateTime.Now.AddDays(7);
                textBoxBudget.Text = "";
            }
        }

        private void comboBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProjectData();
            dataGridAffiliatedTeams.ItemsSource = selectedProject.Teams.ToList();
            UpdateAllTeamsDataGrid();
            checkBoxNewProject.IsChecked = false;
            buttonSave.IsEnabled = false;
            buttonUpdate.IsEnabled = true;
        }

        private void dataGridAffiliatedTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProjects.SelectedIndex > -1)
            {
                buttonAdd.IsEnabled = false;
                buttonRemove.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Vælg et projekt for at fjerne et hold.");
            }
        }

        private void dataGridAllTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProjects.SelectedIndex > -1)
            {
                buttonAdd.IsEnabled = true;
                buttonRemove.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Vælg et projekt for at fjerne et hold.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxProjects.SelectedIndex > -1)
            {
                selectedTeam = dataGridAllTeams.SelectedItem as Team;
                selectedProject.Teams.Add(selectedTeam);
            }

            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            UpdateAffiliatedTeamsDataGrid();
            UpdateAllTeamsDataGrid();
            UpdateProjectData();
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxProjects.SelectedIndex > -1)
            {
                selectedTeam = dataGridAffiliatedTeams.SelectedItem as Team;
                selectedProject.Teams.Remove(selectedTeam);
            }

            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            UpdateAffiliatedTeamsDataGrid();
            UpdateAllTeamsDataGrid();
            UpdateProjectData();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;
            UpdateOrSaveProject(selectedProject);

            try
            {
                model.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            UpdateProjectsComboBox();
            //Tries to update the combobox in the overview usercontrol - does not work
            //overviewUserControl.UpdateProjectsComboBox();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project();
            UpdateOrSaveProject(project);

            try
            {
                model.Projects.Add(project);
                model.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            UpdateProjectsComboBox();
            //Tries to update the combobox in the overview usercontrol - does not work
            //overviewUserControl.UpdateProjectsComboBox();
        }

        private void UpdateOrSaveProject(Project project)
        {
            decimal parsedBudget;

            if (!Validator.IsValidName(textBoxProjectName.Text))
            {
                MessageBox.Show("Ugyldigt navn. Et navn kan kun bestå af bogstaver og feltet må ikke være blankt.");
            }
            else if (!Validator.IsValidDescription(textBoxProjectDescription.Text))
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
            else if (!decimal.TryParse(textBoxBudget.Text, out parsedBudget))
            {
                MessageBox.Show("Ugyldigt beløb. Beløbet kan kun bestå af tal.");
            }
            else if (!Validator.IsValidAmount(parsedBudget))
            {
                MessageBox.Show("Ugyldigt beløb. Beløbet kan ikke være mindre end nul.");
            }
            else
            {
                try
                {
                    project.Name = textBoxProjectName.Text;

                    project.Description = textBoxProjectDescription.Text;

                    project.StartDate = datePickerStartDate.SelectedDate.Value;

                    project.EndDate = datePickerEndDate.SelectedDate.Value;

                    project.Budget = parsedBudget;
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
                }
            }
        }

        private void checkBoxNewProject_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxProjects.SelectedItem = null;
            buttonAdd.IsEnabled = false;
            buttonRemove.IsEnabled = false;
            dataGridAffiliatedTeams.ItemsSource = null;
            buttonSave.IsEnabled = true;
            buttonUpdate.IsEnabled = false;
        }
    }
}

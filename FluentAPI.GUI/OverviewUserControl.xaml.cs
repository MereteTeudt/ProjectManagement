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
    /// Interaction logic for OverviewUserControl.xaml
    /// </summary>
    public partial class OverviewUserControl : UserControl
    {
        protected Model model;
        private Project selectedProject;

        public OverviewUserControl()
        {
            InitializeComponent();
            model = new Model();
            comboBoxProjects.ItemsSource = model.Projects.ToList();
            textBoxExpensesAllProjects.Text = CalculateAllProjectsExpenses().ToString();
        }

        private void comboBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;
            textBoxExpenses.Text = CalculateProjectExpenses(selectedProject).ToString();
        }

        private decimal CalculateProjectExpenses(Project selectedProject)
        {
            decimal projectExpenses = 0;
            foreach (Team t in selectedProject.Teams.ToList())
            {
                projectExpenses += TeamUserControl.CalculateTeamExpenses(t);
            }

            return projectExpenses;
        }
        private decimal CalculateAllProjectsExpenses()
        {
            decimal allProjectsExpenses = 0;

            foreach (Project p in model.Projects.ToList())
            {
                allProjectsExpenses += CalculateProjectExpenses(p);
            }

            return allProjectsExpenses;
        }
    }
}

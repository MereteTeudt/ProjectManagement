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

            try
            {
                model = new Model();
                comboBoxProjects.ItemsSource = model.Projects.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            //Shows the total cost of all projects
            textBlockExpensesAllProjects.Text = CalculateAllProjectsExpenses().ToString();
        }

        private void comboBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = comboBoxProjects.SelectedItem as Project;

            //Shows the cost for the selected project
            textBlockExpenses.Text = CalculateProjectExpenses(selectedProject).ToString();
        }

        /// <summary>
        /// Calculates and returns the total expense for the project
        /// </summary>
        /// <param name="selectedProject"></param>
        /// <returns></returns>
        private decimal CalculateProjectExpenses(Project selectedProject)
        {
            decimal totalPayExpense = 0;
            int durationInMonths = 0;

            durationInMonths = selectedProject.Duration.Days / 30;
            totalPayExpense = durationInMonths * selectedProject.Calculate();

            return totalPayExpense;
        }

        /// <summary>
        /// Calculates and returns the total expense for all projects in model.Projects
        /// </summary>
        /// <returns></returns>
        private decimal CalculateAllProjectsExpenses()
        {
            decimal allProjectsExpenses = 0;

            try
            {
                foreach (Project p in model.Projects.ToList())
                {
                    allProjectsExpenses += CalculateProjectExpenses(p);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en uventet fejl. Venligst prøv igen");
            }

            return allProjectsExpenses;
        }
    }
}

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

namespace FluentAPI.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            userControlEmployees.Content = new EmployeeUserControl();
            userControlTeams.Content = new TeamUserControl();
            userControlProjects.Content = new ProjectUserControl();
            userControlOverview.Content = new OverviewUserControl();
        }

        private void userControlEmployees_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void userControlTeams_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void userControlProjects_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void userControlOverview_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

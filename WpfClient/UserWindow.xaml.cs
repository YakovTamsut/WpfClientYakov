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
using System.Windows.Shapes;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public User currentUser;
        private ServiceModelClient GymService;
        public UserList users;


        public UserWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            GymService = new ServiceModelClient();
            users = GymService.GetAllPlanAdmins();
            LoadTodayWorkout();
        }

        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateProgram_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            MainView.Children.Add(new CreateWorkoutUC(currentUser, this));            
        }
        public void CreateProgramRefresh()
        {
            Clear_Grid();
            MainView.Children.Add(new CreateWorkoutUC(currentUser, this));
        }

        private void Clear_Grid()
        {
            MainView.Children.Clear();
            ProgramView.Children.Clear();
        }

        private void WeeklyPlan_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            MainView.Children.Add(new WeeKPlanUC(currentUser, this));
        }

        public void WeeklyPlan_Selected()
        {
            Clear_Grid();
            MainView.Children.Add(new WeeKPlanUC(currentUser, this));
        }

        private void LoadTodayWorkout()
        {
            Clear_Grid();
            MainView.Children.Add(new WorkoutsUC(GymService.GetTodayWorkoutPlan(currentUser), true));
        }

        private void TodayWorkout_Selected(object sender, RoutedEventArgs e)
        {
            LoadTodayWorkout();
        }

        public void RecommendedWorkour_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            foreach (User u in users)
            {
                EditWorkoutUC editWorkoutUC = new EditWorkoutUC(u);
                editWorkoutUC.MouseUp += ShowPlan;
                editWorkoutUC.Tag = u;
                ProgramView.Children.Add(editWorkoutUC);
            }
        }

        public void SwitchPlan(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            EditWorkoutUC ewu = sender as EditWorkoutUC;
            MainView.Children.Add(new WeeKPlanUC(ewu.Tag as User, this));
        }

        public void SwitchPlan(EditWorkoutUC ewu)
        {
            Clear_Grid();
            MainView.Children.Add(new WeeKPlanUC(ewu.Tag as User, this));
        }

        private void ShowPlan(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            EditWorkoutUC ewu = (sender as EditWorkoutUC);
            WeeKPlanUC weeKPlanUC = new WeeKPlanUC(ewu.user,this);
            weeKPlanUC.Tag = this;
            MainView.Children.Add(weeKPlanUC);

        }
    }
}

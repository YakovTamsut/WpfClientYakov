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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private ServiceModelClient GymService;
        public UserList planlist;
        private User curentUser;
        public ManagerWindow(User user)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            planlist = GymService.GetAllPlanAdmins();
            curentUser = user;
            foreach (User u in planlist) 
            {
                ProgramView.Children.Add(new ProgramPlanUC(u,this));
            }
        }

        public void EditProgram_Selected(object sender, RoutedEventArgs e)
        {
            this.ProgramView.Children.Clear();
            foreach (User u in planlist)
            {
                ProgramView.Children.Add(new ProgramPlanUC(u, this));
            }
        }

        public void EditProgram_Selected()
        {
            this.ProgramView.Children.Clear();
            planlist = GymService.GetAllPlanAdmins();
            foreach (User u in planlist)
            {
                ProgramView.Children.Add(new ProgramPlanUC(u, this));
            }
        }

        private void CreateProgramPlans_Selected(object sender, RoutedEventArgs e)
        {
            this.ProgramView.Children.Clear();
            User user= new User() { Email= "Admin", BirthDay=DateTime.Today, Firstname="Plan Name", Lastname="Admin", IsManager=true, Gender=true, Password="Admin" };
            user.ID=GymService.NewUser(user);
            planlist.Add(user);
            ProgramPlanUC program = new ProgramPlanUC(user, this);
            ProgramView.Children.Add(program);
        }

        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadCreateWorkout(CreateWorkoutUC puc)
        {
            this.ProgramView.Children.Clear();
            this.ProgramView.Children.Add(puc);
        }

        public void ManageUsers_Selected(object sender, RoutedEventArgs e)
        {
            this.ProgramView.Children.Clear();
            ProgramView.Children.Add(new DisplayUsersUC());
        }
    }
}

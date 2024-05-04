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
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for DisplayUsersUC.xaml
    /// </summary>
    public partial class DisplayUsersUC : UserControl
    {
        private ServiceModelClient GymService;
        private UserList users = new UserList();

        public DisplayUsersUC()
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            users = GymService.SelectAllUsers();
            Users.ItemsSource = users;
        }

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Users.SelectedIndex == -1) return;
            User user = Users.SelectedItem as User;

            AdminUserStatus adminUserStatus = new AdminUserStatus(user);
            refreshWithDelay(adminUserStatus);

        }
        public async void refreshWithDelay(AdminUserStatus adminUserStatus)
        {
            EditSP.Children.Clear();
            await Task.Delay(300);
            EditSP.Children.Add(adminUserStatus);

        }

    }
}

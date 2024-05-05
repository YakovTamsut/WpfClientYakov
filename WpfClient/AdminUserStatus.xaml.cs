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
    /// Interaction logic for AdminUserStatus.xaml
    /// </summary>
    public partial class AdminUserStatus : UserControl
    {
        private ServiceModelClient GymService;
        private ManagerWindow window;
        User clickedUser;
        public AdminUserStatus(User user, ManagerWindow window)
        {
            InitializeComponent();
            this.window = window;
            GymService = new ServiceModelClient();
            userItem.Header = user.Firstname + " " + user.Lastname;
            clickedUser = user;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GymService.DeleteUser(clickedUser);
            window.ManageUsers_Selected();
        }

    }
}

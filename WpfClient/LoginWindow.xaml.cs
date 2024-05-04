using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private User user;
        private bool EmailOK, PassOK;
        private ServiceModelClient GymService;

        public LoginWindow()
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            EmailOK = PassOK = false;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!EmailOK || !PassOK)
            {
                MessageBox.Show("Invalid email/password");
                return;
            }
            user = new User { Email = tbxEmail.Text, Password = tbxPass.Password };
            user = GymService.Login(user);
            if (user == null)
            {
                MessageBox.Show("no user detected");
                return;
            }
            if (user.IsManager)
            {
                MessageBox.Show("nice Login");
                ManagerWindow mw = new ManagerWindow(user);
                this.Close();
                mw.ShowDialog();
            }
            else
            {
                MessageBox.Show("Regular user login");
                UserWindow wp = new UserWindow(user);
                this.Close();
                wp.ShowDialog();
            }
            tbxEmail.Text = tbxPass.Password = string.Empty;
        }
        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidEmail email = new ValidEmail();
            ValidationResult result = email.Validate(tbxEmail.Text, null);
            if (!result.IsValid)
            {
                tbxEmail.BorderBrush = Brushes.Red;
                tbxEmail.Foreground = Brushes.Red;
                tbxEmail.ToolTip = result.ErrorContent.ToString();
                HintAssist.SetHelperText(tbxEmail, result.ErrorContent.ToString());
                EmailOK = false;
            }
            else
            {
                tbxEmail.BorderBrush = Brushes.White;
                tbxEmail.Foreground = Brushes.White;
                HintAssist.SetHelperText(tbxEmail, null);
                tbxEmail.ToolTip = null;
                EmailOK = true;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword pass = new ValidPassword();
            ValidationResult result = pass.Validate(tbxPass.Password, null);
            if (!result.IsValid)
            {
                tbxPass.BorderBrush = Brushes.Red;
                tbxPass.Foreground = Brushes.Red;
                tbxPass.ToolTip = result.ErrorContent.ToString();
                HintAssist.SetHelperText(tbxPass, result.ErrorContent.ToString());
                PassOK = false;
            }
            else
            {
                tbxPass.BorderBrush = Brushes.White;
                tbxPass.Foreground = Brushes.White;
                HintAssist.SetHelperText(tbxPass, null);
                tbxPass.ToolTip = null;
                PassOK = true;
            }
        }

        private void xxx_Click(object sender, RoutedEventArgs e)
        {
            user = new User { Email = "moshe@gmail.com", Password ="Moshe_123" };
            user = GymService.Login(user);
            UserWindow wp = new UserWindow(user);
            wp.ShowDialog();
            this.Close();
           
        }

        private void Signup_Click(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow signUp = new SignUpWindow();
            this.Hide();
            signUp.ShowDialog();
            this.Show();
        }
    }
}

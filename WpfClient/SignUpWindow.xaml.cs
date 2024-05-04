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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private ServiceModelClient GymService;
        private User user;
        private bool pass;

        public SignUpWindow()
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            user = new User();
            mainGrid.DataContext = user;
            pass = false;
            tbxBirthDay.DisplayDateStart = DateTime.Today.AddYears(-120);
            tbxBirthDay.DisplayDateEnd = DateTime.Today.AddYears(-16);
            user.BirthDay = DateTime.Today.AddYears(-16);
            tbxEmail.Focus();
        }
        private bool CheckData()
        {
            if (tbxEmail.Text.Equals(string.Empty)) return false;
            if (tbxFirstName.Text.Equals(string.Empty)) return false;
            if (tbxLastName.Text.Equals(string.Empty)) return false;
            if (tbxPass.Password.Equals(string.Empty)) return false;
            if (tbxBirthDay.Text.Equals(string.Empty)) return false;
            if (tbxGender.Text.Equals(string.Empty)) return false;
            if (Validation.GetHasError(tbxEmail)) return false;
            if (Validation.GetHasError(tbxFirstName)) return false;
            if (Validation.GetHasError(tbxLastName)) return false;
            if (Validation.GetHasError(tbxPass)) return false;
            if (Validation.GetHasError(tbxBirthDay)) return false;
            if (Validation.GetHasError(tbxGender)) return false;
            if(!pass) return false;
            return true;
        }
        private void Login_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                MessageBox.Show("error in data");
                return;
            }
            //Check if username is in use?
            if (!GymService.IsEmailFree(tbxEmail.Text))
            {
                MessageBox.Show("email already used");
                return;
            }
            //Username if free, Create new user
            user.Password = tbxPass.Password;
            user.IsManager = false;
            //Send to service
            if (GymService.NewUser(user) != 1)
            {
                MessageBox.Show("account successfuly created");
                LoginWindow login = new LoginWindow();
                this.Close();
                login.ShowDialog();
                return;
            }
            //finish and close
        }
        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword valid = new ValidPassword();

            ValidationResult result = valid.Validate(tbxPass.Password, null);
            if (result.IsValid)
            {
                pass = true;
                tbxPass.BorderBrush = Brushes.White;
                HintAssist.SetHelperText(tbxPass, "Password");
            }
            else
            {
                pass = false;
                tbxPass.BorderBrush = Brushes.Red;
                HintAssist.SetHelperText(tbxPass, result.ErrorContent.ToString());
            }
        }
    }
}

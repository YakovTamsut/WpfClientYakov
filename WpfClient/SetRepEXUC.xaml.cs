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
using WpfClient;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SetRepEXUC.xaml
    /// </summary>
    public partial class SetRepEXUC : UserControl
    {
        private Exercise exercise;
        private CreateWorkoutUC parent;
        public SetRepEXUC(Exercise exercise, CreateWorkoutUC uc)
        {
            InitializeComponent();
            this.exercise = exercise;
            this.parent = uc;
            setsTB.Focus();
        }

        private void Finished_Click(object sender, RoutedEventArgs e)
        {
            ValidNumber valid = new ValidNumber();
            if(repsTB.Text.Length == 0||  setsTB.Text.Length == 0)
            {
                MessageBox.Show("Empty field");
                return;
            }
            if (!valid.Validate(setsTB, null).IsValid || !valid.Validate(repsTB, null).IsValid)
            {
                MessageBox.Show(valid.Validate(setsTB, null).ErrorContent.ToString());
                return;
            }
            parent.AddEx(exercise, setsTB.Text, repsTB.Text);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            parent.Clear();
        }
    }
}

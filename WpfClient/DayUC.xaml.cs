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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for DayUC.xaml
    /// </summary>
    public partial class DayUC : UserControl
    {
        private string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        public DayUC(WorkoutsUC wuc)
        {
            InitializeComponent();
            dayTB.Text = days[wuc.GetDay() - 1];
            workoutST.Children.Add(wuc);
        }
        public DayUC(int num)
        {
            InitializeComponent();
            dayTB.Text = days[num - 1];
        }
    }
}

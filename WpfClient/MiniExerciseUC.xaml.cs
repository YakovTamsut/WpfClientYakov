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
    /// Interaction logic for MiniExerciseUC.xaml
    /// </summary>
    public partial class MiniExerciseUC : UserControl
    {
        public Exercise currentEx;
        private CreateWorkoutUC parent = null;
        public ExerciseInWorkOut exinwo;
        public MiniExerciseUC(JewGymService.Exercise exercise)
        {
            InitializeComponent();
            mainGrid.Children.Remove(deletebtn);
            currentEx = exercise;
            this.DataContext = exercise;
            tbInfo.Text = exercise.TargetMuscle;
            exNameTB.Text = exercise.ExerciseName.Replace('-', ' ');

            try
            {
                img.Source = new BitmapImage(new Uri(exercise.ExerciseUrl));
            }
            catch (Exception)
            {
                img.Source = new BitmapImage(new Uri($"pack://application:,,,/Sources/Logo.png"));
            }
        }

        public MiniExerciseUC(JewGymService.Exercise exercise, ExerciseInWorkOut exinwo, bool isActive, bool isCreate,CreateWorkoutUC createWorkoutUC)
        {
            InitializeComponent();
            parent = createWorkoutUC;
            if (parent == null)
            {
                mainGrid.Children.Remove(deletebtn);
            }   
            else
            {
                this.Width = parent.ExLB.Width-20;
            }
            this.DataContext = exercise;
            this.currentEx = exercise;
            this.exinwo = exinwo;
            tbInfo.Text = exercise.TargetMuscle;
            setsTB.Text = " " + exinwo.Sets + "x" + exinwo.Reps;
            exNameTB.Text = exercise.ExerciseName.Replace('-', ' ');
            if (!isCreate)
            {
                if (!isActive)
                {
                    tbInfo.FontSize = 12;
                    setsTB.FontSize = 12;
                    setsTB.FontSize = 12;
                    exNameTB.FontSize = 12;
                    img.Height = 40;
                    img.Width = 40;
                }
                else
                {
                    tbInfo.FontSize = 20;
                    setsTB.FontSize = 20;
                    setsTB.FontSize = 20;
                    exNameTB.FontSize = 20;
                    img.Height = 60;
                    img.Width = 60;
                }
            }
           
            try
            {
                img.Source = new BitmapImage(new Uri(exercise.ExerciseUrl));
            }
            catch (Exception)
            {
                img.Source = new BitmapImage(new Uri($"pack://application:,,,/Sources/Logo.png"));
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            parent.RemoveMini(this);
            //MiniExerciseUC itemToRemove = parent.ExLB.Items.Cast<MiniExerciseUC>()
            // .FirstOrDefault(item => item == this);
            //if (itemToRemove != null)
            //{
            //    if (currentEx.IsCompound)
            //    {
            //        parent.countCompoundSets -= exinwo.Sets;
            //    }
            //    else
            //    {
            //        parent.countSets -= exinwo.Sets;
            //    }
            //    parent.exInWoList.Remove(this.exinwo);
            //    parent.ExLB.Items.Remove(itemToRemove);
            //}

        }
    }
}

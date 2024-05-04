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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for WorkoutsUC.xaml
    /// </summary>
    /// 
    
    public partial class WorkoutsUC : UserControl
    {
        private ServiceModelClient GymService;
        public WorkoutPlan workPlan;
        private ExerciseInWorkOutList exInWoList;
        private bool isLarge;
        protected bool isUp = false;
        public bool isTrash = false;

        public WorkoutsUC(WorkoutPlan wp, bool isLarge)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            workPlan = wp;
            this.isLarge = isLarge;
            if (workPlan != null)
                LoadData();
            else
            {
                mainBorder.Height = 700;
                mainBorder.Width = 550;
                this.Height = 850;
                this.Width = 600;
                titleTB.Text = "No Workout today";
                today.Height = 40;
                today.FontSize = 24;
            }

        }

        public WorkoutsUC(bool isTrash)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            this.isTrash = isTrash;
            if (this.isTrash)
            {
                titleTB.Text = "Move to trash";
                this.Width = 260;
                this.Height = 140;
                mainBorder.BorderBrush = Brushes.DimGray;
                mainBorder.Background = Brushes.LightGray;
            }
            else
            {
                this.Height = 400;
                this.Width = 320;
                titleTB.Text = "No Workout";
                titleTB.VerticalAlignment = VerticalAlignment.Center;
                mainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ADD8E6")); // Light blue
            }


        }
        private void LoadData()
        {
            MiniExLB.Items.Clear();
            if (workPlan == null)
            {
                titleTB.Text = "No Workout";
                return;
            }
            exInWoList = GymService.SelectExInByWorkOut(workPlan.Workout);
            if (exInWoList == null)
            {
                titleTB.Text = "No Workout";
                return;
            }


            int sum = 0;
            int count = 0;
            double avg = 0;
            if (!isLarge)
            {
                if (workPlan.Day != 0)
                {
                    foreach (ExerciseInWorkOut exInWo in exInWoList)
                    {
                        MiniExLB.Items.Add(new MiniExerciseUC(exInWo.Exercise, exInWo, false, false, null));
                        count++;
                        sum += exInWo.Exercise.Difficulty;
                    }
                }
                else
                {
                    mainBorder.Height = 100;
                    mainBorder.Width = 260;
                    MiniExLB.Height = 0;
                    titleTB.FontSize = 20;
                    this.Height = 150;
                    foreach (ExerciseInWorkOut exInWo in exInWoList)
                    {
                        MiniExLB.Items.Add(new MiniExerciseUC(exInWo.Exercise, exInWo, false, false, null));
                        count++;
                        sum += exInWo.Exercise.Difficulty;
                    }
                }
            }
            else
            {
                foreach (ExerciseInWorkOut exInWo in exInWoList)
                {
                    MiniExLB.Items.Add(new MiniExerciseUC(exInWo.Exercise, exInWo, true, false, null));
                    count++;
                    sum += exInWo.Exercise.Difficulty;
                }
            }
            //Border color by ex difficulty
            avg = sum / count;
            if (avg > 2)
            {
                mainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3333"));
            }
            else
            {
                if (avg > 1.5) 
                {
                    mainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCC00"));
                }
                else
                {
                    mainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33CC33"));
                }
            }

            if (workPlan.Workout.Duration >= 60)
            {
                int hours, minutes;
                hours = workPlan.Workout.Duration / 60;
                minutes = workPlan.Workout.Duration % 60;
                titleTB.Text = workPlan.Workout.Type.ToString() + " " + hours.ToString() + "h " + minutes.ToString() + "m";
            }
            else
            {
                titleTB.Text = workPlan.Workout.Type.ToString() + " " + workPlan.Workout.Duration.ToString() + "m";
            }

            if (isLarge) //Large view
            {
                mainBorder.Height = 700;
                mainBorder.Width = 550;
                titleTB.FontSize = 24;
                this.Height = 850;
                this.Width = 600;
                MiniExLB.Width = 500;
                MiniExLB.Height = 650;
                today.Text = "Today's workout:";
                today.Height = 40;
            }
        }

        public int GetDay()
        {
            return workPlan.Day;
        }

        internal void ChangePlan(WorkoutPlan plan, bool isInUse)
        {
            this.Tag = workPlan = plan;
            this.isLarge = false;
            titleTB.Text = "No Workout";
            titleTB.VerticalAlignment = VerticalAlignment.Center;
            MiniExLB.Items.Clear();
            mainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ADD8E6")); // Light blue
            if (plan!=null && plan.ID!=0) LoadData();
        }

        internal void AnimateSize()
        {
            double targetWidth = isUp ? 300 : 260;
            double targetHeight = isUp ? 380 : 100;
            double listheight = isUp ? 340 : 0; 


            // Create the animation for width
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Create the animation for height
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };

            DoubleAnimation listAnimation = new DoubleAnimation
            {
                To = listheight,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Apply the animations to the stackPanel's width and height properties
            mainBorder.BeginAnimation(Border.WidthProperty, widthAnimation);
            mainBorder.BeginAnimation(Border.HeightProperty, heightAnimation);
            MiniExLB.BeginAnimation(ListBox.HeightProperty, listAnimation);

        }

        public void SetIsUp(bool isup)
        {
            this.isUp = isup;
        }
        public bool GetIsUp()
        {
            return this.isUp;
        }
        internal void Selected()
        {
            throw new NotImplementedException();
        }

        
    }
}

using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CreateWorkoutUC.xaml
    /// </summary>
    public partial class CreateWorkoutUC : UserControl
    {
        private ServiceModelClient GymService;
        public ExerciseInWorkOutList exInWoList;
        private Exercise exercise;
        private Workout workout;
        public int countCompoundSets = 0;
        public int countSets = 0;
        ExerciseUC exerciseUC;
        SetRepEXUC setRepEXUC;
        private UserWindow window=null;
        private ManagerWindow windowManager=null;
        public WorkoutPlan wop;
        private bool isEdit = false;

        public CreateWorkoutUC(User user, UserWindow window)
        {
            InitializeComponent();
            exInWoList=new ExerciseInWorkOutList(); 
            DataContext = this;
            exercise=new Exercise();
            workout=new Workout();                          
            workout.User = user;
            wop = new WorkoutPlan();
            wop.User = user;
            wop.Day = 0;
            GymService = new JewGymService.ServiceModelClient();
            this.window = window;
            mainGrid.Children.Remove(backbtn);
        }
        public CreateWorkoutUC(WorkoutPlan plan, ManagerWindow window)
        {
            InitializeComponent();
            isEdit = true;
            exInWoList = new ExerciseInWorkOutList();
            DataContext = this;
            exercise = new Exercise();
            workout = plan.Workout;
            workout.User = plan.User;
            wop = plan;
            typeTB.Text = wop.Workout.Type;
            GymService = new JewGymService.ServiceModelClient();
            this.windowManager = window;
            if (plan.Workout.ExInWorkout != null)
            {
                foreach (ExerciseInWorkOut exercise in plan.Workout.ExInWorkout)
                {
                    AddEx(exercise.Exercise, exercise.Sets.ToString(), exercise.Reps.ToString());
                }
            }
        }
        public void CloseEXhost()
        {
            exercise = exerciseUC.SelectedEx;
            if (exercise != null)
            {
                Clear();
                setRepEXUC = new SetRepEXUC(exercise, this);
                main.Height = 230;
                main.Width = 280;
                main.ShowDialog(setRepEXUC);
            }
        }

        private void AddEx_Click(object sender, RoutedEventArgs e)
        {
            exerciseUC = new ExerciseUC(this);
            main.Height = 800;
            main.Width = 480;
            main.IsOpen = false;
            main.ShowDialog(exerciseUC);
        }

        internal void AddEx(Exercise exercise, string sets, string reps)
        {
            main.Visibility = Visibility.Visible;
            main.IsOpen = true;
            if (sets == "" || reps == "")
            {
                MessageBox.Show("need to enter sets and reps");
                return;
            }
            ExerciseInWorkOut exInWo = new ExerciseInWorkOut { Exercise = exercise, Sets=int.Parse(sets), Reps=int.Parse(reps)};
            exInWoList.Add(exInWo);
            MiniExerciseUC uc = new MiniExerciseUC(exercise, exInWo, true, true,this);
            ExLB.Items.Add(uc);
            ExLB.ScrollIntoView(ExLB.Items[ExLB.Items.Count - 1]);
            if (exercise.IsCompound)
            {
                countCompoundSets += int.Parse(sets);
            }
            else
            {
                countSets += int.Parse(sets);
            }
            Clear();
        }

        public void Clear()
        {
            main.Content = null;
            main.IsOpen = false;
        }

        private void finish_click(object sender, RoutedEventArgs e)
        {
            if (typeTB.Text.Length == 0)
            {
                MessageBox.Show("workout name required");
                return;
            }
            if (ExLB.HasItems)
            {
                workout.Type = typeTB.Text;
                workout.Duration = countSets * 2 + countCompoundSets * 4;
                workout.ExInWorkout = exInWoList;
                if (workout.ID == 0)
                {
                    workout.ID = GymService.InsertWorkout(workout);
                    wop.Workout = workout;
                    GymService.InsertWorkoutPlan(wop);
                }
                else
                {
                    GymService.UpdateWorkout(workout);
                }
                MessageBox.Show($"added {workout.Type} to your workouts");
                if (windowManager != null && windowManager.GetType() == typeof(ManagerWindow))
                {
                    windowManager.EditProgram_Selected();
                }
                else
                {
                    window.CreateProgramRefresh();
                }
            }
            else
            {
                if (isEdit)
                {
                    GymService.DeleteWorkout(workout);
                    MessageBox.Show("workout set as rest");
                    windowManager.EditProgram_Selected();
                }
                else
                {
                    MessageBox.Show("must add exercises");
                }
                return;
            }
        }

        internal void RemoveMini(MiniExerciseUC miniExerciseUC)
        {
            if (miniExerciseUC.currentEx.IsCompound)
            {
                countCompoundSets -= miniExerciseUC.exinwo.Sets;
            }
            else
            {
                countSets -= miniExerciseUC.exinwo.Sets;
            }
            exInWoList.Remove(miniExerciseUC.exinwo);
            ExLB.Items.Remove(miniExerciseUC);
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            windowManager.EditProgram_Selected();
        }
    }
}

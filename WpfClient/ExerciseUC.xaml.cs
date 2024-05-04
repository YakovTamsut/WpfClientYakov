using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ExerciseUC.xaml
    /// </summary>
    public partial class ExerciseUC : UserControl
    {
        private ServiceModelClient GymService;
        private ExerciseList exercises;
        public Exercise SelectedEx;
        private List<Exercise> exList;
        public StackPanel sp;
        public CreateWorkoutUC parent;
        public ExerciseUC(StackPanel usersp)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            exercises = GymService.SelectAllExercises();
            sp = usersp;
            LoadMiniExercises(exercises,null); // Call the method to load MiniExerciseUC controls
        }
        public ExerciseUC(CreateWorkoutUC uc)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            exercises = GymService.SelectAllExercises();
            parent = uc;
            LoadMiniExercises(exercises, null); // Call the method to load MiniExerciseUC controls
        }
        private void LoadMiniExercises(ExerciseList list,List<Exercise> xlist)
        {            
            MiniExLB.Items.Clear();

            if (list != null)
            {
                // Add MiniExerciseUC controls to the collection
                foreach (var exercise in list)
                    MiniExLB.Items.Add(new MiniExerciseUC(exercise));
            }
            if (xlist != null)
            {
                // Add MiniExerciseUC controls to the collection
                foreach (var exercise in xlist)
                    MiniExLB.Items.Add(new MiniExerciseUC(exercise));
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void BodyPart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
        public void Search()
        {
            if (typeCB == null || bodypartCB == null || exercises==null) return;
            string bodypart = bodypartCB.SelectedValue.ToString().Substring(bodypartCB.SelectedValue.ToString().LastIndexOf(": ") + 2);
            string extype = typeCB.SelectedValue.ToString().Substring(bodypartCB.SelectedValue.ToString().LastIndexOf(": ") + 2);
            string[] words = tbSearch.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string search = string.Join("-", words).Trim().ToUpper();

            bodypart = bodypart == "Any Body Part" ? "" : bodypart.ToUpper(); ;
            extype = extype == "Any Category" ? "" : extype.ToUpper(); ;
            exList = exercises.FindAll(ex => (ex.ExerciseName.ToUpper().Contains(search) && 
                                              ex.TargetMuscle.ToUpper().Contains(bodypart) && 
                                              ex.Type.ToUpper().Contains(extype)));
            LoadMiniExercises(null, exList);
        }


        private void MiniExLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MiniExLB.SelectedItem != null)
            {
                var x = MiniExLB.SelectedItem as MiniExerciseUC;
                SelectedEx = x.currentEx;
                //MessageBox.Show(x.currentEx.ExerciseName.ToString());
                parent.CloseEXhost();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sp != null)
            {
                sp.Children.Clear();
                SelectedEx = null;
            }
            if (parent.main != null)
            {
                parent.Clear();
            }
        }
    }
}

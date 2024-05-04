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
using static System.Net.Mime.MediaTypeNames;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ProgramPlanUC.xaml
    /// </summary>

    public partial class ProgramPlanUC : UserControl
    {

        private ServiceModelClient GymService;
        private ManagerWindow window;
        private User currentUser;
        private bool isEditing;
        public ProgramPlanUC(User user, ManagerWindow window)
        {
            InitializeComponent();
            isEditing = false;
            this.window = window;
            this.currentUser = user;
            PlanNameTextBlock.Text = user.Firstname;
            bool[] empty = new bool[7];
            GymService=new ServiceModelClient();
            //PlanNameSP.Child = new EditWorkoutUC(user);
            foreach (WorkoutPlan wp in GymService.GetUserWorkoutPlans(user))
            {
                if (wp.Day != 0)
                {
                    //ExWP.Children.Add(uc);
                    EditWorkoutUC ewo = new EditWorkoutUC(wp);
                    ewo.Tag = ewo.workPlan;
                    ewo.MouseUp += Ewo_MouseUp;
                    switch (wp.Day)
                    {
                        case 1:
                            SundaySP.Children.Add(ewo);
                            empty[0] = true;
                            break;
                        case 2:
                            MondaySP.Children.Add(ewo);
                            empty[1] = true;
                            break;
                        case 3:
                            TuesdaySP.Children.Add(ewo);
                            empty[2] = true;
                            break;
                        case 4:
                            WednesdaySP.Children.Add(ewo);
                            empty[3] = true;
                            break;
                        case 5:
                            ThursdaySP.Children.Add(ewo);
                            empty[4] = true;
                            break;
                        case 6:
                            FridaySP.Children.Add(ewo);
                            empty[5] = true;
                            break;
                        case 7:
                            SaturdaySP.Children.Add(ewo);
                            empty[6] = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < empty.Length; i++)
            {
                EditWorkoutUC ewo = new EditWorkoutUC();
                WorkoutPlan workPlan = new WorkoutPlan();
                workPlan.Day = i + 1;
                workPlan.User = user;
                workPlan.Workout = new Workout();
                ewo.workPlan = workPlan;
                ewo.Tag = ewo.workPlan;

                if (empty[i] == false)
                {
                    ewo.MouseUp += Ewo_MouseUp;

                    switch (i)
                    {
                        case 0:
                            SundaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 1:
                            MondaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 2:
                            TuesdaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 3:
                            WednesdaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 4:
                            ThursdaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 5:
                            FridaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        case 6:
                            SaturdaySP.Children.Add(ewo);
                            empty[i] = true;
                            break;
                        default:
                            break;

                    }
                }
            }

        }
        private void Ewo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            EditWorkoutUC ewo = sender as EditWorkoutUC;
            WorkoutPlan plan = ewo.workPlan as WorkoutPlan;
            if (plan != null)
            {
                CreateWorkoutUC createWorkoutUC = new CreateWorkoutUC(plan, window);
                window.LoadCreateWorkout(createWorkoutUC);
            }

        }

        private void ToggleEditMode(object sender, RoutedEventArgs e)
        {
            isEditing = !isEditing;  // Toggle the editing state

            if (isEditing)
            {
                // Show the TextBox for editing, hide the TextBlock
                PlanNameTextBlock.Visibility = Visibility.Collapsed;
                PlanNameTextBox.Text = PlanNameTextBlock.Text;  // Set TextBox content
                PlanNameTextBox.Visibility = Visibility.Visible;
                PlanNameTextBlock.Height = 0;
                PlanNameTextBox.Height = 20;
                ToggleButtonText.Text = "Confirm";  // Change button text
            }
            else
            {
                // Hide the TextBox and show the TextBlock with updated text
                currentUser.Firstname = PlanNameTextBox.Text;
                GymService.UpdateUser(currentUser);
                PlanNameTextBlock.Text = PlanNameTextBox.Text;  // Update displayed text
                PlanNameTextBlock.Visibility = Visibility.Visible;
                PlanNameTextBox.Visibility = Visibility.Collapsed;
                PlanNameTextBlock.Height = 20;
                PlanNameTextBox.Height = 0;
                ToggleButtonText.Text = "Edit Name";  // Change button text
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("do you want to delete this plan PEREMENANTLY?");
            GymService.DeleteUser(currentUser);
            window.planlist.Remove(currentUser);
            window.EditProgram_Selected();
        }

       
    }
}


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using DesktopApp.Model;
using DesktopApp.ViewModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace DesktopApp.ViewModel
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Student> users;
        [ObservableProperty]
        public Student selectedUser = null;


        
        public void CloseMainWindow()
        {
            Application.Current.MainWindow.Close();
        }
        



        [RelayCommand]
        public void messeag()
        {

            MessageBox.Show($"{selectedUser.FirstName} GPA value must be between 0 and 4.", "Error");
        }

        [RelayCommand]
        public void AddStudent()
        {
            var vm = new AddStudentVM();
            vm.title = "ADD USER";
            AddStudentWindow window = new AddStudentWindow(vm);

            window.ShowDialog();

            if (vm.student!= null)
            {
                users.Add(vm.student);
            }
            else
                return;
        }

        [RelayCommand]
        public void Delete()
        {
            if (selectedUser != null)
            {
                string name = selectedUser.FirstName;
                users.Remove(selectedUser);
                
                
                
                var messageBox = new Window()
                {
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2e3137")),
                    Foreground = Brushes.White,
                    Width = 300,
                    Height = 100,
                    Topmost = true,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new TextBlock()
                    {
                        Text = $"{name} is Deleted successfully.",
                        FontSize = 16,
                        Padding = new Thickness(20),
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2); // Change the value to adjust how long the message box stays open.
                timer.Tick += (sender, e) => { messageBox.Close(); };
                timer.Start();

                messageBox.ShowInTaskbar = false;
                messageBox.ShowDialog();
                

            }
            else
            {
                
                var messageBox = new Window()
                {
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2e3137")),
                    Foreground = Brushes.White,
                    Width = 300,
                    Height = 100,
                    Topmost = true,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new TextBlock()
                    {
                        Text = $"Delete Failed !! Select any student from the list to delete",
                        FontSize = 16,
                        Padding = new Thickness(20),
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3); // Change the value to adjust how long the message box stays open.
                timer.Tick += (sender, e) => { messageBox.Close(); };
                timer.Start();

                messageBox.ShowInTaskbar = false;
                messageBox.ShowDialog();

            }
                
        }
        [RelayCommand]
        public void ExecuteEditStudentCommand()
        {
            if (selectedUser != null)
            {
                var vm = new AddStudentVM(selectedUser);
                vm.title = "EDIT USER";
                var window = new AddStudentWindow(vm);

                window.ShowDialog();


                int index = users.IndexOf(selectedUser);
                users.RemoveAt(index);
                users.Insert(index, vm.student);



            }
            else
            {
                //MessageBox.Show("Please Select the student", "Warning!");
                var messageBox = new Window()
                {
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2e3137")),
                    Foreground = Brushes.White,
                    Width = 300,
                    Height = 100,
                    Topmost = true,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new TextBlock()
                    {
                        Text = $"Please select a student",
                        FontSize = 16,
                        Padding = new Thickness(20),
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2); // Change the value to adjust how long the message box stays open.
                timer.Tick += (sender, e) => { messageBox.Close(); };
                timer.Start();

                messageBox.ShowInTaskbar = false;
                messageBox.ShowDialog();
            }
        }

        public MainWindowVM()
        {
            users = new ObservableCollection<Student>();
            BitmapImage image1 = new BitmapImage(new Uri("/Model/Images/1.png", UriKind.Relative));
            users.Add(new Student(4001, "Charlie", "Brown", "01/11/1998", image1,3.1));
            BitmapImage image2 = new BitmapImage(new Uri("/Model/Images/2.png", UriKind.Relative));
            users.Add(new Student(4002, "John", "Smith", "01/12/1999", image2,2.044));
            BitmapImage image3 = new BitmapImage(new Uri("/Model/Images/3.png", UriKind.Relative));
            users.Add(new Student(4003, "Chris", "Brown", "07/04/1999", image3,3.15));
            BitmapImage image4 = new BitmapImage(new Uri("/Model/Images/4.png", UriKind.Relative));
            users.Add(new Student(4004, "Ann", "Grande", "18/03/2000", image4,3.01));

        }
    }
}


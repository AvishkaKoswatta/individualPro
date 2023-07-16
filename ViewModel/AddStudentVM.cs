using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using DesktopApp.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace DesktopApp.ViewModel
{
    public partial class AddStudentVM : ObservableObject

    {


        [ObservableProperty]
        public string firstname;


        [ObservableProperty]
        public string lastname;

        [ObservableProperty]
        public int reg;

        [ObservableProperty]
        public string dateofbirth;

        [ObservableProperty]
        public double gpa;



        



        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public BitmapImage selectedImage;



        public AddStudentVM(Student u)
        {
            student = u;

            firstname = student.FirstName;
            lastname = student.LastName;
            reg = student.Reg;
            gpa = student.GPA;
            dateofbirth = student.DateOfBirth;
            selectedImage = student.Image;

        }
        public AddStudentVM()
        {

        }


        //get image 
        [RelayCommand]
        public void UploadPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                selectedImage = new BitmapImage(new Uri(dialog.FileName));


                var messageBox = new Window()
                {
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#545d6a")),
                    Foreground = Brushes.White,
                    Width = 300,
                    Height = 100,
                    Topmost = true,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new TextBlock()
                    {
                        Text = $"Photo added successfully!",
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






        public Student student { get; private set; }
        public Action CloseAction { get; internal set; }

        [RelayCommand]
        public void Save()
        {



            if (gpa < 0 || gpa > 4)
            {
                //MessageBox.Show("GPA value must be between 0 and 4.", "Error");

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
                        Text = $"GPA value must be between 0 and 4",
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

                return;
            }
            if (student == null)
            {

                student = new Student()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Reg = reg,
                    DateOfBirth = dateofbirth,
                    Image = selectedImage,

                    GPA = gpa

                };


            }
            else
            {

                student.FirstName = firstname;
                student.LastName = lastname;
                student.Reg = reg;
                student.GPA = gpa;
                student.Image = selectedImage;
                student.DateOfBirth = dateofbirth;



            }

            if (student.FirstName != null)
            {

                //CloseAction();
                this.CloseAction();
            }
            Application.Current.MainWindow.Show();




        }


    }
}

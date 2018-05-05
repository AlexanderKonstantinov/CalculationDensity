using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CalcEventDensity.Models;
using CalcEventDensity.Services;

namespace CalcEventDensity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string pathToNewFile = string.Empty;
        
        

        public int GridStep
            => Convert.ToInt32(slGridStep.Value);
        
        public MainWindow()
        {

            InitializeComponent();

            //var viewModel = new MainViewModel(this);

            //MainGrid.DataContext = viewModel;
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
           
        }
        
        public void Calculate()
        {
            //this.Hide();

            //var thr = new Thread(ShowScreen);
            //thr.SetApartmentState(ApartmentState.STA);
            //thr.IsBackground = true;
            //thr.Start();


            //if (Dimension == Dimension.D2)
            //    Calculation2D(ref pathToNewFile);
            //else if (Dimension == Dimension.D3)
            //    Calculation3D(ref pathToNewFile);
            
            //if(thr.IsAlive)
            //    HideScreen();
            
            //this.Show();

            //if (File.Exists(pathToNewFile))
            //    Process.Start(pathToNewFile);
        }


        private void Calculation2D(ref string pathToNewFile)
        {
            //List<IPoint> events = new List<IPoint>();
            //List<IPoint> gridPoints = new List<IPoint>();

            //if (ReadDataService.ReadData(events, gridPoints, Dimension))
            //{
            //    var pointContainer = new PointContainer<Point2D>(
            //        events.Cast<Point2D>().ToList(),
            //        gridPoints.Cast<Point2D>().ToList());

            //    var calculationService2D = new CalculationService2D(pointContainer, GridStep, cbGridPoints.IsChecked == true);
            //    calculationService2D.Calculate();

            //    pathToNewFile = WriteDataService<Point2D>.WriteFile(ReadDataService.PathToInitialFile, pointContainer);
            //}
        }

        private void Calculation3D(ref string pathToNewFile)
        {
            //List<IPoint> events = new List<IPoint>();
            //List<IPoint> gridPoints = new List<IPoint>();

            //if (ReadDataService.ReadData(events, gridPoints, Dimension))
            //{
            //    var pointContainer = new PointContainer<Point3D>(
            //        events.Cast<Point3D>().ToList(),
            //        gridPoints.Cast<Point3D>().ToList());

            //    var calculationService3D = new CalculationService3D(pointContainer, GridStep, cbGridPoints.IsChecked == true);
            //    calculationService3D.Calculate();

            //    pathToNewFile = WriteDataService<Point3D>.WriteFile(ReadDataService.PathToInitialFile, pointContainer);
            //}
        }

        //private SplashScreen splashScreen;

        private void ShowScreen()
        {
            //splashScreen = new SplashScreen();

            //splashScreen.Show();

            //Dispatcher.Run();
        }

        private void HideScreen()
        {
            //if (splashScreen != null)
            //{
            //    if (splashScreen.Dispatcher.CheckAccess())
            //        splashScreen.Close();
            //    else
            //        splashScreen.Dispatcher.Invoke(DispatcherPriority.Normal,
            //            new ThreadStart(splashScreen.Close));
            //}
        }

        private void BtnCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            //this.Hide();

            //splashScreen = new SplashScreen();
            //splashScreen.Show();

            //this.Show();
        }
    }
}

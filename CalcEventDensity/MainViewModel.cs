using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CalcEventDensity.Infrastructure;
using CalcEventDensity.Models;
using CalcEventDensity.Services;
using DevExpress.Mvvm;

namespace CalcEventDensity
{
    public class MainViewModel : ViewModelBase
    {
        #region Binding fields
        public bool IsSelected3D { get; set; }
        public int GridStep { get; set; }
        public string ChoosedFile { get; set; } 
        #endregion
        public bool DoItGridPoints { get; set; }


        private readonly MainWindow mainWindow;
        public static string pathToNewFile = string.Empty;

        


        private Dimension Dimension => IsSelected3D
                                        ? Dimension.D3
                                        : Dimension.D2;
        
        #region Constructors
        public MainViewModel()
        {
            IsSelected3D = true;
            DoItGridPoints = true;
            GridStep = 5;
        }
        #endregion

        public ICommand ChooseFileCommand => new DelegateCommand(() =>
        {
            if (ReadDataService.OpenFile(Dimension))
                ChoosedFile = ReadDataService.PathToInitialFile.Name;
        });

        

        private RelayCommand calculateCommand;

        public ICommand CalculateCommand => calculateCommand
                                           ?? (calculateCommand = new RelayCommand(ExecuteCalculateCommandCommand, CanExecuteCalculateCommandCommand));

        private bool CanExecuteCalculateCommandCommand(object o) => true;
        //private bool CanExecuteCalculateCommandCommand(object o)
        //    => File.Exists(ReadDataService.PathToInitialFile?.FullName) &&
        //       int.TryParse(mainWindow.tbGridStep.Text, out int n);
        
        private SplashScreen splashScreen;
        private void ExecuteCalculateCommandCommand(object mainWindow)
        {
            var window = mainWindow as Window;
            if (window != null)
            {
                window.Hide();

                splashScreen = new SplashScreen();
                splashScreen.Show();

                if (Dimension == Dimension.D3)
                    Calculation3D(ref pathToNewFile);
                else
                    Calculation2D(ref pathToNewFile);

                splashScreen.Hide();
                window.Show();
            }
            

            

            //this.Show();

            //MessageBox.Show($"{nameof(Dimension)}\t{Dimension.ToString()}\n" +
            //                $"{nameof(IsSelected3D)}\t{IsSelected3D}\n" +
            //                $"{nameof(GridStep)}\t{GridStep}");
            //mainWindow.Calculate();
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
            List<IPoint> events = new List<IPoint>();
            List<IPoint> gridPoints = new List<IPoint>();

            if (ReadDataService.ReadData(events, gridPoints, Dimension))
            {
                var pointContainer = new PointContainer<Point3D>(
                    events.Cast<Point3D>().ToList(),
                    gridPoints.Cast<Point3D>().ToList());

                var calculationService3D = new CalculationService3D(pointContainer, GridStep, DoItGridPoints);
                calculationService3D.Calculate();

                pathToNewFile = WriteDataService<Point3D>.WriteFile(ReadDataService.PathToInitialFile, pointContainer);
            }
        }
    }
}

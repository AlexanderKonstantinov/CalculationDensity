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
using System.Windows.Threading;
using CalcEventDensity.Infrastructure;
using CalcEventDensity.Models;
using CalcEventDensity.Services;
using CalcEventDensity.Services.Base;
using DevExpress.Mvvm;

namespace CalcEventDensity
{
    public class MainViewModel : ViewModelBase
    {
        private ICalculationService calculationService;

        #region Binding fields
        public bool IsSelected3D { get; set; }
        public int GridStep { get; set; }
        public string ChoosedFile { get; set; } 
        #endregion
        public bool DoItAddGridPoints { get; set; }


        private readonly MainWindow mainWindow;
        public static string pathToNewFile = string.Empty;

        


        private Dimension Dimension => IsSelected3D
                                        ? Dimension.D3
                                        : Dimension.D2;
        
        #region Constructors
        public MainViewModel()
        {
            splashScreen = new SplashScreen();
            IsSelected3D = true;
            DoItAddGridPoints = true;
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
            ?? (calculateCommand = new RelayCommand(ExecuteCalculateCommand, CanExecuteCalculateCommand));

        private bool CanExecuteCalculateCommand(object o)
        {
            
            return true;
        }
        //private bool CanExecuteCalculateCommand(object o)
        //    => File.Exists(ReadDataService.PathToInitialFile?.FullName) &&
        //       int.TryParse(mainWindow.tbGridStep.Text, out int n);

        private SplashScreen splashScreen;
        private void BeginCalculate()
        {
            
            splashScreen.Show();
        }

        private void ExecuteCalculateCommand(object mainWindow)
        {
            BeginCalculate();
            var window = mainWindow as Window;
            if (window != null)
            {
                //SplashScreen splashScreen = new SplashScreen();
                //splashScreen.Show();
                //BeginCalculate();
                window.Hide();

                

                
                if (Dimension == Dimension.D3)
                {
                    if (ReadDataService.ReadData(Dimension, out PointContainer<IPoint> container))
                    {
                        calculationService = new CalculationService3D(GridStep, DoItAddGridPoints, container);

                        //calculationService.OnCalculationEnd += () =>
                        //{
                        //    //splashScreen.Hide();
                        //    window.Show();
                        //};

                        calculationService.Calculate(ref pathToNewFile);

                        //pathToNewFile = WriteDataService<Point3D>.WriteFile(ReadDataService.PathToInitialFile, pointContainer);
                    }
                    else
                        Calculation2D(ref pathToNewFile);
                    
                }

                //MessageBox.Show($"{nameof(Dimension)}\t{Dimension.ToString()}\n" +
                //                $"{nameof(IsSelected3D)}\t{IsSelected3D}\n" +
                //                $"{nameof(GridStep)}\t{GridStep}");
                //mainWindow.Calculate();
            }
        }

        void Calculation2D(ref string pathToNewFile)
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

    }
}

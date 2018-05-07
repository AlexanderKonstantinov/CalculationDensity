using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CalcEventDensity.Infrastructure;
using CalcEventDensity.Models;
using CalcEventDensity.Models.Service;
using CalcEventDensity.Services;
using CalcEventDensity.Services.Base;
using CalcEventDensity.Views;
using DevExpress.Mvvm;

namespace CalcEventDensity.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public static event Action OnCalculationBegin;
        public static event Action OnCalculationEnd;

        private ICalculationService _calculationService;

        #region Binding fields
        public bool IsSelected3D { get; set; }

        public int pointRadius { get; set; }

        public string ChoosedFile { get; set; }

        public bool IsGridPoints { get; set; }
        #endregion
        
        private Dimension Dimension => IsSelected3D
                                        ? Dimension.D3
                                        : Dimension.D2;
        
        #region Constructors
        public MainViewModel()
        {
            IsSelected3D = true;
            IsGridPoints = true;
            pointRadius = 5;
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
        
        private void ExecuteCalculateCommand(object o)
        {
            string pathToNewFile = String.Empty;

            if (ReadDataService.ReadData(Dimension, out PointContainer<IPoint> container))
            {
                OnCalculationBegin?.Invoke();

                var calcParams = new CalculationParameters(pointRadius, IsGridPoints);
                    
                if (Dimension == Dimension.D3)
                    _calculationService = new CalculationService3D(calcParams, container);
                else
                    _calculationService = new CalculationService2D(calcParams, container);

                _calculationService.Calculate();
                
                pathToNewFile = WriteDataService.WriteFile(ReadDataService.PathToInitialFile, container);

                OnCalculationEnd?.Invoke();

                if (File.Exists(pathToNewFile))
                    Process.Start(pathToNewFile);

            }
        }
    }
}
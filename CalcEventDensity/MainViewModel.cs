using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CalcEventDensity.Infrastructure;
using CalcEventDensity.Models;
using CalcEventDensity.Services;

namespace CalcEventDensity
{
    public class MainViewModel
    {
        private readonly MainWindow mainWindow;

        public MainViewModel() { }

        public MainViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private RelayCommand calculateCommand;

        public ICommand CalculateCommand => calculateCommand
                                           ?? (calculateCommand = new RelayCommand(ExecuteCalculateCommandCommand, CanExecuteCalculateCommandCommand));

        private bool CanExecuteCalculateCommandCommand(object o)
            => File.Exists(ReadDataService.PathToInitialFile?.FullName) &&
               int.TryParse(mainWindow.tbGridStep.Text, out int n);

        private void ExecuteCalculateCommandCommand(object o)
        {
            mainWindow.Calculate();
        }
    }
}

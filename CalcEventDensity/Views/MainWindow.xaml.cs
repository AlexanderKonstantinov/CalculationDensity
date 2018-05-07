using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Navigation;
using System.Windows.Threading;
using CalcEventDensity.Services.Base;
using CalcEventDensity.ViewModels;

namespace CalcEventDensity.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private Page mainPage;
        private Page pageScreen;


        public MainWindow()
        {
            mainPage = new MainPage();
            //pageScreen = new PageScreen();

            InitializeComponent();

            mainFrame.Navigate(mainPage);

            //MainViewModel.OnCalculationBegin += ShowSplashScreen;
            //MainViewModel.OnCalculationEnd += HideSplashScreen;
        }

        private void ShowSplashScreen()
        {
            //mainFrame.Navigate(pageScreen);
        }

        private void HideSplashScreen()
        {
            this.Show();
            //mainFrame.Navigate(mainPage);
        }
    }
}

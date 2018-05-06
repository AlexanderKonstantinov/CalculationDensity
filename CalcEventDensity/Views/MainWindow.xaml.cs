using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CalcEventDensity.Services;

namespace CalcEventDensity.Views
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
        
        public void Calculate()
        {
            

            //this.Hide();

            //var thr = new Thread(ShowScreen);
            //thr.SetApartmentState(ApartmentState.STA);
            //thr.IsBackground = true;
            //thr.Start();


            //if (Dimension == Dimension.D2)
            //    CalculationService2D(ref pathToNewFile);
            //else if (Dimension == Dimension.D3)
            //    Calculation3D(ref pathToNewFile);

            //if(thr.IsAlive)
            //    HideScreen();

            //this.Show();

            //if (File.Exists(pathToNewFile))
            //    Process.Start(pathToNewFile);
        }


        //private CalculationScreen splashScreen;


        private CalculationScreen calculationScreen;
        private void ShowScreen()
        {
            calculationScreen = new CalculationScreen();
            
            System.Windows.Threading.Dispatcher.Run();
        }

        private void HideScreen()
        {
            if (calculationScreen != null)
            {
                if (calculationScreen.Dispatcher.CheckAccess())
                    calculationScreen.Close();
                else
                    calculationScreen.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new ThreadStart(calculationScreen.Close));
            }
        }

        private void BtnCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            
            //this.Hide();
            

            //var thr = new Thread(ShowScreen);
            //thr.SetApartmentState(ApartmentState.STA);
            //thr.IsBackground = true;
            //thr.Start();

            //CalculationService3D.OnCalculationEnd += () =>
            //{
            //    if (thr.IsAlive)
            //        HideScreen();

            //    this.Show();
            //};

            //if (Dimension == Dimension.D2)
            //    CalculationService2D(ref pathToNewFile);
            //else if (Dimension == Dimension.D3)
            //    Calculation3D(ref pathToNewFile);



        }
    }
}

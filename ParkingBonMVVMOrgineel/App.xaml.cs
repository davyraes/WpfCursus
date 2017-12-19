using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModel.ParkingBonVM vm = new ViewModel.ParkingBonVM(new Model.Bon());
            View.ParkingBonView view = new View.ParkingBonView();
            view.DataContext = vm;
            view.Show();
        }
    }
}

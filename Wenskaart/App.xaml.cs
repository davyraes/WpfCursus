using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wenskaart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModel.WenskaartVM vm = new ViewModel.WenskaartVM(new Model.Wenskaart());
            View.WenskaartView view = new View.WenskaartView();
            view.DataContext = vm;
            view.Show();
        }
    }
}

﻿using System;
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
            ViewModel.ParkingbonVM vm = new ViewModel.ParkingbonVM(new Model.ParkingBon());
            View.ParkingBonView parkingbon = new View.ParkingBonView();
            parkingbon.DataContext = vm;
            parkingbon.Show();
        }
    }
}

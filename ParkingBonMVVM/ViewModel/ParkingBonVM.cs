using System;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Controls.Ribbon;


namespace ParkingBonMVVM.ViewModel
{
    public class ParkingbonVM:ViewModelBase
    {
        private Model.ParkingBon parkingbon;
        public ParkingbonVM (Model.ParkingBon nparkingbon)
        {
            parkingbon = nparkingbon;
        }
        public DateTime Datum
        {
            get { return parkingbon.Datum; }
            set { parkingbon.Datum = value;
                RaisePropertyChanged("Datum");
            }
        }
        public DateTime Aankomst
        {
            get { return parkingbon.Datum; }
            set { parkingbon.Aankomst = value;
                RaisePropertyChanged("Aankomst");
            }
        }
        public int Bedrag
        {
            get { return parkingbon.Bedrag; }
            set { parkingbon.Bedrag = value;
                RaisePropertyChanged("Bedrag");
            }
        }
        public DateTime Vertrek
        {
            get { return parkingbon.Vertrek; }
            set { parkingbon.Vertrek = value;
                RaisePropertyChanged("Vertrek");
            }
        }
        public RelayCommand MeerCommand
        {
            get { return new RelayCommand(MeerBedrag); }
        }
        private void MeerBedrag()
        {
            if (Vertrek.Hour < 22)
                Bedrag++;
            Vertrek = Aankomst.AddHours(0.5 * Bedrag);
        }
        public RelayCommand MinderCommand
        {
            get { return new RelayCommand(MinderBedrag); }
        }
        private void MinderBedrag()
        {
            if (Bedrag > 0)
                Bedrag--;
            Vertrek = Aankomst.AddHours(0.5 * Bedrag);
        }
        public RelayCommand NewCommand
        {
            get { return new RelayCommand(Nieuw); }
        }
        private void Nieuw()
        {
            Datum = DateTime.Now;
            Aankomst = DateTime.Now;
            Bedrag = 0;
            Vertrek = Aankomst;
        }
        public RelayCommand OpenCommand
        {
            get { return new RelayCommand(OpenBestand); }
        }
        private void OpenBestand()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = ".bon";
            dlg.Filter = "Parkingbonnen |*.bon";
            if(dlg.ShowDialog()==true)
            {
                using (StreamReader bestand = new StreamReader(dlg.FileName))
                {
                    Datum = Convert.ToDateTime(bestand.ReadLine());
                    Aankomst = Convert.ToDateTime(bestand.ReadLine());
                    Bedrag = Convert.ToInt32(bestand.ReadLine());
                    Vertrek = Convert.ToDateTime(bestand.ReadLine());
                }
            }
        }
        public RelayCommand SaveCommand
        {
            get { return new RelayCommand(OpslaanBestand); }
        }
        private void OpslaanBestand()
        {
            try
            {
                string bestandsnaam = $"{Datum.DayOfWeek}-{Datum.Month}-{Datum.Year}om{Aankomst.Hour}-{Aankomst.Minute}";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = bestandsnaam;
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";
                if (dlg.ShowDialog()==true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Datum.ToShortDateString());
                        bestand.WriteLine(Aankomst.ToShortTimeString());
                        bestand.WriteLine(Bedrag);
                        bestand.WriteLine(Vertrek.ToShortTimeString());
                    }
                }
            }
            catch(Exception ex)
            { MessageBox.Show("Opslaan Mislukt : " + ex.Message); }
        }
        public RelayCommand CloseCommand
        {
            get { return new RelayCommand(Sluiten); }
        }
        private void Sluiten()
        {

            Application.Current.MainWindow.Close();
        }
        public RelayCommand<CancelEventArgs>OnWindowClosing
        {
            get { return new RelayCommand<CancelEventArgs>(WindowSluiten); }
        }
        private void WindowSluiten(CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u het programma stoppen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel=true;
            
        }
    }
}

using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingBonMVVM;

namespace ParkingBonMVVM.ViewModel
{
    public class ParkingBonVM: ViewModelBase
    {
        private Model.Bon parkingbon;
        public ParkingBonVM (Model.Bon nParkingBon)
        {
            parkingbon = nParkingBon;
        }
        public DateTime Datum
        {
            get
            {
                return parkingbon.Datum;
            }
            set
            {
                parkingbon.Datum = value;
                RaisePropertyChanged("Datum");
            }
        }
        public DateTime Aankomst
        {
            get
            {
                return parkingbon.BeginTijd;
            }
            set
            {
                parkingbon.BeginTijd = value;
                RaisePropertyChanged("Aankomst");
            }
        }
        public DateTime Vertrek
        {
            get
            {
                return parkingbon.EindTijd;
            }
            set
            {
                parkingbon.EindTijd = value;
                RaisePropertyChanged("Vertrek");
            }
        }
        public int Bedrag
        {
            get
            {
                return parkingbon.Bedrag;
            }
            set
            {
                parkingbon.Bedrag = value;
                RaisePropertyChanged("Bedrag");
            }
        }
        public RelayCommand MeerCommand
        { get { return new RelayCommand(MeerBetalen); } }
        private void MeerBetalen()
        {
            if (Vertrek.Hour < 22)
                Bedrag++;
            Vertrek = Aankomst.AddHours(0.5 * Bedrag);
        }
        public RelayCommand MinderCommand
        { get { return new RelayCommand(MinderBetalen); } }
        private void MinderBetalen()
        {
            if (Bedrag > 0)
                Bedrag--;
            Vertrek = Aankomst.AddHours(0.5 * Bedrag);
        }
        public RelayCommand NewCommand
        { get { return new RelayCommand(Nieuw); } }
        private void Nieuw()
        {
            Bedrag = 0;
            Datum = DateTime.Now;
            Aankomst = DateTime.Now;
            Vertrek = Aankomst;
        }
        public RelayCommand OpenCommand
        {
            get { return new RelayCommand(Openen); }
        }
        private void Openen()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";
                if (dlg.ShowDialog() == true)
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
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Openen mislukt : "+ex.Message);
            }
        }
        public RelayCommand SaveCommand
        { get { return new RelayCommand(Opslaan); } }
        private void Opslaan()
        {
            try
            {
                string bestandsnaam;
                bestandsnaam = $"{Datum.Day}-{Datum.Month}-{Datum.Year}om{Aankomst.Hour}-{Aankomst.Minute}";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = bestandsnaam;
                dlg.DefaultExt = ".bon";
                dlg.Filter = "ParkingBonnen |*.bon";
                if (dlg.ShowDialog() == true)
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
            {
                System.Windows.MessageBox.Show("Opslaan Mislukt : "+ex.Message);
            }
        }
        public RelayCommand CloseCommand
        { get { return new RelayCommand(Afsluiten); } }
        private void Afsluiten()
        {
            Application.Current.MainWindow.Close();
        }
        public RelayCommand<CancelEventArgs> AfsluitenEvent
        {
            get { return new RelayCommand<CancelEventArgs>(OnWindowClosing); }
        }
        private void OnWindowClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u het programma afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}

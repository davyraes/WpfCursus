using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls;

namespace Wenskaart.ViewModel
{
    public class WenskaartVM:ViewModelBase
    {
        private Model.Wenskaart wenskaart;
        public WenskaartVM(Model.Wenskaart nWenskaart)
        {
            wenskaart = nWenskaart;
        }
        public Model.Wenskaart Wenskaart
        { get { return wenskaart; } }
        public ImageBrush Afbeelding
        {
            get { return wenskaart.Afbeelding; }
            set { wenskaart.Afbeelding = value;RaisePropertyChanged("Afbeelding"); }
        }
        public string Wens
        {
            get { return wenskaart.Wens; }
            set { wenskaart.Wens = value;RaisePropertyChanged("Wens"); }
        }
        public int TekstGrootte
        {
            get { return wenskaart.TekstGrootte; }
            set { wenskaart.TekstGrootte = value;RaisePropertyChanged("Tekstgrootte"); }
        }
        public FontFamily Font
        {
            get { return wenskaart.Font; }
            set { wenskaart.Font = value;RaisePropertyChanged("Font"); }
        }
        public string Pad
        {
            get { return wenskaart.Pad; }
            set { wenskaart.Pad = value; RaisePropertyChanged("ImagePad"); }
        }
        public List<Model.Bol>Bollen
        {
            get { return wenskaart.Bollen; }
            set { wenskaart.Bollen = value;RaisePropertyChanged("Bollen"); }
        }
        public RelayCommand FontPlusCommand
        {
            get { return new RelayCommand(GroterFont); }
        }
        private void GroterFont()
        {
            if (TekstGrootte < 40)
                TekstGrootte++;
        }
        public RelayCommand FontMinCommand
        {
            get { return new RelayCommand(KleinerFont); }
        }
        private void KleinerFont()
        {
            if (TekstGrootte > 10)
                TekstGrootte--;
        }
        public RelayCommand NewCommand
        {
            get { return new RelayCommand(Nieuw); }
        }
        private void Nieuw()
        {
            Afbeelding = new ImageBrush();
            TekstGrootte = 20;
            Font = new FontFamily("Arial");
            Wens = string.Empty;
            Pad = "Nieuw";
        }
        public RelayCommand OpenCommand
        {
            get { return new RelayCommand(Openen); }
        }
        private void Openen()
        {

        }
        public RelayCommand SaveCommand
        {
            get { return new RelayCommand(Opslaan); }
        }
        private void Opslaan()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : " + ex.Message);
            }
        }
        public RelayCommand PrintCommand
        {
            get { return new RelayCommand(PrintPreview); }
        }
        private void PrintPreview()
        {

        }
        public RelayCommand CloseCommand
        {
            get { return new RelayCommand(Sluiten); }
        }
        private void Sluiten()
        {
            Application.Current.MainWindow.Close();
        }
        public RelayCommand<CancelEventArgs> OnWindowClosing
        { get { return new RelayCommand<CancelEventArgs>(Afsluiten); }
        }
        private void Afsluiten(CancelEventArgs e)
        {
            if (MessageBox.Show( "Wilt u het programma sluiten ?","Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
        public RelayCommand KerstCommand
        { get { return new RelayCommand(Kerstachtergrond); } }
        private void Kerstachtergrond()
        {
            Nieuw();
            Afbeelding = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/Images/kerstkaart.jpg",UriKind.Absolute)));
            Wens = "Prettige feestdagen !";
            TekstGrootte = 40;
            Font = new FontFamily ("Chiller");
        }
        public RelayCommand GeboorteCommand
        { get { return new RelayCommand(GeboorteAchtergrond); } }
        private void GeboorteAchtergrond()
        {
            Nieuw();
            Afbeelding = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/Images/geboortekaart.jpg",UriKind.Absolute)));
            Wens = "Proficiat met de geboorte van jullie zoon";
            TekstGrootte = 30;
            Font = new FontFamily("Script MT");
        }
        public RelayCommand<DragEventArgs> OnDrop
        { get { return new RelayCommand<DragEventArgs>(Droppen); } }
        private void Droppen(DragEventArgs e)
        {
            
        }
        
    }
}

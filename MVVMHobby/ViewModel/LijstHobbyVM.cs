using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;


namespace MVVMHobby.ViewModel
{
    public class LijstHobbyVM:ViewModelBase
    {
        public LijstHobbyVM()
        {
            hobbylijst.Add(new HobbyVm(new Model.Hobby("sport", "voetbal", new BitmapImage(new Uri("pack://application:,,,/Images/voetbal.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("sport", "atletiek", new BitmapImage(new Uri("pack://application:,,,/Images/atletiek.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("sport", "basketbal", new BitmapImage(new Uri("pack://application:,,,/Images/basketbal.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("sport", "tennis", new BitmapImage(new Uri("pack://application:,,,/Images/tennis.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("sport", "turnen", new BitmapImage(new Uri("pack://application:,,,/Images/turnen.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("muziek", "trompet", new BitmapImage(new Uri("pack://application:,,,/Images/trompet.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("muziek", "drum", new BitmapImage(new Uri("pack://application:,,,/Images/drum.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("muziek", "gitaar", new BitmapImage(new Uri("pack://application:,,,/Images/gitaar.jpg", UriKind.Absolute)))));
            hobbylijst.Add(new HobbyVm(new Model.Hobby("muziek", "piano", new BitmapImage(new Uri("pack://application:,,,/Images/piano.jpg", UriKind.Absolute)))));
        }
        private ObservableCollection<HobbyVm> hobbylijst = new ObservableCollection<HobbyVm>();
        public ObservableCollection<HobbyVm> HobbyLijst
        {
            get { return hobbylijst; }
            set
            {
                hobbylijst = value;
                RaisePropertyChanged("HobbyLijst");
            }
        }
        private HobbyVm selectedHobby;
        public HobbyVm SelectedHobby
        {
            get
            {
                return selectedHobby;
            }
            set
            {
                selectedHobby = value;
                RaisePropertyChanged("SelectedHobby");
            }
        }
        public RelayCommand<RoutedEventArgs> VerwijderCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Verwijder); }
        }
        private void Verwijder(RoutedEventArgs e)
        {
            hobbylijst.Remove(selectedHobby);
        }
        View.ImageView groteview;
        public RelayCommand<MouseEventArgs> MouseDownCommand
        {
            get { return new RelayCommand<MouseEventArgs>(MuisIn); }
        }
        private void MuisIn(MouseEventArgs e)
        {
            try
            {
                Image tg = (Image)e.OriginalSource;
                groteview = new View.ImageView();
                groteview.GroteImage.Source = tg.Source;
                groteview.Show();
            }
            catch(InvalidCastException)
            {
                MessageBox.Show("Geen afbeelding ");
            }
        }
        public RelayCommand<MouseEventArgs> MouseUpCommand
        {
            get { return new RelayCommand<MouseEventArgs>(MuistUit); }
        }
        private void MuistUit(MouseEventArgs e)
        {
            if (groteview != null)
                groteview.Close();
            groteview = null;
        }
    }
}

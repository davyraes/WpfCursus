using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using MVVMHobby.Model;
namespace MVVMHobby.ViewModel
{
    public class HobbyVm:ViewModelBase
    {
        private Hobby hobby;
        public HobbyVm(Hobby nHobby)
        {
            this.hobby = nHobby;
        }
        public string Categorie
        {
            get { return hobby.Categorie; }
            set
            {
                hobby.Categorie = value;
                RaisePropertyChanged("Categorie");
            }
        }
        public string Activiteit
        {
            get { return hobby.Activiteit; }
            set { hobby.Activiteit = value;
                RaisePropertyChanged("Activiteit");
            }
        }
        public BitmapImage Symbool
        {
            get { return hobby.Symbool; }
            set { hobby.Symbool = value;
                RaisePropertyChanged("Symbool");

            }
        }
    }
}

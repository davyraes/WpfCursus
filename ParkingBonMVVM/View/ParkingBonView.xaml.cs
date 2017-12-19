using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParkingBonMVVM.View
{
    /// <summary>
    /// Interaction logic for ParkingBonView.xaml
    /// </summary>
    public partial class ParkingBonView : RibbonWindow
    {
        public ParkingBonView()
        {
            InitializeComponent();
            if(ParkingBonMVVM.Properties.Settings.Default.qat!=null)
                if (ParkingBonMVVM.Properties.Settings.Default.qat.Count>2)
                {
                    System.Collections.Specialized.StringCollection qatlijst = ParkingBonMVVM.Properties.Settings.Default.qat;
                    int lijnnr = 0;
                }
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Collections.Specialized.StringCollection QatLijst = new System.Collections.Specialized.StringCollection();
            if (ParkingBonMVVM.Properties.Settings.Default.qat != null)
                ParkingBonMVVM.Properties.Settings.Default.qat.Clear();
            foreach(RibbonButton rb in Qat.Items)
            {
                RoutedUICommand commando = (RoutedUICommand)rb.Command;
                QatLijst.Add(commando.Text);
                QatLijst.Add(rb.SmallImageSource.ToString());
            }
            if (QatLijst.Count>2)
            {
                ParkingBonMVVM.Properties.Settings.Default.qat = QatLijst;
            }
            ParkingBonMVVM.Properties.Settings.Default.Save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wenskaart.ViewModel;

namespace Wenskaart.View
{
    /// <summary>
    /// Interaction logic for WenskaartView.xaml
    /// </summary>
    public partial class WenskaartView : Window
    {
        public WenskaartView()
        {
            InitializeComponent();
        }
        
        private void Ellipse_mouseMove(object sender, MouseEventArgs e)
        {
            Ellipse SleepCirkel = (Ellipse)sender;
            Ellipse bron =(Ellipse) e.OriginalSource;
            if ((e.LeftButton == MouseButtonState.Pressed)&&(SleepCirkel.Fill!=null))
            {
                DataObject sleepdata = new DataObject("Cirkel",bron);                
                DragDrop.DoDragDrop(SleepCirkel,sleepdata, DragDropEffects.Move);
            }
        }
        private void Ellipse_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Cirkel"))
            {
                
                Canvas canvas = (Canvas)sender;
                Point plaats = e.GetPosition(canvas);
                Ellipse broncirkel =(Ellipse) e.Data.GetData("Cirkel");
                Ellipse cirkel = new Ellipse();
                cirkel.Fill = broncirkel.Fill;
                Canvas.SetLeft(cirkel, plaats.X-20);
                Canvas.SetTop(cirkel, plaats.Y-20);
                canvas.Children.Add(cirkel);
                
            }
        }
    }
}

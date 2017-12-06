using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegenboogDragDrop
{
    /// <summary>
    /// Interaction logic for WindowRegenboog.xaml
    /// </summary>
    public partial class WindowRegenboog : Window
    {
        public WindowRegenboog()
        {
            InitializeComponent();
        }
        private Rectangle sleeprechthoek = new Rectangle();
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            sleeprechthoek = (Rectangle)sender;
            if ((e.LeftButton==MouseButtonState.Pressed)&&(sleeprechthoek.Fill!=Brushes.White))
            {
                DataObject sleepkleur = new DataObject("deKleur", sleeprechthoek.Fill);
                DragDrop.DoDragDrop(sleeprechthoek, sleepkleur, DragDropEffects.Move);
            }
        }
        private void Rectangle_DragEnter(object sender,DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 5;
        }
        private void Rectangle_DragLeave(object sender,DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 3;
        }
        private void Rectangle_Drop(object sender , DragEventArgs e)
        {
            if (e.Data.GetDataPresent("deKleur"))
            {

            }
        }
    }
}


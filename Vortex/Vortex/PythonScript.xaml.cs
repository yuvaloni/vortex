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

namespace Vortex
{
     /// <summary>
     /// Interaction logic for PythonScript.xaml
     /// </summary>
     public partial class PythonScript : Window
     {
          public PythonScript()
          {
               InitializeComponent();
          }
          public void Add(ContentControl c,int x, int y)
          {
               Canvas.Children.Add(c);
               Canvas.SetTop(c, y);
               Canvas.SetLeft(c, x);
          }
          public void Add(Control c, int x, int y)
          {
               Canvas.Children.Add(c);
               Canvas.SetTop(c, y);
               Canvas.SetLeft(c, x);
          }
          public void Add(vortex.KNOB c, int x, int y)
          {
               c.AddToDisplay(this, x, y);
          }

          private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {

          }

     }
}

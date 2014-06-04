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
     /// Interaction logic for SetController.xaml
     /// </summary>
     public partial class SetController : Window
     {
          List<Controller> devs;
          Knob k;
          public SetController(Knob k, List<Controller> devs)
          {
               InitializeComponent();
               this.k = k;
               this.devs = devs;
               
               foreach(Controller c in devs)
               {
                    TreeViewItem v = new TreeViewItem();
                    v.Header = c.name;
                    Controller.Slider[] p = c.GetSliders();
                    foreach(Controller.Slider param in p)
                    {
                         v.Items.Add(param.GetId());
                    }
                    devices.Items.Add(v);
               }
             
              
          }
          public Controller.Slider SearchParam(string name)
          {
               foreach(Controller c in devs)
               {
                    Controller.Slider p = c.SearchById(name);
                    if (p != null) return p;
               }
               return null;

          }
          public void Connect()
          {
               SearchParam(devices.SelectedItem.ToString()).SetKnob(k);
          }

          private void Button_Click(object sender, RoutedEventArgs e)
          {
               Connect();
               this.Close();
          }

          private void Button_Click_1(object sender, RoutedEventArgs e)
          {
               this.Close();
          }
     }
}

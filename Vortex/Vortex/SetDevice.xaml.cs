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
     /// Interaction logic for SetDevice.xaml
     /// </summary>
     public partial class SetDevice : Window
     {
          List<Controllable> devs;
          Knob k;
          public SetDevice(Knob k, List<Controllable> devs)
          {
               InitializeComponent();
               this.k = k;
               this.devs = devs;
               
               foreach(Controllable c in devs)
               {
                    TreeViewItem v = new TreeViewItem();
                    v.Header = c.name;
                    Controllable.Parameter[] p = c.GetParams();
                    foreach(Controllable.Parameter param in p)
                    {
                         v.Items.Add(param.name);
                    }
                    devices.Items.Add(v);
               }
             
              
          }
          public Controllable.Parameter SearchParam(string name)
          {
               foreach(Controllable c in devs)
               {
                    Controllable.Parameter p = c.GetParameter(name);
                    if (p != null) return p;
               }
               return null;

          }
          public void Connect()
          {
               k.SetParam(SearchParam(devices.SelectedItem.ToString()));
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


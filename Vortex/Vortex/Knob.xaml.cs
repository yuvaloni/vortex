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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Vortex
{
     /// <summary>
     /// Interaction logic for Knob.xaml
     /// </summary>
     public partial class Knob : UserControl
     {
          double inval;
          Controllable.Parameter param;
          public delegate void work();
          public bool cont = false;
          List<Controllable> devices;
          List<Controller> controllers;
          public Knob(ref List<Controllable> l, ref List<Controller> sl)
          {
               devices = l;
               controllers = sl;
               InitializeComponent();
               inval = 0;
  
               
          }
          public Knob() { devices = new List<Controllable>(); controllers = new List<Controller>(); InitializeComponent(); }

          private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
          {
               label.Fill = Brushes.Purple;
               cont = true;
               Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new work(GainControl));
          }

          private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
          {
               label.Fill = Brushes.Blue;
               cont = false;
          }
          private void GainControl()


          {

                    double pos = inval - Mouse.GetPosition(indicator).Y;
                    if (pos < 0) inval = 0;
                    else if (pos > 255) inval = 255;
                    else inval = pos;
                    indicator.Width = 25 + (inval/255)*25;
                    Canvas.SetLeft(indicator, 25 - indicator.Width / 2);
                    indicator.Height = indicator.Width;
                    Canvas.SetTop(indicator, 25 - indicator.Height / 2);
                    try
                    {
                         param.sets((int)inval);
                    }
                    catch {}
                    if (Mouse.LeftButton == MouseButtonState.Released)
                    {
                         label.Fill = Brushes.Blue;
                         cont = false;
                    }
                    if (cont)
                         Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new work(GainControl));
   

              }
          public void setControl()
          {
               label.Fill = Brushes.Purple;
               if (inval < 0) inval = 0;
               else if (inval > 255) inval = 255;
               indicator.Width = 25 + (inval / 255) * 25;
               Canvas.SetLeft(indicator, 25 - indicator.Width / 2);
               indicator.Height = indicator.Width;
               Canvas.SetTop(indicator, 25 - indicator.Height / 2);
               try
               {
                    param.sets((int)inval);
               }
               catch {}
               label.Fill = Brushes.Blue;

          }
          public Controllable.Parameter GetParam()
          {
               return param;
          }
          private void Change_Source(object sender, RoutedEventArgs e)
          {
               SetDevice win = new SetDevice(this, devices);
               win.Show();
               
          }
          public void SetParam(Controllable.Parameter p )
          {
               if(p==null)
               {
                    param = null; txt.Content = "Empty"; bg.Fill = Brushes.Gray; 
               }
               param = p;
               txt.Content = param.name;
               Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new work(SetColor));
          }
          public void SetColor()
          {
               try
               {
                    SolidColorBrush b;
                    if (param.getr() <= 127.5)
                         b = new SolidColorBrush(Color.FromRgb((byte)(param.getr()*2), 255, 0));
                    else
                         b = new SolidColorBrush(Color.FromRgb(255, (byte)(255-param.getr()*2), 0));
                    bg.Fill = b;
               }
               catch
               {
                    param = null; txt.Content = "Empty"; bg.Fill = Brushes.Gray; 
               }
               Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new work(SetColor));
          }

          private void Set_slider(object sender, RoutedEventArgs e)
          {
               SetController win = new SetController(this, controllers);
               win.Show();
          }
          public int GetInval()
          {
               return (int)inval;
          }
          public void setinval(int t)
          {
               inval = t;
               Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new work(setControl));
          }
          public void setpval(int v)
          {
               if(param!=null)
                param.setr(v);
          }
          public int getpval()
          {
               if(param!=null)
                    return param.getr();
               return 0;
          }
          public string getname()
          {
               if (param != null) return param.name;
               else return "Empty";
          }
         
              
              
          }



          
     }


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
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Vortex
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          public static void ManageDevices(ref List<Controllable> l, ref Knob[,] knobs)
          {
               Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
               s.Bind(new IPEndPoint(IPAddress.Any, 1234));
               while(true)
               {
                    EndPoint ep = new IPEndPoint(IPAddress.Any, 1234);
                    byte[] buffer = new byte[1000];
                    if(l.Count==0)
                    {
                         s.ReceiveFrom(buffer, ref ep);
                         string get = ASCIIEncoding.ASCII.GetString(buffer);
                              l.Add(new Controllable((IPEndPoint)ep, get));
                    }
                    else
                    {
                    for(int i =0; i<l.Count;i++)
                    {
                         Controllable c = l[i];
                         try
                         {
                              try
                              {
                                   s.SendTo(ASCIIEncoding.ASCII.GetBytes(c.GetSendString()), c.getAddr());


                                   s.ReceiveFrom(buffer, ref ep);
                              }
                              catch
                              {
                                   l.RemoveAt(i);
                                   bool found = false;
                                   for (int j = 0; j < knobs.GetLength(0);j++ )
                                   {
                                        for(int k=0;k<knobs.GetLength(1);k++)
                                        {
                                             if (c.GetParameter(knobs[j, k].GetParam().name) == knobs[j, k].GetParam())
                                             {
                                                  knobs[j, k].SetParam(null);
                                                  found = true;
                                                  break;
                                             }
                                        }
                                        if (found)
                                             break;
                                   }
                                   
                              }
                              while (!ep.Equals(c.getAddr()))
                              {
                                   
                                        l.Add(new Controllable((IPEndPoint)ep, ASCIIEncoding.ASCII.GetString(buffer)));
                                   s.ReceiveFrom(buffer, ref ep);
                              }
                              c.SetReceiveString(ASCIIEncoding.ASCII.GetString(buffer));


                         }

                         catch
                         { }

                    }
                    }
                    Thread.Sleep(10);
               }
          }
          public static void ManageControllers(ref List<Controller> l)
          {
               Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
               s.Bind(new IPEndPoint(IPAddress.Any, 1235));
               while (true)
               {
                    EndPoint ep = new IPEndPoint(IPAddress.Any, 1235);
                    byte[] buffer = new byte[1000];
                    if (l.Count == 0)
                    {
                         s.ReceiveFrom(buffer, ref ep);
                         string get = ASCIIEncoding.ASCII.GetString(buffer);
 
                              l.Add(new Controller(get,(IPEndPoint)ep));
                    }
                    else
                    {
                         for (int i = 0; i < l.Count; i++)
                         {
                              Controller c = l[i];

                              try
                              {
                                   try
                                   {

                                        s.SendTo(ASCIIEncoding.ASCII.GetBytes(c.GetString()), c.GetAddr());



                                        s.ReceiveFrom(buffer, ref ep);
                                   }
                                   catch
                                   {
                                        l.RemoveAt(i);
                                        throw new Exception();
                                   }
                                   while (!ep.Equals(c.GetAddr()))
                                   {
                                        l.Add(new Controller(ASCIIEncoding.ASCII.GetString(buffer), (IPEndPoint)ep));
                                        s.ReceiveFrom(buffer, ref ep);
                                   }
                                   c.setFromString(ASCIIEncoding.ASCII.GetString(buffer));


                              }
                              catch
                              {

                              }

                         }
                    }
                    Thread.Sleep(10);
               }
          }
          List<Controllable> l;
          List<Controller> s;
          public MainWindow()
          {
               InitializeComponent();
               Knob[,] Controls = new Knob[15,10];
               l = new List<Controllable>();
               Thread t = new Thread(() => { ManageDevices(ref l, ref Controls); });
               t.Start();
               s = new List<Controller>();
               Thread ts = new Thread(() => { ManageControllers(ref s); });
               ts.Start();
               for(int i = 0; i<Controls.GetLength(0); i++)
               {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(50);
                    grid.ColumnDefinitions.Add(c);
                    ColumnDefinition c2 = new ColumnDefinition();
                    c2.Width = new GridLength(10);
                    grid.ColumnDefinitions.Add(c2);
                    for(int j = 0; j< Controls.GetLength(1);j++)
                    {
                         RowDefinition r = new RowDefinition();
                         r.Height = new GridLength(50);
                         grid.RowDefinitions.Add(r);
                         RowDefinition r2 = new RowDefinition();
                         r2.Height = new GridLength(10);
                         grid.RowDefinitions.Add(r2);
                         Controls[i, j] = new Knob(ref l,ref s);
                        
                         grid.Children.Add(Controls[i, j]);
                         Grid.SetRow(Controls[i, j], j*2);
                         Grid.SetColumn(Controls[i, j], i*2);
                    }
               }
               
          }

          private void Python(object sender, RoutedEventArgs e)
          {
               Python py = new Python(ref l, ref s);
               py.Show();
          }
     }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Vortex
{
     public class OptionsBox : ComboBox
     {
          public void Add(string s)
          {
               this.Items.Add(s);
          }
     }
     public class vortex
     {

          ScriptScope scope;
          public Slider slider(int min, int max, int width)
          {
               Slider s = new Slider();
               s.Minimum = min;
               s.Maximum = max;
               s.Width = width;
               return s;
          }
          public CheckBox tickbox(string cont)
          {
               CheckBox c = new CheckBox();
               c.Content = cont;
               return c;
          }
          public RadioButton Radiobutton(string s, string g)
          {
               RadioButton r = new RadioButton();
               r.Content = s;
               r.Foreground = Brushes.White;
               r.GroupName = g;
               
               return r;
          }
          public TextBox textbox(int width, string content)
          {
               TextBox t = new TextBox();
               t.Background = Brushes.Gray;
               t.Foreground = Brushes.White;
               t.Text = content;
               return t;
          }
          public device GetDeviceByName(string s)
          {
               foreach(device d in Devices)
               {
                    if (d.name == s)
                         return d;
               }
               return null;
          }
          public controller GetControllerByName(string s)
          {
               foreach (controller d in Controllers)
               {
                    if (d.name == s)
                         return d;
               }
               return null;
          }
          public KNOB knob()
          {
               return new KNOB(ref l, ref c);
          }
          public OptionsBox list(int w)
          {
               OptionsBox l = new OptionsBox();
               l.Foreground = Brushes.White;
               l.Width = w;
               return l;
          }
          public CheckBox checkbox(string cont)
          {
               CheckBox c = new CheckBox();
               c.Content = cont;
               c.Foreground = Brushes.White;
               return c;
          }
          public delegate void work();
          public Button button(string cont, string func,int height, int width)
          {
               Button b = new Button();
               b.Height = height;
               b.Width = width;
               b.Content = cont;
               Action handler = scope.GetVariable<Action>(func);
               b.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { handler(); });
               return b;
               


          }
          public class KNOB
          {
               Knob k;
               public void AddToDisplay(PythonScript win,int x,int y )
               {
                    win.Add(k,x,y);
               }
               controller.input In;
               device.output Out;
               public controller.input input
               {
                    set
                    {
                         In = value;
                         In.setKnob(k);
                         
                    }
                    get
                    {
                         return In;
                    }
               }
               public device.output output
               {
                    set
                    {
                         Out.setKnob(k);
                    }
                    get
                    {
                         return Out;
                    }
               }
               public KNOB(ref List<Controllable> l,ref List<Controller> c)
               {
                    k = new Knob(ref l,ref c);
               }
               
               
          }

          controller[] Controllers;
          device[] Devices;
          public controller[] controllers
          {
               get
               {
                    return Controllers;
               }

          }
          public device[] devices
          {
               get
               {
                    return Devices;
               }

          }
          List<Controllable> l;
          List<Controller> c;
          public vortex(List<Controllable> l, List<Controller> c, ScriptScope scope)
          {
               this.l = l;
               this.c = c;
               this.scope = scope;
               Controllers = new controller[c.Count];
               Devices = new device[l.Count];
               for (int i =0; i<l.Count;i++)
               {
                    Devices[i] = new device(l[i]);
               }
               for (int i = 0; i < c.Count; i++)
               {
                    Controllers[i] = new controller(c[i]);
               }
          }
          public PythonScript window(int height, int width)
          {
               PythonScript win = new PythonScript();
               win.Height = height;
               win.Width = width;
               return win;

          }
          public class controller
          {
               public string name
               {
                    set
                    {

                    }
                    get
                    {
                         return cont.name;
                    }
               }
               private input[] Inputs;
               public input[] inputs
               {
                    get
                    {
                         return Inputs;
                    }
                    set
                    {

                    }
               }
               public controller(Controller c)
               {
                    this.cont = c;
                    Inputs = new input[c.GetSliders().Length];
                    for(int i =0; i<c.GetSliders().Length;i++)
                    {
                         Inputs[i] = new input(ref c.GetSliders()[i]);
                    }
               }
               
               private Controller cont;
               public input GetById(int id)
               {
                    foreach(input nn in  inputs)
                    {
                         if(nn.id==id)
                              return nn;
                    }
                    return null;
               }

               public class input
               {

                    Controller.Slider s;
                    Knob k;
                    public input(ref Controller.Slider s)
                    {
                         this.s = s;
                         if (this.s.getKnob() == null)
                         {
                              k = new Knob();
                              s.SetKnob(k);
                         }
                         else
                              k = s.getKnob();
                    }
                    public int sval
                    {
                         get
                         {
                              if (this.s.getKnob() == null)
                              {
                                   s.SetKnob(k);
                              }
                              else
                                   k = s.getKnob();
                              return s.getsvalue();
                         }
                         set
                         {
                              if (this.s.getKnob() == null)
                              {
                                   s.SetKnob(k);
                              }
                              else
                                   k = s.getKnob();
                              if(value>=0&&value<=255)
                                   s.setsvalue(value);
                         }
                    }
                    public int rval
                    {
                         get
                         {
                              if (this.s.getKnob() == null)
                              {
                                   s.SetKnob(k);
                              }
                              else
                                   k = s.getKnob();
                              return s.getrvalue();
                         }
                         set
                         {
                              if (this.s.getKnob() == null)
                              {
                                   s.SetKnob(k);
                              }
                              else
                                   k = s.getKnob();
                              if (value >= 0 && value <= 255)
                                   s.setrvalue(value);
                         }
                    }
                    public void setKnob(Knob k)
                    {
                         this.k = k;
                         s.SetKnob(k);
                         
                    }
                    public int id
                    {
                         get
                         {
                              return s.id;
                              
                         }
                         set
                         {

                         }
                    }





               }


          }
     }
              public class device
          {
               public string name
               {
                    set
                    {

                    }
                    get
                    {
                         return cont.name;
                    }
               }
               public output GetByName(string name)
               {
                    foreach(output ou in Outputs)
                    {
                         if(ou.name==name)
                              return ou;

                    }
                    return null;
               }
               private output[] Outputs;
               public output[] outputs
               {
                    get
                    {
                         return Outputs;
                    }
                    set
                    {

                    }
               }
               public device(Controllable c)
               {
                    this.cont = c;
                    Outputs = new output[c.GetParams().Length];
                    for(int i =0; i<c.GetParams().Length;i++)
                    {
                         Outputs[i] = new output(ref c.GetParams()[i]);
                    }
               }
               
               private Controllable cont;
               public class output
               {

                    Controllable.Parameter s;
                    public output(ref Controllable.Parameter s)
                    {
                         this.s = s;
                    }
                    public int sval
                    {
                         get
                         {
                              return s.gets();
                         }
                         set
                         {
                              if (value >= 0 && value <= 255)
                                   s.sets(value);
                         }
                    }
                    public int rval
                    {
                         get
                         {
                              return s.getr();
                         }
                         set
                         {
                              if (value >= 0 && value <= 255)
                                   s.setr(value);
                         }
                    }

                    public string name
                    {
                         get
                         {
                              return s.name;
                              
                         }
                         set
                         {

                         }
                    }
                    public void setKnob(Knob k)
                    {
                         k.SetParam(s);
                    }





               }
              }


          }


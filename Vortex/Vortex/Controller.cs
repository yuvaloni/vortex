using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Vortex
{
     public class Controller
     {
          string Name;
          public string name
          {
               get
               {
                    return Name;
               }
               set
               {
                    Name = value;
               }
          }
          IPEndPoint addr;
          public Controller(string s, IPEndPoint ep)
          {
               
               addr = ep;
               string[] sl = s.Split('-');
               name = sl[0].Split(':')[0];
               sliders = new Slider[sl.Length - 1];
               for(int i =1; i<sl.Length;i++)
               {
                    sliders[i - 1] = new Slider(sl[i]);
                    
               }
          }
          public IPEndPoint GetAddr()
          {
               return addr;
          }
          public void setFromString(string s)
          {
               string[] sl = s.Split('-');
               for (int i = 0; i < sl.Length; i++)
                    sliders[i].setfromstring(sl[i]);
          }
          public string GetString()
          {
               string s = sliders[0].getString();
               for(int i=1; i<sliders.Length;i++)
                    s+="-"+sliders[i].getString();
               return s;
          }
          public Slider[] GetSliders()
          {
               return sliders;
          }
          public Slider SearchById(string id)
          {
               foreach(Slider s in sliders)
               {
                    if (s.GetId() == id)
                         return s;
               }
               return null;
          }
          Slider[] sliders;
          public class Slider
          {
               Knob k;
               static int num=1;
               public int id;
               public Slider(string s)
               {
                    id = num++;
                    setfromstring(s);
                    k = null;
               }
               public void SetKnob(Knob k)
               {
                    this.k = k;
               }
               public void setsvalue(int v)
               {
                    if (k != null)
                         k.setinval(v);
               }
               public int getsvalue()
               {    if(k!=null)
                         return k.GetInval();
                    return 0;
               }
               public void setrvalue(int v)
               {

                    if (k != null)
                         k.setpval(v);
               }
               public int getrvalue()
               {
                    if(k!=null)
                         return k.getpval();
                    return 0;
               }
               public void setfromstring(string s)
               {
                    if(k!=null)
                    {
                    setsvalue(int.Parse(s.Split(':')[2]));
                    setrvalue(int.Parse(s.Split(':')[3]));
                    }
               }
               public string getString()
               {
                    string s = id+":"+(k!=null?k.getname():"Empty")+":"+getsvalue()+":"+getrvalue();
                    return s;

               }
               public string GetId()
               {
                    return id.ToString();
               }
               public Knob getKnob()
               {
                    return k;
               }
               
               
          }
     }
}

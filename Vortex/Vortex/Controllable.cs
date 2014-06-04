using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Vortex
{
     public class Controllable
     {
          string Name;
          public string name
          {
               set
               {
                    this.Name = value;
               }
               get
               {
                    return Name; 
               }
          }
          public string type
          {
               set
               {
                    this.Type = value;
               }
               get
               {
                    return this.Type;
               }
          }
          string Type;
          private Parameter[] Params;
          private IPEndPoint dest;
          public Controllable(IPEndPoint d,string init)
          {
               dest = d;
               string[] ps = init.Split('-');
               name = ps[0].Split(':')[0];
               type = ps[0].Split(':')[1];
               Params = new Parameter[ps.Length - 1];
               for(int i = 1; i<ps.Length;i++)
               {
                    Params[i - 1] = new Parameter(ps[i].Split(':')[0]);
                    
               }

               
               

          }
          public string GetSendString()
          {
               string s = Params[0].gets() + ":" + Params[0].getr();
               for(int i =1;i<Params.Length;i++)
                    s += "-" + Params[i].gets() + ":" + Params[i].getr();
               return s;
          }
          public void SetReceiveString(string init)
          {
               string[] ps = init.Split('-');
               for (int i = 0; i < ps.Length; i++)
               {
                    Params[i].sets(int.Parse(ps[i].Split(':')[0]));
                    Params[i].setr(int.Parse(ps[i].Split(':')[1]));

               }
          }
          public IPEndPoint getAddr()
          {
               return dest;
          }
          public Parameter GetParameter(string n)
          {
               foreach(Parameter p in Params)
               {
                    if (p.name == n)
                         return p;
               }
               return null;
          }
          public Parameter[] GetParams()
          {
               return Params;
          }
          public class Parameter
          {
               private int s = 0;
               private int r = 0;
               private string Name;
               public string name
               {
                    get
                    {
                         return Name;
                    }
                    set
                    {
                         this.Name = value;
                    }
               }
              public Parameter(string name)
               {
                    this.name = name;


               }
               public void sets(int v)
               {
                    this.s = v;
               }
               public void setr(int v)
               {
                    this.r = v;
               }
               public int gets()
               {
                    return s;
               }
               public int getr()
               {
                    return r;
               }

          }
     }
}

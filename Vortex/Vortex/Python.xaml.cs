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
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace Vortex
{
     /// <summary>
     /// Interaction logic for Python.xaml
     /// </summary>
    
     public partial class Python : Window
     {
          
          private ScriptEngine m_engine = IronPython.Hosting.Python.CreateEngine();
          private ScriptScope m_scope = null;
          List<Controllable> l;
          List<Controller> c;
          public Python(ref List<Controllable> l, ref List<Controller> c)
          {
               this.l = l;
               this.c = c;
               InitializeComponent();
               m_scope = m_engine.CreateScope();
          }

          private void Button_Click(object sender, RoutedEventArgs e)
          {
               vortex obj = new vortex(l, c,m_scope);
               m_scope.SetVariable("Vortex", obj);
               string code = txt.Text;
               ScriptSource source = m_engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);

               source.Execute(m_scope); 
          }
     }
}

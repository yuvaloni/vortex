using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Vortex
{
     [ServiceContract]
     class Extern
     {

           [OperationContract]
          void AddInputDevice()
          {

          }
           [OperationContract]
           void AddAddOutputDevice()
           {

           }
           [OperationContract]
           void AlterInputSVal(string device, string param, int val)
           {

           }
           [OperationContract]
           void AlterInputRVal(string device, string param, int val)
           {

           }
           [OperationContract]
           void AlterOutputSVal(string device, string param, int val)
           {

           }
           [OperationContract]
           void AlterOutputRVal(string device, string param, int val)
           {

           }






           
           
      }
}

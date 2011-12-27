using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace Behaviors
{
    class ParameterInspector : IParameterInspector
    {
        #region IParameterInspector Members

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            Console.WriteLine(string.Format("Validating parameters of {0}, par1: {1}", operationName, inputs[0]));
            return null;
        }

        #endregion
    }
}

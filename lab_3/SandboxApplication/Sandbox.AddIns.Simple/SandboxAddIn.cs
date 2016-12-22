using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.AddIns.Simple
{
    public class SandboxAddIn : MarshalByRefObject, ISandboxAddIn
    {
        public string GetSecurityInfo()
        {
            return "SimpleSandboxAddIn#doStuff called";
        }
    }
}

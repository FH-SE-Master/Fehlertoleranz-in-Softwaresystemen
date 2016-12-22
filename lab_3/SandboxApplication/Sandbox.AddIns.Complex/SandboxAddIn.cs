using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.AddIns.Complex
{
    public class SandboxAddIn : MarshalByRefObject, ISandboxAddIn
    {
        public string GetSecurityInfo()
        {
            StringBuilder sb = new StringBuilder();

            // 0. Load executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            sb.AppendLine("Assembly Name:         " + assembly.FullName);

            // 1. Security Transparanty Level
            sb.AppendLine("Security Transparanty: " + assembly.SecurityRuleSet);

            // 2. Fully Trusted ?
            sb.AppendLine("Is fully trusted:      " + AppDomain.CurrentDomain.IsFullyTrusted);

            // 3. Type security
            sb.AppendLine("---------- Asembly Types ----------");
            foreach (var type in assembly.GetTypes())
            {
                sb.AppendLine("TypeName:               " + type.Name);
                sb.AppendLine("IsSecurityCritical:     " + type.IsSecurityCritical);
                sb.AppendLine("IsSecuritySafeCritical: " + type.IsSecuritySafeCritical);
                sb.AppendLine("IsSecurityTransparent:  " + type.IsSecurityTransparent);
            }
            sb.AppendLine("---------- Asembly Types ----------");

            // 4. Permission count
            sb.AppendLine("PermissionSet.COUNT: " + assembly.PermissionSet.Count);


            return sb.ToString();
        }
    }
}

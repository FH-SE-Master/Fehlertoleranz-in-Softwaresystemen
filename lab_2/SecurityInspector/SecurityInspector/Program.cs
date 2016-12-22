using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInspector
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0. Load executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Assembly Name:         " + assembly.FullName);

            // 1. Security Transparanty Level
            Console.WriteLine("Security Transparanty: " + assembly.SecurityRuleSet);

            // 2. Fully Trusted ?
            Console.WriteLine("Is fully trusted:      " + AppDomain.CurrentDomain.IsFullyTrusted);

            // 3. Type security
            Console.WriteLine("---------- Asembly Types ----------");
            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine("TypeName:               " + type.Name);
                Console.WriteLine("IsSecurityCritical:     " + type.IsSecurityCritical);
                Console.WriteLine("IsSecuritySafeCritical: " + type.IsSecuritySafeCritical);
                Console.WriteLine("IsSecurityTransparent:  " + type.IsSecurityTransparent);
            }
            Console.WriteLine("---------- Asembly Types ----------");

            // 4. Permission count
            Console.WriteLine("PermissionSet.COUNT: " + assembly.PermissionSet.Count);


            // hold console window open
            int value = Console.Read();
        }
    }
}

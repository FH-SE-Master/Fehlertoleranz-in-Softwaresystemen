using Sandbox.AddIns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SandboxApplication
{
    // Its assumed that the assemblies are all located in this projects bin/[Release|Debug] directory !!
    class Program
    {
        // The expected implementation name
        public static string implName = "SandboxAddIn";
        // the name of the known assemblies containing ISandboxAddIn implementations
        static readonly string[] assemblies = { "Sandbox.AddIns.Simple", "Sandbox.AddIns.Complex" };

        static void Main(string[] args)
        {
            // 1. Create permission set
            PermissionSet permSet = new PermissionSet(PermissionState.None);
            permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            // 2. Create app domain setup (point to executing assembly directory where the other assmeblies are located)
            AppDomainSetup dSetup = new AppDomainSetup();
            dSetup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 3. Create app domain
            AppDomain domain = AppDomain.CreateDomain("SandboxDomain", null, dSetup, permSet);

            // 4. Create instances
            IEnumerable<ISandboxAddIn> instances = CreateInstances(domain, assemblies);

            // 5. Iterate over all found instances and invoke method
            foreach (var item in instances)
            {
                Console.WriteLine(item.GetType().Name + " called");
                Console.WriteLine(item.GetSecurityInfo());
            }

            // 6. Lock conosle, so that it keeps open.
            Console.Read();
        }

        /// <summary>
        /// Creates the ISandboxAddIn instances.
        /// </summary>
        /// <param name="domain">The domain to add instance too.</param>
        /// <param name="assemblies">The assemblies names to load implementations from</param>
        /// <returns>the enumerable of instances of ISandboxAddIn</returns>
        public static IEnumerable<ISandboxAddIn> CreateInstances(AppDomain domain, string[] assemblies)
        {
            IList<ISandboxAddIn> instances = new List<ISandboxAddIn>();

            foreach (var item in assemblies)
            {
                instances.Add(domain.CreateInstanceAndUnwrap(
                                         item,
                                         item + "." + implName) as ISandboxAddIn);
            }

            return instances;
        }
    }
}

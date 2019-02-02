using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAssertThat
{
    public class StartTesting
    {
        public static List<string> NameSpaces = new List<string>();
        public static void GetSetGo()
        {
            Assert.Reset();
            Assert.ConsolidatedResult = true;
            Assert.AttributedUnits = true;
            foreach (var t in GetClasses())
            {
                new TestMethods().StartTesting(t);
            }
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
           
            Assert.Summary();
        }

        private static IEnumerable<Type> GetClasses()
        {
            List<Type> types = new List<Type>();
            foreach (var ns in NameSpaces)
            {
                var assemblyName = Assembly.LoadWithPartialName(ns);

                var query = assemblyName.GetTypes()
                    // .Where(type => NameSpaces.Contains(type.Namespace))
                .Select(type => type);

                types.AddRange(query.ToList());

            }
          
            return types;
        }
    }
}

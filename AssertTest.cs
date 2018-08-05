/*
 * Name: SimpleAssertThat 
 * Description: A Simple, Small Assert.That testing tool for unit testing in your projects.
 * Author: Gautam Mokal (gautammokal@live.com)
 * Please do not remove these comments.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAssertThat
{
    public enum ConditionName
    {
        True,
        False,
        Same,
        NotSame,
        EqualTo,
        NotEqualTo,
        Null,
        NotNull

    }
    public interface IConditionCheck
    {
        ConditionName OperationName { get; set; }
        object OperatorValue { get; set; }
        
    }
    public class Is : IConditionCheck
    {
        public ConditionName OperationName { get; set; }
        public object OperatorValue { get; set; }

        
        public static IConditionCheck True
        {
            get
            {
                var isObj = new Is() { };
                isObj.OperationName = ConditionName.True;
                

                return isObj;
            }
           
        }

       
        public static IConditionCheck False
        {
            get
            {
                var isObj = new Is() { };
                isObj.OperationName = ConditionName.False;


                return isObj;
            }

        }

        public static IConditionCheck Null
        {
            get
            {
                var isObj = new Is() { };
                isObj.OperationName = ConditionName.Null;


                return isObj;
            }

        }

        public static IConditionCheck NotNull
        {
            get
            {
                var isObj = new Is() { };
                isObj.OperationName = ConditionName.NotNull;


                return isObj;
            }

        }

        public static IConditionCheck Same(object o)
        {
            var isObj = new Is() { };
            isObj.OperationName = ConditionName.Same;
            isObj.OperatorValue = o;

            return isObj;
        }

        public static IConditionCheck NotSame(object o)
        {
            var isObj = new Is() { };
            isObj.OperationName = ConditionName.NotSame;
            isObj.OperatorValue = o;

            return isObj;
        }
        
        public static IConditionCheck NotEqualTo(object o)
        {
            var isObj = new Is() { };
            isObj.OperationName = ConditionName.NotEqualTo;
            isObj.OperatorValue = o;

            return isObj;
        }

        public static IConditionCheck EqualTo(object o)
        {
            var isObj = new Is() { };
            isObj.OperationName = ConditionName.EqualTo;
            isObj.OperatorValue = o;

            return isObj;
        }

        
       

        
    }
    public class Assert
    {
        static string  conditionText = "";

        static bool testPassed = false;

        static string assertMessage = "";
       
        public static void That(object o1, IConditionCheck o2 = null, string msg = "")
        {
            assertMessage = msg;
            
            if (o2.OperationName == ConditionName.True)
            {
                testPassed=(bool)o1;
                
            }

            else if (o2.OperationName == ConditionName.False)
            {
                testPassed=!(bool)o1 ;
                
            }

            else if (o2.OperationName == ConditionName.NotEqualTo)
            {
                testPassed = !o1.Equals(o2.OperatorValue);
            }

            else if (o2.OperationName == ConditionName.EqualTo)
            {
                testPassed = o1.Equals(o2.OperatorValue);
            }

            

            else if (o2.OperationName == ConditionName.Same)
            {
                testPassed = RuntimeHelpers.GetHashCode(o1) == RuntimeHelpers.GetHashCode(o2.OperatorValue);
            }
            else if (o2.OperationName ==ConditionName.NotSame)
            {
                testPassed = RuntimeHelpers.GetHashCode(o1) != RuntimeHelpers.GetHashCode(o2.OperatorValue);
            }
            else if (o2.OperationName == ConditionName.Null)
            {
                testPassed = o1==null;
            }
            else if (o2.OperationName == ConditionName.NotNull)
            {
                testPassed = o1 != null;
            }

            ShowConsoleText();
            
        }
        private static void ShowConsoleText()
        {
            string text="";
            if(testPassed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                text = "Passed";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                text = "Failed: "+assertMessage;
            }
            
            Console.WriteLine(text);
        }
    }
}

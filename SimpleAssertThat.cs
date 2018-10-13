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
using System.Text;
using System.Threading.Tasks;

namespace SimpleAssertThat
{
    public interface ICondition
    {
        string ConditionName { get; set; }
        Object ConditionOperandValue { get; set; }
    }

    public class Is : ICondition
    {
        public string ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }

        public static Is True
        {
            get
            {

                return GetIsObject("True", true);


            }

        }
        public static Is False
        {
            get
            {

                return GetIsObject("False", false);


            }

        }
        public static Is NotNull
        {
            get
            {
                return GetIsObject("NotNull", null);
            }
        }
        public static Is Null
        {
            get
            {

                return GetIsObject("Null", null);


            }

        }

        public Is(string conditionName, Object operandValue)
        {
            this.ConditionName = conditionName;
            this.ConditionOperandValue = operandValue;
        }

        public static Is EqualTo(object operandValue)
        {
            return GetIsObject("EqualTo", operandValue);
        }
        public static Is NotEqualTo(object operandValue)
        {
            return GetIsObject("NotEqualTo", operandValue);
        }
        public static Is SameAs(object operandValue)
        {
            return GetIsObject("SameAs", operandValue);
        }
        public static Is NotSameAs(object operandValue)
        {
            return GetIsObject("NotSameAs", operandValue);
        }
        private static Is GetIsObject(string operation, object operandValue)
        {
            var obj = new Is(operation, operandValue);
            return obj;
        }

    }
    public class Assert
    {
        private static object InvokeMethod(Refl reflection)
        {
            var lst = reflection.ClassName.Split('.').ToList();
            string className = lst.LastOrDefault();
            lst.Remove(className);
            string assemblyName = string.Join(",", lst);
            string methodName = reflection.Method;
            object[] parameters = reflection.Parameters;

            //Console.WriteLine(assemblyName);
            Type type = null;
            if (assemblyName != null && assemblyName.Length > 0)
            {
                type = Type.GetType(className + "," + assemblyName);
            }
            else
            {
                type = Type.GetType(className);
            }
            //Console.WriteLine(type);
            ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
            if (magicConstructor != null)
            {
                object magicClassObject = magicConstructor.Invoke(new object[] { });
            }

            MethodInfo method = type.GetMethod(methodName);
            var instance = magicConstructor != null ? magicConstructor : null;

            object magicValue = method.Invoke(instance, parameters);

            return magicValue;
        }
        public static void That(object expression, ICondition condition, string message = "", string testName = "")
        {
            var operandOne = expression;
            Predicate<object> predicate = null;

            if (expression != null && expression.GetType().Name == "Refl")
            {
                try
                {
                    operandOne = InvokeMethod(((Refl)expression));
                    //Console.WriteLine(operandOne);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.InnerException.Message);
                    operandOne = ex.InnerException;
                }
            }


            if (condition.ConditionName == "EqualTo")
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == "SameAs")
            {
                predicate = (p) => p.GetHashCode() == condition.ConditionOperandValue.GetHashCode();
            }
            else if (condition.ConditionName == "NotSameAs")
            {
                predicate = (p) => p.GetHashCode() != condition.ConditionOperandValue.GetHashCode();
            }
            else if (condition.ConditionName == "NotEqualTo")
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == "True")
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == "False")
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == "Null")
            {
                predicate = (p) => p == null;
                condition.ConditionOperandValue = "Null";
            }
            else if (condition.ConditionName == "NotNull")
            {
                predicate = (p) => p != null;
                condition.ConditionOperandValue = "NotNull";
            }
            else if (condition.ConditionName == "Exception")
            {
                //Console.WriteLine(((Exception)operandOne).Message);


                predicate = (p) => p is Exception;
            }
            else if (condition.ConditionName == "Returns")
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == "NotReturn")
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }

            bool testResult = predicate(operandOne);
            TestCondition(testResult, message, testName);
            Console.WriteLine("Expected: " + condition.ConditionOperandValue.ToString());
            Console.WriteLine("Actual: " + operandOne);

        }
        private static void TestCondition(bool testPassed, string message = "", string testName = "")
        {

            if (testPassed)
            {
                Show("Passed");
            }
            else
            {
                Show("Failed" + " " + message);

            }
        }
        public static void Show(string result)
        {
            Console.WriteLine(result);
        }
    }
    public class Refl
    {
        public string Method { get; private set; }
        public string ClassName { get; private set; }
        public object[] Parameters { get; private set; }
        public Refl MethodName(string methodName)
        {
            this.Method = methodName;
            return this;
        }

        public Refl InClass(string className)
        {
            this.ClassName = className;
            return this;
        }

        public Refl WithParameters(params object[] parameters)
        {
            this.Parameters = parameters;
            return this;
        }


    }
    public class AMethod
    {
        public static Refl ByName(string methodName)
        {
            Refl reflection = new Refl();
            reflection.MethodName(methodName);

            return reflection;
        }


    }
    public class Throws : ICondition
    {
        public string ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Exception Ex { get; set; }

        public static Throws Exception(Exception exception)
        {
            return new Throws() { ConditionName = "Exception", Ex = exception };
        }
    }

    public class Does : ICondition
    {
        public string ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }

        public static Is Returns(object operandValue)
        {
            return new Is("Returns", operandValue);
        }
        public static Is NotReturn(object operandValue)
        {
            return new Is("NotReturn", operandValue);
        }
    }

    

}

  /*
  Name: SimpleAssertThat 
  Description: A Simple, Small Assert.That testing tool for unit testing in your projects.
  Author: Gautam Mokal (gautammokal@live.com)
  Please do not remove these comments.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAssertThat
{

    public enum Operators
    {

        True,
        False,
        EqualTo,
        NotEqualTo,
        SameAs,
        NotSameAs,
        Null,
        NotNull,
        Exception,
        NoException,
        Return,
        NotReturn,
        Empty,
        NotEmpty,
        HaveAny,
        HaveAll,
        NotHaveAny,
        NotHaveAll,
        Contain,
        NotContain,
        GreaterThan,
        


    }

    public interface ICondition
    {
        Operators ConditionName { get; set; }
        Object ConditionOperandValue { get; set; }
        Object[] ConditionOperandValues { get; set; }
    }

    public class Is : ICondition
    {
        public Operators ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Object[] ConditionOperandValues { get; set; }



        public Is(Operators conditionName, Object operandValue)
        {
            this.ConditionName = conditionName;
            this.ConditionOperandValue = operandValue;
        }

        public Is(Operators conditionName, Object[] operandValues)
        {
            this.ConditionName = conditionName;
            this.ConditionOperandValues = operandValues;
        }



        public static Is True
        {
            get
            {

                return GetIsObject(Operators.True, true);


            }

        }
        public static Is False
        {
            get
            {

                return GetIsObject(Operators.False, false);


            }

        }
        public static Is NotNull
        {
            get
            {
                return GetIsObject(Operators.NotNull, null);
            }
        }
        public static Is Null
        {
            get
            {

                return GetIsObject(Operators.Null, null);


            }

        }
        public static Is Empty
        {
            get
            {
                return GetIsObject(Operators.Empty, null);
            }
        }
        public static Is NotEmpty
        {
            get
            {
                return GetIsObject(Operators.NotEmpty, null);
            }
        }


        public static Is EqualTo(object operandValue)
        {
            return GetIsObject(Operators.EqualTo, operandValue);
        }
        public static Is NotEqualTo(object operandValue)
        {
            return GetIsObject(Operators.NotEqualTo, operandValue);
        }
        public static Is SameAs(object operandValue)
        {
            return GetIsObject(Operators.SameAs, operandValue);
        }
        public static Is NotSameAs(object operandValue)
        {
            return GetIsObject(Operators.NotSameAs, operandValue);
        }
        private static Is GetIsObject(Operators operation, object operandValue)
        {
            var obj = new Is(operation, operandValue);
            return obj;
        }
        public static Is GreaterThan(object operandValue)
        {
            return GetIsObject(Operators.GreaterThan, operandValue);
        }

    }

    public class Predicates
    {
        public Predicate<object> GetPredicateForEmpty(object operandOne)
        {
            Predicate<object> predicate = null;
            if (operandOne.GetType() == typeof(System.String))
            {
                predicate = (p) => p.ToString() == String.Empty;
            }
            else if (operandOne.GetType().IsArray)
            {
                predicate = (p) => ((System.Array)p).Length == 0;
            }
            else if (operandOne.GetType().IsGenericType)
            {
                var t = operandOne.GetType();
                predicate = (p) => (this.GetCount((ICollection)operandOne) == 0);
            }
            else
            {
                throw new Exception("Condition operator is not valid");
            }

            return predicate;
        }

        public Predicate<object> GetPredicateForNotEmpty(object operandOne)
        {
            Predicate<object> predicate = null;
            if (operandOne.GetType() == typeof(System.String))
            {
                predicate = (p) => p.ToString() != String.Empty;
            }
            else if (operandOne.GetType().IsArray)
            {
                predicate = (p) => ((System.Array)p).Length != 0;
            }
            else if (operandOne.GetType().IsGenericType)
            {
                var t = operandOne.GetType();
                predicate = (p) => (this.GetCount((ICollection)operandOne) != 0);
            }
            else
            {
                throw new Exception("Condition operator is not valid");
            }

            return predicate;
        }

        private int GetCount(ICollection list)
        {
            ICollection collection = list as ICollection;
            if (collection != null)
            {
                return collection.Count;
            }

            return 0;
        }
    }

    public class Assert
    {

        public static int TotalTests = 0;
        public static int TotalFailed = 0;
        public static int TotalPassed = 0;
        private static bool onlyFirst = false;

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
                type = Type.GetType(reflection.ClassName + "," + assemblyName);
            }
            else
            {
                type = Type.GetType(reflection.ClassName);
            }
            //Console.WriteLine(type);
            ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
            if (magicConstructor != null)
            {
                object magicClassObject = magicConstructor.Invoke(new object[] { });

                MethodInfo method = null;
                if (parameters != null)
                {
                    Type[] types = parameters.ToList().Select(t => t.GetType()).ToArray();
                    method = type.GetMethod(methodName, types);
                }
                else
                {
                    method = type.GetMethod(methodName, Type.EmptyTypes);
                }
                var instance = magicClassObject != null ? magicClassObject : null;

                object magicValue = method.Invoke(instance, parameters);

                return magicValue;
            }

            return null;
        }


        public static void Reset()
        {
            TotalFailed = 0;
            TotalPassed = 0;
            TotalTests = 0;
            onlyFirst = false;
        }

        public static void TestOnlyFirst()
        {

            onlyFirst = true;
        }

        public static void That(object expression, string message = "", string testName = "")
        {
            That(expression, null, message, testName);
        }

        public static void That(object expression, ICondition condition, string message = "", string testName = "")
        {


            if (onlyFirst && TotalTests == 1)
            {
                return;
            }

            var operandOne = expression;
            Predicate<object> predicate = null;
            TotalTests++;
            if (condition == null)
            {
                condition = Is.True;
            }

            if (expression == null && (condition.ConditionName != Operators.Null && condition.ConditionName != Operators.NotNull))
            {
                TestCondition(false, "Invalid test");
                return;
            }



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
                    operandOne = ex;
                }
            }



            if (condition.ConditionName == Operators.EqualTo)
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.GreaterThan)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) > Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.SameAs)
            {
                predicate = (p) => p.GetHashCode() == condition.ConditionOperandValue.GetHashCode();
            }
            else if (condition.ConditionName == Operators.NotSameAs)
            {
                predicate = (p) => p.GetHashCode() != condition.ConditionOperandValue.GetHashCode();
            }
            else if (condition.ConditionName == Operators.NotEqualTo)
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.True)
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.False)
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.Null)
            {
                predicate = (p) => p == null;
                condition.ConditionOperandValue = "Null";
            }
            else if (condition.ConditionName == Operators.NotNull)
            {
                predicate = (p) => p != null;
                condition.ConditionOperandValue = "NotNull";
            }
            else if (condition.ConditionName == Operators.Exception)
            {
                predicate = (p) => p != null && (p is Exception) &&

                     (condition.ConditionOperandValue != null ? ((Exception)p).InnerException.GetType() == ((Type)condition.ConditionOperandValue) : true);
            }
            else if (condition.ConditionName == Operators.NoException)
            {
                predicate = (p) => p == null ||
                   (p != null && (p is Exception) &&

                     (condition.ConditionOperandValue != null ?
                     ((Exception)p).InnerException.GetType() != ((Type)condition.ConditionOperandValue)
                     : false));
            }
            else if (condition.ConditionName == Operators.Return)
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.NotReturn)
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.Empty)
            {
                predicate = new Predicates().GetPredicateForEmpty(operandOne);
            }
            else if (condition.ConditionName == Operators.NotEmpty)
            {
                predicate = new Predicates().GetPredicateForNotEmpty(operandOne);
            }
            else if (condition.ConditionName == Operators.HaveAny)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    return Contains(((ICollection)q), condition.ConditionOperandValues);
                };
            }
            else if (condition.ConditionName == Operators.NotHaveAny)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    return !Contains(((ICollection)q), condition.ConditionOperandValues);
                };
            }
            else if (condition.ConditionName == Operators.HaveAll)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    return ContainsAll(((ICollection)q), condition.ConditionOperandValues);
                };
            }
            else if (condition.ConditionName == Operators.NotHaveAll)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    return ContainsNotAll(((ICollection)q), condition.ConditionOperandValues);
                };
            }
            else if (condition.ConditionName == Operators.Contain)
            {
                predicate = (p) =>
                {
                    if (p.GetType().ToString() == "System.String")
                    {
                        return HasSubstrings(p.ToString(), "All", condition.ConditionOperandValues);
                    }
                    else
                    {
                        throw new Exception("Not a valid string");
                    }


                };
            }
            else if (condition.ConditionName == Operators.NotContain)
            {
                predicate = (p) =>
                {
                    if (p.GetType().ToString() == "System.String")
                    {
                        return HasSubstrings(p.ToString(), "Zero", condition.ConditionOperandValues);
                    }
                    else
                    {
                        throw new Exception("Not a valid string");
                    }


                };
            }
            bool testResult = predicate(operandOne);
            TestCondition(testResult, message, testName);

            if (!testResult)
            {
                if (condition.ConditionOperandValue != null)
                {
                    Console.Write("Expected: " + condition.ConditionOperandValue.ToString() + " (" + condition.ConditionName + ")");

                }

                if (condition.ConditionOperandValues != null)
                {
                    Console.Write("Expected: " + GetElementsString(condition.ConditionOperandValues).ToString() + " (" + condition.ConditionName + ")");

                }

                string actualResult = "";
                if (operandOne != null && operandOne.GetType().ToString() == "System.String")
                {
                    actualResult = operandOne.ToString();
                }
                else if (operandOne != null && operandOne is ICollection)
                {

                    actualResult = GetListString(operandOne as ICollection).ToString();
                }
                else
                {
                    actualResult = operandOne != null ? operandOne.ToString() : "";
                }
                Console.WriteLine(" Actual: " + actualResult);

            }

        }

        private static string GetListString(ICollection collection)
        {
            List<string> lst = new List<string>();
            foreach (var e in collection)
            {
                lst.Add(e.ToString());
            }

            return string.Join(",", lst);
        }

        private static string GetElementsString(params object[] parameters)
        {
            List<string> lst = new List<string>();
            foreach (var e in parameters)
            {
                lst.Add(e.ToString());
            }

            return string.Join(",", lst);

        }

        private static bool HasSubstrings(string p, string arity, params object[] parameters)
        {
            int found = 0;
            foreach (object obj in parameters)
            {
                if (p.Contains(obj.ToString()))
                {
                    found++;
                }
            }
            if (arity == "All")
            {
                return parameters.Count() == found;
            }
            else if (arity == "Zero")
            {
                return found == 0;
            }

            return false;

        }
        private static bool ContainsAll(ICollection lst, params object[] parameters)
        {
            return CheckIfContains(lst, "All", parameters);

        }
        private static bool ContainsNotAll(ICollection lst, params object[] parameters)
        {
            return CheckIfContains(lst, "Zero", parameters);

        }
        private static bool Contains(ICollection lst, params object[] parameters)
        {
            return CheckIfContains(lst, "Any", parameters);
        }
        private static bool CheckIfContains(ICollection lst, string allAny, params object[] parameters)
        {
            Array ar = new object[lst.Count];
            lst.CopyTo(ar, 0);
            int found = 0;
            foreach (var p in parameters)
            {
                foreach (var e in ar)
                {
                    var type = p.GetType().BaseType.ToString();
                    var condition = type == "System.ValueType" ? p.ToString() == e.ToString() : p.GetHashCode() == e.GetHashCode();

                    if (condition)
                    {
                        found++;
                        break;
                    }
                }
            }
            if (allAny == "All")
            {
                return parameters.Length == found;
            }
            else if (allAny == "Any")
            {
                return found > 0;
            }
            else if (allAny == "Zero")
            {
                return found == 0;
            }
            else
            {
                return false;
            }
        }

        private static void TestCondition(bool testPassed, string message = "", string testName = "")
        {

            if (testPassed)
            {
                Show("Passed");
                TotalPassed++;
            }
            else
            {
                Show("Failed" + " " + message);
                TotalFailed++;
            }
        }
        public static void Show(string result)
        {
            Console.WriteLine(result);
        }

        public static void Summary()
        {
            Console.WriteLine("Total Test : " + TotalTests + " Passed : " + TotalPassed + " Failed : " + TotalFailed);
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
        public Operators ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Object[] ConditionOperandValues { get; set; }
        public Type Ex { get; set; }

        public static Throws Exception(Type exceptionType)
        {
            return new Throws() { ConditionName = Operators.Exception, ConditionOperandValue = exceptionType };
        }
        public static Throws NoException(Type exceptionType)
        {
            return new Throws() { ConditionName = Operators.NoException, ConditionOperandValue = exceptionType };
        }
        public static Throws Exception()
        {
            return new Throws() { ConditionName = Operators.Exception, ConditionOperandValue = typeof(Exception) };
        }
        public static Throws NoException()
        {
            return new Throws() { ConditionName = Operators.NoException, ConditionOperandValue = typeof(Exception) };
        }
    }

    public class Does : ICondition
    {
        public Operators ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Object[] ConditionOperandValues { get; set; }
        public static Is Returns(object operandValue)
        {
            return new Is(Operators.Return, operandValue);
        }
        public static Is NotReturn(object operandValue)
        {
            return new Is(Operators.NotReturn, operandValue);
        }
        public static Is HaveAny(params object[] elements)
        {
            return new Is(Operators.HaveAny, elements);
        }
        public static Is HaveAll(params object[] elements)
        {
            return new Is(Operators.HaveAll, elements);
        }
        public static Is NotHaveAny(params object[] elements)
        {
            return new Is(Operators.NotHaveAny, elements);
        }
        public static Is NotHaveAll(params object[] elements)
        {
            return new Is(Operators.NotHaveAll, elements);
        }
        public static Is Contain(params object[] elements)
        {
            return new Is(Operators.Contain, elements);
        }
        public static Is NotContain(params object[] elements)
        {
            return new Is(Operators.NotContain, elements);
        }
    }



}

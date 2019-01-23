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
using System.Text.RegularExpressions;
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
        NotGreaterThan,
        GreaterThanOrEqualTo,
        NotGreaterThanOrEqualTo,
        LesserThan,
        NotLesserThan,
        LesserThanOrEqualTo,
        NotLesserThanOrEqualTo,
        Zero,
        NotZero,
        OfType,
        NotOfType,
        ValueType,
        NotValueType,
        ObjectType,
        NotObjectType,
        InOrder,
        InAscendingOrder,
        InDescendingOrder,
        String,
        NotString,
        Boolean,
        NotBoolean,
        Integer,
        Date,
        NotInteger,
        Array,
        NotArray,
        Generic,
        NotGeneric,
        RegexMatch,
        NoRegexMatch,
        Numeric,
        NotNumeric,
        HaveSize,
        NotHaveSize,
        Between,
        NotBetween,
        StartWith,
        EndWith,
        NotStartWith,
        NotEndWith,
        HaveFirstElement,
        NotHaveFirstElement,
        HaveLastElement,
        NotHaveLastElement,
        HaveMaximum,
        HaveMinimum,
        NotHaveMaximum,
        NotHaveMinimum,
        HaveAverage,
        NotHaveAverage,
        HaveTotal,
        NotHaveTotal,
        In,
        NotIn,
    }

    public interface ICondition
    {
        Operators ConditionName { get; set; }
        Object ConditionOperandValue { get; set; }
        Object[] ConditionOperandValues { get; set; }
        string Verb { get; set; }
        bool CaseInSensitive { get; set; }
    }

    public class Is : ICondition
    {
        public Operators ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Object[] ConditionOperandValues { get; set; }
        public string Verb { get; set; }
        public bool CaseInSensitive { get; set; }

        public Is(Operators conditionName, Object operandValue)
        {
            this.ConditionName = conditionName;
            this.ConditionOperandValue = operandValue;
            this.Verb = "Is";
        }

        public Is(Operators conditionName, Object[] operandValues)
        {
            this.ConditionName = conditionName;
            this.ConditionOperandValues = operandValues;
            this.Verb = "Is";
        }

        public static Is Zero
        {
            get
            {
                return GetIsObject(Operators.Zero, true);
            }
        }

        public static Is Date
        {
            get
            {
                return GetIsObject(Operators.Date, true);
            }
        }

        public static Is Boolean
        {
            get
            {
                return GetIsObject(Operators.Boolean, null);
            }
        }
        public static Is ValueType
        {
            get
            {
                return GetIsObject(Operators.ValueType, null);
            }
        }
        public static Is NotValueType
        {
            get
            {
                return GetIsObject(Operators.NotValueType, null);
            }
        }
        public static Is ObjectType
        {
            get
            {
                return GetIsObject(Operators.ObjectType, null);
            }
        }
        public static Is NotObjectType
        {
            get
            {
                return GetIsObject(Operators.NotObjectType, null);
            }
        }
        public static Is NotBoolean
        {
            get
            {
                return GetIsObject(Operators.NotBoolean, null);
            }
        }
        public static Is String
        {
            get
            {
                return GetIsObject(Operators.String, null);
            }
        }
        public static Is Array
        {
            get
            {
                return GetIsObject(Operators.Array, null);
            }
        }
        public static Is NotArray
        {
            get
            {
                return GetIsObject(Operators.NotArray, null);
            }
        }
        public static Is Integer
        {
            get
            {
                return GetIsObject(Operators.Integer, null);
            }
        }
        public static Is NotInteger
        {
            get
            {
                return GetIsObject(Operators.NotInteger, null);
            }
        }
        public static Is Numeric
        {
            get
            {
                return GetIsObject(Operators.Numeric, null);
            }
        }
        public static Is NotNumeric
        {
            get
            {
                return GetIsObject(Operators.NotNumeric, null);
            }
        }

        public static Is NotString
        {
            get
            {
                return GetIsObject(Operators.NotString, null);
            }
        }

        public static Is NotZero
        {
            get
            {
                return GetIsObject(Operators.NotZero, true);
            }
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

        public static Is InAscendingOrder
        {
            get
            {
                return GetIsObject(Operators.InAscendingOrder, null);
            }
        }

        public static Is InDescendingOrder
        {
            get
            {
                return GetIsObject(Operators.InDescendingOrder, null);
            }
        }

        public static Is Generic
        {
            get
            {
                return GetIsObject(Operators.Generic, null);
            }
        }
        public static Is NotGeneric
        {
            get
            {
                return GetIsObject(Operators.NotGeneric, null);
            }
        }

        public static Is EqualTo(object operandValue,bool caseInSensitive=false)
        {
            return GetIsObject(Operators.EqualTo, operandValue, caseInSensitive);
        }
        public static Is NotEqualTo(object operandValue, bool caseInSensitive = false)
        {
            return GetIsObject(Operators.NotEqualTo, operandValue, caseInSensitive);
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
        private static Is GetIsObject(Operators operation, object operandValue,bool caseInSensitive)
        {
            var obj = new Is(operation, operandValue);
            obj.CaseInSensitive = caseInSensitive;
            return obj;
        }
        public static Is GreaterThan(object operandValue)
        {
            return GetIsObject(Operators.GreaterThan, operandValue);
        }

        public static Is GreaterThanOrEqualTo(object operandValue)
        {
            return GetIsObject(Operators.GreaterThanOrEqualTo, operandValue);
        }
        public static Is NotGreaterThanOrEqualTo(object operandValue)
        {
            return GetIsObject(Operators.NotGreaterThanOrEqualTo, operandValue);
        }

        public static Is NotGreaterThan(object operandValue)
        {
            return GetIsObject(Operators.NotGreaterThan, operandValue);
        }
        public static Is NotLesserThan(object operandValue)
        {
            return GetIsObject(Operators.NotLesserThan, operandValue);
        }
        public static Is LesserThanOrEqualTo(object operandValue)
        {
            return GetIsObject(Operators.LesserThanOrEqualTo, operandValue);
        }
        public static Is NotLesserThanOrEqualTo(object operandValue)
        {
            return GetIsObject(Operators.NotLesserThanOrEqualTo, operandValue);
        }
        public static Is LesserThan(object operandValue)
        {
            return GetIsObject(Operators.LesserThan, operandValue);
        }
        public static Is InOrder(params object[] elements)
        {
            return new Is(Operators.InOrder, elements);
        }

        public static Is OfType(string type)
        {
            var sysFriendlyType = GetSystemType(type);

            if (sysFriendlyType == null)
            {
                throw new Exception("Type is not understandable");
            }

            var s = Type.GetType(sysFriendlyType, true);
            return GetIsObject(Operators.OfType, s);
        }

        public static Is OfType(Type t)
        {
            return GetIsObject(Operators.OfType, t);
        }

        public static Is NotOfType(string type)
        {
            var sysFriendlyType = GetSystemType(type);

            if (sysFriendlyType == null)
            {
                throw new Exception("Type is not understandable");
            }
            var s = Type.GetType(sysFriendlyType, true);
            return GetIsObject(Operators.NotOfType, s);
        }

        private static string GetSystemType(string type)
        {
            var s = Type.GetType(type.StartsWith("System.") ? type : "System." + type);
            if (s == null)
            {
                return null;
            }
            return s.ToString();
        }

        public static Is NotOfType(Type t)
        {
            return GetIsObject(Operators.NotOfType, t);
        }

        public static Is Between(object from, object to)
        {
            object[] fromTo = new object[2] { from, to };
            return new Is(Operators.Between, fromTo);
        }

        public static Is NotBetween(object from, object to)
        {
            object[] fromTo = new object[2] { from, to };
            return new Is(Operators.NotBetween, fromTo);
        }

        public static Is In(params object[] elements)
        {
            return new Is(Operators.In, elements);
        }

        public static Is NotIn(params object[] elements)
        {
            return new Is(Operators.NotIn, elements);
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

        private static int TotalTests = 0;
        private static int TotalFailed = 0;
        private static int TotalPassed = 0;
        private static bool onlyFirst = false;
        public bool ConditionResult = false;
        private object Expression = null;

        private static object InvokeMethod(Refl reflection)
        {

            Type type = null;
            object[] parameters = reflection.Parameters;
            string methodName = reflection.Method;

            if (reflection.ClassType != null)
            {
                type = reflection.ClassType;
            }
            else
            {

                var lst = reflection.ClassName.Split('.').ToList();
                string className = lst.LastOrDefault();
                lst.Remove(className);
                string assemblyName = string.Join(",", lst);

                if (assemblyName != null && assemblyName.Length > 0)
                {
                    type = Type.GetType(reflection.ClassName + "," + assemblyName);
                }
                else
                {
                    type = Type.GetType(reflection.ClassName);
                }
            }
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

                return magicValue == null ? "null" : magicValue;
            }

            return null;
        }

        public static int GetTotalPassedCount()
        {
            return TotalPassed;
        }
        public static int GetTotalFailed()
        {
            return TotalFailed;
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
            TotalFailed = 0;
            TotalPassed = 0;
            TotalTests = 0;
            onlyFirst = true;
        }

        public static Assert That(object expression, string testName)
        {
            
            return That(expression, null, "", testName);
            
        }

        public static Assert That(object expression, string message = "", string testName = "")
        {
            
            return That(expression, null, message, testName);
           
        }

        public static Assert That(object expression, ICondition condition, string testName)
        {
            return That(expression, condition, "", testName);
        }

        public static Assert That(object expression, ICondition condition, string message = "", string testName = "")
        {
            bool proceed = false;
            if (onlyFirst && TotalTests == 1)
            {
                return null;
            }
            else
            {
                proceed = true;
            }

            bool testResult = proceed ? GetTestResult(expression, condition, message, testName) : false;


            Assert obj = new Assert();
            obj.ConditionResult = testResult;
            obj.Expression = expression;

            return obj;

        }

        public Assert OR(ICondition condition=null, string message = "", string testName = "")
        {
           
            this.ConditionResult=this.ConditionResult || Assert.That(this.Expression, condition).ConditionResult;
            return this;
        }
        public Assert AND(ICondition condition = null, string message = "", string testName = "")
        {

            this.ConditionResult = this.ConditionResult && Assert.That(this.Expression, condition).ConditionResult;
            return this;
        }

        private static object CheckForString(ICondition condition,object operandValue)
        {
            if(condition.CaseInSensitive)
            {
                if (operandValue.GetType() != typeof(System.String))
                {
                    throw new Exception("Not a valid string to perform this operation");
                }
                else
                {
                    operandValue = operandValue.ToString().ToLower();
                    condition.ConditionOperandValue = condition.ConditionOperandValue.ToString().ToLower();
                }
            }

            return operandValue;
            
        }

        private static bool GetTestResult(object expression, ICondition condition, string message = "", string testName = "")
        {

            #region Validations
            Predicate<object> predicate = null;
            var operandOne = expression;
           
            TotalTests++;
            if (condition == null)
            {
                condition = Is.True;
            }
            operandOne = CheckForString(condition, operandOne);

            if (expression == null && (condition.ConditionName != Operators.Null && condition.ConditionName != Operators.NotNull))
            {
                TestCondition(false, "Invalid test");
                return false;
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
            #endregion

            #region Basic Operators
            if (condition.ConditionName == Operators.EqualTo)
            {
                predicate = (p) => { return p.ToString() == condition.ConditionOperandValue.ToString(); };
            }
            else if (condition.ConditionName == Operators.GreaterThan)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) > Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.NotGreaterThan)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) <= Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.GreaterThanOrEqualTo)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) >= Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.NotGreaterThanOrEqualTo)
            {
                predicate = (p) => !(Decimal.Parse(p.ToString()) >= Decimal.Parse(condition.ConditionOperandValue.ToString()));
            }
            else if (condition.ConditionName == Operators.LesserThan)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) < Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.LesserThanOrEqualTo)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) <= Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.NotLesserThanOrEqualTo)
            {
                predicate = (p) => !(Decimal.Parse(p.ToString()) <= Decimal.Parse(condition.ConditionOperandValue.ToString()));
            }
            else if (condition.ConditionName == Operators.NotLesserThan)
            {
                predicate = (p) => Decimal.Parse(p.ToString()) >= Decimal.Parse(condition.ConditionOperandValue.ToString());
            }
            else if (condition.ConditionName == Operators.SameAs)
            {
                predicate = (p) => p.GetHashCode() == condition.ConditionOperandValue.GetHashCode();
            }
            else if (condition.ConditionName == Operators.NotSameAs)
            {
                predicate = (p) => p.GetHashCode() != condition.ConditionOperandValue.GetHashCode();
            }
            else if(condition.ConditionName==Operators.In)
            {
                predicate = (p) =>
                {
                    return CheckIfPresent(condition.ConditionOperandValues, p);
                };
            }
            else if (condition.ConditionName == Operators.NotIn)
            {
                predicate = (p) =>
                {
                    return !CheckIfPresent(condition.ConditionOperandValues, p);
                };
            }
            else if (condition.ConditionName == Operators.Between)
            {
                decimal from = decimal.Parse(condition.ConditionOperandValues[0].ToString());
                decimal to = decimal.Parse(condition.ConditionOperandValues[1].ToString());

                predicate = (p) =>
                {
                    decimal decValue = decimal.Parse(p.ToString());
                    return decValue >= from && decValue <= to;
                };
            }
            else if (condition.ConditionName == Operators.NotBetween)
            {
                decimal from = decimal.Parse(condition.ConditionOperandValues[0].ToString());
                decimal to = decimal.Parse(condition.ConditionOperandValues[1].ToString());

                predicate = (p) =>
                {
                    decimal decValue = decimal.Parse(p.ToString());
                    return !(decValue >= from && decValue <= to);
                };
            }
            else if (condition.ConditionName == Operators.NotEqualTo)
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.True)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = Convert.ToBoolean(p.ToString());
                    return Convert.ToBoolean(p.ToString()) == true;
                };
            }
            else if (condition.ConditionName == Operators.False)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = Convert.ToBoolean(p.ToString());
                    return Convert.ToBoolean(p.ToString()) == false;
                };
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
            else if (condition.ConditionName == Operators.Zero)
            {
                predicate = (p) => (int)p == 0;
                condition.ConditionOperandValue = "Zero";
            }
            else if (condition.ConditionName == Operators.NotZero)
            {
                predicate = (p) => (int)p != 0;
                condition.ConditionOperandValue = "NotZero";
            }

            #endregion

            #region Exceptions
            else if (condition.ConditionName == Operators.Exception)
            {
                predicate = (p) => p != null && (p is Exception) &&

                     (condition.ConditionOperandValue != null ? ((Exception)p).InnerException.GetType() == ((Type)condition.ConditionOperandValue) : true);
            }
            else if (condition.ConditionName == Operators.NoException)
            {
                predicate = (p) => 
                    !(
                    p == null ||
                   (p != null && (p is Exception) &&

                     (condition.ConditionOperandValue != null ?
                     ((Exception)p).InnerException.GetType() == ((Type)condition.ConditionOperandValue)
                     : false))
                     );
            }
            #endregion

            #region Methods
            else if (condition.ConditionName == Operators.Return)
            {
                predicate = (p) => p.ToString() == condition.ConditionOperandValue.ToString();
            }
            else if (condition.ConditionName == Operators.NotReturn)
            {
                predicate = (p) => p.ToString() != condition.ConditionOperandValue.ToString();
            }

            #endregion

            #region Have
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
                        return p.ToString().Contains(condition.ConditionOperandValue.ToString());
                    }
                    else
                    {
                        return Contains(((ICollection)p), condition.ConditionOperandValue);
                    }
                           
                };
            }
            else if (condition.ConditionName == Operators.NotContain)
            {
                predicate = (p) =>
                {
                    if (p.GetType().ToString() == "System.String")
                    {
                        return !p.ToString().Contains(condition.ConditionOperandValue.ToString());
                    }
                    else
                    {
                        return !Contains(((ICollection)p), condition.ConditionOperandValue);
                    }

                };
            }
            else if(condition.ConditionName==Operators.StartWith)
            {
                predicate = (p) => {return p.ToString().StartsWith(condition.ConditionOperandValue.ToString());};
            }
            else if(condition.ConditionName==Operators.EndWith)
            {
                predicate = (p) => { return p.ToString().EndsWith(condition.ConditionOperandValue.ToString()); };
            }
            else if(condition.ConditionName==Operators.NotStartWith)
            {
                predicate = (p) => { return !p.ToString().StartsWith(condition.ConditionOperandValue.ToString()); };
            }
            else if(condition.ConditionName==Operators.NotEndWith)
            {
                predicate = (p) => { return !p.ToString().EndsWith(condition.ConditionOperandValue.ToString()); };
            }
            else if(condition.ConditionName==Operators.HaveFirstElement)
            {
                predicate = (p) => {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string first=GetFirstObject((ICollection)q).ToString();
                    return first == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveFirstElement)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string first = GetFirstObject((ICollection)q).ToString();
                    return first != condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.HaveLastElement)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string last = GetLastObject((ICollection)q).ToString();
                    Console.WriteLine(last);
                    return last == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveLastElement)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string last = GetLastObject((ICollection)q).ToString();
                    return last != condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.HaveMaximum)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string max = GetList<object>((ICollection)q).Max().ToString();
                    return max == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveMaximum)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string max = GetList<object>((ICollection)q).Max().ToString();
                    return max != condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.HaveMinimum)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string min = GetList<object>((ICollection)q).Min().ToString();
                    return min == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveMinimum)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string min = GetList<object>((ICollection)q).Min().ToString();
                    return min != condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.HaveAverage)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string avg = GetList<decimal>((ICollection)q).Average().ToString();
                    return avg == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveAverage)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string avg = GetList<decimal>((ICollection)q).Average().ToString();
                    return avg != condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.HaveTotal)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string total = GetList<decimal>((ICollection)q).Sum().ToString();
                    return total == condition.ConditionOperandValue.ToString();
                };
            }
            else if (condition.ConditionName == Operators.NotHaveTotal)
            {
                predicate = (p) =>
                {
                    var q = p.GetType().ToString() == "System.String" ? p.ToString().ToList<char>() : p;
                    string total = GetList<decimal>((ICollection)q).Sum().ToString();
                    return total != condition.ConditionOperandValue.ToString();
                };
            }
           
            
            else if (condition.ConditionName == Operators.HaveSize)
            {
                predicate = (p) => IsOfSize(p, condition.ConditionOperandValue);
            }
            else if (condition.ConditionName == Operators.NotHaveSize)
            {
                predicate = (p) => !IsOfSize(p, condition.ConditionOperandValue);
            }
            #endregion

            #region Order
            else if (condition.ConditionName == Operators.InOrder)
            {
                if (operandOne is ICollection)
                {
                    predicate = p => GetList<object>((ICollection)p).SequenceEqual(GetList<object>((ICollection)condition.ConditionOperandValues));
                }
                else
                {
                    throw new Exception("Not a valid collection");
                }
            }
            else if (condition.ConditionName == Operators.InAscendingOrder)
            {
                if (operandOne is ICollection)
                {

                    if (condition.ConditionOperandValue != null)
                    {
                        predicate = p =>
                        {
                            condition.ConditionOperandValues = GetList<object>((ICollection)p).OrderBy(x => x).ToArray();
                            return GetList<object>((ICollection)p).SequenceEqual(condition.ConditionOperandValues);
                        };
                    }
                    else
                    {
                        predicate = p =>
                        {
                            condition.ConditionOperandValues = GetList<object>((ICollection)p).OrderBy(x => x).ToArray();
                            return GetList<object>((ICollection)p).SequenceEqual(condition.ConditionOperandValues);
                        };
                    }
                }
                else
                {
                    throw new Exception("Not a valid collection");
                }
            }

            else if (condition.ConditionName == Operators.InDescendingOrder)
            {
                if (operandOne is ICollection)
                {

                    if (condition.ConditionOperandValue != null)
                    {
                        predicate = p =>
                        {
                            condition.ConditionOperandValues = GetList<object>((ICollection)p).OrderByDescending(x => x).ToArray();
                            return GetList<object>((ICollection)p).SequenceEqual(condition.ConditionOperandValues);
                        };
                    }
                    else
                    {
                        predicate = p =>
                        {
                            condition.ConditionOperandValues = GetList<object>((ICollection)p).OrderByDescending(x => x).ToArray();
                            return GetList<object>((ICollection)p).SequenceEqual(condition.ConditionOperandValues);
                        };
                    }
                }
                else
                {
                    throw new Exception("Not a valid collection");
                }
            }
            #endregion

            #region TypeChecks
            else if (condition.ConditionName == Operators.OfType)
            {
                predicate = (p) => ((Type)(condition.ConditionOperandValue)).IsAssignableFrom(p.GetType());
            }
            else if (condition.ConditionName == Operators.NotOfType)
            {
                predicate = (p) => !((Type)(condition.ConditionOperandValue)).IsAssignableFrom(p.GetType());
            }
            else if (condition.ConditionName == Operators.Generic)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType().IsGenericType;
                    return p.GetType().IsGenericType;
                };

            }
            else if (condition.ConditionName == Operators.NotGeneric)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType().IsGenericType;
                    return !p.GetType().IsGenericType;
                };
            }
            else if (condition.ConditionName == Operators.Array)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType().IsArray;
                };
            }
            else if (condition.ConditionName == Operators.NotArray)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return !p.GetType().IsArray;
                };
            }

            else if (condition.ConditionName == Operators.String)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() == typeof(System.String);
                };
            }
            else if (condition.ConditionName == Operators.NotString)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() != typeof(System.String);
                };
            }
            else if (condition.ConditionName == Operators.Boolean)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() == typeof(System.Boolean);
                };
            }
            else if (condition.ConditionName == Operators.NotBoolean)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() != typeof(System.Boolean);
                };
            }
            else if (condition.ConditionName == Operators.ValueType)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType().IsValueType;
                };
            }
            else if (condition.ConditionName == Operators.ObjectType)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType().BaseType == typeof(Object);
                };
            }
            else if (condition.ConditionName == Operators.NotObjectType)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType().BaseType != typeof(Object);
                };
            }
            else if (condition.ConditionName == Operators.NotValueType)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return !p.GetType().IsValueType;
                };
            }
            else if (condition.ConditionName == Operators.Integer)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() == typeof(System.Int32) || p.GetType() == typeof(System.Int16) || p.GetType() == typeof(System.Int64);
                };
            }
            else if (condition.ConditionName == Operators.Date)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = typeof(System.DateTime);
                    bool isDate=false;
                    try{
                       var dt = DateTime.Parse(p.ToString());
                        isDate=dt.GetType()==typeof(System.DateTime);
                    }
                    catch{}

                    return p.GetType() != typeof(System.Decimal) && isDate;
                };
            }
            else if (condition.ConditionName == Operators.NotInteger)
            {
                predicate = (p) =>
                {
                    condition.ConditionOperandValue = p.GetType();
                    return p.GetType() != typeof(System.Int32) && p.GetType() != typeof(System.Int16) && p.GetType() != typeof(System.Int64);
                };
            }
            else if (condition.ConditionName == Operators.Numeric)
            {

                var regex = GetNumericRegexPattern();
                predicate = (p) =>
                {
                    var res = regex.Match(operandOne.ToString()).Success;
                    condition.ConditionOperandValue = res.ToString();
                    return res;
                };
            }
            else if (condition.ConditionName == Operators.NotNumeric)
            {
                var regex = GetNumericRegexPattern();
                predicate = (p) =>
                {
                    var res = regex.Match(operandOne.ToString()).Success;
                    condition.ConditionOperandValue = res.ToString();
                    return !res;
                };
            }

            #endregion

            #region Regex
            else if (condition.ConditionName == Operators.RegexMatch)
            {
                predicate = (p) =>
                {
                    Match match = Regex.Match(p.ToString(), condition.ConditionOperandValue.ToString());
                    return match.Success;
                };
            }
            else if (condition.ConditionName == Operators.NoRegexMatch)
            {
                predicate = (p) =>
                {
                    Match match = Regex.Match(p.ToString(), condition.ConditionOperandValue.ToString());
                    return !match.Success;
                };
            }
            #endregion

            bool testResult = predicate(operandOne);
            ShowResult(testResult, condition, operandOne, message, testName);
           
            
            return testResult;
        }
        private static object GetFirstObject(ICollection collection)
        {
            object first = null;
            foreach(var each in collection)
            {
                first = each;
                break;
            }
            return first;
        }
        private static object GetLastObject(ICollection collection)
        {
            object last = null;
            int lastIndex = collection.Count;
            int i = 0;
            foreach (var each in collection)
            {
                i++;
                if (i == lastIndex)
                {
                    last = each;
                    break;
                }
            }
            return last;
        }
        private static void ShowResult(bool testResult, ICondition condition, object operandOne, string message,string testName)
        {
            #region ShowResults

            TestCondition(testResult, message, testName);

            if (true)//!testResult
            {

                Console.Write("Expected : ");



                string actualResult = "";

                if (condition.ConditionName == Operators.Exception || condition.ConditionName == Operators.NoException)
                {
                    actualResult = operandOne != null && operandOne is Exception ? ((Exception)operandOne).InnerException.GetType().ToString() : (operandOne != null ? GetActualDisplay(condition, operandOne) : "");
                }
                else
                {
                    actualResult = operandOne != null ? GetActualDisplay(condition, operandOne) : "";
                }


                Console.Write(actualResult);

                string expected = "";

                if (condition.ConditionName == Operators.Exception || condition.ConditionName == Operators.NoException)
                {
                    if (condition.ConditionOperandValue != null)
                    {
                        var ex = condition.ConditionOperandValue != null ? ((Type)condition.ConditionOperandValue).ToString() : typeof(Exception).ToString();
                        expected = ex;

                    }
                    else
                    {
                        expected = GetExpectedDisplay(condition);
                    }
                }
                else
                {
                    expected = GetExpectedDisplay(condition);
                }
                //Console.Write(expected);

                Console.Write(" (" + condition.Verb + "." + condition.ConditionName + ")");

                string valueOrValues = condition.ConditionOperandValues != null && condition.ConditionOperandValues.Length>1? "Values:" : "Value:";

                Console.WriteLine(" " + valueOrValues + expected);

            }
            #endregion
        }

        private static bool IsOfSize(object operand, object sizeObject)
        {
            int size = (int)sizeObject;

            if (operand is ICollection)
            {
                return ((ICollection)operand).Count == size;
            }
            else if (operand is Array)
            {
                return ((Array)operand).Length == size;
            }
            else if (operand is String)
            {
                return ((String)operand).Length == size;
            }

            return false;
        }
        private static Regex GetNumericRegexPattern()
        {
            return new Regex(@"^-?[0-9]?[0-9,\.]+$");
        }
        private static string GetActualDisplay(ICondition condition, object operandOne)
        {
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
                actualResult = operandOne.ToString();

            }



            return actualResult;
        }
        private static string GetExpectedDisplay(ICondition condition)
        {
            string expected = "";
            if (condition.ConditionOperandValue != null)
            {
                expected = condition.ConditionOperandValue.ToString();

            }

            if (condition.ConditionOperandValues != null)
            {
                expected = GetElementsString(condition.ConditionOperandValues).ToString();

            }

            if (condition.ConditionName == Operators.Exception || condition.ConditionName == Operators.NoException)
            {
                expected = (condition.ConditionOperandValue != null ? condition.ConditionOperandValue.ToString() : "Exception ");
            }

            return expected;
        }
        private static IEnumerable<T> GetList<T>(ICollection collection)
        {
            if(typeof(T)==typeof(object))
            {
                return (IEnumerable<T>)GetObjectList(collection);
            }
            else
            {
                return (IEnumerable<T>)GetDecimalList(collection);
            }
        }
        private static IEnumerable GetObjectList(ICollection collection)
        {
            List<object> lst = new List<object>();

            foreach (object el in collection)
            {
                lst.Add(el);
            }

            return lst;
        }

        private static IEnumerable GetDecimalList(ICollection collection)
        {
            List<decimal> lst = new List<decimal>();

            foreach (object el in collection)
            {
                lst.Add(Convert.ToDecimal(el.ToString()));
            }

            return lst;
        }


        private static string GetListString(ICollection collection)
        {
            List<string> lst = new List<string>();
            int i = 1;
            foreach (var e in collection)
            {
                lst.Add(e.ToString());
                if (i++ == 10)
                {
                    lst.Add("...");
                    break;
                }

            }

            return "{" + string.Join(",", lst) + "}";
        }

        private static string GetElementsString(params object[] parameters)
        {
            List<string> lst = new List<string>();
            int i = 1;
            foreach (var e in parameters)
            {
                lst.Add(e.ToString());
                if(i++==10)
                {
                    lst.Add("...");
                    break;
                }
            }

            return "{" + string.Join(",", lst) + "}";

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
            else if (arity == "Any")
            {
                return found > 0;
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
        private static bool CheckIfPresent(ICollection lst, object obj)
        {
            bool found = false;
            foreach (var e in lst)
            {
                var type = e.GetType().BaseType.ToString();
                var condition = type == "System.ValueType" ? obj.ToString() == e.ToString() : obj.GetHashCode() == e.GetHashCode();

                if (condition)
                {
                    found=true;
                    break;
                }
            }
            return found;
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
                    var type = e.GetType().BaseType.ToString();
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
                Show("Passed", testName, "");
                TotalPassed++;
            }
            else
            {
                Show("Failed", testName, message);
                TotalFailed++;
            }
        }
        public static void Show(string result, string testName, string message)
        {
            string textToDisplay = result;
            if (!string.IsNullOrEmpty(testName))
            {
                textToDisplay += " : (n) - " + testName;
            }
            if (!string.IsNullOrEmpty(message))
            {
                textToDisplay += " : (m) - " + message;
            }
            textToDisplay += " : (i) - ";
            Console.Write(textToDisplay);
        }

        public static void Summary()
        {
            Console.WriteLine("Total Test : " + TotalTests + " Passed : " + TotalPassed + " Failed : " + TotalFailed);
            Reset();
        }
    }

    public class Refl
    {
        public string Method { get; private set; }
        public string ClassName { get; private set; }
        public object[] Parameters { get; private set; }
        public Type ClassType { get; private set; }
        public Refl MethodName(string methodName)
        {
            this.Method = methodName;
            return this;
        }
        public Refl InClass(Type type)
        {

            if (!string.IsNullOrEmpty(this.ClassName))
            {
                throw new Exception("Class name has been already defined for this test");
            }

            this.ClassType = type;
            return this;
        }
        public Refl InClass(string className)
        {
            if (this.ClassType != null)
            {
                throw new Exception("Class type has been already defined for this test");
            }

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
        public string Verb { get; set; }
        public bool CaseInSensitive { get; set; }

        public static Throws Exception(Type exceptionType)
        {
            return new Throws() { ConditionName = Operators.Exception, ConditionOperandValue = exceptionType,Verb="Throws" };
        }
        public static Throws NoException(Type exceptionType)
        {
            return new Throws() { ConditionName = Operators.NoException, ConditionOperandValue = exceptionType, Verb = "Throws" };
        }
        public static Throws Exception()
        {
            return new Throws() { ConditionName = Operators.Exception, ConditionOperandValue = null, Verb = "Throws" };
        }
        public static Throws NoException()
        {
            return new Throws() { ConditionName = Operators.NoException, ConditionOperandValue = null, Verb = "Throws" };
        }
    }

    public class Does : ICondition
    {
        public Operators ConditionName { get; set; }
        public Object ConditionOperandValue { get; set; }
        public Object[] ConditionOperandValues { get; set; }
        public string Verb { get; set; }
        public bool CaseInSensitive { get; set; }
        public Does(Operators operatorName,object operandValue)
        {
            this.ConditionName = operatorName;
            this.ConditionOperandValue = operandValue;
            this.Verb = "Does";
        }
        public Does(Operators operatorName, object operandValue,bool caseInsensitive)
        {
            this.ConditionName = operatorName;
            this.ConditionOperandValue = operandValue;
            this.Verb = "Does";
            this.CaseInSensitive = caseInsensitive;
        }
        public Does(Operators operatorName, object[] operandValues)
        {
            this.ConditionName = operatorName;
            this.ConditionOperandValues = operandValues;
            this.Verb = "Does";
        }
        public static Does Return(object operandValue)
        {
            return new Does(Operators.Return, operandValue);
        }
        public static Does NotReturn(object operandValue)
        {
            return new Does(Operators.NotReturn, operandValue);
        }
        public static Does HaveAny(params object[] elements)
        {
            return new Does(Operators.HaveAny, elements);
        }
        public static Does HaveAll(params object[] elements)
        {
            return new Does(Operators.HaveAll, elements);
        }
        public static Does NotHaveAny(params object[] elements)
        {
            return new Does(Operators.NotHaveAny, elements);
        }
        public static Does NotHaveAll(params object[] elements)
        {
            return new Does(Operators.NotHaveAll, elements);
        }
        public static Does Contain(object operand)
        {
            return new Does(Operators.Contain, operand);
        }
        public static Does Contain(object operand,bool caseInsensitive)
        {
            return new Does(Operators.Contain, operand, caseInsensitive);
        }
        public static Does NotContain(object operand)
        {
            return new Does(Operators.NotContain, operand);
        }
        public static Does NotContain(object operand, bool caseInsensitive)
        {
            return new Does(Operators.NotContain, operand, caseInsensitive);
        }
        public static Does RegexMatch(string pattern)
        {
            return new Does(Operators.RegexMatch, pattern);
        }
        public static Does NoRegexMatch(string pattern)
        {
            return new Does(Operators.NoRegexMatch, pattern);
        }
        public static Does HaveSize(int size)
        {
            return new Does(Operators.HaveSize, size);
        }
        public static Does NotHaveSize(int size)
        {
            return new Does(Operators.NotHaveSize, size);
        }
        public static Does StartWith(object operand)
        {
            return new Does(Operators.StartWith, operand);
        }
        public static Does StartWith(object operand, bool caseInsensitive)
        {
            return new Does(Operators.StartWith, operand, caseInsensitive);
        }
        public static Does EndWith(object operand)
        {
            return new Does(Operators.EndWith, operand);
        }
        public static Does EndWith(object operand, bool caseInsensitive)
        {
            return new Does(Operators.EndWith, operand, caseInsensitive);
        }
        public static Does NotStartWith(object operand)
        {
            return new Does(Operators.NotStartWith, operand);
        }
        public static Does NotStartWith(object operand, bool caseInsensitive)
        {
            return new Does(Operators.NotStartWith, operand, caseInsensitive);
        }
        public static Does NotEndWith(object operand)
        {
            return new Does(Operators.NotEndWith, operand);
        }
        public static Does NotEndWith(object operand, bool caseInsensitive)
        {
            return new Does(Operators.NotEndWith, operand, caseInsensitive);
        }
        public static Does HaveFirstElement(object operand)
        {
            return new Does(Operators.HaveFirstElement, operand);
        }
        public static Does NotHaveFirstElement(object operand)
        {
            return new Does(Operators.NotHaveFirstElement, operand);
        }
        public static Does HaveLastElement(object operand)
        {
            return new Does(Operators.HaveLastElement, operand);
        }
        public static Does NotHaveLastElement(object operand)
        {
            return new Does(Operators.NotHaveLastElement, operand);
        }
        public static Does HaveMaximum(object operand)
        {
            return new Does(Operators.HaveMaximum, operand);
        }
        public static Does NotHaveMaximum(object operand)
        {
            return new Does(Operators.NotHaveLastElement, operand);
        }
        public static Does HaveMinimum(object operand)
        {
            return new Does(Operators.HaveMinimum, operand);
        }
        public static Does NotHaveMinimum(object operand)
        {
            return new Does(Operators.NotHaveMinimum, operand);
        }
        public static Does HaveAverage(object operand)
        {
            return new Does(Operators.HaveAverage, operand);
        }
        public static Does NotHaveAverage(object operand)
        {
            return new Does(Operators.NotHaveAverage, operand);
        }
        public static Does HaveTotal(object operand)
        {
            return new Does(Operators.HaveTotal, operand);
        }
        public static Does NotHaveTotal(object operand)
        {
            return new Does(Operators.NotHaveTotal, operand);
        }
    }



}

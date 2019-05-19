using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleAssertThat
{
    public static class Assert
    {
        public static int TotalPassed{get;set;}
        public static int TotalFailed { get; set; }
        public static bool ThrowExceptionOnFalse { get; set; }

        static Assert()
        {
            TotalFailed = TotalPassed = 0;
        }

        public static void Summary()
        {
            Console.WriteLine("Total Tests : " + (TotalPassed+TotalFailed) + "    Passed : " + TotalPassed+"   Failed: "+ TotalFailed);
        }

        public static void Reset()
        {
            TotalFailed = 0;
            TotalPassed = 0;
        }

        public static bool DisplayResult<T>(T operand, Predicate<T> predicate)
        {
            StringBuilder resultToDisplay = new StringBuilder();
            //IList<T> lst=operand as IList<T>;
            resultToDisplay.Append(predicate(operand));
            resultToDisplay.Append(" : ");
            resultToDisplay.Append(GetTestValue(operand));
            resultToDisplay.Append(" ");
            var displayResult = GetVerbName(predicate.Method) + " " + GetTestName(predicate.Method.Name);
            resultToDisplay.Append(" ");
            resultToDisplay.Append(displayResult);
            resultToDisplay.Append(" ");
            if (predicate.Target != null)
            {
                resultToDisplay.Append(GetParameterValue(predicate.Target));
            }

            Console.WriteLine(resultToDisplay.ToString());

            return false;
        }

        private static string GetResult(bool result, string values, System.Reflection.MethodInfo methodInfo, object target)
        {
            StringBuilder resultToDisplay = new StringBuilder();
            resultToDisplay.Append(result==true ? "Passed" : "Failed");
            resultToDisplay.Append(" : ");
            resultToDisplay.Append(values);
            resultToDisplay.Append(" ");
            var displayResult = GetVerbName(methodInfo) + " " + GetTestName(methodInfo.Name);
            resultToDisplay.Append(" ");
            resultToDisplay.Append(displayResult);
            resultToDisplay.Append(" ");
            if (target != null)
            {
                resultToDisplay.Append(GetParameterValue(target));
            }
            return resultToDisplay.ToString();
        }

        public static void That<T>(T source, Predicate<T> predicate)
        {
            var result = predicate(source);
            var values = GetTestValue(source);

            var display = GetResult(result, values, predicate.Method, predicate.Target);

            CheckResult(result, display);
            WriteMessage(display);
        }

        private static bool CheckResult(bool p,string message)
        {
            if(p)
            {
                TotalPassed++;
            }
            else
            {
                TotalFailed++;
            }
            if(ThrowExceptionOnFalse && !p)
            {
                throw new Exception(message);
            }
            else
            {
                return p;
            }

        }

        private static void WriteMessage(string display)
        {
            if (!ThrowExceptionOnFalse)
            {
                Console.WriteLine(display);
            }
        }

        public static void That<T>(IList<T> source, ITest<T> test)
        {

            var result = test.TestItems(source);
            var values = GetTestValue(source);

            var display = GetResult(result, values, test.TestItems.Method, test.TestItems.Target);
            
            CheckResult(result,display);
            WriteMessage(display);
        }

        public static void That<T>(T source, ITest<T> test)
        {
            bool result =  test.TestParameters != null ? test.TestParameters(source) : test.TestItem(source);
            
            var value = GetTestValue(source);
            var display = test.TestParameters != null ?
                GetResult(result, value, test.TestParameters.Method, test.TestParameters.Target)
                : GetResult(result, value, test.TestItem.Method, test.TestItem.Target);

            CheckResult(result, display);
            WriteMessage(display);
        }

       

        private static string GetTestValue<T>(T val)
        {
            var r = val is IEnumerable && !(val is String);
            if (r)
            {
                return GetTestValues((IEnumerable)val);
            }
            else
            {
                if (val == null)
                {
                    return "null";
                }
                return val.ToString();
            }
        }

        private static string GetTestValues(IEnumerable list)
        {
            string testValue = "{";

            //var lst = (ICollection)list;
            bool firstDone = false;
            foreach (var v in list)
            {
                if (!firstDone)
                {
                    firstDone = true;
                    testValue += v.ToString();
                }
                else
                {
                    testValue += "," + v.ToString();
                }
            }

            return testValue + "}";
        }

        private static string GetVerbName(System.Reflection.MethodInfo method)
        {
            string verb = "";
            if (method.ReflectedType.ReflectedType != null)
            {
                verb = method.ReflectedType.ReflectedType.Name.ToString();
            }
            else
            {
                verb = method.ReflectedType.Name;
            }

            return verb;
        }

        private static string GetParameterValue(object target)
        {
            string values = "";
            var obj = target;

            object valObj = null;
            var fields = target.GetType().GetFields();
            if (target.GetType().GetFields().Count() == 1)
            {
                valObj = fields[0].GetValue(obj);

                var r = valObj is IEnumerable && !(valObj is String);
                if (r)
                {
                    return GetTestValues((IEnumerable)valObj);
                }
                else
                {
                    values = valObj.ToString();
                    return values;
                }


            }
            else
            {
                bool firstDone = false;
                for (int i = 0; i < fields.Count(); i++)
                {
                    if (!firstDone)
                    {
                        firstDone = true;
                        valObj = fields[i].GetValue(obj);
                        values += "(" + valObj.ToString();
                    }
                    else
                    {
                        valObj = fields[i].GetValue(obj);
                        values += "," + valObj.ToString();
                    }
                }

                values += ")";

                return values;
            }


        }

        private static string GetTestName(string testName)
        {
            var reg = new Regex(@"[A-Z]{1}[a-zA-Z]+");
            var result = reg.Match(testName);
            return result.Value.ToString();
        }
    }

    public class ITest<T>
    {
        public Predicate<IList<T>> TestItems { get; set; }
        public Predicate<T> TestItem { get; set; }
        public Predicate<T> TestParameters { get; set; }
    }

    public class Does
    {

        public static Predicate<string> RegexMatch(string regex)
        {
            return (x) =>
            {
                Match match = Regex.Match(x.ToString(), regex);
                return match.Success;
            };
        }

        public static Predicate<string> NotRegexMatch(string regex)
        {
            return (x) =>
            {
                Match match = Regex.Match(x.ToString(), regex);
                return !match.Success;
            };
        }

        public static ITest<T> Contain<T>(T val)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList<T>)x).Contains(val);
                    else if(x is string)
                        return x.ToString().Contains(val.ToString());
                    else return false;
                },
                TestItem = (x) =>
                {
                    if (!(x is IEnumerable) && !(x is string))
                        return x.ToString().Contains(val.ToString());
                    else if (x is string)
                        return x.ToString().Contains(val.ToString());
                    else return false;
                }
            };
        }

        public static ITest<T> NotContain<T>(T val)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    if (!(x is IEnumerable) && !(x is string))
                        return !((IList<T>)x).Contains(val);
                    else if (x is string)
                        return !(x.ToString().Contains(val.ToString()));
                    else return false;
                },
                TestItem = (x) =>
                {
                    if (!(x is IEnumerable) && !(x is string))
                        return !x.ToString().Contains(val.ToString());
                    else if (x is string)
                        return !(x.ToString().Contains(val.ToString()));
                    else return false;
                }
            };
        }

        public static ITest<T> HaveAny<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return (from li in ((IList<T>)x).ToList()
                            join e in expectedList on li equals e
                            select li).Count() > 0;

                }
            };
        }

        public static ITest<T> HaveAll<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return (from li in ((IList<T>)x).ToList()
                            join e in expectedList on li equals e
                            select li).Count() == expectedList.Count();

                }
            };
        }

        public static ITest<T> NotHaveAll<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return (from li in ((IList<T>)x).ToList()
                            join e in expectedList on li equals e
                            select li).Count() != expectedList.Count();

                }
            };
        }


        public static ITest<T> NotHaveAny<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return (from li in ((IList<T>)x).ToList()
                            join e in expectedList on li equals e
                            select li).Count() == 0;

                }
            };
        }

        public static ITest<T> HaveMinimum<T>(T targetValue)
        {
            Predicate<IList<T>> p = (x) => x.Min().ToString() == targetValue.ToString();

            return new ITest<T> { TestItems = p };
        }

        public static ITest<T> NotHaveMaximum<T>(T targetValue)
        {
            Predicate<IList<T>> p = (x) => !(x.Max().ToString() == targetValue.ToString());

            return new ITest<T> { TestItems = p };
        }

        public static ITest<T> HaveMaximum<T>(T targetValue)
        {
            Predicate<IList<T>> p = (x) => x.Max().ToString() == targetValue.ToString();

            return new ITest<T> { TestItems = p };
        }

        public static ITest<T> NotHaveMinimum<T>(T targetValue)
        {
            Predicate<IList<T>> p = (x) => !(x.Min().ToString() == targetValue.ToString());

            return new ITest<T> { TestItems = p };
        }


        public static ITest<object> HaveSize(int targetValue)
        {
            return new ITest<object>()
            {
                TestItem = (x) =>
                 {
                     if (x is IEnumerable && !(x is System.String))
                         return ((IList)x).Count == targetValue;
                     if (!(x is IEnumerable))
                         return x.ToString().Length == targetValue;
                     if (x is string)
                         return x.ToString().Length == targetValue;

                     else return false;
                 }
            };
        }

        public static ITest<object> NotHaveSize(int targetValue)
        {
            return new ITest<object>()
            {

                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is System.String))
                        return ((IList)x).Count != targetValue;
                    if (!(x is IEnumerable))
                        return x.ToString().Length != targetValue;
                    else return false;
                }
            };
        }

        public static ITest<T> EndWith<T>(T value)
        {
            return new ITest<T>
            {
                TestItem = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return (((IList<T>)x).Last().ToString() == value.ToString());
                    }
                    else
                    {
                        return (x.ToString().EndsWith(value.ToString()));
                    }
                },

                TestItems = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return (((IList<T>)x).Last().ToString() == value.ToString());
                    }
                    else
                    {
                        return (x.ToString().EndsWith(value.ToString()));
                    }
                },


            };
        }

        public static ITest<T> NotEndWith<T>(T value)
        {
            return new ITest<T>
            {
                TestItem = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return !(((IList<T>)x).Last().ToString() == value.ToString());
                    }
                    else
                    {
                        return !(x.ToString().EndsWith(value.ToString()));
                    }
                },

                TestItems = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return !(((IList<T>)x).Last().ToString() == value.ToString());
                    }
                    else
                    {
                        return !(x.ToString().EndsWith(value.ToString()));
                    }
                },


            };
        }

        public static ITest<T> StartWith<T>(T value)
        {
            return new ITest<T>
            {
                TestItem = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return (((IList<T>)x).First().ToString() == value.ToString());
                    }
                    else
                    {
                        return (x.ToString().StartsWith(value.ToString()));
                    }
                },

                TestItems = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return (((IList<T>)x).First().ToString() == value.ToString());
                    }
                    else
                    {
                        return (x.ToString().StartsWith(value.ToString()));
                    }
                },


            };
        }

        public static ITest<T> NotStartWith<T>(T value)
        {
            return new ITest<T>
            {
                TestItem = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return !(((IList<T>)x).First().ToString() == value.ToString());
                    }
                    else
                    {
                        return !(x.ToString().StartsWith(value.ToString()));
                    }
                },

                TestItems = (x) =>
                {
                    if (x is IList<T> && !(x is String))
                    {
                        return !(((IList<T>)x).First().ToString() == value.ToString());
                    }
                    else
                    {
                        return !(x.ToString().StartsWith(value.ToString()));
                    }
                },


            };
        }

        public static Predicate<Object> Return(object targetValue)
        {
            return (x) => x.ToString() == targetValue.ToString();
        }

        public static Predicate<Object> NotReturn(object targetValue)
        {
            return (x) => x.ToString() != targetValue.ToString();
        }

    }

    public class Is
    {
        public static Predicate<object> Date
        {
            get
            {
                DateTime dt;
                return (x) => DateTime.TryParse(x.ToString(), out dt);
            }
        }
        public static Predicate<object> NotDate
        {
            get
            {
                DateTime dt;
                return (x) =>!(DateTime.TryParse(x.ToString(), out dt));
            }
        }

        public static Predicate<object> Numeric
        {
            get
            {
                return (x) =>
                {
                    var regex = GetNumericRegexPattern();
                    var res = regex.Match(x.ToString()).Success;
                    return res;
                };
            }
        }
        public static Predicate<object> ValueType
        {
            get
            {
                return (x) =>
                {
                    return x.GetType().IsValueType;
                   
                };
            }
        }

        public static Predicate<object> NotValueType
        {
            get
            {
                return (x) =>
                {
                    return !(x.GetType().IsValueType);

                };
            }
        }

        public static Predicate<object> ObjectType
        {
            get
            {
                return (x) =>
                {
                    return !(x.GetType().IsValueType);

                };
            }
        }

        public static Predicate<object> NotObjectType
        {
            get
            {
                return (x) =>
                {
                    return (x.GetType().IsValueType);

                };
            }
        }

        public static Predicate<object> NotNumeric
        {
            get
            {
                return (x) =>
                {
                    var regex = GetNumericRegexPattern();
                    var res = !(regex.Match(x.ToString()).Success);
                    return res;
                };
            }
        }

        private static Regex GetNumericRegexPattern()
        {
            return new Regex(@"^-?[0-9]?[0-9,\.]+$");
        }

        public static Predicate<object> InAscendingOrder
        {
            get
            {
                return (x) =>
                {
                    var sorted = GetList<object>((ICollection)x).OrderBy(o => o);
                    return GetList<object>((ICollection)x).SequenceEqual(sorted);
                };
            }
        }

        public static Predicate<object> NotInAscendingOrder
        {
            get
            {
                return (x) =>
                {
                    var sorted = GetList<object>((ICollection)x).OrderBy(o => o);
                    return !(GetList<object>((ICollection)x).SequenceEqual(sorted));
                };
            }
        }

        public static Predicate<object> InDescendingOrder
        {
            get
            {
                return (x) =>
                {
                    var sorted = GetList<object>((ICollection)x).OrderByDescending(o => o);
                    return GetList<object>((ICollection)x).SequenceEqual(sorted);
                };
            }
        }

        public static Predicate<object> NotInDescendingOrder
        {
            get
            {
                return (x) =>
                {
                    var sorted = GetList<object>((ICollection)x).OrderByDescending(o => o);
                    return !(GetList<object>((ICollection)x).SequenceEqual(sorted));
                };
            }
        }

        private static IEnumerable<T> GetList<T>(ICollection collection)
        {
            if (typeof(T) == typeof(object))
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


        public static ITest<T> InOrder<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return ((IList<T>)x).OrderBy(o => o).ToList().SequenceEqual(expectedList);
                }
            };
        }
        public static ITest<T> NotInOrder<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    var expectedList = targetList.ToList();
                    return !(((IList<T>)x).OrderBy(o => o).ToList().SequenceEqual(expectedList));
                }
            };
        }

        public static ITest<T> In<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestParameters = (x) =>
                {
                    return ((T[])targetList).Any(a => a.ToString() == x.ToString());
                }
            };
        }

        public static ITest<object> In<T>(T targetList)
        {
            return new ITest<object>()
            {
                TestItem = (x) =>
                {
                    var lst = targetList as IEnumerable;
                    return Any(lst, x);
                }
            };
        }


        public static ITest<T> NotIn<T>(params T[] targetList)
        {
            return new ITest<T>()
            {
                TestParameters = (x) =>
                {
                    return !((T[])targetList).Any(a => a.ToString() == x.ToString());
                }
            };
        }

        public static ITest<object> NotIn<T>(T targetList)
        {
            return new ITest<object>()
            {
                TestItem = (x) =>
                {
                    var lst = targetList as IEnumerable;
                    return !Any(lst, x);
                }
            };
        }

        public static Predicate<object> Same(object target)
        {
            return (x) => x.GetHashCode() == target.GetHashCode();
        }

        public static Predicate<object> NotSame(object target)
        {
            return (x) => x.GetHashCode() != target.GetHashCode();
        }

        private static bool Any(IEnumerable collection, object item)
        {
            bool found = false;

            foreach (object c in collection)
            {
                if (c.ToString() == item.ToString())
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public static ITest<Decimal> Between(Decimal startValue, Decimal endValue)
        {
            return new ITest<Decimal>()
            {
                TestItem = (x) =>
                {
                    return endValue >= x && startValue <= x;
                }
            };
        }

        public static ITest<Decimal> NotBetween(Decimal startValue, Decimal endValue)
        {
            return new ITest<Decimal>()
            {
                TestItem = (x) =>
                {
                    return !(endValue >= x && startValue <= x);
                }
            };
        }

        public static ITest<T> EqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    if (x is IEnumerable)
                        return SequenceMatching((IEnumerable)x, (IEnumerable)targetValue);
                    else return false;
                },
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is System.String))
                        return SequenceMatching((IEnumerable)x, (IEnumerable)targetValue);
                    if (!(x is IEnumerable))
                        return x.Equals(targetValue);
                    else return false;
                }
            };


        }

        private static bool SequenceMatching(IEnumerable listOne, IEnumerable listTwo)
        {
            IList l1 = (IList)listOne;
            IList l2 = (IList)listTwo;
            bool matched = true;
            matched = l2.Count == l1.Count;
            if (matched)
            {
                for (int i = 0; i < l2.Count; i++)
                {
                    if (l1[i].ToString() != l2[i].ToString())
                    {
                        matched = false;
                        break;
                    }
                }
            }

            return matched;
        }

        public static ITest<T> NotEqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItems = (x) =>
                {
                    if (x is IEnumerable)
                        return !SequenceMatching((IEnumerable)x, (IEnumerable)targetValue);
                    else return false;
                },
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is System.String))
                        return !SequenceMatching((IEnumerable)x, (IEnumerable)targetValue);
                    if (!(x is IEnumerable))
                        return !x.Equals(targetValue);
                    else return false;
                }
            };
        }

        public static ITest<T> GreaterThan<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList)x).Count > ((IList)targetValue).Count;
                    else if ((x is String) || (x is Char))
                        return x.ToString().CompareTo(targetValue.ToString()) > 0;
                    else if (!(x is IEnumerable) && !(x is String))
                        return (Convert.ToDecimal(x.ToString()) > (Convert.ToDecimal(targetValue.ToString())));
                    else return false;
                }
            };
        }

        public static ITest<T> NotGreaterThan<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList)x).Count <= ((IList)targetValue).Count;
                    else if ((x is String) || (x is Char))
                        return x.ToString().CompareTo(targetValue.ToString()) <= 0;
                    else if (!(x is IEnumerable) && !(x is String))
                        return (Convert.ToDecimal(x.ToString()) <= (Convert.ToDecimal(targetValue.ToString())));
                    else return false;
                }
            };
        }

        public static ITest<T> GreaterThanOrEqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList)x).Count >= ((IList)targetValue).Count;
                    else if ((x is String) || (x is Char))
                        return x.ToString().CompareTo(targetValue.ToString()) >= 0;
                    else if (!(x is IEnumerable) && !(x is String))
                        return (Convert.ToDecimal(x.ToString()) >= (Convert.ToDecimal(targetValue.ToString())));
                    else return false;
                }
            };
        }

        public static ITest<T> NotGreaterThanOrEqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return !(((IList)x).Count >= ((IList)targetValue).Count);
                    else if ((x is String) || (x is Char))
                        return !(x.ToString().CompareTo(targetValue.ToString()) >= 0);
                    else if (!(x is IEnumerable) && !(x is String))
                        return !((Convert.ToDecimal(x.ToString()) >= (Convert.ToDecimal(targetValue.ToString()))));
                    else return false;
                }
            };
        }

        public static ITest<T> LesserThan<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList)x).Count < ((IList)targetValue).Count;
                    else if ((x is String) || (x is Char))
                        return x.ToString().CompareTo(targetValue.ToString()) < 0;
                    else if (!(x is IEnumerable) && !(x is String))
                        return (Convert.ToDecimal(x.ToString()) < (Convert.ToDecimal(targetValue.ToString())));
                    else return false;
                }
            };
        }

        public static ITest<T> NotLesserThan<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return !(((IList)x).Count < ((IList)targetValue).Count);
                    else if ((x is String) || (x is Char))
                        return !(x.ToString().CompareTo(targetValue.ToString()) < 0);
                    else if (!(x is IEnumerable) && !(x is String))
                        return !((Convert.ToDecimal(x.ToString()) < (Convert.ToDecimal(targetValue.ToString()))));
                    else return false;
                }
            };
        }

        public static ITest<T> LesserThanOrEqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return ((IList)x).Count <= ((IList)targetValue).Count;
                    else if ((x is String) || (x is Char))
                        return x.ToString().CompareTo(targetValue.ToString()) <= 0;
                    else if (!(x is IEnumerable) && !(x is String))
                        return (Convert.ToDecimal(x.ToString()) <= (Convert.ToDecimal(targetValue.ToString())));
                    else return false;
                }
            };
        }

        public static ITest<T> NotLesserThanOrEqualTo<T>(T targetValue)
        {
            return new ITest<T>()
            {
                TestItem = (x) =>
                {
                    if (x is IEnumerable && !(x is String))
                        return !(((IList)x).Count <= ((IList)targetValue).Count);
                    else if ((x is String) || (x is Char))
                        return !(x.ToString().CompareTo(targetValue.ToString()) <= 0);
                    else if (!(x is IEnumerable) && !(x is String))
                        return !((Convert.ToDecimal(x.ToString()) <= (Convert.ToDecimal(targetValue.ToString()))));
                    else return false;
                }
            };
        }

        public static Predicate<object> OfType(Type expectedType)
        {
            return (x) => x.GetType().FullName == expectedType.FullName;
        }

        public static Predicate<object> NotOfType(Type expectedType)
        {
            return (x) => x.GetType().FullName != expectedType.FullName;
        }

        public static Predicate<bool> True
        {
            get
            {
                return x => x == true;
            }
        }

        public static Predicate<bool> False
        {
            get
            {
                return x => x == false;
            }
        }

        public static ITest<object> Null
        {
            get
            {
                return new ITest<object>()
                {
                    TestItems = (x) =>
                    {
                        return x == null;
                    },

                    TestItem = (x) =>
                    {
                        return x == null;
                    }
                };
            }
        }

        public static ITest<object> NotNull
        {
            get
            {
                return new ITest<object>()
                {
                    TestItems = (x) =>
                    {
                        return x != null;
                    },

                    TestItem = (x) =>
                    {
                        return x != null;
                    }
                };
            }
        }

        public static Predicate<object> Boolean
        {
            get
            {
                return x => x.GetType().FullName == typeof(System.Boolean).FullName;
            }
        }

        public static Predicate<object> NotBoolean
        {
            get
            {
                return x => !(x.GetType().FullName == typeof(System.Boolean).FullName);
            }
        }

        public static Predicate<Decimal> Zero
        {
            get
            {
                return x => x == 0;
            }
        }

        public static Predicate<Decimal> NotZero
        {
            get
            {
                return x => x != 0;
            }

        }

        public static Predicate<Object> Empty
        {
            get
            {
                Predicate<Object> p = null;

                p = (x) =>
                {
                    if (x is IEnumerable && !(x is string))
                    {
                        return IsSizeZero((IEnumerable)x);
                    }
                    else
                    {
                        return x.ToString().Length == 0;
                    }
                };


                return p;
            }
        }

        private static bool IsSizeZero(IEnumerable list)
        {
            IList l1 = (IList)list;
            return l1.Count == 0;
        }

        public static Predicate<Object> NotEmpty
        {
            get
            {
                Predicate<Object> p = null;

                p = (x) =>
                {
                    if (x is IEnumerable && !(x is string))
                    {
                        return !IsSizeZero((IEnumerable)x);
                    }
                    else
                    {
                        return x.ToString().Length != 0;
                    }
                };


                return p;
            }
        }

        public static Predicate<object> String
        {
            get
            {
                return (x) => x.GetType().FullName == typeof(System.String).FullName;
            }
        }

        public static Predicate<object> NotString
        {
            get
            {
                return (x) => x.GetType().FullName != typeof(System.String).FullName;
            }
        }
    }


    public class Throws
    {
        public static Predicate<object> Exception()
        {
            return (x) => x is Exception;
        }

        public static Predicate<object> NoException()
        {
            return (x) => !(x is Exception);
        }

        public static Predicate<object> Exception(Type type)
        {
            return (x) => (x is Exception) && ((Exception)x).InnerException.GetType().FullName == type.FullName;
        }

        public static Predicate<object> NoException(Type type)
        {
            return (x) => !(x is Exception && ((Exception)x).InnerException.GetType().FullName == type.FullName);
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
        public object InClass(Type type)
        {

            if (!string.IsNullOrEmpty(this.ClassName))
            {
                throw new Exception("Class name has been already defined for this test");
            }

            this.ClassType = type;
            return InvokeMethod(this);
        }
        public object InClass(string className)
        {
            if (this.ClassType != null)
            {
                throw new Exception("Class type has been already defined for this test");
            }

            this.ClassName = className;
            return InvokeMethod(this);
        }

        public Refl WithParameters(params object[] parameters)
        {
            this.Parameters = parameters;
            return this;
        }
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
                    // type = Type.GetType(reflection.ClassName + "," + assemblyName);

                    var an = Assembly.LoadWithPartialName(assemblyName);
                    var classType = an.GetTypes().Where(t => t.Name == className).FirstOrDefault();
                    if (classType != null)
                    {
                        type = classType;
                    }
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

                try
                {
                    object magicValue = method.Invoke(instance, parameters);
                    return magicValue == null ? "null" : magicValue;
                }
                catch(Exception ex)
                {
                    return ex;
                }
                
            }

            return null;
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
}

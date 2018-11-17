# SimpleAssertThat
A Simple, Small Assert.That unit testing tool for .Net projects.

#### Features:
1. Simple to use(Even non C# programmer can write unit testing code to test various technical/functional scenarios)
2. More readable unit testing code
3. Small Size(less than 50KB)
4. Test almost every units of your project.
5. Open source, code can be changed if required.

## Example 1:
```
void Main()
{
	Assert.That(Add(2,4),Is.EqualTo(6));
}
public static int Add(int first,int second)
{
	return first+second;
}
```

## Output:
Passed


## Example 2:
```
void Main()
{
	Assert.That(Add(2,4),Is.EqualTo(6),"Logic is not as per design");
}
public static int Add(int first,int second)
{
	return first+second+1;//some wrong logic
}
```

## Output:
Failed: Logic is not as per design

## Example 3:
```
void Main()
{
	Assert.That(AMethod.ByName("GetSubString").WithParameters("Sample").InClass("MyName.Test"), Throws.NoException(),"Method throws exception");
}
public static string GetSubString(string str)
{
	return str.Substring(2);
}
```
## Output:
Passed

### Assert.That assertive operators:
1. Is.True,
2. Is.False,
3. Is.SameAs,
4. Is.NotSameAs,
5. Is.EqualTo,
6. Is.NotEqualTo,
7. Is.Null,
8. Is.NotNull,
9. Does.HaveAny,
10. Does.NotHaveAll,
11. Does.Return,
12. Does.NotRetun,
13. Does.Contain,
14. Is.Numeric,
15. Is.ValueType,
16. Is.Array,
17. Is.Generic,
18. Does.RegexMatch,
19. Does.NoRegexMatch,
20. Throws.Exception,
21. Throws.NoException,
22. and many more

##### Note: More documenation will be added.

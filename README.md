# SimpleAssertThat
A Simple, Small Assert.That testing tool for unit testing in your projects.

#### Features:
1. Simple to use
2. More readable unit testing code
3. Small Size(less than 6KB)
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
	Assert.That(AMethod.ByName("GetSubString").WithParameters("Sample").InClass("MyName.Test"), Throws.Exception(),"Method does not throw exception");
}
public static string GetSubString(string str)
{
	return "a".Substring(2);
}
```
## Output:
Passed

### Assert.That assertive operators:
1. True,
2. False,
3. Same,
4. NotSame,
5. EqualTo,
6. NotEqualTo,
7. Null,
8. NotNull
9. Exception
10. NoException
11. Does. Return
12. Does.NotRetun
 
##### Note: More documenation will be added.

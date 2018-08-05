# SimpleAssertThat
A Simple, Small Assert.That testing tool for unit testing in your projects.

#### Features:
1. Simple
2. Small Size(less than 10KB)
3. Test almost every units of your project.
4. Open source, code can be changes if required.

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

More available Assert.That assertive operators:
 __True,
__False,
__Same,
__NotSame,
__EqualTo,
__NotEqualTo,
__Null,
__NotNull
 
 ### Note: More documenation will be added.

How to use SimpleAssertThat with Microsoft's Unit Test Projects

** Step 1: 
Create new Microsoft Unit Test Project in Visual Studio

** Step 2:
Install SimpleAssertThat from nuget package manager.

** Step 3: 
Add a class with TestClass attribute and a method with TestMethod attribute as shown below.
```
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAssertThat;
using System.Collections.Generic;
namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SimpleAssertThat.Assert.That(new List<int> { 1, 2, 3, 4, 5 }, Does.HaveTotal(15));
        }
    }
}
```
![MS Unit Tests with SimpleAssertThat](https://github.com/GautamSMok/SimpleAssertThat/blob/master/MSUTest.PNG)


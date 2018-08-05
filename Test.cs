using System;
using SimpleAssertThat;
namespace AssertTestDemo
{
    public class Program
    {
        public static void Main()
        {
            Assert.That(2 < 34, Is.True);
            Assert.That(Add(2, 4), Is.EqualTo(6), "Logic is not as per design");
            Console.ReadKey();
        }
        public static int Add(int first, int second)
        {
            return first + second + 1;//some wrong logic
        }
    }

}

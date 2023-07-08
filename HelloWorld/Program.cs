// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Console.Write("First");
// Console.Write("Second"); // println() vs print() in java

using System;

namespace HelloWorld // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(args[0]);

            // Useful for storing constants more efficiently
            // sbyte maxValueSbyte = 127;
            // sbyte minValueSbyte = -128;
            // byte maxValueByte = 255;
            // byte minValueByte = 0;

            // short minValueShort = -32768;
            // ushort maxValueUshort = 65535;

            // int maxValueInt = 2147483647;
            // int minValueInt = -2147483648;

            // long maxLong = 9223372036854775807;
            // long minLong = -9223372036854775808;

            // float myFloat = 0.751f;
            // double myDouble = 0.75d;

            // decimal myDecimal = 0.751m;
            // float mySecondFloat = 0.75f;
            // double mySecondDouble = 0.751d;

            // decimal mySecondDecimal = 0.75m;

            // Console.WriteLine(myFloat - mySecondFloat);
            // Console.WriteLine(myDouble - mySecondDouble);
            // Console.WriteLine(myDecimal - mySecondDecimal);

            // string myString = "Hello World!";
            // Console.WriteLine(myString);

            // bool myBool = myString.Length == 0;

            // myString = myBool == true ? "blah blah" : "fjkdj";

            // string[] myGroceryArray = new string[2];

            string[] myGroceryArray = {"Apples", "Eggs", "Milk"};
            // string[] array = ["Apples", "Eggs", "Milk"]; // not valid

            List<string> myGroceryList = new List<string>() {
                "apples",
                "bob",
                "milk",
                "eggs",
            }; // able to push pop etc

            myGroceryList.Add("more milk");
            

            IEnumerable<string> myGroceryIenumerable = myGroceryList;

            Console.WriteLine(myGroceryIenumerable.First()); // Ienumerable is good for read only or initializing stuff and then use it for sorting, filtering, etc

            //So if you need the ability to make permanent changes of any kind to your collection (add & remove), 
            // you'll need List. If you just need to read, sort and/or filter your collection, IEnumerable is sufficient for that purpose.


            string[,] twoDimensionalArray = new string[,] {
                { "1", "2"},
                { "3", "4"},
            };

        }
    }
}
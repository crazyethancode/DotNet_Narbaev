using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string s = "У меня 10 долларов и 3 яблока.";
        Console.WriteLine("Исходная строка:");
        Console.WriteLine(s);
        Console.WriteLine("-----------------------");

        Regex regex = new Regex(@"(?<consonant>[бвгджзйклмнпрстфхцчшщ])", RegexOptions.IgnoreCase);

        string result = regex.Replace(s, m => m.Groups["consonant"].Value + m.Groups["consonant"].Value);
        Console.WriteLine("Результат:");
        Console.WriteLine(result);
    }
}

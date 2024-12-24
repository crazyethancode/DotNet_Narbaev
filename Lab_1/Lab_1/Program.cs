using System;

class Program
{
    static void Main()
    {
        string input = "У меня 10 долларов и 3 яблока.";
        Console.WriteLine(input);
        string output = DuplicateConsonants(input);
        Console.WriteLine(output);
    }

    static string DuplicateConsonants(string input)
    {
        string consonants = "бвгджзйклмнпрстфхцчшщ";
        string result = "";

        foreach (char c in input)
        {
            if (consonants.Contains(char.ToLower(c)))
            {
                result += c.ToString() + c.ToString();
            }
            else
            {
                result += c;
            }
        }

        return result;
    }
}

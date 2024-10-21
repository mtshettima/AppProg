using System;
using System.IO;
using System.Text.RegularExpressions;

Console.Write("Enter your name: ");
string name = Console.ReadLine();

string birthdateInput;
do
{
    Console.Write("Enter your birthdate (MM/dd/yyyy): ");
    birthdateInput = Console.ReadLine().Trim();
} while (!Regex.IsMatch(birthdateInput, @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$"));

// Validate and parse birthdate
DateTime birthdate = DateTime.ParseExact(birthdateInput, "MM/dd/yyyy", null);

// Calculate age
int age = CalculateAge(birthdate);

// Display user's age
Console.WriteLine($"Hello, {name}! You are {age} years old.");

// Save user's information to a file named "user_info.txt"
string fileName = "user_info.txt";
using (StreamWriter writer = new StreamWriter(fileName))
{
    writer.WriteLine($"Name: {name}");
    writer.WriteLine($"Birthdate: {birthdate.ToShortDateString()}");
    writer.WriteLine($"Age: {age}");
}
Console.WriteLine($"User information saved to {fileName}");

// Read and display contents of "user_info.txt"
Console.WriteLine("\nContents of user_info.txt:");
using (StreamReader reader = new StreamReader(fileName))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine(line);
    }
}

// Prompt the user to enter a directory path
Console.Write("\nEnter a directory path to list files: ");
string directoryPath = Console.ReadLine().Trim();

// List all files within the specified directory
if (Directory.Exists(directoryPath))
{
    string[] files = Directory.GetFiles(directoryPath);
    Console.WriteLine($"\nFiles in directory '{directoryPath}':");
    foreach (string file in files)
    {
        Console.WriteLine(Path.GetFileName(file));
    }
}
else
{
    Console.WriteLine($"Directory '{directoryPath}' does not exist.");
}

// Prompt the user to input a string and format it to title case
Console.Write("\nEnter a string to format to title case: ");
string inputString = Console.ReadLine().Trim();
string titleCaseString = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower());
Console.WriteLine($"Formatted string (title case): {titleCaseString}");

// Explicitly trigger garbage collection
GC.Collect();
Console.WriteLine("\nGarbage collection triggered.");

Console.WriteLine("\nPress any key to exit.");
Console.ReadKey();

// Helper method to calculate age based on birthdate
int CalculateAge(DateTime birthdate)
{
    DateTime now = DateTime.Today;
    int age = now.Year - birthdate.Year;
    if (birthdate > now.AddYears(-age))
        age--;
    return age;
}

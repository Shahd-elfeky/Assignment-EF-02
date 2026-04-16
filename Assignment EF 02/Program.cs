using System;
using Assignment_EF_02.Data;

namespace Assignment_EF_02;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("EventHub Entity Framework Core Console App.");

        using var context = new EventHubDbContext();

        Console.WriteLine("App configured successfully!");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public enum DeveloperType
{
    OnContract,
    OnPayroll
}

public class Developer
{
    public int Id;
    public string DeveloperName;
    public DateTime JoinDate;
    public string ProjectAssigned;
    public DeveloperType Type;

    // For OnContract Developers
    public int Duration;
    public decimal ChargesPerDay;

    // For OnPayroll Developers
    public string Dept;
    public string Manager;
    public decimal NetSalary;
    public int Exp;
    public decimal DA;
    public decimal HRA;
    public decimal LTA;
    public decimal PF;

    // Constructors
    public Developer(int id, string name, DateTime joinDate, string project, DeveloperType type)
    {
        Id = id;
        DeveloperName = name;
        JoinDate = joinDate;
        ProjectAssigned = project;
        Type = type;
    }

    // Method to calculate salary components for OnPayroll Developers
    public void CalculateSalary()
    {
        if (Type == DeveloperType.OnPayroll)
        {
            DA = 0.1m * NetSalary;
            HRA = 0.2m * NetSalary;
            LTA = 0.05m * NetSalary;
            PF = 0.12m * NetSalary;
        }
    }
}

class Program
{
    static List<Developer> developers = new List<Developer>();

    static void Main()
    {
        char ch;
        do
        {
            Console.WriteLine("1. Create Developer");
            Console.WriteLine("2. Display Details");
            Console.WriteLine("3. LINQ Queries");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateDeveloper();
                        break;

                    case 2:
                        DisplayDetails();
                        break;

                    case 3:
                        RunLINQQueries();
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Do you wish to run again? (y/n)");
            ch = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
        } while (ch == 'y');
    }

    static void CreateDeveloper()
    {
        Console.WriteLine("Enter Developer Details:");

        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Joining Date (yyyy-MM-dd): ");
        DateTime joinDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Project Assigned: ");
        string project = Console.ReadLine();

        Console.WriteLine("Select Developer Type:");
        Console.WriteLine("1. OnContract");
        Console.WriteLine("2. OnPayroll");
        Console.Write("Enter your choice: ");

        int typeChoice = int.Parse(Console.ReadLine());

        switch (typeChoice)
        {
            case 1:
                Console.Write("Duration (in days): ");
                int duration = int.Parse(Console.ReadLine());

                Console.Write("Charges Per Day: ");
                decimal charges = decimal.Parse(Console.ReadLine());

                developers.Add(new Developer(id, name, joinDate, project, DeveloperType.OnContract)
                {
                    Duration = duration,
                    ChargesPerDay = charges
                });
                break;

            case 2:
                Console.Write("Department: ");
                string dept = Console.ReadLine();

                Console.Write("Manager: ");
                string manager = Console.ReadLine();

                Console.Write("Net Salary: ");
                decimal netSalary = decimal.Parse(Console.ReadLine());

                Console.Write("Experience (in years): ");
                int exp = int.Parse(Console.ReadLine());

                Developer payrollDeveloper = new Developer(id, name, joinDate, project, DeveloperType.OnPayroll)
                {
                    Dept = dept,
                    Manager = manager,
                    NetSalary = netSalary,
                    Exp = exp
                };

                payrollDeveloper.CalculateSalary();
                developers.Add(payrollDeveloper);
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    static void DisplayDetails()
    {
        if (developers.Count == 0)
        {
            Console.WriteLine("No developers to display.");
            return;
        }

        Console.WriteLine("\nDeveloper Details:");

        foreach (var developer in developers)
        {
            Console.WriteLine($"\nId: {developer.Id}");
            Console.WriteLine($"Name: {developer.DeveloperName}");
            Console.WriteLine($"Joining Date: {developer.JoinDate:yyyy-MM-dd}");
            Console.WriteLine($"Project Assigned: {developer.ProjectAssigned}");

            if (developer.Type == DeveloperType.OnContract)
            {
                Console.WriteLine($"Type: OnContract");
                Console.WriteLine($"Duration: {developer.Duration} days");
                Console.WriteLine($"Charges Per Day: {developer.ChargesPerDay}");
            }
            else
            {
                Console.WriteLine($"Type: OnPayroll");
                Console.WriteLine($"Department: {developer.Dept}");
                Console.WriteLine($"Manager: {developer.Manager}");
                Console.WriteLine($"Net Salary: {developer.NetSalary}");
                Console.WriteLine($"Experience: {developer.Exp} years");
                Console.WriteLine($"DA: {developer.DA}");
                Console.WriteLine($"HRA: {developer.HRA}");
                Console.WriteLine($"LTA: {developer.LTA}");
                Console.WriteLine($"PF: {developer.PF}");
            }
        }
    }

    static void RunLINQQueries()
    {
        if (developers.Count == 0)
        {
            Console.WriteLine("No developers to perform LINQ queries.");
            return;
        }

        Console.WriteLine("\nLINQ Queries:");

        
        var allRecords = developers;
        DisplayQueryResult("All Records", allRecords);

        
        var highNetSalaryRecords = developers.Where(d => d.Type == DeveloperType.OnPayroll && d.NetSalary > 20000);
        DisplayQueryResult("Net Salary > 20000", highNetSalaryRecords);

        
        var nameContainsDRecords = developers.Where(d => d.DeveloperName.Contains("D", StringComparison.OrdinalIgnoreCase));
        DisplayQueryResult("DeveloperName contains 'D'", nameContainsDRecords);

       
        var dateRangeRecords = developers.Where(d => d.JoinDate >= new DateTime(2020, 01, 01) && d.JoinDate <= new DateTime(2022, 01, 01));
        DisplayQueryResult("Joining Date between 01/01/2020 and 01/01/2022", dateRangeRecords);

       
        var specificDateRecords = developers.Where(d => d.JoinDate == new DateTime(2022, 01, 12));
        DisplayQueryResult("Joining Date was 12 Jan 2022", specificDateRecords);
    }

    static void DisplayQueryResult(string queryName, IEnumerable<Developer> result)
    {
        Console.WriteLine($"\n{queryName}:");

        foreach (var developer in result)
        {
            Console.WriteLine($"\nId: {developer.Id}");
            Console.WriteLine($"Name: {developer.DeveloperName}");
            Console.WriteLine($"Joining Date: {developer.JoinDate:yyyy-MM-dd}");
            Console.WriteLine($"Project Assigned: {developer.ProjectAssigned}");

            if (developer.Type == DeveloperType.OnContract)
            {
                Console.WriteLine($"Type: OnContract");
                Console.WriteLine($"Duration: {developer.Duration} days");
                Console.WriteLine($"Charges Per Day: {developer.ChargesPerDay}");
            }
            else
            {
                Console.WriteLine($"Type: OnPayroll");
                Console.WriteLine($"Department: {developer.Dept}");
                Console.WriteLine($"Manager: {developer.Manager}");
                Console.WriteLine($"Net Salary: {developer.NetSalary}");
                Console.WriteLine($"Experience: {developer.Exp} years");
                Console.WriteLine($"DA: {developer.DA}");
                Console.WriteLine($"HRA: {developer.HRA}");
                Console.WriteLine($"LTA: {developer.LTA}");
                Console.WriteLine($"PF: {developer.PF}");
            }
        }
    }
}

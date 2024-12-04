using System;

public class Person
{

    public string Name { get; set; }
    public string Address { get; set; }
    public double Salary { get; set; }


    public Person(string name, string address, double salary)
    {
        Name = name;
        Address = address;
        Salary = salary;
    }


    public static Person InputPersonInfo()
    {
        string name, address;
        double salary;

        Console.Write("Enter name: ");
        name = Console.ReadLine();

        Console.Write("Enter address: ");
        address = Console.ReadLine();

        while (true)
        {
            try
            {
                Console.Write("Enter salary: ");
                string sSalary = Console.ReadLine();
                if (!double.TryParse(sSalary, out salary) || salary <= 0)
                {
                    throw new Exception("Salary must be a positive number.");
                }
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return new Person(name, address, salary);
    }


    public void Display()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Salary: {Salary}");
    }

 
    public static void Sort(Person[] p)
    {
        int n = p.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (p[j].Salary > p[j + 1].Salary)
                {
             
                    Person tmp = p[j];
                    p[j] = p[j + 1];
                    p[j + 1] = tmp;
                }
            }
        }
    }

public class Program
{
    public static void Main(string[] args)
    {
     
        Person[] persons = new Person[3];

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Enter information for person {i + 1}:");
            persons[i] = Person.InputPersonInfo();
        }

        Console.WriteLine("Information of Persons you have entered:");
        foreach (Person person in persons)
        {
            person.Display();
            Console.WriteLine("------------------------");
        }

      
        Person.Sort(persons);
        Console.WriteLine("Sorted Person information:");
        foreach (Person person in persons)
        {
            person.Display();
            Console.WriteLine("------------------------");
        }
    }
}

}

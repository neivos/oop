using System;
using System.Collections.Generic;

public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }

    public Item(string name, double price, double discount)
    {
        this.Name = name;
        this.Price = price;
        this.Discount = discount;
    }
}

public class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        this.Name = name;
    }
}

public class BillLine
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public BillLine(Item item, int quantity)
    {
        this.Item = item;
        this.Quantity = quantity;
    }

    public double GetLineTotal()
    {
        return Item.Price * Quantity;
    }

    public double GetLineDiscount()
    {
        return Item.Discount * Quantity;
    }
}

public class GroceryBill
{
    public Employee Clerk { get; private set; }
    protected List<BillLine> billLines;

    public GroceryBill(Employee clerk)
    {
        this.Clerk = clerk;
        billLines = new List<BillLine>();
    }

    public void Add(BillLine billLine)
    {
        billLines.Add(billLine);
    }

    public virtual double GetTotal()
    {
        double total = 0;
        foreach (var billLine in billLines)
        {
            total += billLine.GetLineTotal();
        }
        return total;
    }

    public virtual void PrintReceipt()
    {
        Console.WriteLine($"Clerk: {Clerk.Name}");
        Console.WriteLine("Items:");
        foreach (var billLine in billLines)
        {
            Console.WriteLine($"- {billLine.Item.Name} x {billLine.Quantity} @ {billLine.Item.Price:C} each");
        }
        Console.WriteLine($"Total: {GetTotal():C}");
    }
}

public class DiscountBill : GroceryBill
{
    private bool preferred;
    private int discountCount;
    private double discountAmount;

    public DiscountBill(Employee clerk, bool preferred) : base(clerk)
    {
        this.preferred = preferred;
    }

    public override double GetTotal()
    {
        double total = base.GetTotal();

        if (preferred)
        {
            discountCount = 0;
            discountAmount = 0;

            foreach (var billLine in billLines)
            {
                if (billLine.Item.Discount > 0)
                {
                    discountCount++;
                    discountAmount += billLine.GetLineDiscount();
                }
            }

            total -= discountAmount;
        }

        return total;
    }

    public override void PrintReceipt()
    {
        base.PrintReceipt();

        if (preferred)
        {
            Console.WriteLine($"Discounts: {discountAmount:C} ({GetDiscountPercent():P})");
        }
    }

    public int GetDiscountCount()
    {
        return discountCount;
    }

    public double GetDiscountAmount()
    {
        return discountAmount;
    }

    public double GetDiscountPercent()
    {
        double totalWithoutDiscount = base.GetTotal();
        return totalWithoutDiscount > 0 ? discountAmount / totalWithoutDiscount : 0;
    }
}

class Program
{
    static void Main()
    {

        Employee clerk = new Employee("John Doe");
        Item item1 = new Item("Candy Bar", 1.35, 0.25);
        Item item2 = new Item("Milk", 3.00, 0.50);
        Item item3 = new Item("Bread", 2.50, 0);
        GroceryBill bill = new GroceryBill(clerk);
        bill.Add(new BillLine(item1, 2)); 
        bill.Add(new BillLine(item2, 1)); 
        bill.Add(new BillLine(item3, 1)); 
        Console.WriteLine("Regular Bill:");
        bill.PrintReceipt();
        DiscountBill discountBill = new DiscountBill(clerk, true);
        discountBill.Add(new BillLine(item1, 2)); 
        discountBill.Add(new BillLine(item2, 1)); 
        discountBill.Add(new BillLine(item3, 1)); 
        Console.WriteLine("\nDiscount Bill:");
        discountBill.PrintReceipt();


        Console.WriteLine($"Discount Count: {discountBill.GetDiscountCount()}");
        Console.WriteLine($"Total Discount: {discountBill.GetDiscountAmount():C}");
        Console.WriteLine($"Discount Percent: {discountBill.GetDiscountPercent():P}");
    }
}

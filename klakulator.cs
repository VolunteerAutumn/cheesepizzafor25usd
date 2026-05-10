class Calculator
{
    private int a;
    private int b;

    public Calculator(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
    public int A
    {
        get { return a; }
        set { a = value; }
    }
    public int B
    {
        get { return b; }
        set { b = value; }
    }
    public int Add()
    {
        return a + b;
    }
    public int Sub()
    {         
        return a - b;
    }
    public int Mul()
    {
        return a * b;
    }
    public double Div()
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return (double)a / b;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the first number:");
        int num1, num2;
        try
        {
            num1 = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            return;
        }
        Console.WriteLine("Enter the second number:");
        try
        {
            num2 = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            return;
        }
        Console.WriteLine("Choose an operation: +, -, *, /");
        string operation = Console.ReadLine();
        
        Calculator calculator = new Calculator(num1, num2);
        try
        {
            switch (operation)
            {
                case "+":
                    Console.WriteLine($"Result: {calculator.Add()}");
                    break;
                case "-":
                    Console.WriteLine($"Result: {calculator.Sub()}");
                    break;
                case "*":
                    Console.WriteLine($"Result: {calculator.Mul()}");
                    break;
                case "/":
                    Console.WriteLine($"Result: {calculator.Div()}");
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

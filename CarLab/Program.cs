namespace CarLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grant Chirpus' Used Car Emporium!");
            Console.WriteLine();

            Car.Cars.Add(new Car("Nikolai", "Model S", 2017, 54999.90m));
            Car.Cars.Add(new Car("Fourd", "Escapade", 2017, 31999.90m));
            Car.Cars.Add(new Car("Chewie", "Vette", 2017, 44989.95m));
            Car.Cars.Add(new UsedCar("Hyonda", "Prior", 2015, 14795.50m, 35987.6));
            Car.Cars.Add(new UsedCar("GC", "Chirpus", 2013, 8500.00m, 3500.3));
            Car.Cars.Add(new UsedCar("GC", "Witherell", 2016, 14450.00m, 3500.3));

            Car.ListCars();
            
            bool goOn = true;
            while (goOn)
            {
                Console.WriteLine("Which car would you like?");
                int input = -1;
                bool invalid = true;
                while (invalid)
                {
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                        if (input >= 1 && input <= Car.Cars.Count)
                        {
                            invalid = false;
                            input--;
                        }
                        else if (input == -42)
                        {
                            Car.Admin();
                            Console.WriteLine();
                            Car.ListCars();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input: Out of Range, let's try that again");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input: Not a Number, let's try that again");
                    }
                }
                Console.WriteLine(Car.Cars[input]);
                Console.WriteLine("Excellent! Our finance department will be in touch shortly.");
                Console.WriteLine("Have a great day!");
                Car.Remove(input);
                Car.ListCars();
                goOn = Car.Continue();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLab
{
    public class Car
    {

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public static List<Car> Cars { get; set; } = new List<Car>();
        public static List<Car> SoldCars { get; set; } = new List<Car>();
        public Car(string Make, string Model, int Year, decimal Price)
        {
            this.Make = Make;
            this.Model = Model;
            this.Year = Year;
            this.Price = Price;
            
        }
        public override string ToString()
        {
            return String.Format("{0, -10} {1, -10} {2, -5} {3, 10}", $"{Make}", $"{Model}", $"{Year}", $"${Price}");
        }
        public static void ListCars()
        {
            for(int i = 1; i <= Cars.Count; i++)
            {
                Console.Write(i + ". ");
                Console.WriteLine(Cars[i-1]);
            }
        }
        public static void Remove(int num)
        {
            SoldCars.Add(Cars[num]);
            Cars.Remove(Cars[num]); 
        }
        public static void Admin()
        {
            Console.WriteLine("Would you like to use Admin mode? (y/n): ");
            string input1 = Console.ReadLine().Trim().ToLower();
            if (input1 == "y")
            {
                Console.WriteLine("Do you want to BuyBack, Add, Modify? (press anything else to stop)");
                string input2 = Console.ReadLine().Trim().ToLower();
                if (input2 == "buyback")
                {
                    BuyBack();
                }
                else if (input2 == "add")
                {
                    AddCar();
                }
                else if (input2 == "modify")
                {
                    Modify();
                }
            }
            else if (input1 == "n")
            {
                return;
            }
            else
            {
                Admin();
            }
        }
        public static void Modify()
        {
            Console.WriteLine("What Car do you want to Modify?: [1-"+Cars.Count+"]");
            int input1 = -1;
            bool goOn = true;
            while (true) 
            {
                try
                {
                    input1 = int.Parse(Console.ReadLine());
                    if (input1 >= 1 && input1 <= Cars.Count)
                    {
                        input1--;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, Out of Range");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input, not a number.");
                }
            }
            while (goOn)
            {
                Console.WriteLine("What do you want to modify? (Make, Model, Year, Price)");
                string input2 = Console.ReadLine().Trim().ToLower();
                if (input2 == "make")
                {
                    Console.WriteLine("Input car Make: ");
                    Cars[input1].Make = Console.ReadLine();
                }
                else if (input2 == "model")
                {
                    Console.WriteLine("Input car Model: ");
                    Cars[input1].Model = Console.ReadLine();
                }
                else if (input2 == "year")
                {
                    Console.WriteLine("Input the car's Year: ");
                    try
                    {
                        Cars[input1].Year = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Year");
                    }
                }
                else if (input2 == "price")
                {
                    Console.WriteLine("Input the car's Price: ");
                    try
                    {
                        decimal price = decimal.Parse(Console.ReadLine());
                        if (price >= 0)
                        {
                            Cars[input1].Price = price;
                        }
                        else
                        {
                            Console.WriteLine("Car can't be free");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Price");
                    }
                }
                goOn = Continue();
            }
            
        }
        public static void AddCar()
        {   
            Console.WriteLine("Input car Make: ");
            string input1 = Console.ReadLine();
            Console.WriteLine("Input car Model: ");
            string input2 = Console.ReadLine();
            int input3 = 0;
            while (true)
            {
                Console.WriteLine("Input the car's Year: ");
                try
                {
                    input3 = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Year");
                }
            }
            decimal input4 = 0;
            while (true)
            {
                Console.WriteLine("Input the car's Price: ");
                try
                {
                    input4 = decimal.Parse(Console.ReadLine());
                    if (input4 >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Car can't be free");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Price");
                }
            }
            double input5 = 0;
            while (true)
            {
                Console.WriteLine("Input the car's Mileage (put 0 if not used): ");
                try
                {
                    input5 = double.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Mileage");
                }
            }
            if (input5 <= 0)
            {
                Cars.Add(new Car(input1, input2, input3, input4));
            }
            else
            {
                Cars.Add(new UsedCar(input1, input2, input3, input4, input5));
            }
            
        }
        public static void BuyBack()
        {
            if (SoldCars.Count < 1)
            {
                Console.WriteLine("No cars avaliable");
            }
            else
            {
                Console.WriteLine("Select a car for BuyBack: ");
                for (int i = 1; i <= SoldCars.Count; i++)
                {
                    Console.Write(i + ". ");
                    Console.WriteLine(SoldCars[i - 1]);
                }
                int buyBackInput = -1;
                try
                {
                    buyBackInput = int.Parse(Console.ReadLine());
                    buyBackInput--;
                    if (buyBackInput >= 0 && buyBackInput <= SoldCars.Count - 1)
                    {

                        Console.WriteLine(SoldCars[buyBackInput] + " has been bought back");
                        Console.WriteLine("Edit mileage: ");
                        double mileage = -1;
                        try
                        {
                            mileage = double.Parse(Console.ReadLine());
                            Cars.Add(new UsedCar(SoldCars[buyBackInput].Make, SoldCars[buyBackInput].Model, SoldCars[buyBackInput].Year, SoldCars[buyBackInput].Price, mileage));
                            SoldCars.Remove(SoldCars[buyBackInput]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid mileage, retrying.");
                            BuyBack();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid car, retrying.");
                        BuyBack();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input, retrying.");
                    BuyBack();
                }
            }
        }
        public static bool Continue()
        {
            Console.WriteLine("Continue? (y/n)");
            string input = Console.ReadLine().Trim().ToLower();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                return Continue();
            }
        }
    }
}

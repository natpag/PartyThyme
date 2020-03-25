using System;
using System.Collections.Generic;
using System.Linq;

namespace PartyThyme
{
  class Program
  {
    static void Main(string[] args)
    {
      var db = new PlantsContext();
      var isRunning = true;
      while (isRunning)
      {
        Console.WriteLine("Welcome to your Garden!");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("(View - V) (Plant something - P) (Remove - R) (Water - W) (View thirsty plants - VT) (Location Summary - L) (Quit - Q)");

        var input = Console.ReadLine().ToLower();

        if (input == "v")
        {
          Console.WriteLine("Here are your current plants!");
          var plants = db.Plant.OrderBy(p => p.LocatedPlanted);
          foreach (var plant in plants)
          {
            Console.WriteLine($"\n{plant.Species} {plant.LocatedPlanted}");
          }
          Console.WriteLine("");
        }
        else if (input == "p")
        {
          Console.WriteLine("What would you like to plant?");
          var species = Console.ReadLine().ToLower();

          Console.WriteLine("Where would you like to plant it?");
          var locatedPlanted = Console.ReadLine().ToLower();

          Console.WriteLine("How many hours of sunlight are needed?");
          var lightNeeded = Console.ReadLine().ToLower();

          Console.WriteLine("How many ML of water are needed?");
          var waterNeeded = Console.ReadLine().ToLower();

          var plant = new Plants
          {
            Species = species,
            LocatedPlanted = locatedPlanted,
            LightNeeded = lightNeeded,
            WaterNeeded = waterNeeded,
          };
          db.Plant.Add(plant);
          db.SaveChanges();
        }
        else if (input == "r")
        {
          Console.WriteLine("\nWhat plant would you like to remove?");
          var plants = db.Plant.OrderBy(p => p.Species);
          foreach (var plant in plants)
          {
            Console.WriteLine($"{plant.Species}");
          }
          Console.WriteLine("");
          var userInput = Console.ReadLine();
          var plantToRemove = db.Plant.First(r => r.Species == userInput);
          db.Plant.Remove(plantToRemove);
          db.SaveChanges();
          Console.WriteLine("");
          Console.WriteLine("Your plant has been removed!");
          Console.WriteLine("");
        }
        else if (input == "w")
        {
          Console.WriteLine("\nWhat plant would you like to water?");
          var plants = db.Plant.OrderBy(p => p.Species);
          foreach (var plant in plants)
          {
            Console.WriteLine($"{plant.Species}");
            Console.WriteLine("");
          }
          var userInput = Console.ReadLine().ToLower();
          var plantToWater = db.Plant.First(p => p.Species == userInput);
          plantToWater.LastWateredDate = DateTime.Now;
          db.Plant.Update(plantToWater);

          db.SaveChanges();
          Console.WriteLine("");
          Console.WriteLine("Your plant has been watered!");
          Console.WriteLine("");
        }
        else if (input == "vt")
        {
          Console.WriteLine("These are your thirsty plants...");
          var plants = db.Plant.OrderBy(p => p.Species);
          var todaysDate = DateTime.Today.Date;
          var needToWater = db.Plant.Where(plant => (plant.LastWateredDate.Date != todaysDate));
          foreach (var plant in needToWater)
          {
            Console.WriteLine($"\n{plant.Species}");
          }
          Console.WriteLine("");
        }
        else if (input == "l")
        {
          Console.WriteLine("These are your plants with specific locations...");
          var plants = db.Plant.OrderBy(p => p.Species);
          var plantWithLocation = db.Plant.Where(plant => plant.LocatedPlanted != null && plant.LocatedPlanted != "");
          foreach (var plant in plantWithLocation)
          {
            Console.WriteLine($"\n{plant.Species}");
          }
        }
        else if (input == "q")
        {
          isRunning = false;
        }

      }

    }
  }
}

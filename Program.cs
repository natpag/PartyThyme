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

      Console.WriteLine("Welcome to your Garden!");

      bool leavingGarden = false;
      while (!leavingGarden)
      {

        Console.WriteLine("What would you like to do?");
        Console.WriteLine("(View - V) (Plant something - P) (Remove - R) (Water - W) (View thirsty plants - VT) (Location Summary - L) (Quit - Q)");

        var input = Console.ReadLine().ToLower();

        if (input == "v")
        {
          Console.WriteLine("Here are your current plants!");
          var viewPlants = db.Plant.OrderBy(pl => pl.LocatedPlanted);
          Console.WriteLine($"{viewPlants}");

        }
        if (input == "p")
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

        }
        else if (input == "q")
        {
          leavingGarden = true;
        }
      }


    }
  }
}

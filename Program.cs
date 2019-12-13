using System;
using System.Collections.Generic;
using System.Linq;
namespace JurassicPark
{
  class Program
  {

    static List<Dinosaur> CurrentDinosaurs = new List<Dinosaur>();
    static void DinoInventory()
    {
      CurrentDinosaurs.AddRange(new List<Dinosaur> {
        new Dinosaur {
        Name = "Velociraptor",
        DietType = "carnivore",
        DateAcquired = DateTime.Parse("12/2/1990"),
        Weight = 400,
        EnclosureNumber = 1
      },
        new Dinosaur {
        Name = "Brontosaurus",
        DietType = "herbivore",
        DateAcquired = DateTime.Parse("12/1/2018"),
        Weight = 300,
        EnclosureNumber = 2
      },
        new Dinosaur {
        Name = "T-Rex",
        DietType = "carnivore",
        DateAcquired = DateTime.Parse("12/2/2019"),
        Weight = 200,
        EnclosureNumber = 3
      },
        new Dinosaur {
        Name = "Triceratops",
        DietType = "herbivore",
        DateAcquired = DateTime.Parse("12/3/2000"),
        Weight = 100,
        EnclosureNumber = 4
      }
      });
    }




    static void AddDino()
    {
      Console.WriteLine("Enter Name of New Dinosaur");
      var dinoName = Console.ReadLine();
      Console.WriteLine("What is their diet type?");
      Console.WriteLine("Available commands are: carnivore, herbivore");
      var dietType = Console.ReadLine();
      Console.WriteLine("What is the Dinosaur's weight?");
      var dinoWeight = Console.ReadLine();
      Console.WriteLine("What enclosure number is the Dinosaur currently located?");
      var dinoEnclosureNumber = Console.ReadLine();


      var dino = new Dinosaur();
      dino.Name = dinoName;
      dino.DateAcquired = DateTime.Now;
      dino.DietType = dietType;
      dino.Weight = int.Parse(dinoWeight);
      dino.EnclosureNumber = int.Parse(dinoEnclosureNumber);

      CurrentDinosaurs.Add(dino);
    }

    static void ViewAll()
    {
      DisplayListOfDinosaurs(CurrentDinosaurs);
    }

    static void DisplayListOfDinosaurs(IEnumerable<Dinosaur> CurrentDinosaurs)
    {
      Console.WriteLine("Complete list of current Dinosaurs");
      Console.WriteLine("----------------");
      foreach (var dino in CurrentDinosaurs.OrderBy(dino => dino.DateAcquired))
      {
        Console.WriteLine($"We have a {dino.Name}, which is a {dino.DietType}.");
        Console.WriteLine($"We received {dino.Name}  on {dino.DateAcquired}. {dino.Name} weighs {dino.Weight}lbs and is in {dino.EnclosureNumber}");
      }
    }
    static void DeleteDino()
    {
      Console.WriteLine("What is the name of the dinosaur you want to remove?");
      var dinoName = Console.ReadLine();
      CurrentDinosaurs.RemoveAll(dino => dino.Name == dinoName);
    }

    static void ViewHeaviestDinos()
    {
      Console.WriteLine("Jurassic Park's top three dinosaurs by weight are:");
      Console.WriteLine("--------------------------------------------------");

      DisplayListOfDinosaurs(CurrentDinosaurs.OrderByDescending(dino => dino.Weight).Take(3));

      Console.WriteLine("--------------------------------------------------");
    }
    static void UnknownCommand()
    {
      Console.WriteLine("I don't understand that, try again");
    }
    static void DietSummary()
    {
      Console.WriteLine("Do you want the diet summary for carnivores or herbivores?");
      var dinoDietType = Console.ReadLine();
      var dinoDiet = CurrentDinosaurs.Count(dino => dino.DietType.ToLower() == dinoDietType.ToLower());
      Console.WriteLine($"There are {dinoDiet} {dinoDietType}");

    }

    static void UpdateDinoEnclosure()
    {
      Console.WriteLine("Who would you like to move?");
      var dinoName = Console.ReadLine();
      Console.WriteLine($"Where would you like to move {dinoName} to?");
      var dinoEnclosureNumber = Console.ReadLine();
      var moveDino = CurrentDinosaurs
        .FirstOrDefault(dino => dino.Name.ToLower() == dinoName.ToLower());
      moveDino.EnclosureNumber = int.Parse(dinoEnclosureNumber);
    }

    static void QuitProgramMessage()
    {
      Console.WriteLine("Good Bye!");
    }


    static void Main(string[] args)
    {
      DinoInventory();
      Console.WriteLine("Welcome to Jurassic PArk");
      var input = "";
      while (input != "quit")
      {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("Available commands are: view, add, remove, transfer, heaviest, diet, quit");
        input = Console.ReadLine().ToLower();

        if (input == "add")
        {
          AddDino();
        }
        else if (input == "remove")
        {
          DeleteDino();
        }
        else if (input == "view")
        {
          ViewAll();
        }
        else if (input == "transfer")
        {
          UpdateDinoEnclosure();
        }
        else if (input == "diet")
        {
          DietSummary();
        }
        else if (input == "heaviest")
        {
          ViewHeaviestDinos();
        }
        else
        {
          UnknownCommand();
        }
      }
    }
  }
}

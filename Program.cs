using System;
using System.Collections.Generic;
using System.Linq;
namespace JurassicPark
{
  class Program
  {

    // static List<Dinosaur> CurrentDinosaurs = new List<Dinosaur>();
    static DatabaseContext Db = new DatabaseContext();
    static void DinoInventory()
    {
      Db.Dinosaurs.AddRange(new List<Dinosaur> {
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

      // CurrentDinosaurs.Add(dino);
      Db.Dinosaurs.Add(dino);
      Db.SaveChanges();
    }

    static void ViewAll()
    {
      DisplayListOfDinosaurs(Db.Dinosaurs);
    }

    static void DisplayListOfDinosaurs(IEnumerable<Dinosaur> CurrentDinosaurs)
    {
      Console.WriteLine("Complete list of current Dinosaurs");
      Console.WriteLine("----------------");
      foreach (var dino in CurrentDinosaurs.OrderBy(dino => dino.DateAcquired))
      {
        Console.WriteLine($"id:{dino.Id} -We have a {dino.Name}, which is a {dino.DietType}.");
        Console.WriteLine($"We received {dino.Name}  on {dino.DateAcquired}. {dino.Name} weighs {dino.Weight}lbs and is in {dino.EnclosureNumber}");
      }
    }

    static void HatchDino()
    {
      string[] names = { "Bridgette", "Wonda", "Roderick", "Ginny", "Saundra", "Sook", "Dick", "Mari", "Sparkle", "Chara", "Ericka", "Waldo", "Nieves", "Gertrudis", "Verla", "Donte", "Gregorio", "Olivia", "Breann", "Sung", "Salley", "Markita", "Vonnie", "Jason", "Ona", "Mimi", "Delmar", "Mariana", "Pearle", "Amira", "Dorine", "Mitzie", "Leslee", "Prudence", "Tennie", "Fabiola", "Janna", "Doreen", "Luther", "Su", "Johana", "Willodean", "Werner", "Rosalina", "Paula", "Nicole", "Allena", "Natasha", "Nakita", "Jeff" };
      string[] diet = { "carnivore", "herbivore" };
      Console.WriteLine("It's hatching");

      var dino = new Dinosaur();
      Random random = new Random();
      dino.Name = names[random.Next(0, 50)];
      dino.DietType = diet[random.Next(0, 2)];
      dino.DateAcquired = DateTime.Now;
      dino.Weight = random.Next(0, 1000);
      dino.EnclosureNumber = 3;


      Console.WriteLine($"Your new dinosaur is named {dino.Name}, weighs {dino.Weight}lbs, and is a {dino.DietType}!");

      Db.Dinosaurs.Add(dino);
      Db.SaveChanges();

    }



    static void ViewHeaviestDinos()
    {
      Console.WriteLine("Jurassic Park's top three dinosaurs by weight are:");
      Console.WriteLine("--------------------------------------------------");

      DisplayListOfDinosaurs(Db.Dinosaurs.OrderByDescending(dino => dino.Weight).Take(3));

      Console.WriteLine("--------------------------------------------------");
    }

    static void NeedsASheep()
    {
      Console.WriteLine("Jurassic Park's lightest three dinosaurs by weight are:");
      Console.WriteLine("--------------------------------------------------");
      DisplayListOfDinosaurs(Db.Dinosaurs.OrderBy(dino => dino.DietType).ThenBy(dino => dino.Weight).Take(3));
      Db.SaveChanges();
    }
    static void UnknownCommand()
    {
      Console.WriteLine("I don't understand that, try again");
    }
    static void DietSummary()
    {
      Console.WriteLine("Do you want the diet summary for carnivores or herbivores?");
      var dinoDietType = Console.ReadLine();
      var dinoDiet = Db.Dinosaurs.Count(dino => dino.DietType.ToLower() == dinoDietType.ToLower());
      Console.WriteLine($"There are {dinoDiet} {dinoDietType}");

    }

    static void UpdateDinoEnclosure()
    {
      Console.WriteLine("Who would you like to move?");
      var dinoName = Console.ReadLine();
      Console.WriteLine($"Where would you like to move {dinoName} to?");
      var dinoEnclosureNumber = Console.ReadLine();
      var moveDino = Db.Dinosaurs
        .FirstOrDefault(dino => dino.Name.ToLower() == dinoName.ToLower());
      moveDino.EnclosureNumber = int.Parse(dinoEnclosureNumber);
      Db.SaveChanges();
    }

    static void ReleaseDinoEnclosure()
    {
      Console.WriteLine("What enclosure number would you like to reset?");
      var enclosureNumber = Console.ReadLine();
      var dinosaurToReset = Db.Dinosaurs.FirstOrDefault(dino => dino.EnclosureNumber == int.Parse(enclosureNumber));
      dinosaurToReset.EnclosureNumber = 0;
      Db.SaveChanges();

    }
    static void DeleteDino()
    {
      Console.WriteLine("What is the name of the dinosaur you want to remove?");
      var dinoName = Console.ReadLine();
      // Db.Dinosaurs.RemoveAll(dino => dino.Name == dinoName);
      var dino = Db.Dinosaurs.Where(d => d.Name == dinoName).First();
      Db.Dinosaurs.Remove(dino);
      Db.SaveChanges();
    }
    static void QuitProgramMessage()
    {
      Console.WriteLine("Good Bye!");
    }

    static void SearchForDino()
    {
      Console.WriteLine("What are you searhing for?");
      var searchTerm = Console.ReadLine();
      // Search our list????
      // var db = new DatabaseContext();

      var results = Db.Dinosaurs
            .Where(creature =>
                creature.Name.ToLower()
                    .Contains(searchTerm.ToLower()));

      DisplayListOfDinosaurs(results);
    }






    static void Main(string[] args)
    {
      DinoInventory();
      Console.WriteLine("Welcome to Jurassic Park");
      var input = "";
      while (input != "quit")
      {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("Available commands are: view, add, remove, reset, transfer, heaviest, sheep,  hatch, diet, quit");
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
        else if (input == "reset")
        {
          ReleaseDinoEnclosure();
        }
        else if (input == "sheep")
        {
          NeedsASheep();
        }
        else if (input == "hatch")
        {
          HatchDino();
        }
        else
        {
          UnknownCommand();
        }
      }
    }
  }
}

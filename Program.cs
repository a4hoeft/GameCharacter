using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "json/mario.json";
List<Mario> marios = new List<Mario>();
if (File.Exists(marioFileName))
{
    try
    {
        marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
        logger.Info($"File deserialized {marioFileName}");
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error deserializing file");
        Console.WriteLine("Error deserializing file");
        return;
    }
}

// deserialize dk json from file into List<DonkeyKong>
string dkFileName = "json/dk.json";
List<DonkeyKong> donkyKongs = new List<DonkeyKong>();
if (File.Exists(dkFileName))
{
    try
    {
        donkyKongs = JsonSerializer.Deserialize<List<DonkeyKong>>(File.ReadAllText(dkFileName))!;
        logger.Info($"File deserialized {dkFileName}");
    }
    catch (Exception ex)
    {
        logger.Error(ex, $"Error deserializing {dkFileName}");
        Console.WriteLine("Error deserializing file");
        return;
    }
}
else
{
    logger.Info($"File not found {dkFileName}");
}

// deserialize street fighter II json from file into List<StreetFighterII>
string sf2FileName = "json/sf2.json";
List<StreetFighterII> streetFighterIIs = new List<StreetFighterII>();
if (File.Exists(sf2FileName))
{
    try
    {
        streetFighterIIs = JsonSerializer.Deserialize<List<StreetFighterII>>(File.ReadAllText(sf2FileName))!;
        logger.Info($"File deserialized {sf2FileName}");
    }
    catch (Exception ex)
    {
        logger.Error(ex, $"Error deserializing {sf2FileName}");
        Console.WriteLine("Error deserializing file");
        return;
    }
}
else
{
    logger.Info($"File not found {sf2FileName}");
}

do
{
    // display choices to user
    Console.WriteLine("1) Display Mario Characters");
    Console.WriteLine("2) Add Mario Character");
    Console.WriteLine("3) Remove Mario Character");
    Console.WriteLine("4) Display Donkey Kong Characters");
    Console.WriteLine("5) Add Donkey Kong Character");
    Console.WriteLine("6) Remove Donkey Kong Character");
    Console.WriteLine("7) Display Street Fighter II Characters");
    Console.WriteLine("8) Add Street Fighter II Character");
    Console.WriteLine("9) Remove Street Fighter II Character");
    Console.WriteLine("Enter to quit");

    // input selection from user
    string? choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
        // Display Mario Characters
        foreach (var c in marios)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "2")
    {
        // Add Mario Character
        // Generate unique Id
        Mario mario = new()
        {
            Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
        };
        InputCharacter(mario);
        // Add Character
        marios.Add(mario);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character added: {mario.Name}");
    }
    else if (choice == "3")
    {
        // Remove Mario Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            Mario? character = marios.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                marios.Remove(character);
                // serialize list<marioCharacter> into json file
                File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
                logger.Info($"Character Id {Id} removed");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (choice == "4")
    {
        // Display Donkey Kong Characters
        foreach (var c in donkyKongs)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "5")
    {
        // Add Donkey Kong Character
        // Generate unique Id
        DonkeyKong donkyKong = new()
        {
            Id = donkyKongs.Count == 0 ? 1 : donkyKongs.Max(c => c.Id) + 1
        };
        InputCharacter(donkyKong);
        // Add Character
        donkyKongs.Add(donkyKong);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(donkyKongs));
        logger.Info($"Character added: {donkyKong.Name}");
    }
    else if (choice == "6")
    {
        // Remove Donkey Kong Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            DonkeyKong? character = donkyKongs.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                donkyKongs.Remove(character);
                // serialize list<DonkeyKong> into json file
                File.WriteAllText(dkFileName, JsonSerializer.Serialize(donkyKongs));
                logger.Info($"Character Id {Id} removed");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (choice == "7")
    {
        // Display Street Fighter II Characters
        foreach (var c in streetFighterIIs)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "8")
    {
        // Add Street Fighter II Character
        // Generate unique Id
        StreetFighterII streetFighterII = new()
        {
            Id = streetFighterIIs.Count == 0 ? 1 : streetFighterIIs.Max(c => c.Id) + 1
        };
        InputCharacter(streetFighterII);
        // Add Character
        streetFighterIIs.Add(streetFighterII);
        File.WriteAllText(sf2FileName, JsonSerializer.Serialize(streetFighterIIs));
        logger.Info($"Character added: {streetFighterII.Name}");
    }
    else if (choice == "9")
    {
        // Remove Street Fighter II Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            StreetFighterII? character = streetFighterIIs.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                streetFighterIIs.Remove(character);
                // serialize list<StreetFighterII> into json file
                File.WriteAllText(sf2FileName, JsonSerializer.Serialize(streetFighterIIs));
                logger.Info($"Character Id {Id} removed");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (string.IsNullOrEmpty(choice))
    {
        break;
    }
    else
    {
        logger.Info("Invalid choice");
    }
} while (true);

logger.Info("Program ended");

static void InputCharacter(Character character)
{
    Type type = character.GetType();
    PropertyInfo[] properties = type.GetProperties();
    var props = properties.Where(p => p.Name != "Id");
    foreach (PropertyInfo prop in props)
    {
        if (prop.PropertyType == typeof(string))
        {
            Console.WriteLine($"Enter {prop.Name}:");
            prop.SetValue(character, Console.ReadLine());
        }
        else if (prop.PropertyType == typeof(List<string>))
        {
            List<string> list = new List<string>();
            do
            {
                Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
                string response = Console.ReadLine()!;
                if (string.IsNullOrEmpty(response))
                {
                    break;
                }
                list.Add(response);
            } while (true);
            prop.SetValue(character, list);
        }
    }
}
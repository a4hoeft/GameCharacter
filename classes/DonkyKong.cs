public class DonkeyKong : Character
{
    public string Species { get; set; } = string.Empty;

    public override string Display()
    {
        return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {Species}\n";
    }
}
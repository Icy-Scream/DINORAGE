public interface IFood
{
    public string Name { get; }
    public string Description { get; set; }
    public float Fat { get; }
    public int Happiness { get; }
    
    public int HungerPoints { get; }
}

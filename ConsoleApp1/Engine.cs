namespace ConsoleApp1;

public class Engine
{
    public string Make { get; set; }
    public int CylinderCount { get; set; }
    public double Volume { get; set; }
    public EngineShape Shape { get; set; }

    public Engine(string make, int cylinderCount, double volume, EngineShape shape)
    {
        Make = make;
        CylinderCount = cylinderCount;
        Volume = volume;
        Shape = shape;
    }

    public Engine()
    {
        Make = "";
        CylinderCount = -1;
        Volume = -1;
        Shape = EngineShape.None;
    }
    
    public override string ToString()
    {
        return Make + " " + Volume + "L " + Shape + "-" + CylinderCount;
    }
}
using Event = Unity.Services.Analytics.Event;

public class AnalyticsMaxDistanceMade : Event
{
    public AnalyticsMaxDistanceMade() : base("MaxDistanceMade")
    {
        
    }
    
    public int MaxDistance { set { SetParameter("MaxDistance", value); } }
    public string SpaceShip_Name { set { SetParameter("SpaceShip_Name", value); } }
}

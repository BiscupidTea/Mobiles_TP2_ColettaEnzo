using Event = Unity.Services.Analytics.Event;

public class AnalyticPurchaseEvent : Event
{
    public AnalyticPurchaseEvent() : base("PurchaseSpaceShip")
    {
        
    }
    public string SpaceShip_Name { set { SetParameter("SpaceShip_Name", value); } }
    public int SpaceShip_Value { set { SetParameter("SpaceShip_Value", value); } }
}

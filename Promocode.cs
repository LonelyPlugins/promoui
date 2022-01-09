using System.Collections.Generic;

namespace Promocode
{
  public class Promocode
  {
    public string Name;
    public int Times;
    public bool HaveExperience;
    public uint Experience;
    public bool HaveVehicles;
    public uint VehicleID;
    public bool HaveItems;
    public List<ItemData> Items;
    public List<string> SteamIDS;
  }
}

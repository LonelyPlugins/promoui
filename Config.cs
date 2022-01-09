using Rocket.API;
using System.Collections.Generic;

namespace Promocode
{
  public class Config : IRocketPluginConfiguration, IDefaultable
  {
    public ushort EffectID;
    public short LayerID;
    public List<Promocode> promocodes;

    public void LoadDefaults()
    {
      this.EffectID = (ushort) 12312;
      this.LayerID = (short) 110;
      this.promocodes = new List<Promocode>();
      this.promocodes.Add(new Promocode()
      {
        Experience = 100U,
        Times = 10,
        SteamIDS = new List<string>(),
        HaveExperience = true,
        HaveItems = true,
        HaveVehicles = true,
        Items = new List<ItemData>()
        {
          new ItemData() { Amount = 1, ItemID = 15U }
        },
        Name = "test",
        VehicleID = 1U
      });
    }
  }
}

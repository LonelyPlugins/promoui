// Decompiled with JetBrains decompiler
// Type: Promocode.Config
// Assembly: Promocode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 084AF92B-0413-4443-9B63-8F15A32B5F70
// Assembly location: C:\Users\Александр\Desktop\Promocode.dll

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

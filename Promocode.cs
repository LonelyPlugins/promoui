// Decompiled with JetBrains decompiler
// Type: Promocode.Promocode
// Assembly: Promocode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 084AF92B-0413-4443-9B63-8F15A32B5F70
// Assembly location: C:\Users\Александр\Desktop\Promocode.dll

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

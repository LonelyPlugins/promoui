// Decompiled with JetBrains decompiler
// Type: Promocode.Plugin
// Assembly: Promocode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 084AF92B-0413-4443-9B63-8F15A32B5F70
// Assembly location: C:\Users\Александр\Desktop\Promocode.dll

using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Promocode
{
  public class Plugin : RocketPlugin<Config>
  {
        
    public static Plugin Instance;
    public List<UIData> UIDatas;

        protected override void Load()
        {
            Plugin.Instance = this;
            EffectManager.onEffectTextCommitted = (EffectManager.EffectTextCommittedHandler)Delegate.Combine(EffectManager.onEffectTextCommitted, new EffectManager.EffectTextCommittedHandler(this.OnText));
            EffectManager.onEffectButtonClicked = (EffectManager.EffectButtonClickedHandler)Delegate.Combine(EffectManager.onEffectButtonClicked, new EffectManager.EffectButtonClickedHandler(this.OnButton));
            this.UIDatas = new List<UIData>();
            base.Load();
            Rocket.Core.Logging.Logger.Log("####################################", color: ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#   Thank you for buying baby    #", color: ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#    Plugin Created By Kirichek    #", color: ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#       Plugin Version: 1.0.0        #", color: ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("####################################", color: ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("");
            Rocket.Core.Logging.Logger.Log("PoromoUI is successfully loaded!", color: ConsoleColor.Green);
        }


        private void OnText(Player uplayer, string buttonname, string text)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(uplayer);
            if (buttonname == "Promocode")
            {
                this.UIDatas.Find((UIData p) => p.player.CSteamID == player.CSteamID).Promocode = text;
            }
        }

        private void OnButton(Player uplayer, string buttonname)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(uplayer);
            if (buttonname == "CloseCode")
            {
                UIData item = this.UIDatas.Find((UIData p) => p.player.CSteamID == player.CSteamID);
                this.UIDatas.Remove(item);
                EffectManager.askEffectClearByID(Plugin.Instance.Configuration.Instance.EffectID, player.CSteamID);
                player.Player.setPluginWidgetFlag((EPluginWidgetFlags)1, false);
            }
            if (buttonname == "ActivateCode")
            {
                UIData uidata = this.UIDatas.Find((UIData p) => p.player.CSteamID == player.CSteamID);
                if (!this.IsCodeExist(uidata.Promocode))
                {
                    EffectManager.sendUIEffectText(Plugin.Instance.Configuration.Instance.LayerID, player.CSteamID, true, "TipText", "Промокод не существует");
                    return;
                }
                if (this.IsPlayerAlreadyActivated(player, uidata.Promocode))
                {
                    EffectManager.sendUIEffectText(Plugin.Instance.Configuration.Instance.LayerID, player.CSteamID, true, "TipText", "Вы уже использовали этот промокод");
                    return;
                }
                Promocode promocodeByName = this.GetPromocodeByName(uidata.Promocode);
                if (promocodeByName.Times == 0)
                {
                    EffectManager.sendUIEffectText(Plugin.Instance.Configuration.Instance.LayerID, player.CSteamID, true, "TipText", "Промокод уже нельзя использовать");
                    return;
                }
                EffectManager.sendUIEffectText(Plugin.Instance.Configuration.Instance.LayerID, player.CSteamID, true, "TipText", "Промокод активирован");
                promocodeByName.SteamIDS.Add(player.CSteamID.ToString());
                promocodeByName.Times--;
                this.GiveItems(this.GetPromocodeByName(uidata.Promocode), player);
                Plugin.Instance.Configuration.Save();
            }
        }

        private void GiveItems(Promocode code, UnturnedPlayer player)
        {
            if (code.HaveExperience)
            {
                player.Experience += code.Experience;
            }
            if (code.HaveVehicles)
            {
                player.GiveVehicle((ushort)code.VehicleID);
            }
            if (code.HaveItems)
            {
                foreach (ItemData itemData in code.Items)
                {
                    player.GiveItem((ushort)itemData.ItemID, (byte)itemData.Amount);
                }
            }
        }

        private bool IsPlayerAlreadyActivated(UnturnedPlayer player, string codetext)
    {
      foreach (string str in this.GetPromocodeByName(codetext).SteamIDS)
      {
        if (str == player.CSteamID.ToString())
          return true;
      }
      return false;
    }

    private bool IsCodeExist(string code)
    {
      foreach (Promocode promocode in Plugin.Instance.Configuration.Instance.promocodes)
      {
        if (promocode.Name == code)
          return true;
      }
      return false;
    }

    private Promocode GetPromocodeByName(string code)
    {
      foreach (Promocode promocode in Plugin.Instance.Configuration.Instance.promocodes)
      {
        if (promocode.Name == code)
          return promocode;
      }
      return (Promocode) null;
    }

    protected override void Unload() => Plugin.Instance.Configuration.Save();
  }
}

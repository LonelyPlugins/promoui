using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;

namespace Promocode
{
  internal class Commands : IRocketCommand
  {
    public AllowedCaller AllowedCaller => (AllowedCaller) 1;

    public string Name => "promo";

    public string Help => "";

    public string Syntax => "";

    public List<string> Aliases => new List<string>()
    {
      "promo"
    };

    public List<string> Permissions => new List<string>()
    {
      "lonely.promo"
    };

    public void Execute(IRocketPlayer caller, string[] command)
    {
      UnturnedPlayer unturnedPlayer = (UnturnedPlayer) caller;
      EffectManager.sendUIEffect(Plugin.Instance.Configuration.Instance.EffectID, Plugin.Instance.Configuration.Instance.LayerID, unturnedPlayer.CSteamID, true);
      unturnedPlayer.Player.setPluginWidgetFlag((EPluginWidgetFlags) 1, true);
      Plugin.Instance.UIDatas.Add(new UIData()
      {
        Promocode = "",
        player = unturnedPlayer
      });
    }
  }
}

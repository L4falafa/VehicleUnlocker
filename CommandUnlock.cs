using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.VehicleUnlock
{
    class CommandUnlock : IRocketCommand
    {
        public AllowedCaller AllowedCaller => throw new NotImplementedException();

        public string Name => "unlock";

        public string Help => "Unlock all vehicles from a player";

        public string Syntax => "/unlock [Player Name]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { VehicleUnlocker.instance.Configuration.Instance.CommandPermission };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            UnturnedPlayer victim = UnturnedPlayer.FromName(command[0]);
            if (command.Length != 1)
            {
                ChatManager.serverSendMessage(string.Format(Syntax), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
                return;
            }
            if (victim == null)
            {
                ChatManager.serverSendMessage(string.Format(VehicleUnlocker.instance.Translations.Instance.Translate("not_found").Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
      
                return;
            }
            if (victim.HasPermission(VehicleUnlocker.instance.Configuration.Instance.ByPassPermission))
            {
                ChatManager.serverSendMessage(string.Format(VehicleUnlocker.instance.Translations.Instance.Translate("bypass").Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
                return;
            }
            int count = 0;
            foreach (InteractableVehicle vehicle in VehicleManager.vehicles)
            {


                if (vehicle.isLocked)
                {
                    if (vehicle.lockedOwner == victim.CSteamID)
                    {
                        count++;
                        VehicleManager.instance.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID, player.CSteamID, player.SteamGroupID, false);

                    }
                }


            }
            if (count == 0)
            {
                ChatManager.serverSendMessage(string.Format(VehicleUnlocker.instance.Translations.Instance.Translate("not_have_vehicles").Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
            }
            else {

                ChatManager.serverSendMessage(string.Format(VehicleUnlocker.instance.Translations.Instance.Translate("successful_unlocked",count,victim.CSteamID.m_SteamID).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME,VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
                ChatManager.serverSendMessage(string.Format(VehicleUnlocker.instance.Translations.Instance.Translate("victim_message_unlocked", player.CSteamID.m_SteamID).Replace('(', '<').Replace(')', '>')), Color.white, null, victim.SteamPlayer(), EChatMode.WELCOME, VehicleUnlocker.instance.Configuration.Instance.ImageUrl, true);
            }
       
        }
    }
}

using Rocket.API.Collections;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lafalafa.VehicleUnlock
{
    public class VehicleUnlocker : RocketPlugin<VehicleUnlockerConfiguration>
    {
        public static VehicleUnlocker instance;
        protected override void Load()
        {

            instance = this;    

        }


        public override TranslationList DefaultTranslations =>
          new TranslationList
          {
                { "bypass", "Can´t unlock vehicles from player because him have a bypass" },
                { "successful_unlocked", "You unlocked (color=red){0}(/color) vehicles from {1}" },
                { "not_found", "Can´t found the player" },
                { "not_have_vehicles", "The player don´t have any vehicle" },
                { "victim_message_unlocked", "All your vehicles were unlocked by (color=cyan){0}(/color)" }
          };

    }
}

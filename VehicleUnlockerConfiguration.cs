using Rocket.API;

namespace Lafalafa.VehicleUnlock
{
    public class VehicleUnlockerConfiguration : IRocketPluginConfiguration
    {

        public string ByPassPermission;
        public string CommandPermission;
        public string ImageUrl;
        public void LoadDefaults()
        {
            CommandPermission = "vehicleunlocker";
            ByPassPermission = "vehicleunlocker.bypass";
            ImageUrl = "https://cdn.discordapp.com/attachments/661993286046711808/765199895475912714/clave.png";
        }
    }
}
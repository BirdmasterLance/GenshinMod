using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;

namespace GenshinMod.Invasions
{
    internal class DomainWorld : ModSystem
    {
        // A check that lets us know if there is a domain active in the world
        public static bool domainActive = false;

        public override void Load()
        {
            domainActive = false;
        }

        public override void PostUpdateWorld()
        {
            if(domainActive)
            {
                DomainInvasion.CheckDomainProgress();
                if(Main.invasionProgress >= Main.invasionSize)
                {
                    Main.invasionProgress = Main.invasionSize;
                    NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral("Domain End"), 255, 175f, 75f, 255f, 0, 0, 0);
                    DomainInvasion.EndDomain();
                }
            }
        }
    }
}

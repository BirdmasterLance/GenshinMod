using System;
using GenshinMod.Invasions;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
    public class DomainStarter : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FragmentStardust;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Starts a domain");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1;
            Item.maxStack = 99;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 1;
            Item.consumable = false;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 3;
        }

        public override bool? UseItem(Player player)
        {
            if (!DomainWorld.domainActive)
            {
                DomainInvasion.StartDomain(DomainType.TestDomain, DomainReward.TestReward, player.position);
                return true;
            }
            else
            {
                Main.NewText("ending domain", new Color(175, 75, 255));
                DomainInvasion.EndDomain();
                return true;
            }
        }
    }
}
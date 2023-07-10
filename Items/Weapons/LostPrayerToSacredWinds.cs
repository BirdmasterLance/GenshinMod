using System;
using GenshinMod.Invasions;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace GenshinMod.Items.Weapons
{
    internal class LostPrayerToSacredWinds : ModItem
    {

        public int timer;
        public int level = 0;

        public override string Texture => "Terraria/Images/Item_" + ItemID.RazorbladeTyphoon;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases Movement SPD by 10%. When in battle, gain an 8% Elemental DMG Bonus every 4s. Max 4 stacks. Lasts until the character falls or leaves combat.");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1;
            Item.maxStack = 99;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = 1;
            Item.consumable = false;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Quest;

            // Testing values
            Item.damage = 50;
            Item.defense = 50;
            Item.healLife = 50;
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<Characters.Yanfei.YanfeiProjectile>(), 7, 29, false);
        }

        public override void HoldItem(Player player)
        {
            player.moveSpeed += player.moveSpeed * 0.1f;
            if (timer++ % 60 == 0)
            {
                if(level > 4) level++;
            }

            player.GetDamage(DamageClass.Magic) += 0.08f * level;
            player.GetCritChance(DamageClass.Magic) += 7.2f;
        } 
    }
}

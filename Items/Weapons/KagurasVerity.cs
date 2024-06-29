using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items.Weapons
{
    internal class KagurasVerity : Weapon
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.CrystalStorm;

        public override void SetDefaults()
        {

            // Set up Genshin related information for this weapon
            base.weaponSet = WeaponSet.KagurasVerity;
            base.weaponType = WeaponType.Catalyst;
            base.mainStat = ItemStats.CritDmg;
            base.mainStatValue = 14.4;

            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.maxStack = 1;

            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            //Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

        }
    }
}

using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;

namespace GenshinMod.Items
{
    internal class WeaponEXP : GlobalItem
    {
        public int experience;
        public int ascensionLevel;
        public int refinementLevel;
        public int level;
        // TODO: make some kind of list or dictionary that has all the exp to level conversion

        // TODO: make a list of our Genshin weapons so that this class only affects them
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.whoAmI == ModContent.ItemType<YanfeiAttacks>();
        }

        public override bool InstancePerEntity => true;

        public override void LoadData(Item item, TagCompound tag)
        {
            experience = tag.Get<int>("experience");
            ascensionLevel = tag.Get<int>("ascension");
            refinementLevel = tag.Get<int>("refinement");
            level = WeaponEXPValues.FourStarExpToLevel(experience);
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag["experience"] = experience;
            tag["ascension"] = ascensionLevel;
            tag["refinement"] = refinementLevel;
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(experience);
            writer.Write(ascensionLevel);
            writer.Write(refinementLevel);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            refinementLevel = reader.ReadInt32();
            ascensionLevel = reader.ReadInt32();
            experience = reader.ReadInt32();
            level = WeaponEXPValues.FourStarExpToLevel(experience);
        }

        // TODO: We have to figure out how much each level affects the weapon's damage by
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            // TODO: weapon substats?
            damage += (float) (0.13 * level);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "level", $"Level: {level}") { OverrideColor = Color.LightGreen });
            string levelString = $" ({WeaponEXPValues.FourStarExpToNextLevel(experience)} to next level)";
            tooltips.Add(new TooltipLine(Mod, "experience", $"Experience: {experience}{levelString}") { OverrideColor = Color.White });
        }
    }
}

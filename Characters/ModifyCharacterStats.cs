using System;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Characters
{
    // Static class so we can call methods from anywhere
    static class ModifyCharacterStats
    {
        // We're going to use default item attributes as substitures for Genshin stats
        // Item.defense = character defense
        // Item.damage = character elemental AND physical damage bc we havent differentiated between them yet
        // Item.crit = character crit chance
        // Item.healLife = character hp
        // Item.healMana = healing bonus
        // Item.stringColor = elemental mastery
        // Values still needed: crit damage, energy recharge
        public static void AdjustCharacterStats(Character character)
        {
            int lifeModifier = 0;
            int damageModifier = 0;
            int defenseModifier = 0;
            int critModifier = 0;
            int critDmgModifier = 0;
            int elementalMasteryModifier = 0;
            int energyRechargeModifier = 0;
            int healingBonusModifier = 0;
            if (character.weapon != null)
            {
                lifeModifier += character.weapon.healLife;
                damageModifier += character.baseDamage + character.weapon.damage;
                defenseModifier += character.weapon.defense;
                critModifier += character.weapon.crit;
                elementalMasteryModifier += character.weapon.stringColor;
                healingBonusModifier += character.weapon.healMana;
            }
            if (character.artifact1 != null)
            {
                lifeModifier += character.artifact1.healLife;
                damageModifier += character.baseDamage + character.artifact1.damage;
                defenseModifier += character.artifact1.defense;
                critModifier += character.artifact1.crit;
                elementalMasteryModifier += character.artifact1.stringColor;
                healingBonusModifier += character.artifact1.healMana;
            }
            if (character.artifact2 != null)
            {
                lifeModifier += character.artifact2.healLife;
                damageModifier += character.baseDamage + character.artifact2.damage;
                defenseModifier += character.artifact2.defense;
                critModifier += character.artifact2.crit;
                elementalMasteryModifier += character.artifact2.stringColor;
                healingBonusModifier += character.artifact2.healMana;
            }
            if (character.artifact3 != null)
            {
                lifeModifier += character.artifact3.healLife;
                damageModifier += character.baseDamage + character.artifact3.damage;
                defenseModifier += character.artifact3.defense;
                critModifier += character.artifact3.crit;
                elementalMasteryModifier += character.artifact3.stringColor;
                healingBonusModifier += character.artifact3.healMana;
            }
            if (character.artifact4 != null)
            {
                lifeModifier += character.artifact4.healLife;
                damageModifier += character.baseDamage + character.artifact4.damage;
                defenseModifier += character.artifact4.defense;
                critModifier += character.artifact4.crit;
                elementalMasteryModifier += character.artifact4.stringColor;
                healingBonusModifier += character.artifact4.healMana;
            }
            if (character.artifact5 != null)
            {
                lifeModifier += character.artifact5.healLife;
                damageModifier += character.baseDamage + character.artifact5.damage;
                defenseModifier += character.artifact5.defense;
                critModifier += character.artifact5.crit;
                elementalMasteryModifier += character.artifact5.stringColor;
                healingBonusModifier += character.artifact5.healMana;
            }

            float currentLifeModifier = (character.life / character.lifeMax);
            character.lifeMax = character.baseLifeMax + lifeModifier;
            character.life = (int) (character.lifeMax * currentLifeModifier); // Adjust current life accordingly (based on how much health was had at a %)
            character.damage = character.baseDamage + damageModifier;
            character.defense = character.baseDefense + defenseModifier;
            character.crit = character.baseCrit + critModifier;
            character.critDmg = character.baseCritDmg + critDmgModifier;
            character.elementalMastery = character.baseElementalMastery + elementalMasteryModifier;
            character.energyRecharge = character.baseEnergyRecharge + energyRechargeModifier;
            character.healingBonus = character.baseHealingBonus + healingBonusModifier;
        }
    }

    internal class ModifyCharacterProjectile : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.LocalPlayer;
            PlayerCharacterCode modPlayer = player.GetModPlayer<PlayerCharacterCode>();
            foreach(Character character in modPlayer.activeCharacters) // For every character that is active
            {
                if(projectile.ai[1] == character.GetNPCID()) // If a projectile shot as its ai[1] value equal to an active character's ID
                {
                    projectile.damage += character.damage; // Modify the damage based on the character's saved damage
                }
            }
        }
    }
}

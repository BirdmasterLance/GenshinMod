using GenshinMod.Items;
using System;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Characters
{
    // Static class so we can call methods from anywhere
    static class ModifyCharacterStats
    {
        public static void AdjustCharacterStats(Character character)
        {
            int lifeModifier = 0;
            double lifePercent = 0;
            int damageModifier = 0;
            double damagePercent = 0;
            int defenseModifier = 0;
            double defensePercent = 0;
            int critModifier = 0;
            int critDmgModifier = 0;
            int elementalMasteryModifier = 0;
            int energyRechargeModifier = 0;
            int healingBonusModifier = 0;

            int anemoDmgModifier = 0;
            int geoDmgModifier = 0;
            int electroDmgModifier = 0;
            int dendroDmgModifier = 0;
            int hydroDmgModifier = 0;
            int pyroDmgModifier = 0;
            int cryoDmgModifier = 0;
            int physicalDmgModifier = 0;

            Weapon weapon = character.GetWeapon();
            Artifact flower = character.GetArtifact(ArtifactType.Flower);
            Artifact plume = character.GetArtifact(ArtifactType.Plume);
            Artifact sands = character.GetArtifact(ArtifactType.Sands);
            Artifact goblet = character.GetArtifact(ArtifactType.Goblet);
            Artifact circlet = character.GetArtifact(ArtifactType.Circlet);

            if (weapon != null)
            {
                damageModifier += character.baseDamage + character.GetWeapon().Item.damage;
                switch (weapon.GetMainStat())
                {
                    case ItemStats.DefensePercentage:
                        defensePercent += weapon.GetMainStatValue();
                        break;
                    case ItemStats.AttackPercentage:
                        damagePercent += weapon.GetMainStatValue();
                        break;
                    case ItemStats.ElementalMastery:
                        elementalMasteryModifier += (int)weapon.GetMainStatValue();
                        break;
                    case ItemStats.EnergyRecharge:
                        energyRechargeModifier += (int)weapon.GetMainStatValue();
                        break;
                    case ItemStats.PhysicalDmgBonus:
                        physicalDmgModifier += (int)weapon.GetMainStatValue();
                        break;
                    case ItemStats.CritRate:
                        critModifier += (int)weapon.GetMainStatValue();
                        break;
                    case ItemStats.CritDmg:
                        critDmgModifier += (int)weapon.GetMainStatValue();
                        break;
                }
            }
            if (flower != null)
            {
                // Flowers always have HP as the main stat
                lifeModifier += (int)flower.GetMainStatValue();
                foreach((ItemStats, int) statValuePair in flower.GetSubStats())
                {
                    switch(statValuePair.Item1)
                    {
                        case ItemStats.Health:
                            lifeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.HealthPercentage:
                            lifePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Defense:
                            defenseModifier += statValuePair.Item2;
                            break;
                        case ItemStats.DefensePercentage:
                            defensePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Attack:
                            damageModifier += statValuePair.Item2;
                            break;
                        case ItemStats.AttackPercentage:
                            damagePercent += statValuePair.Item2;
                            break;
                        case ItemStats.ElementalMastery:
                            elementalMasteryModifier += statValuePair.Item2;
                            break;
                        case ItemStats.EnergyRecharge:
                            energyRechargeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritRate:
                            critModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritDmg:
                            critDmgModifier += statValuePair.Item2;
                            break;
                    }
                }    

            }
            if (plume != null)
            {
                // Plumes always have ATK as the main stat
                damageModifier += character.baseDamage + (int)plume.GetMainStatValue();
                foreach ((ItemStats, int) statValuePair in flower.GetSubStats())
                {
                    switch (statValuePair.Item1)
                    {
                        case ItemStats.Health:
                            lifeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.HealthPercentage:
                            lifePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Defense:
                            defenseModifier += statValuePair.Item2;
                            break;
                        case ItemStats.DefensePercentage:
                            defensePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Attack:
                            damageModifier += statValuePair.Item2;
                            break;
                        case ItemStats.AttackPercentage:
                            damagePercent += statValuePair.Item2;
                            break;
                        case ItemStats.ElementalMastery:
                            elementalMasteryModifier += statValuePair.Item2;
                            break;
                        case ItemStats.EnergyRecharge:
                            energyRechargeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritRate:
                            critModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritDmg:
                            critDmgModifier += statValuePair.Item2;
                            break;
                    }
                }


            }
            if (sands != null)
            {
                switch(sands.GetMainStat())
                {
                    case ItemStats.HealthPercentage:
                        lifePercent += sands.GetMainStatValue();
                        break;
                    case ItemStats.DefensePercentage:
                        defensePercent += sands.GetMainStatValue();
                        break;
                    case ItemStats.AttackPercentage:
                        damagePercent += sands.GetMainStatValue();
                        break;
                    case ItemStats.ElementalMastery:
                        elementalMasteryModifier += (int)sands.GetMainStatValue();
                        break;
                    case ItemStats.EnergyRecharge:
                        energyRechargeModifier += (int)sands.GetMainStatValue();
                        break;
                }

                foreach ((ItemStats, int) statValuePair in flower.GetSubStats())
                {
                    switch (statValuePair.Item1)
                    {
                        case ItemStats.Health:
                            lifeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.HealthPercentage:
                            lifePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Defense:
                            defenseModifier += statValuePair.Item2;
                            break;
                        case ItemStats.DefensePercentage:
                            defensePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Attack:
                            damageModifier += statValuePair.Item2;
                            break;
                        case ItemStats.AttackPercentage:
                            damagePercent += statValuePair.Item2;
                            break;
                        case ItemStats.ElementalMastery:
                            elementalMasteryModifier += statValuePair.Item2;
                            break;
                        case ItemStats.EnergyRecharge:
                            energyRechargeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritRate:
                            critModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritDmg:
                            critDmgModifier += statValuePair.Item2;
                            break;
                    }
                }
            }
            if (goblet != null)
            {
                switch (goblet.GetMainStat())
                {
                    case ItemStats.HealthPercentage:
                        lifePercent += goblet.GetMainStatValue();
                        break;
                    case ItemStats.DefensePercentage:
                        defensePercent += goblet.GetMainStatValue();
                        break;
                    case ItemStats.AttackPercentage:
                        damagePercent += goblet.GetMainStatValue();
                        break;
                    case ItemStats.ElementalMastery:
                        elementalMasteryModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.AnemoDmgBonus:
                        anemoDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.GeoDmgBonus:
                        geoDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.ElectroDmgBonus:
                        electroDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.DendroDmgBonus:
                        dendroDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.HydroDmgBonus:
                        hydroDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.PyroDmgBonus:
                        pyroDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.CryoDmgBonus:
                        cryoDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                    case ItemStats.PhysicalDmgBonus:
                        physicalDmgModifier += (int)goblet.GetMainStatValue();
                        break;
                }

                foreach ((ItemStats, int) statValuePair in flower.GetSubStats())
                {
                    switch (statValuePair.Item1)
                    {
                        case ItemStats.Health:
                            lifeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.HealthPercentage:
                            lifePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Defense:
                            defenseModifier += statValuePair.Item2;
                            break;
                        case ItemStats.DefensePercentage:
                            defensePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Attack:
                            damageModifier += statValuePair.Item2;
                            break;
                        case ItemStats.AttackPercentage:
                            damagePercent += statValuePair.Item2;
                            break;
                        case ItemStats.ElementalMastery:
                            elementalMasteryModifier += statValuePair.Item2;
                            break;
                        case ItemStats.EnergyRecharge:
                            energyRechargeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritRate:
                            critModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritDmg:
                            critDmgModifier += statValuePair.Item2;
                            break;
                    }
                }
            }
            if (circlet != null)
            {
                switch (circlet.GetMainStat())
                {
                    case ItemStats.HealthPercentage:
                        lifePercent += circlet.GetMainStatValue();
                        break;
                    case ItemStats.DefensePercentage:
                        defensePercent += circlet.GetMainStatValue();
                        break;
                    case ItemStats.AttackPercentage:
                        damagePercent += circlet.GetMainStatValue();
                        break;
                    case ItemStats.ElementalMastery:
                        elementalMasteryModifier += (int)circlet.GetMainStatValue();
                        break;
                    case ItemStats.CritRate:
                        critModifier += (int)circlet.GetMainStatValue();
                        break;
                    case ItemStats.CritDmg:
                        critDmgModifier += (int)circlet.GetMainStatValue();
                        break;
                    case ItemStats.HealingBonus:
                        healingBonusModifier += (int)circlet.GetMainStatValue();
                        break;
                }

                foreach ((ItemStats, int) statValuePair in flower.GetSubStats())
                {
                    switch (statValuePair.Item1)
                    {
                        case ItemStats.Health:
                            lifeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.HealthPercentage:
                            lifePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Defense:
                            defenseModifier += statValuePair.Item2;
                            break;
                        case ItemStats.DefensePercentage:
                            defensePercent += statValuePair.Item2;
                            break;
                        case ItemStats.Attack:
                            damageModifier += statValuePair.Item2;
                            break;
                        case ItemStats.AttackPercentage:
                            damagePercent += statValuePair.Item2;
                            break;
                        case ItemStats.ElementalMastery:
                            elementalMasteryModifier += statValuePair.Item2;
                            break;
                        case ItemStats.EnergyRecharge:
                            energyRechargeModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritRate:
                            critModifier += statValuePair.Item2;
                            break;
                        case ItemStats.CritDmg:
                            critDmgModifier += statValuePair.Item2;
                            break;
                    }
                }
            }

            float currentLifeModifier = character.life / character.lifeMax;
            character.lifeMax = ((int) (character.baseLifeMax * (1 + lifePercent))) + lifeModifier;
            character.life = (int) (character.lifeMax * currentLifeModifier); // Adjust current life accordingly (based on how much health was had at a %)
            character.damage = ((int) (character.baseDamage * (1 + damagePercent / 100))) + damageModifier;
            character.defense = ((int) (character.baseDefense * (1 + defensePercent / 100))) + defenseModifier;
            character.crit = character.baseCrit + critModifier;
            character.critDmg = character.baseCritDmg + critDmgModifier;
            character.elementalMastery = character.baseElementalMastery + elementalMasteryModifier;
            character.energyRecharge = character.baseEnergyRecharge + energyRechargeModifier;
            character.healingBonus = character.baseHealingBonus + healingBonusModifier;
            character.anemoDamage = character.baseAnemoDamage + anemoDmgModifier;
            character.geoDamage = character.baseGeoDamage + geoDmgModifier;
            character.electroDamage = character.baseElectroDamage + electroDmgModifier;
            character.dendroDamage = character.baseDendroDamage + dendroDmgModifier;
            character.hydroDamage = character.baseHydroDamage + hydroDmgModifier;
            character.pyroDamage = character.basePyroDamage + pyroDmgModifier;
            character.cryoDamaage = character.baseCryoDamage + cryoDmgModifier;
            character.physicalDamage = character.physicalDamage + physicalDmgModifier;
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
                    int appliedDamage = character.damage;
                    Random critChance = new Random();
                    if(critChance.Next() <= (character.crit / 100.0))
                    {
                        appliedDamage *= (int) (1 + (character.critDmg / 100.0));
                    }
                    projectile.damage += appliedDamage; // Modify the damage based on the character's saved damage
                }
            }
        }
    }
}

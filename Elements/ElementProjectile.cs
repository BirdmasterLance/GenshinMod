using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class ElementProjectile : GlobalProjectile
    {
      
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

            Color OverloadColor = new Color(250, 125, 170);
            Color CrystalizeColor = new Color(200, 140, 50);

            if (Elements.AnemoProjectiles.Contains(projectile.type)) // Anemo Reactions
            {
                target.AddBuff(ModContent.BuffType<AnemoBuff>(), 10);
            }

            else if (Elements.GeoProjectiles.Contains(projectile.type)) // Geo Reactions
            {
                if (target.HasBuff(ModContent.BuffType<ElectroBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
                    CombatText.NewText(target.getRect(), CrystalizeColor, "Crystalize");
                }
                else if (target.HasBuff(BuffID.Wet))
                {
                    target.DelBuff(target.FindBuffIndex(BuffID.Wet));
                    CombatText.NewText(target.getRect(), CrystalizeColor, "Crystalize");
                }
                else if (target.HasBuff(ModContent.BuffType<PyroBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
                    CombatText.NewText(target.getRect(), CrystalizeColor, "Crystalize");
                }
                else if (target.HasBuff(ModContent.BuffType<CryoBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
                    CombatText.NewText(target.getRect(), CrystalizeColor, "Crystalize");
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<GeoBuff>(), 10);
                }
            }

            else if (Elements.ElectroProjectiles.Contains(projectile.type)) // Electro Reactions
            {
                target.AddBuff(ModContent.BuffType<ElectroBuff>(), 600);
            }

            else if (Elements.DendroProjectiles.Contains(projectile.type)) // Dendro Reactions
            {
                target.AddBuff(ModContent.BuffType<DendroBuff>(), 600);
            }

            else if (Elements.HydroProjectiles.Contains(projectile.type)) // Hydro Reactions
            {
                if (target.HasBuff(ModContent.BuffType<PyroBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
                    CombatText.NewText(target.getRect(), new Color(235, 197, 107), "Vaporize");
                    damage *= 2;
                }
                else
                {
                    target.AddBuff(BuffID.Wet, 600);
                }
            }

            else  if (Elements.PyroProjectiles.Contains(projectile.type)) // Pyro Reactions
            {
                if (target.HasBuff(ModContent.BuffType<CryoBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
                    CombatText.NewText(target.getRect(), new Color(235, 197, 107), "Melt");
                    damage *= 2;
                }
                else if (target.HasBuff(BuffID.Wet))
                {
                    target.DelBuff(target.FindBuffIndex(BuffID.Wet));
                    CombatText.NewText(target.getRect(), new Color(235, 197, 107), "Vaporize");
                    damage = (int)(damage * 1.5);
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<PyroBuff>(), 600);
                }
            }

            else if (Elements.CryoProjectiles.Contains(projectile.type)) // Cryo Reactions
            {
                if (target.HasBuff(ModContent.BuffType<PyroBuff>()))
                {
                    target.DelBuff(target.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
                    CombatText.NewText(target.getRect(), new Color(235, 197, 107), "Melt");
                    damage = (int)(damage * 1.5);
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<CryoBuff>(), 600);
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Color AnemoColor = new Color(90, 240, 190);
            Color AnemoCritColor = new Color();
            Color GeoColor = new Color(247, 200, 100);
            Color GeoCritColor = new Color();
            Color ElectroColor = new Color(229, 153, 255);
            Color ElectroCritColor = new Color();
            Color DendroColor = new Color(165, 200, 59);
            Color DendroCritColor = new Color();
            Color HydroColor = new Color(50, 200, 255);
            Color HydroCritColor = new Color();
            Color PyroColor = new Color(229, 137, 20);
            Color PyroCritColor = new Color();
            Color CryoColor = new Color(120, 220, 240);
            Color CryoCritColor = new Color();

            if (Elements.AnemoProjectiles.Contains(projectile.type)) // Anemo
            {
                if (!crit) UpdateColorOfRecentCombatText(AnemoColor);
                else UpdateColorOfRecentCombatText(new Color(46, 171, 144));
            }
            else if (Elements.GeoProjectiles.Contains(projectile.type)) // Geo
            {
                if (!crit) UpdateColorOfRecentCombatText(GeoColor);
                else UpdateColorOfRecentCombatText(new Color(250, 182, 50));
            }
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) // Electro
            {
                if (!crit) UpdateColorOfRecentCombatText(ElectroColor);
                else UpdateColorOfRecentCombatText(new Color(143, 0, 214));
            }
            else if (Elements.DendroProjectiles.Contains(projectile.type)) // Dendro
            {
                if (!crit) UpdateColorOfRecentCombatText(DendroColor);
                else UpdateColorOfRecentCombatText(new Color(97, 204, 2));
            }
            else if (Elements.HydroProjectiles.Contains(projectile.type)) // Hydro
            {
                if (!crit) UpdateColorOfRecentCombatText(HydroColor);
                else UpdateColorOfRecentCombatText(new Color(25, 76, 207));
            }
            else if (Elements.PyroProjectiles.Contains(projectile.type)) // Pyro
            {
                if (!crit) UpdateColorOfRecentCombatText(PyroColor);
                else UpdateColorOfRecentCombatText(new Color(18, 0, 222));
            }
            else if (Elements.CryoProjectiles.Contains(projectile.type)) // Cryo
            {
                if (!crit) UpdateColorOfRecentCombatText(CryoColor);
                else UpdateColorOfRecentCombatText(new Color(0, 180, 204));
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoProjectiles.Contains(projectile.type)) target.AddBuff(ModContent.BuffType<CryoBuff>(), 600);
            else if (Elements.DendroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoProjectiles.Contains(projectile.type)) target.AddBuff(ModContent.BuffType<CryoBuff>(), 600);
            else if (Elements.DendroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        /// <summary>
        /// Changes the latest CombatText color to the specified color.
        /// </summary>
        public void UpdateColorOfRecentCombatText(Color color)
        {
            foreach (CombatText combatText in Main.combatText)
            {

                // NOTE: There is a bug in this code
                // Sometimes, an additional CombatText may change its color that might not be the one the user was expecting
                // Best seen when Stardust Dragon attacks an enemy while you are using an elemental attack
                // One or more of the Stardust Dragon's damage numbers will be incorrectly colored

                // When a CombatText first spawns, it has a lifeTime of either 60 or 120
                // 60 for normal, 120 for a crit
                // So if this happens as we go through the array, change the recently spawned damage to the color
                if (combatText.lifeTime == 60 || combatText.lifeTime == 120)
                {
                    // Don't change the color of these reaction messages
                    if (combatText.text == "Melt" ||
                        combatText.text == "Vaporize" ||
                        combatText.text == "Overload" ||
                        combatText.text == "Electro-Charged" ||
                        combatText.text == "Swirl" ||
                        combatText.text == "Crystalize" ||
                        combatText.text == "Burning" ||
                        combatText.text == "Bloom" ||
                        combatText.text == "Burgeon" ||
                        combatText.text == "Bloom" ||
                        combatText.text == "Quicken" ||
                        combatText.text == "Aggravate" ||
                        combatText.text == "Spread") continue;

                    if(combatText.alpha == 1f)
                    {
                        combatText.color = color;
                    }
                }
            }
        }

    }
}

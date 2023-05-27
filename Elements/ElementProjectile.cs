using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class ElementProjectile : GlobalProjectile
    {

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if(!crit) UpdateColorOfRecentCombatText(new Color(239, 121, 56));
                else UpdateColorOfRecentCombatText(new Color(194, 50, 10));
            }
            else if (Elements.HydroProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Wet, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(76, 94, 241));
                else UpdateColorOfRecentCombatText(new Color(18, 0, 222));
            }
            else if (Elements.ElectroProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(175, 142, 193));
                else UpdateColorOfRecentCombatText(new Color(143, 0, 214));
            }
            else if (Elements.CryoProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(159, 214, 227));
                else UpdateColorOfRecentCombatText(new Color(0, 180, 204));
            }
            else if (Elements.DendroProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(165, 200, 59));
                else UpdateColorOfRecentCombatText(new Color(97, 204, 2));
            }
            else if (Elements.AnemoProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(116, 194, 168));
                else UpdateColorOfRecentCombatText(new Color(0, 204, 194));
            }
            else if (Elements.GeoProjectiles.Contains(projectile.type))
            {
                target.AddBuff(BuffID.Stinky, 600);
                if (!crit) UpdateColorOfRecentCombatText(new Color(250, 182, 50));
                else UpdateColorOfRecentCombatText(new Color(239, 121, 56));
            }          
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
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
                    if(combatText.alpha == 1f)
                    {
                        combatText.color = color;
                    }
                }
            }
        }

    }
}

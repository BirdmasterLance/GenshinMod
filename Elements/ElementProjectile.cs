using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class ElementProjectile : GlobalProjectile
    {

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Elements.PyroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Wet, 600);
        }
    }
}

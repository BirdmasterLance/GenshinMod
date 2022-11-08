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
            else if (Elements.ElectroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoProjectiles.Contains(projectile.type)) target.AddBuff(BuffID.Stinky, 600);
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
    }
}

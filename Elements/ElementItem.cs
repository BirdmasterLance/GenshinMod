using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class ElementItem : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Elements.PyroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroItems.Contains(item.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            if (Elements.PyroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroItems.Contains(item.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoItems.Contains(item.type)) target.AddBuff(BuffID.Stinky, 600);
        }
    }
}

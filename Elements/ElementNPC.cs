using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class ElementNPC : GlobalNPC
    {

        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
        {
            if (Elements.PyroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (Elements.PyroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.HydroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Wet, 600);
            else if (Elements.ElectroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.CryoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.DendroNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.AnemoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
            else if (Elements.GeoNPCs.Contains(npc.type)) target.AddBuff(BuffID.Stinky, 600);
        }

        public override bool PreAI(NPC npc)
        {
            return !npc.HasBuff(ModContent.BuffType<FrozenNPCBuff>());
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<FrozenNPCBuff>()))
            {
                npc.velocity = Vector2.Zero;
                npc.position = npc.oldPosition;
            }
        }
    }
}

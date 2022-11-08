using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    public class Sucrouse_Ultimate : ModProjectile
    {


        public override string Texture => "Terraria/Images/Item_" + ItemID.ChlorophyteBullet;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SucrouseUltimate");
        }
        public override void SetDefaults()
        {
            Projectile.damage = 100;
            Projectile.width = 150;
            Projectile.height = 150;
            //Projectile.friendly = false;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            //Projectile.scale = 0.01f;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];


            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);

            //GetAlpha(Color.White);


            Projectile.alpha = 255;
            //for (int k = 0; k < 1; k++)
            //{
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurificationPowder, 0f, 0f, 0, Color.LightCyan);
            //}
            //Projectile.velocity.Y = 0f;

        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            //Player player = Main.player[Projectile.owner];
            float speed = 21f;
            float inertia = 1f;

            distanceFromTarget = 80f;
            targetCenter = Projectile.position;
            foundTarget = false;
            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];



                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;

                            Vector2 RelPosPlayer/*abovePlayer*/ = Projectile.position ;

                            Vector2 /*toAbovePlayer*/toRelPosPlayer = RelPosPlayer/*abovePlayer*/ - npc.Center;

                            Vector2 toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ = toRelPosPlayer.SafeNormalize(Vector2.UnitY);
                            Vector2 moveTo = toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ * speed;
                            //NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
                            npc.velocity = (npc.velocity * (inertia - 1) + moveTo) / inertia;
                        }
                    }
                }
            }
        }
    }
}
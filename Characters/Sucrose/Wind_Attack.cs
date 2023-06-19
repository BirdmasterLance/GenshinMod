using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    class Wind_Attack : ModProjectile

    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ChlorophyteBullet;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Attack");
        }

        public override void SetDefaults()
        {
            Projectile.damage = 100;
            Projectile.width = 50;
            Projectile.height = 50;
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

            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
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
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                            Projectile.position = npc.position;
                        }

                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            for (int k = 0; k < 60; k++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Clentaminator_Green, 0f, 0f, 0, Color.LightCyan);
            }
            for (int g = 0; g < 2; g++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Pixie, 0f, 0f, 0, Color.LightCyan);

            }


        }

    }
}

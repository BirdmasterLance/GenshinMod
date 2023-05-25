using GenshinMod.Elements;
using Hh1.BulletsorProjectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
	public class Flask : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ToxicFlask;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Swoop"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            DisplayName.SetDefault("Flask");
        }

        public override void SetDefaults()
		{
            Projectile.damage = 1;
            Projectile.width = 100;
            Projectile.height = 100;
            //Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 90;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;

        }

        public override void AI()
        {




        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {


            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Wind_Attack>(),100,0f, Main.myPlayer);

            return false;
        }


        //public override void Kill(int timeLeft)
        //{



            //}

        

	}
}
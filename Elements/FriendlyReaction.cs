using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;


namespace GenshinMod.Elements
{
    public class FriendlyReaction : ModProjectile
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.ChlorophyteBullet;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reaction");
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
			Projectile.timeLeft = 10;
			Projectile.ignoreWater = true;
			//Projectile.scale = 0.01f;
			Projectile.friendly = true;
		}


        public override void AI()
        {


            Projectile.velocity.X = 0f;



			Projectile.velocity.Y = 0f;


			//Projectile.localNPCHitCooldown = 0;


		}
		//public override void Kill(int timeLeft)
		//{



		//}



	}
}
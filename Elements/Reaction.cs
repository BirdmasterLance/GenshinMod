using Terraria.ID;
using Terraria.ModLoader;


namespace GenshinMod.Elements
{
    public class Reaction : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ChlorophyteBullet;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaction");
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
            //Projectile.scale = 0.01f;
            //Projectile.noUseGraphic= true;
        }


        public override void AI()
        {
            
            


        }
        //public override void Kill(int timeLeft)
        //{



        //}



    }
}
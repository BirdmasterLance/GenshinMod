using Hh1.BulletsorProjectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Hh1.Items
{
	public class Swoop : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MagnetSphere;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Swoop"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ModContent.ProjectileType<Wind_Attack>();

			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			  
                    position = Main.MouseWorld;
            
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			//ModContent.ProjectileType<Wind_Attack>();
			//Player owner = Main.player[Projectile.owner];

			// For more context, see ExampleProjectileModifications.cs
			//SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			//if (true)
			//{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			type = ModContent.ProjectileType<Wind_Attack>();
			Projectile.NewProjectile(source, position, velocity, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
			//}
			return false;
		}


	}
}
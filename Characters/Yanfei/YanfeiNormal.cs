using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Yanfei
{

	internal class YanfeiProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Seal of Approval");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;

			Projectile.ai[1] = -1;
		}

		public override void AI()
		{
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Torch, 0f, 0f, 150, default(Color), 2.5f);
			Main.dust[dustnumber].noGravity = true;

			// Auto target code from Flamelash
			Vector2 vector = Vector2.Zero;
			float amount = 1;
			int target = Projectile.FindTargetWithLineOfSight();
			if (Main.npc.IndexInRange(target))
			{
				NPC nPC = Main.npc[target];
				if (nPC.CanBeChasedBy(this))
				{
					vector = nPC.Center;
					float t = Projectile.Distance(nPC.Center);
					float num7 = Utils.GetLerpValue(0f, 100f, t, clamped: true) * Utils.GetLerpValue(600f, 400f, t, clamped: true);
					amount = MathHelper.Lerp(0f, 0.2f, Utils.GetLerpValue(200f, 20f, 1f - num7, clamped: true));
				}
				else
				{
					Projectile.netUpdate = true;
				}
			}
			if (vector.X != 0 && vector.Y != 0)
			{
				Vector2 value = vector;
				if (Projectile.Distance(value) >= 64f)
				{
					Vector2 v = value - Projectile.Center;
					Vector2 vector2 = v.SafeNormalize(Vector2.Zero);
					float num8 = Math.Min(10, v.Length()); // Set the number in the left side based on the shootSpeed of whatever shot it
					Vector2 value2 = vector2 * num8;
					if (Projectile.velocity.Length() < 4f)
					{
						Projectile.velocity += Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(0.7853981852531433).SafeNormalize(Vector2.Zero) * 4f;
					}
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, value2, amount);
					Projectile.netUpdate = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Torch, 0f, 0f, 150, default(Color), 4f);
				Main.dust[dustnumber].noGravity = true;
				Main.dust[dustnumber].scale = Main.rand.NextFloat() * 3f;
				Main.dust[dustnumber].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[dustnumber].velocity *= Main.rand.NextFloat() * 9f;
			}
			base.Kill(timeLeft);
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Projectile.ai[1] != -1)
			{
				NPC npc = Main.npc[(int) Projectile.ai[1]];
				if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
				{
					npc.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
				{
					npc.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff3>()));
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
				{
					npc.AddBuff(ModContent.BuffType<ScarletSealBuff3>(), 600);
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff2>()));
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
				{
					npc.AddBuff(ModContent.BuffType<ScarletSealBuff2>(), 600);
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff1>()));
				}
				else
				{
					npc.AddBuff(ModContent.BuffType<ScarletSealBuff1>(), 600);
				}
			}
			else
            {
				if (Main.myPlayer == Projectile.owner)
				{
					Player player = Main.player[Projectile.owner];
					if (player.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
					{
						player.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
					{
						player.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff3>());
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
					{
						player.AddBuff(ModContent.BuffType<ScarletSealBuff3>(), 600);
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff2>());
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
					{
						player.AddBuff(ModContent.BuffType<ScarletSealBuff2>(), 600);
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff1>());
					}
					else
					{
						player.AddBuff(ModContent.BuffType<ScarletSealBuff1>(), 600);
					}
				}
			}
		}
	}

	internal class YanfeiCharged : ModProjectile
	{
		private NPC alreadyHit;

		public override string Texture => "GenshinMod/Characters/Yanfei/YanfeiChargedAttack";
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Charged Seal of Approval");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.scale = 2;

			Projectile.ai[1] = -1;
		}

		public override void AI()
		{
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 150, default(Color), 3f);
			Main.dust[dustnumber].noGravity = true;

			Projectile.velocity.Y = Projectile.velocity.Y + 16f; // 0.1f for arrow gravity, 0.4f for knife gravity
			if (Projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				Projectile.velocity.Y = 16f;
			}
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			alreadyHit = target;

			if(Projectile.ai[1] != -1)
            {
				NPC npc = Main.npc[(int) Projectile.ai[1]];
				if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
				{
					// TModLoader recommends we don't directly adjust damage like this anymore
					// because too many mods modifying damage directly may be bad
					// But since this code only applies to projectiles shot by the Yanfei NPC
					// it's fine to use
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int) (hitInfo.Damage * 1.18);
					};

					modifiers.Knockback += 0.18f;
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
				{
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.36);
					};
					modifiers.Knockback += 0.36f;
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
				{
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.54);
					};
					modifiers.Knockback += 0.54f;
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
				{
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.72);
					};
					modifiers.Knockback += 0.72f;
				}

				if (npc.HasBuff(ModContent.BuffType<BrillianceBuff>()))
				{
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.5);
					};
					modifiers.Knockback += 0.25f;
				}
			}
			else
            {
				if (Main.myPlayer == Projectile.owner)
				{
					Player player = Main.player[Projectile.owner];
					if (player.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
					{
						modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
							hitInfo.Damage = (int)(hitInfo.Damage * 1.18);
						};
						modifiers.Knockback += 0.18f;
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
					{
						modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
							hitInfo.Damage = (int)(hitInfo.Damage * 1.36);
						};
						modifiers.Knockback += 0.36f;
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
					{
						modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
							hitInfo.Damage = (int)(hitInfo.Damage * 1.54);
						};
						modifiers.Knockback += 0.54f;
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
					{
						modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
							hitInfo.Damage = (int)(hitInfo.Damage * 1.72);
						};
						modifiers.Knockback += 0.72f;
					}

					if (player.HasBuff(ModContent.BuffType<BrillianceBuff>()))
					{
						modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
							hitInfo.Damage = (int)(hitInfo.Damage * 1.5);
						};
						modifiers.Knockback += 0.25f;
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Damage();

			for (int i = 0; i < 25; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;


			}
			base.Kill(timeLeft);
		}

		private void Damage()
		{
			int damage = Projectile.damage;
			float knockback = Projectile.knockBack;

			if(Projectile.ai[1] != -1)
            {
				NPC npc = Main.npc[(int) Projectile.ai[1]];
				if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
				{
					damage = (int)(damage * 1.18);
					knockback = (float)(knockback * 1.18);
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
				{
					damage = (int)(damage * 1.36);
					knockback = (float)(knockback * 1.36);
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
				{
					damage = (int)(damage * 1.54);
					knockback = (float)(knockback * 1.54);
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
				{
					damage = (int)(damage * 1.72);
					knockback = (float)(knockback * 1.72);
				}

				if (npc.HasBuff(ModContent.BuffType<BrillianceBuff>()))
				{
					damage = (int)(damage * 1.5);
					knockback = (float)(knockback * 1.25);
				}

				if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
				{
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff1>()));
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
				{
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff2>()));
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
				{
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff3>()));
				}
				else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
				{
					npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff4>()));
				}
			}
			else
            {
				if (Main.myPlayer == Projectile.owner)
				{
					Player player = Main.player[Projectile.owner];
					if (player.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
					{
						damage = (int)(damage * 1.18);
						knockback = (float)(knockback * 1.18);
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
					{
						damage = (int)(damage * 1.36);
						knockback = (float)(knockback * 1.36);
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
					{
						damage = (int)(damage * 1.54);
						knockback = (float)(knockback * 1.54);
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
					{
						damage = (int)(damage * 1.72);
						knockback = (float)(knockback * 1.72);
					}

					if (player.HasBuff(ModContent.BuffType<BrillianceBuff>()))
					{
						damage = (int)(damage * 1.5);
						knockback = (float)(knockback * 1.25);
					}

					if (player.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
					{
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff1>());
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
					{
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff2>());
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
					{
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff3>());
					}
					else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
					{
						player.ClearBuff(ModContent.BuffType<ScarletSealBuff4>());
					}
				}
			}

			Projectile.width = 80;
			Projectile.height = 80;

			// From explosive bullet code
			Rectangle projRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
			int[] localImmunities = Projectile.localNPCImmunity;
			for (int i = 0; i < 200; i++)
			{
				bool flag = (!Projectile.usesLocalNPCImmunity && !Projectile.usesIDStaticNPCImmunity) || (Projectile.usesLocalNPCImmunity && localImmunities[i] == 0) || (Projectile.usesIDStaticNPCImmunity && Projectile.IsNPCIndexImmuneToProjectileType(Projectile.type, i));
				if (!(Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag) || (Main.npc[i].aiStyle == 112 && Main.npc[i].ai[2] > 1f) || Main.npc[i].friendly || Projectile.ownerHitCheck)
				{
					continue;
				}
				if (alreadyHit != null && Main.npc[i] == alreadyHit)
				{
					continue;
				}


				if (Projectile.Colliding(projRect, Main.npc[i].getRect()))
				{
					NPC.HitInfo hitInfo = new NPC.HitInfo();
					hitInfo.Damage = damage;
					hitInfo.Knockback = knockback;
					hitInfo.HitDirection = Projectile.direction;
					hitInfo.Crit = Main.rand.Next(1, 101) <= Main.player[Projectile.owner].GetCritChance(DamageClass.Magic);
					Main.npc[i].StrikeNPC(hitInfo);
				}
			}
		}
	}	
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class Elements : ILoadable
    {
        // List of weapons
        public static readonly List<int> AnemoItems = new();
        public static readonly List<int> GeoItems = new();
        public static readonly List<int> ElectroItems = new();
        public static readonly List<int> DendroItems = new();
        public static readonly List<int> PyroItems = new();
        public static readonly List<int> HydroItems = new();
        public static readonly List<int> CryoItems = new();
                      
        public static readonly List<int> AnemoNPCs = new();
        public static readonly List<int> GeoNPCs = new();
        public static readonly List<int> ElectroNPCs = new();
        public static readonly List<int> DendroNPCs = new();
        public static readonly List<int> PyroNPCs = new();
        public static readonly List<int> HydroNPCs = new();
        public static readonly List<int> CryoNPCs = new();
                      
        public static readonly List<int> AnemoProjectiles = new();
        public static readonly List<int> GeoProjectiles = new();
        public static readonly List<int> ElectroProjectiles = new();
        public static readonly List<int> DendroProjectiles = new();
        public static readonly List<int> PyroProjectiles = new();
        public static readonly List<int> HydroProjectiles = new();
        public static readonly List<int> CryoProjectiles = new();

        public void Load(Mod mod)
        {
            #region Pyro Projectiles
            
            PyroProjectiles.Add(ProjectileID.FireArrow);
            PyroProjectiles.Add(ProjectileID.BallofFire);
            PyroProjectiles.Add(ProjectileID.Flamarang);
            PyroProjectiles.Add(ProjectileID.Flamelash);
            PyroProjectiles.Add(ProjectileID.Sunfury);
            PyroProjectiles.Add(ProjectileID.HellfireArrow);
            PyroProjectiles.Add(ProjectileID.FlamingArrow);
            PyroProjectiles.Add(ProjectileID.Flames);
            PyroProjectiles.Add(ProjectileID.CursedFlameFriendly);
            PyroProjectiles.Add(ProjectileID.CursedFlameHostile);
            PyroProjectiles.Add(ProjectileID.EyeFire);
            PyroProjectiles.Add(ProjectileID.BombSkeletronPrime);
            PyroProjectiles.Add(ProjectileID.CursedArrow);
            PyroProjectiles.Add(ProjectileID.CursedBullet);
            PyroProjectiles.Add(ProjectileID.Explosives);
            PyroProjectiles.Add(ProjectileID.GrenadeI);
            PyroProjectiles.Add(ProjectileID.RocketI);
            PyroProjectiles.Add(ProjectileID.RocketII);
            PyroProjectiles.Add(ProjectileID.RocketIII);
            PyroProjectiles.Add(ProjectileID.RocketIV);
            PyroProjectiles.Add(ProjectileID.ProximityMineI);
            PyroProjectiles.Add(ProjectileID.ProximityMineII);
            PyroProjectiles.Add(ProjectileID.ProximityMineIII);
            PyroProjectiles.Add(ProjectileID.ProximityMineIV);
            PyroProjectiles.Add(ProjectileID.Flare);
            PyroProjectiles.Add(ProjectileID.RocketFireworkRed);
            PyroProjectiles.Add(ProjectileID.RocketFireworkGreen);
            PyroProjectiles.Add(ProjectileID.RocketFireworkBlue);
            PyroProjectiles.Add(ProjectileID.RocketFireworkYellow);
            PyroProjectiles.Add(ProjectileID.FlamethrowerTrap);
            PyroProjectiles.Add(ProjectileID.FlamesTrap);
            PyroProjectiles.Add(ProjectileID.Fireball);
            PyroProjectiles.Add(ProjectileID.ExplosiveBunny);
            PyroProjectiles.Add(ProjectileID.InfernoFriendlyBolt);
            PyroProjectiles.Add(ProjectileID.InfernoFriendlyBlast);
            PyroProjectiles.Add(ProjectileID.InfernoHostileBolt);
            PyroProjectiles.Add(ProjectileID.InfernoHostileBlast);
            PyroProjectiles.Add(ProjectileID.FlamingJack);
            PyroProjectiles.Add(ProjectileID.FlamingWood);
            PyroProjectiles.Add(ProjectileID.GreekFire1);
            PyroProjectiles.Add(ProjectileID.GreekFire2);
            PyroProjectiles.Add(ProjectileID.GreekFire3);
            PyroProjectiles.Add(ProjectileID.FlamingScythe);
            PyroProjectiles.Add(ProjectileID.ImpFireball);
            PyroProjectiles.Add(ProjectileID.MolotovFire);
            PyroProjectiles.Add(ProjectileID.MolotovFire2);
            PyroProjectiles.Add(ProjectileID.MolotovFire3);
            PyroProjectiles.Add(ProjectileID.CultistBossFireBall);
            PyroProjectiles.Add(ProjectileID.CultistBossFireBallClone);
            PyroProjectiles.Add(ProjectileID.CursedDart);
            PyroProjectiles.Add(ProjectileID.CursedDartFlame);
            PyroProjectiles.Add(ProjectileID.Hellwing);
            PyroProjectiles.Add(ProjectileID.HelFire);
            PyroProjectiles.Add(ProjectileID.SolarWhipSword);
            PyroProjectiles.Add(ProjectileID.SolarWhipSwordExplosion);
            PyroProjectiles.Add(ProjectileID.Daybreak);
            PyroProjectiles.Add(ProjectileID.DaybreakExplosion);
            PyroProjectiles.Add(ProjectileID.GeyserTrap);
            PyroProjectiles.Add(ProjectileID.DesertDjinnCurse);
            PyroProjectiles.Add(ProjectileID.SpiritFlame);
            PyroProjectiles.Add(ProjectileID.DD2FlameBurstTowerT1Shot);
            PyroProjectiles.Add(ProjectileID.DD2FlameBurstTowerT2Shot);
            PyroProjectiles.Add(ProjectileID.DD2FlameBurstTowerT3Shot);
            PyroProjectiles.Add(ProjectileID.DD2BetsyFireball);
            PyroProjectiles.Add(ProjectileID.DD2BetsyFlameBreath);
            PyroProjectiles.Add(ProjectileID.DD2ExplosiveTrapT1Explosion);
            PyroProjectiles.Add(ProjectileID.DD2ExplosiveTrapT2Explosion);
            PyroProjectiles.Add(ProjectileID.DD2ExplosiveTrapT3Explosion);
            PyroProjectiles.Add(ProjectileID.DD2PhoenixBowShot);
            PyroProjectiles.Add(ProjectileID.MonkStaffT3_AltShot);
            PyroProjectiles.Add(ProjectileID.DD2BetsyArrow);
            PyroProjectiles.Add(ProjectileID.ApprenticeStaffT3Shot);
            PyroProjectiles.Add(ProjectileID.Celeb2Rocket);
            PyroProjectiles.Add(ProjectileID.Celeb2RocketLarge);
            PyroProjectiles.Add(ProjectileID.Celeb2RocketExplosive);
            PyroProjectiles.Add(ProjectileID.Celeb2RocketExplosiveLarge);
            PyroProjectiles.Add(ProjectileID.ClusterGrenadeI);
            PyroProjectiles.Add(ProjectileID.ClusterGrenadeII);
            PyroProjectiles.Add(ProjectileID.ClusterMineI);
            PyroProjectiles.Add(ProjectileID.ClusterMineII);
            PyroProjectiles.Add(ProjectileID.ClusterRocketI);
            PyroProjectiles.Add(ProjectileID.ClusterRocketII);
            PyroProjectiles.Add(ProjectileID.MiniNukeGrenadeI);
            PyroProjectiles.Add(ProjectileID.MiniNukeGrenadeII);
            PyroProjectiles.Add(ProjectileID.MiniNukeMineI);
            PyroProjectiles.Add(ProjectileID.MiniNukeMineII);
            PyroProjectiles.Add(ProjectileID.MiniNukeRocketI);
            PyroProjectiles.Add(ProjectileID.MiniNukeRocketII);
            PyroProjectiles.Add(ProjectileID.FireWhip);
            PyroProjectiles.Add(ProjectileID.FireWhipProj);
            PyroProjectiles.Add(ProjectileID.FlamingMace);
            PyroProjectiles.Add(ProjectileID.RocketSnowmanI);
            PyroProjectiles.Add(ProjectileID.RocketSnowmanII);
            PyroProjectiles.Add(ProjectileID.RocketSnowmanIII);
            PyroProjectiles.Add(ProjectileID.RocketSnowmanIV);
            PyroProjectiles.Add(ProjectileID.MiniNukeSnowmanRocketI);
            PyroProjectiles.Add(ProjectileID.MiniNukeSnowmanRocketII);
            PyroProjectiles.Add(ProjectileID.ClusterSnowmanRocketI);
            PyroProjectiles.Add(ProjectileID.ClusterSnowmanRocketII);

            #endregion

            #region Hydro Projectiles

            HydroProjectiles.AddRange(new int[]
            {
                ProjectileID.WaterStream,
                ProjectileID.WaterBolt,
                ProjectileID.BlueMoon,
                ProjectileID.RainFriendly,
                ProjectileID.RainNimbus,
                ProjectileID.Sharknado,
                ProjectileID.SharknadoBolt,
                ProjectileID.Cthulunado,
                ProjectileID.Flairon,
                ProjectileID.FlaironBubble,
                ProjectileID.MiniSharkron,
                ProjectileID.Typhoon,
                ProjectileID.WetRocket,
                ProjectileID.WetSnowmanRocket,
                ProjectileID.GoldenShowerFriendly,
                ProjectileID.GoldenShowerHostile
            });            

            #endregion
        }

        public void Unload()
        {
        }
    }
}

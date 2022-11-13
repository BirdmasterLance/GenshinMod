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
            
            PyroProjectiles.AddRange(new int[] 
            {
                ProjectileID.FireArrow,
                ProjectileID.BallofFire,
                ProjectileID.Flamarang,
                ProjectileID.Flamelash,
                ProjectileID.Sunfury,
                ProjectileID.HellfireArrow,
                ProjectileID.FlamingArrow,
                ProjectileID.Flames,
                ProjectileID.CursedFlameFriendly,
                ProjectileID.CursedFlameHostile,
                ProjectileID.EyeFire,
                ProjectileID.BombSkeletronPrime,
                ProjectileID.CursedArrow,
                ProjectileID.CursedBullet,
                ProjectileID.Explosives,
                ProjectileID.GrenadeI,
                ProjectileID.RocketI,
                ProjectileID.RocketII,
                ProjectileID.RocketIII,
                ProjectileID.RocketIV,
                ProjectileID.ProximityMineI,
                ProjectileID.ProximityMineII,
                ProjectileID.ProximityMineIII,
                ProjectileID.ProximityMineIV,
                ProjectileID.Flare,
                ProjectileID.RocketFireworkRed,
                ProjectileID.RocketFireworkGreen,
                ProjectileID.RocketFireworkBlue,
                ProjectileID.RocketFireworkYellow,
                ProjectileID.FlamethrowerTrap,
                ProjectileID.FlamesTrap,
                ProjectileID.Fireball,
                ProjectileID.ExplosiveBunny,
                ProjectileID.InfernoFriendlyBolt,
                ProjectileID.InfernoFriendlyBlast,
                ProjectileID.InfernoHostileBolt,
                ProjectileID.InfernoHostileBlast,
                ProjectileID.FlamingJack,
                ProjectileID.FlamingWood,
                ProjectileID.GreekFire1,
                ProjectileID.GreekFire2,
                ProjectileID.GreekFire3,
                ProjectileID.FlamingScythe,
                ProjectileID.ImpFireball,
                ProjectileID.MolotovFire,
                ProjectileID.MolotovFire2,
                ProjectileID.MolotovFire3,
                ProjectileID.CultistBossFireBall,
                ProjectileID.CultistBossFireBallClone,
                ProjectileID.CursedDart,
                ProjectileID.CursedDartFlame,
                ProjectileID.Hellwing,
                ProjectileID.HelFire,
                ProjectileID.SolarWhipSword,
                ProjectileID.SolarWhipSwordExplosion,
                ProjectileID.Daybreak,
                ProjectileID.DaybreakExplosion,
                ProjectileID.GeyserTrap,
                ProjectileID.DesertDjinnCurse,
                ProjectileID.SpiritFlame,
                ProjectileID.DD2FlameBurstTowerT1Shot,
                ProjectileID.DD2FlameBurstTowerT2Shot,
                ProjectileID.DD2FlameBurstTowerT3Shot,
                ProjectileID.DD2BetsyFireball,
                ProjectileID.DD2BetsyFlameBreath,
                ProjectileID.DD2ExplosiveTrapT1Explosion,
                ProjectileID.DD2ExplosiveTrapT2Explosion,
                ProjectileID.DD2ExplosiveTrapT3Explosion,
                ProjectileID.DD2PhoenixBowShot,
                ProjectileID.MonkStaffT3_AltShot,
                ProjectileID.DD2BetsyArrow,
                ProjectileID.ApprenticeStaffT3Shot,
                ProjectileID.Celeb2Rocket,
                ProjectileID.Celeb2RocketLarge,
                ProjectileID.Celeb2RocketExplosive,
                ProjectileID.Celeb2RocketExplosiveLarge,
                ProjectileID.ClusterGrenadeI,
                ProjectileID.ClusterGrenadeII,
                ProjectileID.ClusterMineI,
                ProjectileID.ClusterMineII,
                ProjectileID.ClusterRocketI,
                ProjectileID.ClusterRocketII,
                ProjectileID.MiniNukeGrenadeI,
                ProjectileID.MiniNukeGrenadeII,
                ProjectileID.MiniNukeMineI,
                ProjectileID.MiniNukeMineII,
                ProjectileID.MiniNukeRocketI,
                ProjectileID.MiniNukeRocketII,
                ProjectileID.FireWhip,
                ProjectileID.FireWhipProj,
                ProjectileID.FlamingMace,
                ProjectileID.RocketSnowmanI,
                ProjectileID.RocketSnowmanII,
                ProjectileID.RocketSnowmanIII,
                ProjectileID.RocketSnowmanIV,
                ProjectileID.MiniNukeSnowmanRocketI,
                ProjectileID.MiniNukeSnowmanRocketII,
                ProjectileID.ClusterSnowmanRocketI,
                ProjectileID.ClusterSnowmanRocketII
            });

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

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

            #region Anemo Projectiles

            AnemoProjectiles.AddRange(new int[]
            {
                ProjectileID.HarpyFeather, // Unsure
                ProjectileID.DandelionSeed,
                ProjectileID.DD2ApprenticeStorm,
                ProjectileID.WeatherPainShot

            });

            #endregion

            #region Geo Projectiles

            GeoProjectiles.AddRange(new int[]
            {
                ProjectileID.Boulder,
                ProjectileID.CrimsandBallFalling,
                ProjectileID.BoulderStaffOfEarth,

                // Unsure if Geo or Pyro
                ProjectileID.Meteor1,
                ProjectileID.Meteor2,
                ProjectileID.Meteor3,
                ProjectileID.Geode,
                ProjectileID.WhiteTigerPounce // Unsure
            });

            #endregion

            #region Electro Projectiles

            ElectroProjectiles.AddRange(new int[]
            {
                ProjectileID.GreenLaser,
                ProjectileID.DemonSickle,
                ProjectileID.DemonScythe,
                ProjectileID.DarkLance, // Unsure
                ProjectileID.EyeLaser,
                ProjectileID.PinkLaser,
                ProjectileID.PurpleLaser,
                ProjectileID.UnholyTridentFriendly,
                ProjectileID.UnholyTridentHostile,

                // Unsure
                ProjectileID.AmethystBolt,
                ProjectileID.TopazBolt,
                ProjectileID.SapphireBolt,
                ProjectileID.EmeraldBolt,
                ProjectileID.RubyBolt,
                ProjectileID.DiamondBolt,
                ProjectileID.AmberBolt,

                ProjectileID.MagnetSphereBall,
                ProjectileID.MagnetSphereBolt,
                ProjectileID.ChargedBlasterOrb,
                ProjectileID.ChargedBlasterLaser,
                ProjectileID.MiniRetinaLaser,
                ProjectileID.UFOLaser,
                ProjectileID.ScutlixLaserFriendly,
                ProjectileID.MartianTurretBolt,
                ProjectileID.BrainScramblerBolt,
                ProjectileID.GigaZapperSpear,
                ProjectileID.RayGunnerLaser,
                ProjectileID.LaserMachinegunLaser,
                ProjectileID.ScutlixLaserCrosshair,
                ProjectileID.Electrosphere,
                ProjectileID.SaucerDeathray,
                ProjectileID.InfluxWaver,
                ProjectileID.CultistBossLightningOrb,
                ProjectileID.CultistBossLightningOrbArc,

                // Unsure
                ProjectileID.NebulaLaser,
                ProjectileID.VortexLaser,

                ProjectileID.VortexLightning,
                ProjectileID.VortexVortexLightning,
                ProjectileID.MinecartMechLaser,
                ProjectileID.MartianWalkerLaser,
                ProjectileID.ScutlixLaser,
                ProjectileID.DD2LightningBugZap,
                ProjectileID.ThunderSpear,
                ProjectileID.ThunderStaffShot,
                ProjectileID.ThunderSpearShot,
                ProjectileID.ZapinatorLaser,

                ModContent.ProjectileType<ElectroFriendlySwirl>()

            });

            #endregion

            #region Dendro Projectiles

            DendroProjectiles.AddRange(new int[]
            {
                ProjectileID.VilethornBase,
                ProjectileID.VilethornTip,
                ProjectileID.IvyWhip,
                ProjectileID.ThornChakram,
                ProjectileID.Seed,
                ProjectileID.PoisonedKnife, // Unsure
                ProjectileID.Stinger,
                ProjectileID.NettleBurstRight,
                ProjectileID.NettleBurstLeft,
                ProjectileID.NettleBurstEnd,
                ProjectileID.JungleSpike,
                ProjectileID.Bee,
                ProjectileID.Wasp,

                // Unsure
                ProjectileID.ChlorophyteBullet,
                ProjectileID.ChlorophytePartisan,
                ProjectileID.ChlorophyteDrill,
                ProjectileID.ChlorophyteChainsaw,
                ProjectileID.ChlorophyteArrow,
                ProjectileID.CrystalLeafShot,
                ProjectileID.SporeCloud,
                ProjectileID.ChlorophyteOrb,
                ProjectileID.ChlorophyteJackhammer,

                ProjectileID.Leaf,
                ProjectileID.FlowerPow,
                ProjectileID.FlowerPowPetal,
                ProjectileID.PoisonFang, // Unsure
                ProjectileID.SeedPlantera,
                ProjectileID.PoisonSeedPlantera,
                ProjectileID.ThornBall,
                ProjectileID.VenomArrow, // Unsure
                ProjectileID.PineNeedleFriendly,
                ProjectileID.PineNeedleHostile,
                ProjectileID.HornetStinger,

                // Unsure
                ProjectileID.VenomSpider,
                ProjectileID.JumperSpider,
                ProjectileID.DangerousSpider,
                ProjectileID.BeeArrow,

                ProjectileID.JungleYoyo,
                ProjectileID.Yelets,

                ProjectileID.GiantBee,
                ProjectileID.SporeTrap,
                ProjectileID.SporeTrap2,
                ProjectileID.SporeGas,
                ProjectileID.SporeGas2,
                ProjectileID.SporeGas3,
                ProjectileID.DryadsWardCircle,
                ProjectileID.TruffleSpore,
                ProjectileID.RollingCactus,
                ProjectileID.RollingCactusSpike,
                ProjectileID.Shroomerang,
                ProjectileID.ThornWhip

                // ProjectileId.BladeOfGrass

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
                ProjectileID.GoldenShowerHostile,

                // Unsure
                ProjectileID.ToxicBubble,
                ProjectileID.IchorSplash,

                ProjectileID.Kraken,

                ModContent.ProjectileType<HydroFriendlySwirl>()

                // ProjectileID.Muramasa

            });

            #endregion

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
                ProjectileID.ClusterSnowmanRocketII,
                ProjectileID.SolarFlareChainsaw,
                ProjectileID.SolarFlareDrill,
                ProjectileID.CultistBossFireBall,
                ProjectileID.CultistBossFireBallClone,
                ProjectileID.SolarFlareRay,
                ProjectileID.FireWhip,
                ProjectileID.FireWhipProj,

                ModContent.ProjectileType<PyroFriendlySwirl>()

                // ProjectileID.Volcano

            });

            #endregion

            #region Cryo Projectiles

            CryoProjectiles.AddRange(new int[]
            {
                ProjectileID.IceBlock,
                ProjectileID.SnowBallHostile,
                ProjectileID.IceBoomerang,
                ProjectileID.IceBolt,
                ProjectileID.FrostBoltSword,
                ProjectileID.FrostArrow,
                ProjectileID.FrostBlastHostile,
                ProjectileID.SnowBallFriendly,
                ProjectileID.FrostburnArrow,
                ProjectileID.IceSpike,
                ProjectileID.IcewaterSpit,
                ProjectileID.SlushBall,
                ProjectileID.BallofFrost,
                ProjectileID.FrostBeam,
                ProjectileID.IceSickle,
                ProjectileID.FrostBlastFriendly,
                ProjectileID.Blizzard,
                ProjectileID.NorthPoleWeapon,
                ProjectileID.NorthPoleSpear,
                ProjectileID.NorthPoleSnowflake,
                ProjectileID.FrostWave,
                ProjectileID.FrostShard,
                ProjectileID.FrostBoltStaff,
                ProjectileID.CultistBossIceMist,
                ProjectileID.FrostDaggerfish,
                ProjectileID.Amarok,
                ProjectileID.CoolWhip,
                ProjectileID.CoolWhipProj,
                ProjectileID.DeerclopsIceSpike,

                ModContent.ProjectileType<CryoFriendlySwirl>()

                // ProjectileID.WandOfFrostingSpark
            });

            #endregion

            #region Anemo NPCs

            AnemoNPCs.AddRange(new int[]
            {
                NPCID.WyvernHead,
                NPCID.WindyBalloon,
                NPCID.Harpy

            });

            #endregion

            #region Geo NPCs

            GeoNPCs.AddRange(new int[]
            {
                NPCID.GraniteGolem,
                NPCID.GraniteFlyer,
                NPCID.RockGolem

            });

            #endregion

            #region Electro NPCs

            ElectroNPCs.AddRange(new int[]
            {
                NPCID.MartianTurret,
                NPCID.MartianDrone,
                NPCID.MartianSaucer,
                NPCID.MartianSaucerCannon,
                NPCID.MartianSaucerTurret,
                NPCID.MartianSaucerCore,
                NPCID.MartianProbe,
                NPCID.MartianWalker,
                NPCID.AncientLight

            });

            #endregion

            #region Dendro NPCs

            DendroNPCs.AddRange(new int[]
            {
                NPCID.JungleSlime,
                NPCID.ManEater,
                NPCID.Snatcher,
                NPCID.AngryTrapper,
                NPCID.MossHornet,
                NPCID.SpikedJungleSlime,
                NPCID.ZombieMushroom,
                NPCID.ZombieMushroomHat,
                NPCID.FungoFish,
                NPCID.AnomuraFungus,
                NPCID.MushiLadybug,
                NPCID.GiantFungiBulb,
                NPCID.FungiSpore,
                NPCID.Plantera,
                NPCID.PlanterasHook,
                NPCID.PlanterasTentacle,
                NPCID.Spore,
                NPCID.SporeBat,
                NPCID.SporeSkeleton

            });

            #endregion

            #region Hydro NPCs

            HydroNPCs.AddRange(new int[]
            {
                NPCID.WaterSphere,
                NPCID.AngryNimbus,
                NPCID.DukeFishron,
                NPCID.Sharkron,
                NPCID.Sharkron2

            });

            #endregion

            #region Pyro NPCs

            PyroNPCs.AddRange(new int[]
            {
                NPCID.BurningSphere,
                NPCID.MeteorHead,
                NPCID.LavaSlime,
                NPCID.Hellbat,
                NPCID.BlazingWheel,
                NPCID.Lavabat,
                NPCID.HellArmoredBones,
                NPCID.HellArmoredBonesMace,
                NPCID.HellArmoredBonesSpikeShield,
                NPCID.HellArmoredBonesSword,
                NPCID.SolarCrawltipedeHead,
                NPCID.SolarDrakomire,
                NPCID.SolarDrakomireRider,
                NPCID.SolarSroller,
                NPCID.SolarCorite,
                NPCID.SolarSolenian,
                NPCID.SolarFlare,
                NPCID.LunarTowerSolar,
                NPCID.SolarSpearman,
                NPCID.SolarGoop

            });

            #endregion

            #region Cryo NPCs

            CryoNPCs.AddRange(new int[]
            {
                NPCID.SnowmanGangsta,
                NPCID.MisterStabby,
                NPCID.SnowBalla,
                NPCID.IceSlime,
                NPCID.IceBat,
                NPCID.IceTortoise,
                NPCID.IceElemental,
                NPCID.SpikedIceSlime,
                NPCID.IceGolem,
                NPCID.IceQueen,
                NPCID.Flocko,
                NPCID.IceMimic

            });

            #endregion
        }

        public void Unload()
        {
        }
    }
}

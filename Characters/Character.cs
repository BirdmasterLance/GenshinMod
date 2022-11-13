using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GenshinMod
{
    public class Character
    {
        public string Name { get; protected set; }
        public int AttackLevel { get; set; }
        public int SkillLevel { get; set; }
        public int BurstLevel { get; set; }
        public int Constellation { get; set; }
        public int ConstellationUpgrade; // Needed because it should be the player's decision to upgrade their character's constellation

        #region Description Texts

        public string NormalAttack = "";
        public string NormalAttackDesc = "";
        public string Skill = "";
        public string SkillDesc = "";
        public string Burst = "";
        public string BurstDesc = "";
        public string Passive1 = "";
        public string Passive1Desc = "";
        public string Passive2 = "";
        public string Passive2Desc = "";
        public string Constellation1 = "";
        public string Constellation1Desc = "";
        public string Constellation2 = "";
        public string Constellation2Desc = "";
        public string Constellation3 = "";
        public string Constellation3Desc = "";
        public string Constellation4 = "";
        public string Constellation4Desc = "";
        public string Constellation5 = "";
        public string Constellation5Desc = "";
        public string Constellation6 = "";
        public string Constellation6Desc = "";
        public string Talent1Cost = "";
        public string Talent2Cost = "";
        public string Talent3Cost = "";
        public string Talent4Cost = "";
        public string Talent5Cost = "";
        public string Talent6Cost = "";
        public string Talent7Cost = "";
        public string Talent8Cost = "";
        public string Talent9Cost = "";
        public string Talent10Cost = "";

        #endregion

        public Character(string name="", int atkLVL=1, int skillLVL=1, int burstLVL=1, int constellationLVL=0, int constellationUpgrade=0)
        {
            Name = name;
            AttackLevel = atkLVL;
            SkillLevel = skillLVL;
            BurstLevel = burstLVL;
            Constellation = constellationLVL;
            ConstellationUpgrade = constellationUpgrade;
        }
    }

    public class Yanfei : Character
    {       
        public Yanfei(int atkLVL=1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Yanfei", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {

            #region Description Texts

            NormalAttack = "Seal of Approval";
            NormalAttackDesc = "[c/DEBC77:Normal Attack]\nShoots fireballs that deal up to three counts of [c/fc6f82:Pyro DMG].\nWhen Yanfei's Normal Attacks hit enemies, they will grant her a single Scarlet Seal. Yanfei may possess a maximum of 3 Scarlet Seals, and each time this effect is triggered, the duration of currently possessed Scarlet Seals will refresh.\nEach Scarlet Seal will decrease Yanfei's Stamina consumption and will disappear when she leaves the field.\n\n[c/DEBC77:Charged Attack]\nConsumes Stamina and all Scarlet Seals before dealing [c/fc6f82:AoE Pyro DMG] to the opponents after a short casting time.\nThis Charged Attack's AoE and DMG will increase according to the amount of Scarlet Seals consumed.\n\n[c/DEBC77:Plunging Attack]\nGathering the power of Pyro, Yanfei plunges towards the ground from mid-air, damaging all opponents in her path. Deals [c/fc6f82:AoE Pyro DMG] upon impact with the ground.";
            Skill = "Signed Edict";
            SkillDesc = "Summons blistering flames \nthat deal [c/fc6f82:AoE Pyro DMG].\nOpponents hit by the flames \nwill grant Yanfei the maximum number \nof Scarlet Seals.";
            Burst = "Done Deal";
            BurstDesc = "Triggers a spray of intense flames that rush at nearby opponents, dealing [c/fc6f82:AoE Pyro DMG], granting Yanfei the maximum number of Scarlet Seals, and applying Brilliance to her.\n\nBrilliance\nHas the following effects:\n  Grants Yanfei a Scarlet Seal at fixed intervals.\n  Increases the DMG dealt by her Charged Attacks.\nThe effects of Brilliance will end if Yanfei leaves the field or falls in battle.";
            Passive1 = "Proviso";
            Passive1Desc = "When Yanfei consumes Scarlet Seals by using a Charged Attack, each Scarlet Seal will increase Yanfei's [c/fc6f82:Pyro DMG Bonus] by 5%. This effects lasts for 6s. When a Charged Attack is used again during the effect's duration, it will dispel the previous effect.";
            Passive2 = "Blazing Eye";
            Passive2Desc = "When Yanfei's Charged Attack deals a CRIT Hit to opponents, she will deal an additional instance of [c/fc6f82:AoE Pyro DMG] equal to 80% of her ATK. This DMG counts as Charged Attack DMG.";
            Constellation1 = "The Law Knows No Kindness";
            Constellation1Desc = "When Yanfei uses her Charged Attack, \neach existing Scarlet Seal additionally reduces the \nstamina cost of this Charged Attack by 10%.";
            Constellation2 = "Right of Final Interpretation";
            Constellation2Desc = "Increases Yanfei's Charged Attack CRIT Rate by 20% \nagainst enemies below 50% HP.";
            Constellation3 = "Samadhi Fire-Forged";
            Constellation3Desc = "Increases the Level of Signed Edict by 3.\nMaximum upgrade level is 15.";
            Constellation4 = "Supreme Amnesty";
            Constellation4Desc = "When Done Deal is used:\nCreates a shield that absorbs \nup to 45% of Yanfei's Max HP for 15s.\nThis shield absorbs [c/fc6f82:Pyro DMG] \n250% more effectively.";
            Constellation5 = "Abiding Affidavit";
            Constellation5Desc = "Increases the Level of Done Deal by 3.\nMaximum upgrade level is 15.";
            Constellation6 = "Extra Clause";
            Constellation6Desc = "Increases the maximum number \nof Scarlet Seals by 1.";
            Talent1Cost = "";
            Talent2Cost = "";
            Talent3Cost = "";
            Talent4Cost = "";
            Talent5Cost = "";
            Talent6Cost = "";
            Talent7Cost = "";
            Talent8Cost = "";
            Talent9Cost = "";
            Talent10Cost = "";

            #endregion

        }
    }

    public class Kaeya : Character
    {
        public Kaeya(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Kaeya", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    public class Noelle : Character
    {
        public Noelle(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Noelle", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    public class Barbara : Character
    {
        public Barbara(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Barbara", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    public class RaidenShogun : Character
    {
        public RaidenShogun(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Raiden Shogun", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    public class YaeMiko : Character
    {
        public YaeMiko(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Yae Miko", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    public class Sucrose : Character
    {
        public Sucrose(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int constellationLVL = 0, int constellationUpgrade = 0) : base("Sucrose", atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade)
        {
        }
    }

    // TODO: artifacts

    // For saving character info to the player
    public class CharacterSerializer : TagSerializer<Character, TagCompound>
    {
        public override TagCompound Serialize(Character value) => new TagCompound
        {
            ["name"] = value.Name,
            ["atkLVL"] = value.AttackLevel,
            ["skillLVL"] = value.SkillLevel,
            ["burstLVL"] = value.BurstLevel,
            ["constellationLVL"] = value.Constellation,
            ["constellationUpgrade"] = value.ConstellationUpgrade
        };

        public override Character Deserialize(TagCompound tag)
        {
            string name = tag.GetString("name");
            int atkLVL = tag.GetInt("atkLVL");
            int skillLVL = tag.GetInt("skillLVL");
            int burstLVL = tag.GetInt("burstLVL");
            int constellationLVL = tag.GetInt("constellationLVL");
            int constellationUpgrade = tag.GetInt("constellationUpgrade");

            switch (name)
            {
                case "Yanfei":
                    return new Yanfei(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
                case "Kaeya":
                    return new Kaeya(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
                case "Noelle":
                    return new Noelle(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
                case "Barbara":
                    return new Barbara(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
                case "Yae Miko":
                    return new YaeMiko(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
                case "Raiden Shogun":
                    return new RaidenShogun(atkLVL, skillLVL, burstLVL, constellationLVL, constellationUpgrade);
            }
            return new Character(tag.GetString("name"), tag.GetInt("atkLVL"), tag.GetInt("skillLVL"), tag.GetInt("burstLVL"), tag.GetInt("constellationLVL"), tag.GetInt("constellationUpgrade"));
        }
    }

    public class CharacterLists : ILoadable
    {
        public static List<string> FourStarCharacters = new();
        public static List<string> FiveStarCharacters = new();
        public static List<string> AllCharacters = new();

        // A list only meant for testing characters that have been added in and are functional
        public static List<string> AddedCharacters = new();

        // 4 Stars
        public const string Amber = "Amber";
        public const string Kaeya = "Kaeya";
        public const string Lisa = "Lisa";
        public const string Barbara = "Barbara";
        public const string Noelle = "Noelle";
        public const string Beidou = "Beidou";
        public const string Bennett = "Bennett";
        public const string Chongyun = "Chongyun";
        public const string Collei = "Collei";
        public const string Diona = "Diona";
        public const string Fischl = "Fischl";
        public const string Gorou = "Gorou";
        public const string KujouSara = "Kujou Sara";
        public const string KukiShinobu = "Kuki Shinobu";
        public const string Ningguang = "Ningguang";
        public const string Razor = "Razor";
        public const string Rosaria = "Rosaria";
        public const string Sayu = "Sayu";
        public const string ShikanoinHeizou = "Shikanoin Heizou";
        public const string Sucrose = "Sucrose";
        public const string Thoma = "Thoma";
        public const string Xiangling = "Xiangling";
        public const string Xinyan = "Xinyan";
        public const string Yanfei = "Yanfei";
        public const string YunJin = "Yun Jin";

        // 5 Stars
        public const string Traveller = "Traveller";
        public const string Albedo = "Albedo";
        public const string Aloy = "Aloy";
        public const string Itto = "Arataki Itto";
        public const string Diluc = "Diluc";
        public const string Eula = "Eula";
        public const string Ganyu = "Ganyu";
        public const string HuTao = "Hu Tao";
        public const string Jean = "Jean";
        public const string Kazuha = "Kaedehara Kahuza";
        public const string Ayaka = "Kamisato Ayaka";
        public const string Ayato = "Kamisato Ayato";
        public const string Keqing = "Keqing";
        public const string Klee = "Klee";
        public const string Mona = "Mona";
        public const string Qiqi = "Qiqi";
        public const string RaidenShogun = "Raiden Shogun";
        public const string Kokomi = "Sangonomiya Kokomi";
        public const string Shenhe = "Shenhe";
        public const string Childe = "Childe";
        public const string Tighnari = "Tighnari";
        public const string Venti = "Venti";
        public const string Xiao = "Xiao";
        public const string Xingqiu = "Xingqiu";
        public const string YaeMiko = "Yae Miko";
        public const string Yelan = "Yelan";
        public const string Yoimiya = "Yoimiya";
        public const string Zhongli = "Zhongli";

        /// <summary>
        /// Gets the character's display name
        /// </summary>
        public static Character GetNewCharacter(string name)
        {
            switch(name)
            {
                case "Alebdo" : return null;
                case "Aloy" : return null;
                case "Amber" : return null;
                case "Barbara" : return new Barbara();
                case "Beidou" : return null;
                case "Bennett" : return null;
                case "Childe" : return null;
                case "Chongyun" : return null;
                case "Diluc" : return null;
                case "Diona" : return null;
                case "Eula" : return null;
                case "Fischl" : return null;
                case "Ganyu" : return null;
                case "HuTao" : return null;
                case "Itto" : return null;
                case "Jean" : return null;
                case "Klee" : return null;
                case "Kaeya" : return new Kaeya();
                case "Kazuha" : return null;
                case "Keqing" : return null;
                case "Lisa" : return null;
                case "Mona" : return null;
                case "Ningguang" : return null;
                case "Noelle" : return new Noelle();
                case "Qiqi" : return null;
                case "Raiden Shogun" : return new RaidenShogun();
                case "Sayu" : return null;
                case "Sucrose" : return new Sucrose();
                case "Venti" : return null;
                case "Xiao" : return null;
                case "Xiangling" : return null;
                case "Xinyan" : return null;
                case "Yae Miko" : return new YaeMiko();
                case "Yelan": return null;
                case "Yanfei" : return new Yanfei();
                case "Yoimiya" : return null;
                case "Yunjin" : return null;
                case "Zhongli" : return null;
            }
            return null;
        }

        // TODO: add traveler
        /// <summary>
        /// Get the element of the specified character.
        /// </summary>
        public static string GetElement(string character)
        {
            switch(character)
            {
                case Jean:
                case Kazuha:
                case Sayu:
                case ShikanoinHeizou:
                case Sucrose:
                case Venti:
                case Xiao:
                    return "Anemo";
                case Aloy:
                case Chongyun:
                case Diona:
                case Eula:
                case Ganyu:
                case Kaeya:
                case Ayaka:
                case Qiqi:
                case Rosaria:
                case Shenhe:
                    return "Cryo";
                case Collei:
                case Tighnari:
                    return "Dendro";
                case Beidou:
                case Fischl:
                case Keqing:
                case KujouSara:
                case KukiShinobu:
                case Lisa:
                case RaidenShogun:
                case Razor:
                case YaeMiko:
                    return "Electro";
                case Albedo:
                case Itto:
                case Gorou:
                case Ningguang:
                case Noelle:
                case YunJin:
                case Zhongli:
                    return "Geo";
                case Barbara:
                case Ayato:
                case Mona:
                case Kokomi:
                case Childe:
                case Xingqiu:
                case Yelan:
                    return "Hydro";
                case Amber:
                case Bennett:
                case Diluc:
                case HuTao:
                case Klee:
                case Thoma:
                case Xiangling:
                case Xinyan:
                case Yanfei:
                case Yoimiya:
                    return "Pyro";

            }
            return "None";
        }

        public void Load(Mod mod)
        {
            AddedCharacters.AddRange(new string[] 
            {
                Yanfei,
                Sucrose
            });

            FourStarCharacters.Add(Amber);
            FourStarCharacters.Add(Kaeya);
            FourStarCharacters.Add(Lisa);
            FourStarCharacters.Add(Barbara);
            FourStarCharacters.Add(Noelle);
            FourStarCharacters.Add(Beidou);
            FourStarCharacters.Add(Bennett);
            FourStarCharacters.Add(Chongyun);
            FourStarCharacters.Add(Collei);
            FourStarCharacters.Add(Diona);
            FourStarCharacters.Add(Fischl);
            FourStarCharacters.Add(Gorou);
            FourStarCharacters.Add(KujouSara);
            FourStarCharacters.Add(KukiShinobu);
            FourStarCharacters.Add(Ningguang);
            FourStarCharacters.Add(Razor);
            FourStarCharacters.Add(Rosaria);
            FourStarCharacters.Add(Sayu);
            FourStarCharacters.Add(ShikanoinHeizou);
            FourStarCharacters.Add(Sucrose);
            FourStarCharacters.Add(Thoma);
            FourStarCharacters.Add(Xiangling);
            FourStarCharacters.Add(Xingqiu);
            FourStarCharacters.Add(Xinyan);
            FourStarCharacters.Add(Yanfei);
            FourStarCharacters.Add(YunJin);

            FiveStarCharacters.Add(Albedo);
            FiveStarCharacters.Add(Aloy);
            FiveStarCharacters.Add(Itto);
            FiveStarCharacters.Add(Diluc);
            FiveStarCharacters.Add(Eula);
            FiveStarCharacters.Add(Ganyu);
            FiveStarCharacters.Add(HuTao);
            FiveStarCharacters.Add(Jean);
            FiveStarCharacters.Add(Kazuha);
            FiveStarCharacters.Add(Ayaka);
            FiveStarCharacters.Add(Ayato);
            FiveStarCharacters.Add(Keqing);
            FiveStarCharacters.Add(Klee);
            FiveStarCharacters.Add(Mona);
            FiveStarCharacters.Add(Qiqi);
            FiveStarCharacters.Add(RaidenShogun);
            FiveStarCharacters.Add(Kokomi);
            FiveStarCharacters.Add(Shenhe);
            FiveStarCharacters.Add(Childe);
            FiveStarCharacters.Add(Tighnari);
            FiveStarCharacters.Add(Venti);
            FiveStarCharacters.Add(Xiao);
            FiveStarCharacters.Add(YaeMiko);
            FiveStarCharacters.Add(Yelan);
            FiveStarCharacters.Add(Yoimiya);
            FiveStarCharacters.Add(Zhongli);

            AllCharacters.AddRange(FourStarCharacters);
            AllCharacters.AddRange(FiveStarCharacters);

            //AllCharacters.Add(Albedo);
            //AllCharacters.Add(Aloy);
            //AllCharacters.Add(Amber);
            //AllCharacters.Add(Barbara);
            //AllCharacters.Add(Beidou);
            //AllCharacters.Add(Bennett);
            //AllCharacters.Add(Childe);
            //AllCharacters.Add(Chongyun);
            //AllCharacters.Add(Diluc);
            //AllCharacters.Add(Diona);
            //AllCharacters.Add(Eula);
            //AllCharacters.Add(Fischl);
            //AllCharacters.Add(Ganyu);
            //AllCharacters.Add(HuTao);
            //AllCharacters.Add(Itto);
            //AllCharacters.Add(Jean);
            //AllCharacters.Add(Klee);
            //AllCharacters.Add(Kaeya);
            //AllCharacters.Add(Kazuha);
            //AllCharacters.Add(Keqing);
            //AllCharacters.Add(Lisa);
            //AllCharacters.Add(Mona);
            //AllCharacters.Add(Ningguang);
            //AllCharacters.Add(Noelle);
            //AllCharacters.Add(Qiqi);
            //AllCharacters.Add(RaidenShogun);
            //AllCharacters.Add(Sayu);
            //AllCharacters.Add(Sucrose);
            //AllCharacters.Add(Venti);
            //AllCharacters.Add(Xiao);
            //AllCharacters.Add(Xingqiu);
            //AllCharacters.Add(Xiangling);
            //AllCharacters.Add(Xinyan);
            //AllCharacters.Add(YaeMiko);
            //AllCharacters.Add(Yanfei);
            //AllCharacters.Add(Yelan);
            //AllCharacters.Add(Yoimiya);
            //AllCharacters.Add(YunJin);
            //AllCharacters.Add(Zhongli);
        }

        public void Unload()
        {
        }

        // Unused content below might delete later

        ///// <summary>
        ///// Returns a string List of every character added.
        ///// </summary>
        //public static List<string> GetListOfAllCharacters()
        //{
        //    List<string> output = new();
        //    output.Add(Albedo);
        //    output.Add(Aloy);
        //    output.Add(Amber);
        //    output.Add(Barbara);
        //    output.Add(Beidou);
        //    output.Add(Bennett);
        //    output.Add(Childe);
        //    output.Add(Chongyun);
        //    output.Add(Diluc);
        //    output.Add(Diona);
        //    output.Add(Eula);
        //    output.Add(Fischl);
        //    output.Add(Ganyu);
        //    output.Add(HuTao);
        //    output.Add(Itto);
        //    output.Add(Jean);
        //    output.Add(Klee);
        //    output.Add(Kaeya);
        //    output.Add(Kazuha);
        //    output.Add(Keqing);
        //    output.Add(Lisa);
        //    output.Add(Mona);
        //    output.Add(Ningguang);
        //    output.Add(Noelle);
        //    output.Add(Qiqi);
        //    output.Add(RaidenShogun);
        //    output.Add(Sayu);
        //    output.Add(Venti);
        //    output.Add(Xiao);
        //    output.Add(Xingqiu);
        //    output.Add(Xiangling);
        //    output.Add(Xinyan);
        //    output.Add(YaeMiko);
        //    output.Add(Yanfei);
        //    output.Add(Yelan);
        //    output.Add(Yoimiya);
        //    output.Add(YunJin);
        //    output.Add(Zhongli);

        //    return output;
        //}

        ///// <summary>
        ///// Returns a string List of every 4 star character added.
        ///// </summary>
        //public static List<string> GetAll4Stars()
        //{
        //    List<string> output = new();
        //    output.Add(Amber);
        //    output.Add(Kaeya);
        //    output.Add(Lisa);
        //    output.Add(Barbara);
        //    output.Add(Noelle);
        //    output.Add(Beidou);
        //    output.Add(Bennett);
        //    output.Add(Chongyun);
        //    output.Add(Collei);
        //    output.Add(Diona);
        //    output.Add(Fischl);
        //    output.Add(Gorou);
        //    output.Add(KujouSara);
        //    output.Add(KukiShinobu);
        //    output.Add(Ningguang);
        //    output.Add(Razor);
        //    output.Add(Rosaria);
        //    output.Add(Sayu);
        //    output.Add(ShikanoinHeizou);
        //    output.Add(Sucrose);
        //    output.Add(Thoma);
        //    output.Add(Xiangling);
        //    output.Add(Xingqiu);
        //    output.Add(Xinyan);
        //    output.Add(Yanfei);
        //    output.Add(YunJin);

        //    return output;
        //}

        ///// <summary>
        ///// Returns a string List of every 5 star character added.
        ///// </summary>
        //public static List<string> GetAll5Stars()
        //{
        //    List<string> output = new();
        //    output.Add(Albedo);
        //    output.Add(Aloy);
        //    output.Add(Itto);
        //    output.Add(Diluc);
        //    output.Add(Eula);
        //    output.Add(Ganyu);
        //    output.Add(HuTao);
        //    output.Add(Jean);
        //    output.Add(Kazuha);
        //    output.Add(Ayaka);
        //    output.Add(Ayato);
        //    output.Add(Keqing);
        //    output.Add(Klee);
        //    output.Add(Mona);
        //    output.Add(Qiqi);
        //    output.Add(RaidenShogun);
        //    output.Add(Kokomi);
        //    output.Add(Shenhe);
        //    output.Add(Childe);
        //    output.Add(Tighnari);
        //    output.Add(Venti);
        //    output.Add(Xiao);
        //    output.Add(YaeMiko);
        //    output.Add(Yelan);
        //    output.Add(Yoimiya);
        //    output.Add(Zhongli);

        //    return output;
        //}
    }
}

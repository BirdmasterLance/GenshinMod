using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace GenshinMod
{
    public class Character
    {
        public string Name { get; protected set; }
        public int AttackLevel { get; protected set; }
        public int SkillLevel { get; protected set; }
        public int BurstLevel { get; protected set; }
        public int AscenstionTalentLevel { get; protected set; }
        public int Constellation { get; protected set; }

        public string NormalAttackName { get; protected set; }
        public string SkillName { get; protected set; }
        public string BurstName { get; protected set; }
        public string AscensionTalent1Name { get; protected set; }
        public string AscensionTalent2Name { get; protected set; }

        public Character(string name="", int atkLVL=1, int skillLVL=1, int burstLVL=1, int ascensionTalentLevel=1, int constellationLVL=1)
        {
            Name = name;
            AttackLevel = atkLVL;
            SkillLevel = skillLVL;
            BurstLevel = burstLVL;
            AscenstionTalentLevel = ascensionTalentLevel;
            Constellation = constellationLVL;
        }
    }

    public class Yanfei : Character
    {   
        public Yanfei(int atkLVL=1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Yanfei", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Seal of Approval";
            SkillName = "Signed Edict";
            BurstName = "Done Deal";
            AscensionTalent1Name = "Proviso";
            AscensionTalent2Name = "Blazing Eye";
        }
    }

    public class Kaeya : Character
    {
        public Kaeya(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Kaeya", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Ceremonial Bladework";
            SkillName = "Frostgnaw";
            BurstName = "Glacial Waltz";
            AscensionTalent1Name = "Cold-Blooded Strike";
            AscensionTalent2Name = "Glacial Heart";
        }
    }

    public class Noelle : Character
    {
        public Noelle(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Noelle", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Favonius Bladework - Maid";
            SkillName = "Breastplate";
            BurstName = "Sweeping Time";
            AscensionTalent1Name = "Devotion";
            AscensionTalent2Name = "Nice and Clean";
        }
    }

    public class Barbara : Character
    {
        public Barbara(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Barbara", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Whisper of Water";
            SkillName = "Let the Show Begin♪";
            BurstName = "Shining Miracle♪";
            AscensionTalent1Name = "Glorious Season";
            AscensionTalent2Name = "Encore";
        }
    }

    public class RaidenShogun : Character
    {
        public RaidenShogun(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Raiden Shogun", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Origin";
            SkillName = "Transcendence: Baleful Omen";
            BurstName = "Secret Art: Musou Shinsetsu";
            AscensionTalent1Name = "Wishes Unnumbered";
            AscensionTalent2Name = "Enlightened One";
        }
    }

    public class YaeMiko : Character
    {
        public YaeMiko(int atkLVL = 1, int skillLVL = 1, int burstLVL = 1, int ascensionTalentLevel = 1, int constellationLVL = 1) : base("Yae Miko", atkLVL, skillLVL, burstLVL, ascensionTalentLevel, constellationLVL)
        {
            NormalAttackName = "Spiritfox Sin-Eater";
            SkillName = "Yakan Evocation: Sesshou Sakura";
            BurstName = "Great Secret Art: Tenko Kenshin";
            AscensionTalent1Name = "The Shrine's Sacred Shade";
            AscensionTalent2Name = "Enlightened Blessing";
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
            ["constellationLVL"] = value.Constellation
        };

        public override Character Deserialize(TagCompound tag)
        {
            string name = tag.GetString("name");
            int atkLVL = tag.GetInt("atkLVL");
            int skillLVL = tag.GetInt("skillLVL");
            int burstLVL = tag.GetInt("burstLVL");
            int constellationLVL = tag.GetInt("constellationLVL");

            switch (name)
            {
                case "Yanfei":
                    return new Yanfei(atkLVL, skillLVL, burstLVL, constellationLVL);
                case "Kaeya":
                    return new Kaeya(atkLVL, skillLVL, burstLVL, constellationLVL);
                case "Noelle":
                    return new Noelle(atkLVL, skillLVL, burstLVL, constellationLVL);
                case "Barbara":
                    return new Barbara(atkLVL, skillLVL, burstLVL, constellationLVL);
                case "Yae Miko":
                    return new YaeMiko(atkLVL, skillLVL, burstLVL, constellationLVL);
                case "Raiden Shogun":
                    return new RaidenShogun(atkLVL, skillLVL, burstLVL, constellationLVL);
            }
            return new Character(tag.GetString("name"), tag.GetInt("atkLVL"), tag.GetInt("skillLVL"), tag.GetInt("burstLVL"), tag.GetInt("constellationLVL"));
        }
    }

    public static class CharacterLists
    {
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

        /// <summary>
        /// Returns a string List of every character added.
        /// </summary>
        public static List<string> GetListOfAllCharacters()
        {
            List<string> output = new();
            output.Add(Albedo);
            output.Add(Aloy);
            output.Add(Amber);
            output.Add(Barbara);
            output.Add(Beidou);
            output.Add(Bennett);
            output.Add(Childe);
            output.Add(Chongyun);
            output.Add(Diluc);
            output.Add(Diona);
            output.Add(Eula);
            output.Add(Fischl);
            output.Add(Ganyu);
            output.Add(HuTao);
            output.Add(Itto);
            output.Add(Jean);
            output.Add(Klee);
            output.Add(Kaeya);
            output.Add(Kazuha);
            output.Add(Keqing);
            output.Add(Lisa);
            output.Add(Mona);
            output.Add(Ningguang);
            output.Add(Noelle);
            output.Add(Qiqi);
            output.Add(RaidenShogun);
            output.Add(Sayu);
            output.Add(Venti);
            output.Add(Xiao);
            output.Add(Xingqiu);
            output.Add(Xiangling);
            output.Add(Xinyan);
            output.Add(YaeMiko);
            output.Add(Yanfei);
            output.Add(Yelan);
            output.Add(Yoimiya);
            output.Add(YunJin);
            output.Add(Zhongli);

            return output;
        }

        /// <summary>
        /// Returns a string List of every 4 star character added.
        /// </summary>
        public static List<string> GetAll4Stars()
        {
            List<string> output = new();
            output.Add(Amber);
            output.Add(Kaeya);
            output.Add(Lisa);
            output.Add(Barbara);
            output.Add(Noelle);
            output.Add(Beidou);
            output.Add(Bennett);
            output.Add(Chongyun);
            output.Add(Collei);
            output.Add(Diona);
            output.Add(Fischl);
            output.Add(Gorou);
            output.Add(KujouSara);
            output.Add(KukiShinobu);
            output.Add(Ningguang);
            output.Add(Razor);
            output.Add(Rosaria);
            output.Add(Sayu);
            output.Add(ShikanoinHeizou);
            output.Add(Sucrose);
            output.Add(Thoma);
            output.Add(Xiangling);
            output.Add(Xingqiu);
            output.Add(Xinyan);
            output.Add(Yanfei);
            output.Add(YunJin);

            return output;
        }

        /// <summary>
        /// Returns a string List of every 5 star character added.
        /// </summary>
        public static List<string> GetAll5Stars()
        {
            List<string> output = new();

            output.Add(Traveller);
            output.Add(Albedo);
            output.Add(Aloy);
            output.Add(Itto);
            output.Add(Diluc);
            output.Add(Eula);
            output.Add(Ganyu);
            output.Add(HuTao);
            output.Add(Jean);
            output.Add(Kazuha);
            output.Add(Ayaka);
            output.Add(Ayato);
            output.Add(Keqing);
            output.Add(Klee);
            output.Add(Mona);
            output.Add(Qiqi);
            output.Add(RaidenShogun);
            output.Add(Kokomi);
            output.Add(Shenhe);
            output.Add(Childe);
            output.Add(Tighnari);
            output.Add(Venti);
            output.Add(Xiao);
            output.Add(YaeMiko);
            output.Add(Yelan);
            output.Add(Yoimiya);
            output.Add(Zhongli);

            return output;
        }
    }

}

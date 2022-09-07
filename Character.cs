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
        public int AscensionLevel { get; protected set; }
        public int Constellation { get; protected set; }

        public Character(string name)
        {
            Name = name;

            AttackLevel = 1;
            SkillLevel = 1;
            BurstLevel = 1;
            AscensionLevel = 1;
            Constellation = 1;
        }

        public Character(string name, int atkLVL, int skillLVL, int burstLVL, int ascentionLVL, int constellationLVL)
        {
            Name = name;
            AttackLevel = atkLVL;
            SkillLevel = skillLVL;
            BurstLevel = burstLVL;
            AscensionLevel = ascentionLVL;
            Constellation = constellationLVL;
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
            ["ascensionLVL"] = value.AscensionLevel,
            ["constellationLVL"] = value.Constellation
        };

        public override Character Deserialize(TagCompound tag) => new Character(
            tag.GetString("name"),
            tag.GetInt("atkLVL"),
            tag.GetInt("skillLVL"),
            tag.GetInt("burstLVL"),
            tag.GetInt("ascensionLVL"),
            tag.GetInt("constellationLVL")
            );
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
        public static string GetDisplayName(string name)
        {
            switch(name)
            {
                case "Alebdo" : return Albedo;
                case "Aloy" : return Aloy;
                case "Amber" : return Amber;
                case "Barbara" : return Barbara;
                case "Beidou" : return Beidou;
                case "Bennett" : return Bennett;
                case "Childe" : return Childe;
                case "Chongyun" : return Chongyun;
                case "Diluc" : return Diluc;
                case "Diona" : return Diona;
                case "Eula" : return Eula;
                case "Fischl" : return Fischl;
                case "Ganyu" : return Ganyu;
                case "HuTao" : return HuTao;
                case "Itto" : return Itto;
                case "Jean" : return Jean;
                case "Klee" : return Klee;
                case "Kaeya" : return Kaeya;
                case "Kazuha" : return Kazuha;
                case "Keqing" : return Keqing;
                case "Lisa" : return Lisa;
                case "Mona" : return Mona;
                case "Ningguang" : return Ningguang;
                case "Noelle" : return Noelle;
                case "Qiqi" : return Qiqi;
                case "RaidenShogun" : return RaidenShogun;
                case "Sayu" : return Sayu;
                case "Venti" : return Venti;
                case "Xiao" : return Xiao;
                case "Xiangling" : return Xiangling;
                case "Xinyan" : return Xinyan;
                case "YaeMiko" : return YaeMiko;
                case "Yelan": return Yelan;
                case "Yanfei" : return Yanfei;
                case "Yoimiya" : return Yoimiya;
                case "Yunjin" : return YunJin;
                case "Zhongli" : return Zhongli;
            }
            return "None";
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

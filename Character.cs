using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinMod
{
    public static class Character
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
        public const string YaeMiko = "Yae Miko";
        public const string Yelan = "Yelan";
        public const string Yoimiya = "Yoimiya";
        public const string Zhongli = "Zhongli";

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
            output.Add(Xiangling);
            output.Add(Xinyan);
            output.Add(Yelan);
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

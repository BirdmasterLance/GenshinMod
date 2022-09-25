using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace GenshinMod.CharacterClasses
{
    internal static class CharacterText
    {

        public static Dictionary<string, string> Texts = new Dictionary<string, string>();

        public static void Initialize()
        {
            string directory = "";
            Console.WriteLine("THIS IS THE DIRECTORY: " + directory);
            if (File.Exists(directory))
            {
                string fileText = File.ReadAllText(directory);
                string[] characterDescriptions = fileText.Split('{');
                List<string> characters = CharacterLists.GetListOfAllCharacters();
                int charNum = 0;
                foreach (string characterDescription in characterDescriptions)
                {
                    string[] individualLine = characterDescription.Split('=');
                    string characterName = individualLine[0];
                    Texts[characterName + " Normal Attack"] = individualLine[1];
                    Texts[characterName + " Normal Attack Desc"] = individualLine[2];
                    Texts[characterName + " Skill"] = individualLine[3];
                    Texts[characterName + " Skill Desc"] = individualLine[4];
                    Texts[characterName + " Burst"] = individualLine[5];
                    Texts[characterName + " Burst Desc"] = individualLine[8];
                    Texts[characterName + " Passive 1"] = individualLine[7];
                    Texts[characterName + " Passive 1 Desc"] = individualLine[8];
                    Texts[characterName + " Passive 2"] = individualLine[9];
                    Texts[characterName + " Passive 2 Desc"] = individualLine[10];
                    Texts[characterName + " Constellation1"] = individualLine[11];
                    Texts[characterName + " Constellation1 Desc"] = individualLine[12];
                    Texts[characterName + " Constellation2"] = individualLine[13];
                    Texts[characterName + " Constellation2 Desc"] = individualLine[14];
                    Texts[characterName + " Constellation3"] = individualLine[15];
                    Texts[characterName + " Constellation3 Desc"] = individualLine[16];
                    Texts[characterName + " Constellation4"] = individualLine[17];
                    Texts[characterName + " Constellation4 Desc"] = individualLine[18];
                    Texts[characterName + " Constellation5"] = individualLine[19];
                    Texts[characterName + " Constellation5 Desc"] = individualLine[20];
                    Texts[characterName + " Constellation6"] = individualLine[21];
                    Texts[characterName + " Constellation6 Desc"] = individualLine[22];
                    Texts[characterName + " Talent Cost1"] = individualLine[23];
                    Texts[characterName + " Talent Cost2"] = individualLine[24];
                    Texts[characterName + " Talent Cost3"] = individualLine[25];
                    Texts[characterName + " Talent Cost4"] = individualLine[26];
                    Texts[characterName + " Talent Cost5"] = individualLine[27];
                    Texts[characterName + " Talent Cost6"] = individualLine[28];
                    Texts[characterName + " Talent Cost7"] = individualLine[29];
                    Texts[characterName + " Talent Cost8"] = individualLine[30];
                    Texts[characterName + " Talent Cost9"] = individualLine[31];
                    Texts[characterName + " Talent Cost10"] = individualLine[32];

                    charNum++;
                }
            }
        }

        public static string GetDescription(string key)
        {
            if (key == null) return "None";
            if (!Texts.ContainsKey(key)) return "None";
            return Texts[key];
        }
    }
}

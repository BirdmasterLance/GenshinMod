using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
    internal class WeaponEXPValues : ILoadable
    {

        private static Dictionary<int, int> FourStarWeaponExpToLevel = new();
        private static Dictionary<int, int> FiveStarWeaponExpToLevel = new();


        public void Load(Mod mod)
        {
            #region FourStarWeaponExpToLevel

            FourStarWeaponExpToLevel[0] = 1;
            FourStarWeaponExpToLevel[400] = 2;
            FourStarWeaponExpToLevel[1025] = 3;
            FourStarWeaponExpToLevel[1925] = 4;
            FourStarWeaponExpToLevel[3125] = 5;
            FourStarWeaponExpToLevel[4675] = 6;
            FourStarWeaponExpToLevel[6625] = 7;
            FourStarWeaponExpToLevel[8975] = 8;
            FourStarWeaponExpToLevel[11775] = 9;
            FourStarWeaponExpToLevel[15075] = 10;
            FourStarWeaponExpToLevel[18875] = 11;
            FourStarWeaponExpToLevel[23225] = 12;
            FourStarWeaponExpToLevel[28150] = 13;
            FourStarWeaponExpToLevel[33675] = 14;
            FourStarWeaponExpToLevel[39825] = 15;
            FourStarWeaponExpToLevel[46625] = 16;
            FourStarWeaponExpToLevel[54125] = 17;
            FourStarWeaponExpToLevel[62325] = 18;
            FourStarWeaponExpToLevel[71275] = 19;
            FourStarWeaponExpToLevel[81000] = 20;
            FourStarWeaponExpToLevel[91500] = 21;
            FourStarWeaponExpToLevel[103400] = 22;
            FourStarWeaponExpToLevel[116175] = 23;
            FourStarWeaponExpToLevel[129875] = 24;
            FourStarWeaponExpToLevel[144525] = 25;
            FourStarWeaponExpToLevel[160150] = 26;
            FourStarWeaponExpToLevel[176775] = 27;
            FourStarWeaponExpToLevel[194425] = 28;
            FourStarWeaponExpToLevel[213125] = 29;
            FourStarWeaponExpToLevel[232900] = 30;

            #endregion
        }

        public void Unload()
        {
        }

        public static int FourStarExpToLevel(int expValue)
        {
            foreach(int exp in FourStarWeaponExpToLevel.Keys)
            {
                if(expValue <= exp)
                {
                    return FourStarWeaponExpToLevel[exp];
                }
            }

            return 0;
        }

        public static int FourStarExpToNextLevel(int expValue)
        {
            foreach (int exp in FourStarWeaponExpToLevel.Keys)
            {
                if (expValue <= exp)
                {
                    return exp-expValue;
                }
            }

            return 0;
        }
    }
}

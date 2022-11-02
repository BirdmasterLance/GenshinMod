using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace GenshinMod.Invasions
{
    internal enum DomainType
    {
        TestDomain
    }

    internal enum DomainReward
    {
        TestReward
    }

    internal static class DomainList
    {
        // Make each domain a List of int[]
        // Where each index of the list is the "wave" in the domain
        // Each int[] is going to be the enemies that spawn in each wave
        private static List<int[]> testDomain = new()
        {
            new int[] { NPCID.Hornet, NPCID.Hornet, NPCID.HoppinJack, NPCID.HoppinJack },
            new int[] { NPCID.SandShark, NPCID.Shark },
            new int[] { NPCID.IceMimic }
        };

        // Make each reward list a List of int[]
        // Each int[] will have 2 values: the item ID, and how much of the item
        private static List<int[]> testReward = new()
        {
            new int[] { ItemID.Acorn, 10 }
        };

        /// <summary>
        /// Gets the list of enemies from a specified DomainType
        /// </summary>
        public static List<int[]> GetList(DomainType type)
        {
            switch(type)
            {
                case DomainType.TestDomain:
                    return testDomain;
            }
            return null;
        }

        /// <summary>
        /// Gets the list of items from a specified DomainReward
        /// </summary>
        public static List<int[]> GetReward(DomainReward reward)
        {
            switch(reward)
            {
                case DomainReward.TestReward:
                    return testReward;
            }
            return null;
        }
    }
}

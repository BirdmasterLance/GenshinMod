using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

// Code modified from: https://forums.terraria.org/index.php?threads/tutorial-custom-invasion.57771/

namespace GenshinMod.Invasions
{
    internal class DomainInvasion
    {

        // We will access this domain so other classes know
        // what enemies will be spawned
        public static List<int[]> activeDomain = new();
        // Used to check how many enemies have been killed in each wave
        public static int enemiesKilled = 0;

        // Make each domain a List of int[]
        // Where each index of the list is the "wave" in the domain
        // Each int[] is going to be the enemies that spawn in each wave
        public static List<int[]> testDomain = new()
        {
            new int[] { NPCID.BlueSlime, NPCID.BlueSlime, NPCID.BlueSlime },
            new int[] { NPCID.BlueSlime, NPCID.BlueSlime },
            new int[] { NPCID.BlueSlime }
        };

        // TODO: allow us to choose which domain to start via some argument
        public static void StartDomain()
        {
            // Make sure no other invasion is occuring right now
            if (Main.invasionType == 0)
            {
                DomainWorld.domainActive = true;
                activeDomain = testDomain; // TODO: make this better
                enemiesKilled = 0;

                Main.invasionType = -1; //Not going to be using an invasion that is positive since those are vanilla invasions
                Main.invasionSize = GetSizeOfDomainList(testDomain);
                Main.invasionSizeStart = Main.invasionSize;
                Main.invasionProgress = 0;               
                Main.invasionProgressWave = 1;
                Main.invasionProgressMax = Main.invasionSizeStart;
                Main.invasionProgressIcon = 0 + 3;
                Main.invasionWarn = 3600; //This doesn't really matter, as this does not work
                Main.invasionX = 0.0; //Starts invasion immediately rather than wait for it to spawn

                SpawnWaveEnemies();

                Main.NewText(Main.invasionSize);
            }
        }

        public static void EndDomain()
        {
            DomainWorld.domainActive = false;
            enemiesKilled = 0;
            Main.invasionType = 0;
        }

        public static void CheckDomainProgress()
        {
            int icon = 0;
            Main.ReportInvasionProgress(Main.invasionProgress, Main.invasionSize, icon, Main.invasionProgressWave);

            //Syncing start of invasion
            foreach (Player p in Main.player)
            {
                NetMessage.SendData(78, p.whoAmI, -1, null, Main.invasionProgress, Main.invasionSize, icon, Main.invasionProgressWave, 0, 0, 0);
            }
        }

        /// <summary>
        /// Spawn all the enemies in a specific wave
        /// </summary>
        public static void SpawnWaveEnemies()
        {
            foreach (int enemy in activeDomain[Main.invasionProgressWave - 1])
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(Main.myPlayer, enemy);
                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBoss, number: Main.myPlayer, number2: enemy);
                }
            }
        }

        /// <summary>
        /// Returns how many enemies are in a specified domain.
        /// </summary>
        private static int GetSizeOfDomainList(List<int[]> domain)
        {
            int size = 0;
            foreach(int[] enemies in domain)
            {
                foreach(int enemy in enemies)
                {
                    size++;
                }
            }

            return size;
        }
    }
}

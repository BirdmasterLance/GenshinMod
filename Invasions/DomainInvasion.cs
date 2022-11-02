using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
        
        public static List<int[]> domainReward = new();

        // Used to check how many enemies have been killed in each wave
        public static int enemiesKilled = 0;

        public static Rectangle domainRectangle;

        // Players who aren't there when the domain starts aren't counted for rewards
        public static List<Player> playersInDomain = new();

        // Active (alive) enemies in the domain
        private static List<NPC> activeDomainEnemies = new();

        public static void StartDomain(DomainType domain, DomainReward reward, Vector2 position)
        {
            Point positionTile = position.ToTileCoordinates();
            Rectangle domainRect = new Rectangle(positionTile.X-50, positionTile.Y+2, 100, 10); // Creates a rectangle in Tile Coordinates
            for (int x = domainRect.X; x < domainRect.X + domainRect.Width; x++) // For every x in the rectangle
            {
                for(int y = domainRect.Y; y > domainRect.Y - domainRect.Height; y--) // For every y in the rectangle
                {
                    Tile tile = Main.tile[x, y];
                    // If the tile is: Not air, solid, and not top solid (like platforms)
                    if (tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType])
                    {
                        Main.NewText("Invalid domain region! You must be in a 100x10 area with non solid tiles!");
                        return;
                    }
                }
            }


            // Make sure no other invasion is occuring right now
            if (Main.invasionType == 0)
            {
                Main.NewText("The custom invasion is starting.......", new Color(175, 75, 255));

                DomainWorld.domainActive = true;
                activeDomain = DomainList.GetList(domain); // Get the correct domain list
                domainReward = DomainList.GetReward(reward); // Get the correct domain reward

                // Area of the domain is a rectangle in world coordinates
                // 1 Tile coordinate = 16 world coordinates
                domainRectangle = new Rectangle(domainRect.X*16, domainRect.Y*16, 1600, 160);
                enemiesKilled = 0;

                foreach(Player p in Main.player)
                {
                    if (PlayerInDomainArea(p)) playersInDomain.Add(p);
                }

                Main.invasionType = -1; //Not going to be using an invasion that is positive since those are vanilla invasions
                Main.invasionSize = GetSizeOfDomainList(activeDomain);
                Main.invasionSizeStart = Main.invasionSize;
                Main.invasionProgress = 0;               
                Main.invasionProgressWave = 1;
                Main.invasionProgressMax = Main.invasionSizeStart;
                Main.invasionProgressIcon = 0 + 3;
                Main.invasionWarn = 3600; //This doesn't really matter, as this does not work
                Main.invasionX = position.X; 

                SpawnWaveEnemies();

                Main.NewText(Main.invasionSize);
            }
        }

        public static void EndDomain()
        {
            DomainWorld.domainActive = false;
            enemiesKilled = 0;
            Main.invasionType = 0;
            playersInDomain.Clear();

            // When a domain ends, kill all of the enemies
            foreach(NPC npc in activeDomainEnemies)
            {
                npc.active = false;
                npc.life = 0;
            }
            activeDomainEnemies.Clear();
        }

        public static void GiveRewards()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                foreach(Player p in playersInDomain)
                {
                    foreach (int[] itemDrop in domainReward)
                    {
                        // Spawns a new item on the player, with the specified amount
                        Item.NewItem(Item.GetSource_NaturalSpawn(), p.getRect(), itemDrop[0], itemDrop[1]);
                    }
                }              
            }
        }

        public static void CheckDomainProgress()
        {
            int icon = 0;
            Main.ReportInvasionProgress(Main.invasionProgress, Main.invasionSize, icon, Main.invasionProgressWave);

            //Syncing start of invasion
            foreach (Player p in Main.player)
            {
                NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null, Main.invasionProgress, Main.invasionSize, icon, Main.invasionProgressWave, 0, 0, 0);
            }
        }

        /// <summary>
        /// Spawn all the enemies in a specific wave
        /// </summary>
        public static void SpawnWaveEnemies()
        {
            activeDomainEnemies.Clear();
            int spawnPosIndex = 0;
            int spawnPosOffset = 24; // Set how many tiles away the enemies will spawn at based on the center of the arean
            foreach (int enemy in activeDomain[Main.invasionProgressWave - 1])
            {
                // (50+spawnPosOffset)*16) needed because rectangles start at the bottom left, not at the center, so we need to convert
                int index = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), domainRectangle.X+((50+spawnPosOffset)*16), domainRectangle.Y, enemy);
                NPC npc = Main.npc[index];
                npc.timeLeft = 1000;

                spawnPosOffset *= -1; // Swap side around the domain's center location for every enemy
                if (spawnPosIndex % 2 != 0) // For every other enemy, we have to push their spawn backwards by an offset 
                {
                    spawnPosOffset += 8; // Other enemies will be 8 tiles away from each other
                }
                spawnPosIndex++;

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }

                activeDomainEnemies.Add(npc);
            }
        }

        /// <summary>
        /// Returns whether or not the player is in the specified domain area
        /// </summary>
        public static bool PlayerInDomainArea(Player p)
        {
            return domainRectangle.Intersects(p.getRect());
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

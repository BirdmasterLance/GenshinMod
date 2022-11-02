using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace GenshinMod.Invasions
{
    internal class DomainNPC : GlobalNPC
    {
        // When an enemy dies, and that enemy is the correct type 
        // decrement the invasion progress by 1
        public override void OnKill(NPC npc)
        {
            if(DomainWorld.domainActive)
            {
                foreach (int enemy in DomainInvasion.activeDomain[Main.invasionProgressWave-1])
                {
                    if (npc.type == enemy)
                    {
                        Main.invasionProgress += 1;
                        DomainInvasion.enemiesKilled += 1;
                        
                        // Increments the wave counter when the correct enemy dies 
                        if (DomainInvasion.activeDomain[Main.invasionProgressWave-1].Length == DomainInvasion.enemiesKilled && Main.invasionProgressWave < DomainInvasion.activeDomain.Count)
                        {
                            Main.invasionProgressWave += 1;
                            DomainInvasion.enemiesKilled = 0;
                            DomainInvasion.SpawnWaveEnemies();
                        }
                        return;
                    }
                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            //Change spawn stuff if invasion up and player is in invasion zone
            if (DomainWorld.domainActive && DomainInvasion.PlayerInDomainArea(player))
            {
                spawnRate = 1000; // Higher number means less chance to spawn
                maxSpawns = 10000; //Max spawns of NPCs depending on NPC value
            }
        }
    }
}

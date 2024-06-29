using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
    public enum ArtifactType
    {
        Flower,
        Plume,
        Sands,
        Goblet,
        Circlet
    }

    // To save which artfact set an artifact belogns to
    // So we only have to check this enum, rather than what class it is
    public enum ArtifactSet
    {
        GildedDreams,
        MarechausseeHunter
    }

    // Overloading the ModItem with an Artifact type
    // So we can still have Terraria items with data
    // normally not found in a standard ModItem.
    public abstract class Artifact : ModItem
    {
        protected ArtifactSet artifactSet;
        protected ArtifactType artifactType;

        protected ItemStats mainStat;
        protected int mainStatValue;

        protected List<(ItemStats, int)> subStats = new List<(ItemStats, int)>();

        public ItemStats GetMainStat() { return mainStat; }
        public double GetMainStatValue() { return mainStatValue; }

        public List<(ItemStats, int)> GetSubStats() { return subStats; }
    }
}

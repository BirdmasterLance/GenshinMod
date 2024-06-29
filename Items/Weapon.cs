using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{

    public enum WeaponType
    {
        Sword,
        Claymore,
        Polearm,
        Bow,
        Catalyst
    }

    // To save which artfact set an artifact belogns to
    // So we only have to check this enum, rather than what class it is
    public enum WeaponSet
    {
        LostPrayerToSacredWinds,
        KagurasVerity
    }

    public abstract class Weapon : ModItem
    {
        // C# defaults to private, so we make it protected so this and child classes can access it
        protected WeaponSet weaponSet;
        protected WeaponType weaponType;

        // Technically on the wiki its called "Secondary Stat" because the main stat is always attack up
        protected ItemStats mainStat;
        protected double mainStatValue;

        protected int refinementLevel = 1;

        public ItemStats GetMainStat() { return mainStat; }
        public double GetMainStatValue() { return mainStatValue; }
    }
}

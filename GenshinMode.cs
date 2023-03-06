using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.States;
using OnUIWorldCreation = On.Terraria.GameContent.UI.States.UIWorldCreation; // Simplify name
using ModDifficultyLibrary;

// Interesting Resource:
// https://github.com/Ikersfletch/ModDifficultyLibrary 

// NOTE: On.Terraria.* are detours and IL.Terraria.* are injections
// This class uses On.Terraria to reroute the original vanilla method with our own

namespace GenshinMod
{
    [ExtendsFromMod("ModDifficultyLibrary")]
    internal class GenshinModeSystem : ModDifficulty
    {
        public override string DisplayName => "Genshin";
        public override string Description => "Enables Genshin features in this world.";
        //public override void OnModLoad()
        //{
        //    //// Make a new GameModeData for the game to use
        //    //GameModeData genshinMode = new GameModeData
        //    //{
        //    //    Id = 4, // The first ID Terraria doesn't use
        //    //    EnemyMaxLifeMultiplier = 1f,
        //    //    EnemyDamageMultiplier = 1f,
        //    //    DebuffTimeMultiplier = 1f,
        //    //    KnockbackToEnemiesMultiplier = 1f,
        //    //    TownNPCDamageMultiplier = 1f,
        //    //    EnemyDefenseMultiplier = 1f,
        //    //    EnemyMoneyDropMultiplier = 1f
        //    //};

        //    //Main.RegisteredGameModes.Add(4, genshinMode);

        //    //OnUIWorldCreation.BuildPage += BuildPage;

        //}

        // public void BuildPage(OnUIWorldCreation.orig_BuildPage orig, UIWorldCreation self) {
        //    // this reroute just makes the world creation screen larger.
        //    /*
        //    MethodInfo info = self.GetType().GetMethod("SetDefaultOptions", yes);
        //    byte[] arr = info.GetMethodBody().GetILAsByteArray();
        //    string printme = "";
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        printme += arr[i].ToString("X2")+"\n";
        //    }
        //    Mod.Logger.Info(printme);
        //    */

        //    /*
        //    */
        //    orig(self);
        //    //creation_screen = self;
        //    UIElement baseElement = self.Children.First<UIElement>();
        //    baseElement.Width = StyleDimension.FromPercent(0.9f);
        //    baseElement.Height = StyleDimension.FromPixels(434f + 18);
        //    baseElement.Top = StyleDimension.FromPixels(170f - 18);
        //    baseElement.HAlign = 0.5f;
        //    baseElement.VAlign = 0f;

        //}
    }

}

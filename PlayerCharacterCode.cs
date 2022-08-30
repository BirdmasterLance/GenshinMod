using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GenshinMod
{
    class PlayerCharacterCode : ModPlayer
    {
        private List<string> characters;
        public string activeCharacter;

        public bool replaceTexture; // Checks if we want to replace the texture

        public override void ResetEffects()
        {
            // replaceTexture = false;
        }

        public override void Initialize()
        {
            characters = new List<string>();
        }

        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            if(tag.ContainsKey("characters"))
            {
                tag.Add("characters", characters);
            }
            else
            {
                tag.Set("characters", characters);
            }

            if (tag.ContainsKey("activeChar"))
            {
                tag.Add("activeChar", activeCharacter);
            }
            else
            {
                tag.Set("activeChar", activeCharacter);
            }
        }

        public override void LoadData(TagCompound tag)
        {
            characters = (List<string>)tag.GetList<string>("characters");
            activeCharacter = tag.Get<string>("activeChar");
        }

        // Get the list of the characters the player has
        public List<string> GetCharacters()
        {
            return characters;
        }

        public void ChangeActiveCharacter(string character)
        {
            activeCharacter = character;
            replaceTexture = true;
            Main.NewText($"Your active character is: {activeCharacter}");

            // Handles equipping the correct texture for the character
            int equipSlotHead = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Head);
            int equipSlotBody = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Body);
            int equipSlotLegs = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Legs);

            // Determines whether we show the head, body, and legs
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
            ArmorIDs.Body.Sets.HidesBottomSkin[equipSlotBody] = true;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
            ArmorIDs.Legs.Sets.HidesTopSkin[equipSlotLegs] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;         
            ArmorIDs.Legs.Sets.OverridesLegs[equipSlotLegs] = true;
        }

        public void RemoveActiveCharacter()
        {
            replaceTexture = false;
            activeCharacter = "None";
            Main.NewText($"Removed active character");
        }



        //public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        //{
        //    base.OnHitNPC(item, target, damage, knockback, crit);
        //}

        //public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        //{
        //    //Pyro damage
        //    if (proj.type == ProjectileID.FireArrow || proj.type == ProjectileID.Flamarang || proj.type == ProjectileID.Sunfury
        //        || proj.type == ProjectileID.Flamelash || proj.type == ProjectileID.HellfireArrow || proj.type == ProjectileID.Flames
        //        || proj.type == ProjectileID.CursedFlameFriendly
        //        || proj.type == ProjectileID.RocketI || proj.type == ProjectileID.RocketII || proj.type == ProjectileID.RocketIII || proj.type == ProjectileID.RocketIV
        //        || proj.type == ProjectileID.GrenadeI || proj.type == ProjectileID.GrenadeII || proj.type == ProjectileID.GrenadeIII || proj.type == ProjectileID.GrenadeIV
        //        || proj.type == ProjectileID.ProximityMineI || proj.type == ProjectileID.ProximityMineII || proj.type == ProjectileID.ProximityMineIII || proj.type == ProjectileID.ProximityMineIV
        //        || proj.type == ProjectileID.Flare || proj.type == ProjectileID.InfernoFriendlyBlast || proj.type == ProjectileID.InfernoFriendlyBolt)
        //    {
        //        //target.AddBuff(ModContent.BuffType<Buffs.ElementPyro>(), 10);
        //    }
        //}

        //// If the accessory is in the vanity slot, do stuff
        //public override void UpdateVisibleVanityAccessories()
        //{
        //    // Go through all the accessory slots
        //    for (int n = 13; n < 18 + Player.extraAccessorySlots; n++)
        //    {
        //        Item item = Player.armor[n];
        //        if (item.type == ModContent.ItemType<CharacterCostume>())
        //        {
        //            replaceTexture = true;
        //        }
        //    }
        //}

        // If the accessory is equipped, do stuff
        public override void UpdateEquips()
        {
        }

        // Handles replacing the player animations
        // According to TModLoader, this method does not run when the game is paused
        // This means that the changed character will not show when the game is paused (whenever the game is frozen)
        public override void FrameEffects()
        {
            if(replaceTexture)
            {
                // Replaces the player's head/body/legs with the correct equipment
                // We find the equipment based on its name when we loaded it all in GenshinTest.cs
                //var exampleCostume = ModContent.GetInstance<CharacterCostume>();
                Player.head = EquipLoader.GetEquipSlot(Mod, activeCharacter, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, activeCharacter, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, activeCharacter, EquipType.Legs);
            }
        }
    }
}

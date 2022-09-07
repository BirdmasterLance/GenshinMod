using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GenshinMod
{
    class PlayerCharacterCode : ModPlayer
    {
        private List<Character> characters;
        public List<Character> partyCharacters;
        public Character activeCharacter;

        public bool replaceTexture; // Checks if we want to replace the texture

        public override void ResetEffects()
        {
            // replaceTexture = false;
        }

        public override void Initialize()
        {
            characters = new List<Character>();
            partyCharacters = new List<Character>();
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

            if(tag.ContainsKey("partyCharacters"))
            {
                tag.Add("partyCharacters", partyCharacters);
            }
            else
            {
                tag.Set("partyCharacters", partyCharacters);
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
            characters = (List<Character>)tag.GetList<Character>("characters");
            partyCharacters = (List<Character>)tag.GetList<Character>("partyCharacters");
            activeCharacter = tag.Get<Character>("activeChar");
        }

        /// <summary>
        /// Get the string List of all the characters a player has.
        /// </summary>
        public List<Character> GetCharacters()
        {
            return characters;
        }

        /// <summary>
        /// Returns the character class based on the name provided.
        /// </summary>
        public Character GetCharacter(string character)
        {
            foreach(Character c in characters)
            {
                if(c.Name == character)
                { return c; }
            }
            return null;
        }

        /// <summary>
        /// Returns true or false depending on if the player has said character.
        /// </summary>
        public bool HasCharacter(string character)
        {
            foreach(Character c in characters)
            {
                if(c.Name == character)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a character to the player's list of characters, if they don't have it.
        /// </summary>
        public bool AddCharacter(string character)
        {
            if (HasCharacter(character))
            {
                return false; // TODO: let the game/player know that they can raise the constellation of this character at this point
            }
            else
            {
                characters.Add(new Character(character));
                return true;
            }
        }

        /// <summary>
        /// Removes a character from the player's list of characters, if they have it.
        /// </summary>
        public bool RemoveCharacter(string character)
        {
            if (!HasCharacter(character))
            {
                return false;
            }
            else
            {

                characters.Remove(GetCharacter(character));
                return true;
            }
        }

        public List<Character> GetPartyCharacters()
        {
            return partyCharacters;
        }

        public void ChangePartyCharacters(string character, int slot)
        {
            if (slot >= 4) return;
            if(character == "Remove" || character == "None")
            {
                partyCharacters.RemoveAt(slot);
            }
            if (!HasCharacter(character)) return;
            partyCharacters.Insert(slot, GetCharacter(character));
        }

        /// <summary>
        /// Changes the player's active character, as well as change their sprite.
        /// </summary>
        public bool ChangeActiveCharacter(string character)
        {
            if (!HasCharacter(character))
            {
                return false;
            }
            activeCharacter = GetCharacter(character);
            replaceTexture = true;
            Main.NewText($"Your active character is: {activeCharacter.Name}");

            // TODO: when sprites are added, enable all of this code

            // Handles equipping the correct texture for the character
            //int equipSlotHead = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Head);
            //int equipSlotBody = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Body);
            //int equipSlotLegs = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Legs);

            //// Determines whether we show the head, body, and legs
            //ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            //ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
            //ArmorIDs.Body.Sets.HidesBottomSkin[equipSlotBody] = true;
            //ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
            //ArmorIDs.Legs.Sets.HidesTopSkin[equipSlotLegs] = true;
            //ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;         
            //ArmorIDs.Legs.Sets.OverridesLegs[equipSlotLegs] = true;

            return true;
        }

        public void RemoveActiveCharacter()
        {
            replaceTexture = false;
            activeCharacter = null;
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
                Player.head = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Legs);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.ModLoader.IO;

namespace Naruto
{
	// This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
	public class MyPlayer : ModPlayer
	{

		public static MyPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<MyPlayer>();
		}


		public int QuestLVL = 0;





		#region chakra


		#region Variables 
		public bool Fragment1;
		public bool Fragment2;
		public bool Fragment3;
		public bool Fragment4;
		public bool Fragment5;

		public int KiLevel = 0;
		public static ModHotKey EnergyCharge;

		// Vanilla only really has damage multipliers in code
		// And crit and knockback is usually just added to
		// As a modder, you could make separate variables for multipliers and simple addition bonuses
		public float chakraDamageAdd;
		public float chakraDamageMult = 1f;
		public float chakraKnockback;
		public int chakraCrit;

		// Here we include a custom resource, similar to mana or health.
		// Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
		public int chakraResourceCurrent;
		public const int DefaultChakraResourceMax = 1000;
		public int chakraResourceMax;
		public int chakraResourceMax2;
		public bool chakraResourceFull;
		public float chakraResourceRegenRate = -10;
		internal int chakraResourceRegenTimer = -10;
		public static readonly Color HealChakraResource = new Color(187, 91, 201); // We can use this for CombatText, if you create an item that replenishes exampleResourceCurrent.

		/*
		In order to make the Example Resource example straightforward, several things have been left out that would be needed for a fully functional resource similar to mana and health. 
		Here are additional things you might need to implement if you intend to make a custom resource:
		- Multiplayer Syncing: The current example doesn't require MP code, but pretty much any additional functionality will require this. ModPlayer.SendClientChanges and clientClone will be necessary, as well as SyncPlayer if you allow the user to increase exampleResourceMax.
		- Save/Load and increased max resource: You'll need to implement Save/Load to remember increases to your exampleResourceMax cap.
		- Resouce replenishment item: Use GlobalNPC.NPCLoot to drop the item. ModItem.OnPickup and ModItem.ItemSpace will allow it to behave like Mana Star or Heart. Use code similar to Player.HealEffect to spawn (and sync) a colored number suitable to your resource.
		*/
		#endregion








		
	
	
	
		/*
		In order to make the Example Resource example straightforward, several things have been left out that would be needed for a fully functional resource similar to mana and health. 
		Here are additional things you might need to implement if you intend to make a custom resource:
		- Multiplayer Syncing: The current example doesn't require MP code, but pretty much any additional functionality will require this. ModPlayer.SendClientChanges and clientClone will be necessary, as well as SyncPlayer if you allow the user to increase exampleResourceMax.
		- Save/Load and increased max resource: You'll need to implement Save/Load to remember increases to your exampleResourceMax cap.
		- Resouce replenishment item: Use GlobalNPC.NPCLoot to drop the item. ModItem.OnPickup and ModItem.ItemSpace will allow it to behave like Mana Star or Heart. Use code similar to Player.HealEffect to spawn (and sync) a colored number suitable to your resource.
		*/

		public override void Initialize()
		{
			chakraResourceMax = DefaultChakraResourceMax;
		}

		public override void ResetEffects()
		{
			ResetVariables();
		}

		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			chakraDamageAdd = 0f;
			chakraDamageMult = 1f;
			chakraKnockback = 0f;
			chakraCrit = 0;
			chakraResourceRegenRate = 1f;
			chakraResourceMax2 = chakraResourceMax;
		}

		public override void PostUpdateMiscEffects()
		{
			UpdateResource();
		}

		// Lets do all our logic for the custom resource here, such as limiting it, increasing it and so on.
		private void UpdateResource()
		{
			
			// For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
			//Increase it by 60 per second, or 1 per tick.

			// A simple timer that goes up to 3 seconds, increases the exampleResourceCurrent by 1 and then resets back to 0.
			if (chakraResourceRegenTimer > 1 * chakraResourceRegenRate)
			{
				chakraResourceCurrent += 5;
				chakraResourceRegenTimer = 0;
			}


			// Limit exampleResourceCurrent from going over the limit imposed by exampleResourceMax.
			chakraResourceCurrent = Utils.Clamp(chakraResourceCurrent, 0, chakraResourceMax2);
		}

        #endregion



        #region Load&Save
        public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();
			tag.Add("KiLevel", KiLevel);
			tag.Add("Fragment1", Fragment1);
			tag.Add("Fragment2", Fragment2);
			tag.Add("Fragment3", Fragment3);
			tag.Add("Fragment4", Fragment4);
			tag.Add("Fragment5", Fragment5);
			tag.Add("chakraResourceCurrent", chakraResourceCurrent);
			tag.Add("DefaultChakraResourceMax", DefaultChakraResourceMax);
			tag.Add("chakraResourceMax", chakraResourceMax);
			tag.Add("chakraResourceMax2", chakraResourceMax2);
			tag.Add("chakraResourceFull", chakraResourceFull);
			tag.Add("chakraResourceRegenRate", chakraResourceRegenRate);
			tag.Add("chakraResourceRegenTimer", chakraResourceRegenTimer);
			tag.Add("QuestLVL", QuestLVL)


			return tag;
		}

		public override void Load(TagCompound tag)
		{
			KiLevel = tag.Get<int>("KiLevel");
			Fragment1 = tag.Get<bool>("Fragment1");
			Fragment2 = tag.Get<bool>("Fragment2");
			Fragment3 = tag.Get<bool>("Fragment3");
			Fragment4 = tag.Get<bool>("Fragment4");
			Fragment5 = tag.Get<bool>("Fragment5");
			chakraResourceMax = tag.Get<int>("kiResourceMax");

		}

		#endregion


		#region Controls

		public static ModHotKey ActivateSharingan;
		public static ModHotKey ActivateTailedBeastMode;
		public static ModHotKey ChargeChakra;
		public static ModHotKey Susano;


		#endregion

	}
}
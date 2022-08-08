using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using System.IO;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using Naruto.Items;

namespace Naruto.NPCs
{

    [AutoloadHead]
	public class WindNPC : ModNPC
	{

		

		public override string Texture => "Naruto/NPCs/WindNPC";


		public int QuestLVL = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind Style Ninja");
			Main.npcFrameCount[npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 1500;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 25;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Guide);
			npc.townNPC = true;
			npc.friendly = true;
			npc.aiStyle = 7;
			npc.damage = 40;
			drawOffsetY = -2;
			npc.defense = 30;
			npc.lifeMax = 550;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.knockBackResist = 0.4f;
			animationType = NPCID.Guide;
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			if(NPC.downedBoss1)
            {
				return true;
            }
			return false;
		}

		public override string TownNPCName()
		{
			string[] names = { "Temari" };
			return Main.rand.Next(names);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire);
			}
		}

		public override void FindFrame(int frameHeight)
		{
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}


		public override string GetChat()
		{
		
			switch (Main.rand.Next(7))
			{
				case 0:
					return "I am Temari of the Sand and can teach you how to use wind chakra nature!";
				case 1:		
						return "Wind Style is not easy to master you know!";	
				case 2:
					return "I don't do this for free...";
				case 3:
					return "You must helpdefeat Madara!";
				case 4:			
						return "Blah blah blah...";
				case 5:	
						return "Idk what else to put lol.";
				case 6:
					return "There is nothing to say.";
				default:
					return "Go do stuff";
			}
		}



        public override void SetChatButtons(ref string button, ref string button2)
        {
			button = ("Learn Wind Style");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if(firstButton)
            {
				if(QuestLVL == 0)
                {
					Main.npcChatText = "So you wish to learn wind style, your first task is to bring me 5 copper bar.";
					QuestLVL = 1;
                }

				else if(QuestLVL == 1)
                {
					if (Main.LocalPlayer.HasItem(ItemType<Sharingan>()))
                    {
						Main.npcChatText = "Well done! Your next task is to get me another one.";
						QuestLVL = 2;
					}

					


				else if (QuestLVL == 2)
                    {
						if (Main.LocalPlayer.HasItem(ItemType<Sharingan>()))
						{
							Main.npcChatText = "Well done! Your final task is to get me another one.";
							QuestLVL = 3;
						}

					}

					else if (QuestLVL == 3)
					{
						if (Main.LocalPlayer.HasItem(ItemType<Sharingan>()))
						{
							Main.npcChatText = "Well done! I will now give you a scroll to learn new wind moves!.";
							QuestLVL = 0;
						}

					}




				}
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 200;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 5;
			randExtraCooldown = 5;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
		{
			projType = !Main.hardMode ? ProjectileID.InfernoFriendlyBolt : ProjectileID.InfernoFriendlyBolt;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 7f;
		}



	}
}
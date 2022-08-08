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

namespace Naruto
{
	public class Naruto : Mod
	{

        public override void Unload()
        {
            
        }

        public override void Load()
        {


            MyPlayer.ActivateSharingan = RegisterHotKey("Activate Sharingan", "Z");
            MyPlayer.ActivateTailedBeastMode = RegisterHotKey("Activate Tailed Beast Mode", "X");
            MyPlayer.ChargeChakra = RegisterHotKey("Charge Chakra", "C");
            MyPlayer.Susano = RegisterHotKey("Susanoo", "G");

     
            


        }

    }
}
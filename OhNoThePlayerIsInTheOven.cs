using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WeaponOven
{
	internal class OhNoThePlayerIsInTheOven : ModPlayer
	{
		public static OhNoThePlayerIsInTheOven LocalPlayer => Main.LocalPlayer.GetModPlayer<OhNoThePlayerIsInTheOven>();
		public int timeSinceOpen = 1;
	}
}

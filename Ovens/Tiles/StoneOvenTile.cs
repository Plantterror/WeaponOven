using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WeaponOven
{
	public class StoneOvenTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			
		}
		public override void PostSetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = false;
			Main.tileLighted[Type] = true;
			Main.tileBlockLight[Type] = false;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			ItemDrop = ModContent.ItemType<StoneOven>();
			AddMapEntry(new Color(100, 100, 100));
			TileObjectData.addTile(Type);
		}
	}
}
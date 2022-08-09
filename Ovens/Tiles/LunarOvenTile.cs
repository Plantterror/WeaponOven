using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WeaponOven.Ovens.Items;

namespace WeaponOven.Ovens.Tiles
{
	public class LunarOvenTile : ModTile
	{
		public override void PostSetDefaults()
		{
			//misc properties
			Main.tileSolid[Type] = false;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true;
			//set boundaries of the tile when placing
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			//displaying the name on the map and it's colour
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Lunar Oven");
			AddMapEntry(new Color(128, 85, 0), name);
			//animation frame stuff used later in code
			AnimationFrameHeight = 36;
		}
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			// Spend 30 ticks on each of 4 frames, looping
			frameCounter++;
			if (++frameCounter >= 30)
			{
				frameCounter = 0;
				frame = ++frame % 4;
			}

		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<LunarOven>());
		}
	}
}
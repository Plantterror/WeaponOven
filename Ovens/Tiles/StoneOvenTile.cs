using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WeaponOven.Ovens.Items;
using WeaponOven;
using WeaponOven.UI;
using Terraria.GameContent.ObjectInteractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeaponOven.Ovens.Tiles
{
	public class StoneOvenTile : ModTile
	{
		public override void PostSetDefaults()
		{
			Main.tileSolid[Type] = false; //tile can be moved through
			Main.tileNoAttach[Type] = true; 
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true; //frameimportants are usually furniture
			TileID.Sets.DisableSmartCursor[Type] = true;
			//set boundaries of the tile when placing
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			//displaying the name on the map and it's colour
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Stone Oven");
			AddMapEntry(new Color(100, 100, 100), name);
			//height of each frame in the sprite
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
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<StoneOven>());
		}
		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			//Is the player already in the oven UI?
			if (OvenUISystem.instance.oveninterface.CurrentState == null) 
			{
				Main.mouseRightRelease = false;
				base.RightClick(i, j);
				//Close any previous UIs the player may be in
				if (player.sign > -1)
				{
					player.sign = -1;
					Main.editSign = false;
					Main.npcChatText = string.Empty;
				}

				if (Main.editChest)
				{
					Main.editChest = false;
					Main.npcChatText = string.Empty;
				}

				if (player.editedChestName)
				{
					NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
					player.editedChestName = false;
				}

				if (player.talkNPC > -1)
				{
					player.SetTalkNPC(-1);
					Main.npcChatCornerItem = 0;
					Main.npcChatText = string.Empty;
				}
				bool hadChestOpen = player.chest != -1;
				player.chest = -1;
				Main.stackSplit = 600;

				OvenUISystem.instance.SetUI(true);
			}

			return true;
		}
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if(OvenUISystem.instance.oveninterface.CurrentState != null)
			{
				{
					//lookup player coordinates
					var PlayerPos = Main.LocalPlayer.position.ToTileCoordinates();
					//if the player walks away from the oven...
					if (PlayerPos.X - i > 7
					   || PlayerPos.X - i < -7
					   || PlayerPos.Y - j > 6
					   || PlayerPos.Y - j < -6)
					{//...close the UI
						OvenUISystem.instance.SetUI(false);
					}
				}
			}
		}
	}
}
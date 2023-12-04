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
			Main.tileNoAttach[Type] = true;  //tile does not attach to other blocks like dirt
			Main.tileLavaDeath[Type] = true; //tile dies by lava
			Main.tileFrameImportant[Type] = true; //frameimportants are usually furniture
			TileID.Sets.DisableSmartCursor[Type] = true;
			//set boundaries of the tile when placing
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			//displaying the name on the map and its colour
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Stone Oven");
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
		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			//Is the player already in the oven UI?
			if (OvenUISystem.Instance.oveninterface.CurrentState == OvenUISystem.Instance.ovenUI)
			{
				//close the UI since the player is already in the ui and clicked on the tile again
				OvenUISystem.SetUI(false);

			}
			else
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
				player.chest = -1;
				Main.stackSplit = 600;


				OhNoThePlayerIsInTheOven.LocalPlayer.timeSinceOpen = 0;
				OvenUISystem.SetUI(true);
			}
			return true;
		}
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if(OvenUISystem.Instance.oveninterface.CurrentState != null)
			{
				
					//lookup player coordinates
					var PlayerPos = Main.LocalPlayer.position.ToTileCoordinates();
					//if the player walks away from the oven...
					if (PlayerPos.X - i + 1 > Player.tileRangeX //too far right
					|| i - PlayerPos.X - 1 > Player.tileRangeX //too far left
					|| PlayerPos.Y - j - 1 > Player.tileRangeY //too far down
					|| j - PlayerPos.Y + 1 > Player.tileRangeY) //too far up
					{//...close the UI
						OvenUISystem.SetUI(false);
					}
				if (OhNoThePlayerIsInTheOven.LocalPlayer.timeSinceOpen < 1)
				{
					OhNoThePlayerIsInTheOven.LocalPlayer.Player.SetTalkNPC(-1);
					Main.playerInventory = true;
					OhNoThePlayerIsInTheOven.LocalPlayer.timeSinceOpen++;
				}

				if (OhNoThePlayerIsInTheOven.LocalPlayer.Player.chest != -1 || !Main.playerInventory || OhNoThePlayerIsInTheOven.LocalPlayer.Player.sign > -1 || OhNoThePlayerIsInTheOven.LocalPlayer.Player.talkNPC > -1)
				{ //If the player is in a chest, closes the inventory, enters a sign, or talks to an npc, close the UI.
					OvenUISystem.SetUI(false);
					Recipe.FindRecipes();
				}//TODO: close when other modded UIs open up?
			}
		}
	}
}
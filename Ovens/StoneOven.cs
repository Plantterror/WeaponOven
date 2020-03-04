using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WeaponOven.Ovens
{
	public class StoneOvenItem : ModItem
	{
		public override void SetDefaults()
		{
			item.consumable = true;
			item.createTile = mod.TileType("StoneOvenTile");
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.value = Item.buyPrice(0, 0, 0, 20);
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Oven");
			Tooltip.SetDefault("Place an item inside to increase it's damage over time!\nWait approximately 1 hour for maximum damage");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 20);
			recipe.AddIngredient(ItemID.Torch, 3);
			recipe.AddIngredient(ItemID.Coal);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 20);
			recipe.AddIngredient(ItemID.Torch, 3);
			recipe.AddRecipeGroup("Wood", 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class StoneOvenTile : ModTile
	{

		public override bool NewRightClick(int i, int j) => throw new NotImplementedException();
		public override bool HasSmartInteract() => true;
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.WaterPlacement = Terraria.Enums.LiquidPlacement.NotAllowed;
			TileObjectData.addTile(Type);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{

		}
	}
}

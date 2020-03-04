using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WeaponOven.Ovens
{
	public class LunarOvenItem : ModItem
	{
		public override void SetDefaults()
		{
			item.consumable = true;
			item.createTile = mod.TileType("LunarOvenTile");
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.value = Item.buyPrice(0, 25, 25, 0);
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Oven");
			Tooltip.SetDefault("Place an item inside to increase it's damage over time!\nWait approximately 1 minute for maximum damage\nAt some point, you just wanna know where it all went wrong.");
			//1 minute cooldown is wack, not gonna lie
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PlantOvenItem"));
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient(ItemID.UltrabrightTorch, 3);
			recipe.AddRecipeGroup("Fragment", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class LunarOvenTile : ModTile
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
			TileObjectData.newTile.WaterPlacement = Terraria.Enums.LiquidPlacement.Allowed;
			TileObjectData.newTile.LavaPlacement = Terraria.Enums.LiquidPlacement.Allowed;
			TileObjectData.addTile(Type);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{

		}
	}
}

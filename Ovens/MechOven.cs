using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace WeaponOven.Ovens
{
	public class MechOvenItem : ModItem
	{
		public override void SetDefaults()
		{
			item.consumable = true;
			item.createTile = mod.TileType("MechOvenTile");
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Oven");
			Tooltip.SetDefault("Place an item inside to increase it's damage over time!\nWait approximately 35 minutes for maximum damage\nThe Industrial Revolution strikes again!");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("HellOvenItem"));
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.CursedTorch, 3);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("HellOvenItem"));
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.IchorTorch, 3);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class MechOvenTile : ModTile
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
			Item.NewItem(i * 16, j * 16, 48, 32, ItemType<MechOvenItem>());
		}
	}
}

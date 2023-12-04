using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponOven.Ovens.Tiles;

namespace WeaponOven.Ovens.Items
{
	public class HellOven : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hellstone Oven");
			// Tooltip.SetDefault("Used to cook weapons and accessories\nRight click to open the GUI\nCooks 1.5x faster than a normal oven!");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.consumable = true;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.rare = ItemRarityID.Orange;
			Item.createTile = ModContent.TileType<HellOvenTile>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
			recipe.AddIngredient<StoneOven>();
			recipe.AddTile(TileID.Hellforge);
			recipe.Register();
		}
	}
}

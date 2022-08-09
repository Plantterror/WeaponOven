using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponOven
{
	public class HallowedOven : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Oven");
			Tooltip.SetDefault("Used to cook weapons and accessories\nRight click to open the GUI\nCooks 2x faster than a normal oven!");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Pink;
			Item.createTile = ModContent.TileType<HallowedOvenTile>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient<HellOven>();
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}

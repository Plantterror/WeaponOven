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
	public class LunarOven : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Oven");
			Tooltip.SetDefault("Used to cook weapons and accessories\nRight click to open the GUI\nCooks 5x faster than a normal oven!\n'Too much? Too bad.'");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Red;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient<ChloroOven>();
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace WeaponOven
{
	public class WeaponOvenModifier : GlobalItem
	{
		public bool CanBeCooked(Item item) => item.accessory = true || item.damage >= 1;
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CanBeCooked(item))
			{
				TooltipLine tooltipLine = new TooltipLine(mod, "CookAlert", "This item can be cooked.");
				tooltips.Add(tooltipLine);
			}
		}
	}
}

using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponOven
{
	public class WeaponOvenModifier : GlobalItem
	{
		public int IsCooked(Item item)
		{
			/*todo: make a float dictating how long it's been in an oven and tell from that how much it has cooked
			 * scaling from 0 to 8 probs, scales based on stone oven with each upgrade cutting the time
			 * 0 = uncooked
			 * 1 = 5 minutes
			 * 2 = 15 minutes
			 * 3 = 30 minutes
			 * 4 = 45 minutes
			 * 5 = 60 minutes (fully cooked)
			 * 6 = 65 minutes (overcooked past this point)
			 * 7 = 80 minutes
			 * 8 = 100 minutes
			 */
			return 0;
		}
		public bool CanBeCooked(Item item) => item.accessory == true && item.vanity == false || item.type != 0 && item.stack > 0 && item.useStyle > 0 && (item.damage > 0 || item.useAmmo > 0 && item.useAmmo != AmmoID.Solution);
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CanBeCooked(item))
			{
				switch (IsCooked(item))
				{
					case 1:
					case 2:
					case 3:
					case 4:
						TooltipLine tooltipLine = new TooltipLine(mod, "CookAlmost", "This item is almost done cooking! It needs " + "X" + " more minutes.");
						tooltips.Add(tooltipLine);
						break;
					case 5:
						tooltipLine = new TooltipLine(mod, "CookDone", "This item is fully cooked.");
						tooltips.Add(tooltipLine);
						break;
					case 6:
					case 7:
					case 8:
						tooltipLine = new TooltipLine(mod, "CookOver", "You have overcooked this weapon! It will deal less damage.");
						tooltips.Add(tooltipLine);
						break;
					default:
						tooltipLine = new TooltipLine(mod, "CookAlert", "This item can be cooked.");
						tooltips.Add(tooltipLine);
						break;
				}
			}
		}
	}
}
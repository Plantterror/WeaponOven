using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace WeaponOven
{
	public class WeaponOvenModifier : GlobalItem
	{
		public bool CanBeCooked(Item item) => item.accessory = true || item.damage != 0;
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{

		}
		public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
		{
			base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
		}
	}
}

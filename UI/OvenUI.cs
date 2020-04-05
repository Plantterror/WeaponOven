using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.GameContent.UI.Elements;

namespace WeaponOven.UI
{
	public class OvenUI : UIState
	{
		public OvenUI()
		{

		}
		internal static void GenerateNewUI() //Opening a new state of the oven
		{
			WeaponOven.instance.Interface.SetState(WeaponOven.instance.ovenUI);
		}
		internal static void CloseUI() //closing that state
		{
			WeaponOven.instance.Interface.SetState(null);
		}
		public override void OnInitialize()
		{
			UIPanel ovenpanel = new UIPanel(); //todo: check these values over
			ovenpanel.Width.Set(900, 0);
			ovenpanel.Height.Set(400, 0);
			ovenpanel.Left.Set(200, 0);
			ovenpanel.Top.Set(200, 0);
			ovenpanel.Append(ovenpanel);

			UIText starttext = new UIText("Place a weapon or an accessory inside to begin.", 2);
			ovenpanel.Append(starttext);
		}
	}
}

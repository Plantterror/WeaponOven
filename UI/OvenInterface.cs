using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using WeaponOven;
using WeaponOven.Ovens.Tiles;

namespace WeaponOven.UI
{
	class OvenInterface : UIState
	{
		public UIPanel panel;
		public override void OnInitialize()
		{
			//setup the main panel
			panel = new UIPanel();
			panel.SetPadding(0);
			panel.Left.Set(65f, 0f);
			panel.Top.Set(290f, 0f);
			panel.Width.Set(375, 0f);
			panel.Height.Set(150, 0f);
			panel.BackgroundColor = new Color(73, 94, 171);
			//setup the text displayed above the item slot
			var text = new UIText("testing")
			{
				HAlign = 0.5f,
				VAlign = 0.25f
			};
			panel.Append(text);
			//setup the item slot used to store the cooked weapon
			var VanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 170 },
				Top = { Pixels = 90 }
			};
			panel.Append(VanillaItemSlot);
			Append(panel);
		}
	}
}

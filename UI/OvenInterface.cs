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
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameInput;
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
			Item item = new Item();
			item.TurnToAir();
			/*
			UIItemSlot uiItemSlot = new(new Item[1] {item}, 0, ItemSlot.Context.BankItem);
			uiItemSlot.Left.Set(160f, 0f);
			uiItemSlot.Top.Set(70f,0f);
			uiItemSlot.Width.Set(TextureAssets.InventoryBack9.Value.Width,0f);
			uiItemSlot.Height.Set(TextureAssets.InventoryBack9.Value.Height,0f);
			panel.Append(uiItemSlot);*/

			UIItemSlots itemSlots = new(new ItemSlot(), TextureAssets.InventoryBack9, Color.Red);
			itemSlots.Left.Set(160f, 0f);
			itemSlots.Top.Set(70f,0f);
			itemSlots.Width.Set(TextureAssets.InventoryBack9.Value.Width,0f);
			itemSlots.Height.Set(TextureAssets.InventoryBack9.Value.Height,0f);
			panel.Append(itemSlots);

			Append(panel);
		}
	}
}

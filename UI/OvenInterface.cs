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
			panel = new UIPanel();
			panel.SetPadding(0);
			panel.Left.Set(65f, 0f);
			panel.Top.Set(290f, 0f);
			panel.Width.Set(375, 0);
			panel.Height.Set(200, 0);
			panel.BackgroundColor = new Color(73, 94, 171);

			UIText text = new UIText("testing");
			text.HAlign = 0.5f;
			text.VAlign = 0.2f;
			panel.Append(text);

			Append(panel);
		}
		public override void Update(GameTime gameTime)
		{
			if (!Main.playerInventory)
			{
				OvenUISystem.instance.SetUI(false);
			}
			base.Update(gameTime);
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}

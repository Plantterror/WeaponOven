using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using WeaponOven.UI;

namespace WeaponOven
{
	public class WeaponOven : Mod
	{
		public WeaponOven() { }
		internal static WeaponOven instance;
		internal UserInterface Interface;
		internal OvenUI ovenUI;
		public override void Load()
		{
			Interface = new UserInterface();
			ovenUI.Activate();
			instance = this;
		}
		public override void Unload()
		{
			Interface = null;
			ovenUI = null;
			instance = null;
		}
		private GameTime _lastUpdateUiGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUiGameTime = gameTime;
			if (Interface?.CurrentState != null)
			{
				Interface.Update(gameTime);
			}
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"WeaponOven: OvenUI",
					delegate
					{
						if (_lastUpdateUiGameTime != null && Interface?.CurrentState != null)
						{
							Interface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}
						return true;
					},
					   InterfaceScaleType.UI));
			}
		}
	}
}
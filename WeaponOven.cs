using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using WeaponOven.Ovens.Tiles;
using WeaponOven.UI;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace WeaponOven
{
	public class WeaponOven : Mod
	{

	}
	class OvenUISystem : ModSystem
	{
		internal UserInterface oveninterface;
		internal OvenInterface ovenUI;
		internal 
		static OvenUISystem Instance { get; private set; }
		public override void Load()
		{
			Instance=this;
			if (!Main.dedServ)
			{
				oveninterface = new UserInterface();

				ovenUI = new OvenInterface();
				ovenUI.Activate();
			}
		}

		public override void Unload()
		{
			Instance = null;
			oveninterface = null;
			ovenUI = null;
		}
		private GameTime _lastUpdateUiGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUiGameTime = gameTime;
			if (Instance.oveninterface?.CurrentState != null)
			{
				Instance.oveninterface.Update(gameTime);
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
						if (_lastUpdateUiGameTime != null && Instance.oveninterface?.CurrentState != null)
						{
							Instance.oveninterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}
						return true;
					},
					   InterfaceScaleType.UI));
			}
		}
		internal static void SetUI(bool open)
		{
			if (open == true)
			{
				Main.playerInventory = true;
				Instance.oveninterface.SetState(Instance.ovenUI);
				SoundEngine.PlaySound(SoundID.MenuOpen);
			}
			else
			{
				Instance.oveninterface.SetState(null);
				SoundEngine.PlaySound(SoundID.MenuClose);
			}
		}
	}
}
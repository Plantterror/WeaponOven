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
		internal static WeaponOven instance;
		
		public override void Load()
		{
			instance = this;
		}
		public override void Unload()
		{
			instance = null;
		}
		
	}
	class OvenUISystem : ModSystem
	{
		internal UserInterface oveninterface;
		internal OvenInterface ovenUI;
		internal 
		static OvenUISystem instance { get; private set; }
		public override void Load()
		{
			instance=this;
			if (!Main.dedServ)
			{
				oveninterface = new UserInterface();

				ovenUI = new OvenInterface();
				ovenUI.Activate();
			}
		}

		public override void Unload()
		{
			instance = null;
			oveninterface = null;
			ovenUI = null;
		}
		private GameTime _lastUpdateUiGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUiGameTime = gameTime;
			if (instance.oveninterface?.CurrentState != null)
			{
				instance.oveninterface.Update(gameTime);
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
						if (_lastUpdateUiGameTime != null && instance.oveninterface?.CurrentState != null)
						{
							instance.oveninterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}
						return true;
					},
					   InterfaceScaleType.UI));
			}
		}
		internal void SetUI(bool open)
		{
			if (open == true)
			{
				Main.playerInventory = true;
				instance.oveninterface.SetState(instance.ovenUI);
				SoundEngine.PlaySound(SoundID.MenuOpen);
			}
			else
			{
				instance.oveninterface.SetState(null);
				SoundEngine.PlaySound(SoundID.MenuClose);
			}
		}
	}
}
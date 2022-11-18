using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace WeaponOven.Ovens
{
    abstract class OvenEntity : ModTileEntity
    {
        private Ref<Item> _inputItem;
        public Item inputItem => _inputItem.Value;
        private int progression = 0;
        private string ovenName = "";
        public OvenEntity()
        {
            
        }

        public override void Update()
        {
            
        }

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
            TileObjectData data = TileObjectData.GetTileData(type, style);
            //not in a multiplayer server, place down at coordinates
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i - data.Origin.X, j - data.Origin.Y);
			return -1;
		}

		public enum CookStages
        {
            None,
            LightlyWarmed,
            Heated,
            Cooked,
            SlightlyBurnt,
            Darkened
        }
    }
}

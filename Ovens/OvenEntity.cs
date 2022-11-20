using System;
using System.Reflection;
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
using WeaponOven.Utilities;

namespace WeaponOven.Ovens
{
    abstract class OvenEntity : ModTileEntity
    {
		protected abstract Type TileType { get; }
		public static MethodInfo? TileTypeMethod;

        private Ref<Item> _inputItem;
        public Item InputItem => _inputItem.Value;

        private int progression = 0;
        protected int cooktimer;
        private string ovenName = "";
        public OvenEntity()
        {
            
        }
		public override void OnNetPlace()
		{
            //make room for item in tmod file?
			base.OnNetPlace();
		}
		public override void Update()
        {
            //check if there is an item in the oven
            if (InputItem != null)
            {
                //incrememnt the counter
                cooktimer++;
            }
        }
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
            int tiletype = (int)TileTypeMethod.MakeGenericMethod(TileType).Invoke(null, null);
            return tile.HasTile && tile.TileType == tiletype && TileUtils.GetTileOrigin(x, y) == Point16.Zero;
		}
		public override void OnKill()
		{
            //remove item from file?
			base.OnKill();
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
            TileObjectData data = TileObjectData.GetTileData(type, style);
            //not in a multiplayer server, place down at coordinates
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i - data.Origin.X, j - data.Origin.Y);

			NetMessage.SendTileSquare(Main.myPlayer, i - data.Origin.X, j - data.Origin.Y, data.Width, data.Height);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i - data.Origin.X, j - data.Origin.Y, Type);
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

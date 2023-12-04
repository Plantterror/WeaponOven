using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using static Terraria.UI.ItemSlot;
using Microsoft.Xna.Framework;
using Humanizer;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;

namespace WeaponOven.UI
{
    public class UIItemSlots : UIElement //please murder dradon thanks
    {
        public ItemSlot item { get; private set; }
        public Asset<Texture2D> Texture { get; set; }
        public Color inventoryBackground { get; set; }

        public UIItemSlots(ItemSlot itemSlot, Asset<Texture2D> texture, Color inventoryBackground)
        {
            item = itemSlot;
            Texture = texture;
            this.inventoryBackground = inventoryBackground;
        }

        public override void OnInitialize()
        {
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            HandleItem();
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            Draw(spriteBatch, item.item, position, inventoryBackground);
        }

        private void HandleItem()
        {
            if (!this.IsMouseHovering)
                return;

            Main.LocalPlayer.mouseInterface = true;
            Item inv = item.item;
            Terraria.UI.ItemSlot.OverrideHover(ref inv, 3);
            Terraria.UI.ItemSlot.LeftClick(ref inv, 3);
            Terraria.UI.ItemSlot.RightClick(ref inv, 3);
            Terraria.UI.ItemSlot.MouseHover(ref inv, 3);
            item.item = inv; 
        }

        public static float DrawItemIcon(Item item, int context, SpriteBatch spriteBatch, Vector2 screenPositionForItemCenter, float scale, float sizeLimit, Color environmentColor)
        {
            int num = item.type;
            if ((uint)(num - 5358) <= 3u)
                num = 5437;

            Main.instance.LoadItem(num);
            Texture2D value = TextureAssets.Item[num].Value;
            Rectangle frame = ((Main.itemAnimations[num] == null) ? value.Frame() : Main.itemAnimations[num].GetFrame(value));
            DrawItem_GetColorAndScale(item, scale, ref environmentColor, sizeLimit, ref frame, out var itemLight, out var finalDrawScale);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 origin = frame.Size() / 2f;

            if (!ItemLoader.PreDrawInInventory(item, spriteBatch, screenPositionForItemCenter, frame, item.GetAlpha(itemLight), item.GetColor(environmentColor), origin, finalDrawScale))
                goto SkipVanillaItemDraw;

            spriteBatch.Draw(value, screenPositionForItemCenter, frame, item.GetAlpha(itemLight), 0f, origin, finalDrawScale, effects, 0f);
            if (item.color != Color.Transparent)
            {
                Color newColor = environmentColor;
                if (context == 13)
                    newColor.A = byte.MaxValue;

                spriteBatch.Draw(value, screenPositionForItemCenter, frame, item.GetColor(newColor), 0f, origin, finalDrawScale, effects, 0f);
            }

        SkipVanillaItemDraw:
            ItemLoader.PostDrawInInventory(item, spriteBatch, screenPositionForItemCenter, frame, item.GetAlpha(itemLight), item.GetColor(environmentColor), origin, finalDrawScale);

            return finalDrawScale;
        }


        public virtual void Draw(SpriteBatch spriteBatch, Item item, Vector2 position, Color inventoryBackgroundColor, Color lightColor = default(Color))
        {
            if (item != null || item.type != 0)
            {
                Main.instance.LoadItem(item.type);
            }

            Rectangle rectangle = Texture.Value.Bounds;
            rectangle.Width -= 2;
            rectangle.Height -= 2;
            var style = GetDimensions();

            Main.inventoryScale = 0.755f;

            Vector2 vector2 = Texture.Size() * Main.inventoryScale;
            float inventoryScale = Main.inventoryScale;
            Color color1 = Color.White;
            spriteBatch.Draw(Texture.Value, position, null, inventoryBackgroundColor, 0.0f, new Vector2(), inventoryScale, SpriteEffects.None, 0.0f);
            Item weapon = item;
            Main.instance.LoadItem(weapon.type);
            Texture2D texture2D = TextureAssets.Item[weapon.type].Value;
            rectangle = Main.itemAnimations[weapon.type] == null
                ? texture2D.Frame()
                : Main.itemAnimations[weapon.type].GetFrame(texture2D);
            Color currentColor = color1;
            float scale1 = 1f;
            Terraria.UI.ItemSlot.GetItemLight(ref currentColor, ref scale1, weapon);
            float num12 = 1f;
            if (rectangle.Width > 48 || rectangle.Height > 48)
                num12 = rectangle.Width <= rectangle.Height ? 48 / (float)rectangle.Height : 48 / (float)rectangle.Width;
            float scale2 = num12;
            
            Vector2 position1 = style.Position();
            inventoryScale = 0.85f;
            DrawItemIcon(weapon, -1, spriteBatch, position1 + vector2 / 2f, Main.inventoryScale, 32, Color.White);
            Vector2 origin = rectangle.Size() * (float)((double)scale1 / 2.0 - 0.5);

            /*
            if (ItemLoader.PreDrawInInventory(weapon, spriteBatch, position1, rectangle, weapon.GetAlpha(currentColor),
                    weapon.GetColor(color1), origin, scale2 * scale1))
            {
                spriteBatch.Draw(texture2D, position1, new Rectangle?(rectangle), weapon.GetAlpha(currentColor), 0.0f,
                    origin, Main.inventoryScale, SpriteEffects.None, 0.0f);
                if (weapon.color != Color.Transparent)
                {
                    Color newColor = color1;
                    spriteBatch.Draw(texture2D, position1, new Rectangle?(rectangle), weapon.GetColor(newColor), 0.0f,
                        origin, Main.inventoryScale, SpriteEffects.None, 0.0f);
                }
            }

            ItemLoader.PostDrawInInventory(weapon, spriteBatch, position1, rectangle, weapon.GetAlpha(currentColor),
                weapon.GetColor(color1), origin, scale2 * scale1);*/
            if (ItemID.Sets.TrapSigned[weapon.type])
                spriteBatch.Draw(TextureAssets.Wire.Value, (style.Position() + new Vector2(16f)) + new Vector2(40f, 40f) * inventoryScale,
                    new Rectangle?(new Rectangle(4, 58, 8, 8)), color1, 0.0f, new Vector2(4f), 1f, SpriteEffects.None,
                    0.0f);
            if (weapon.stack > 1)
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, weapon.stack.ToString(),
                    (style.Position()) + new Vector2(10f, 26f) * inventoryScale, color1, 0.0f, Vector2.Zero,
                    new Vector2(inventoryScale), spread: inventoryScale);

            if ((weapon.expertOnly && !Main.expertMode || weapon.masterOnly && !Main.masterMode))
            {
                Vector2 position3 = style.Center() + Texture.Value.Size() * inventoryScale / 2f -
                                    TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
                Color white = Color.White;
                spriteBatch.Draw(TextureAssets.Cd.Value, position3, new Rectangle?(), white, 0.0f, new Vector2(), scale2,
                    SpriteEffects.None, 0.0f);
            }
        }
    }
}
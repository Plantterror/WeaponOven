using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader.IO;

namespace WeaponOven.UI
{
    public class ItemSlot : TagSerializable
    {
        public static readonly Func<TagCompound, ItemSlot> DESERIALIZER = LoadData;

        public Item item;

        public ItemSlot()
        {
            item = new Item();
            item.TurnToAir();
        }

        public ItemSlot(Item item)
        {
            this.item = item;
        }

        public TagCompound SerializeData() => new TagCompound { [nameof(item)] = item };

        public static ItemSlot Load(TagCompound tag)
        {
            var itemSlot = new ItemSlot();
            itemSlot.item = tag.Get<Item>(nameof(item));
            return itemSlot;
        }

        public void Swap(Ref<Item> otherItem)
        {
            Terraria.Utils.Swap<Item>(ref item, ref otherItem.Value);
        }

        public void RemoveIndividual(Ref<Item> otherItem)
        {
            var other = otherItem.Value;

            if (other.type != item.type)
            {
                return;
            }

            if (item.stack <= 0)
            {
                item.TurnToAir();
            }

            item.stack--;
            other.stack++;
        }

        public void WriteData(BinaryWriter writer)
        {
            ItemIO.Send(item, writer, true);
        }

        public void ReadData(BinaryReader reader)
        {
            item = ItemIO.Receive(reader);
        }

        public static ItemSlot LoadData(TagCompound tag)
        {
            var data = new ItemSlot();
            data.item = tag.Get<Item>(nameof(item));
            return data;
        }
    }
}
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        [Fact]
        public void NormalItem_SellInValue_LowersBy1EachDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "allItems", SellIn = 10, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].SellIn);
        }

        [Fact]
        public void NormalItem_QualityValue_LowersBy1EachDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "allItems", SellIn = 10, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].Quality);
        }


        [Fact]
        public void NormalItemPastExpirationDate_Quality_LowersTwiceAsMuchEachDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "allItems", SellIn = 0, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(8, Items[0].Quality);
        }


        [Fact]
        public void NormalItemWith0Quality_Quality_CannotBeLowerThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "allItems", SellIn = 5, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void GoodWine_Quality_ImprovesTheOlderItGets()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 5, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(11, Items[0].Quality);
        }


        [Fact]
        public void AllItems_Quality_NeverIncreasesAbove50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 5, Quality = 50 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(50, Items[0].Quality);
        }


        [Fact]
        public void LegendaryItems_QualityAndSellIn_NeverIncreasesAbove50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 5, Quality = 80 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(5, Items[0].SellIn);
            Assert.Equal(80, Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Quality_IncreasesBy1Before10Days()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 20, Quality = 20 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(19, Items[0].SellIn);
            Assert.Equal(21, Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Quality_IncreasesBy2In10Days()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 20 }, new Item { Name = "Backstage passes for Re:factor", SellIn = 9, Quality = 20 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(22, Items[0].Quality);
            Assert.Equal(8, Items[1].SellIn);
            Assert.Equal(22, Items[1].Quality);
        }

        [Fact]
        public void BackstagePasses_Quality_IncreasesBy3In5Days()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 20 }, new Item { Name = "Backstage passes for Re:factor", SellIn = 4, Quality = 20 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(23, Items[0].Quality);
            Assert.Equal(3, Items[1].SellIn);
            Assert.Equal(23, Items[1].Quality);
        }

        [Fact]
        public void BackstagePasses_Quality_DropsTo0AfterEvent()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 20 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }


        [Fact]
        public void SmellyItems_Quality_DropsTwiceAsFastAsNormalItemsEachDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Duplicate Code", SellIn = 10, Quality = 20 }, new Item { Name = "Long Methods", SellIn = 10, Quality = 20 }, new Item { Name = "Ugly Variable Names", SellIn = 10, Quality = 20 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(18, Items[0].Quality);
            Assert.Equal(9, Items[1].SellIn);
            Assert.Equal(18, Items[1].Quality);
            Assert.Equal(9, Items[2].SellIn);
            Assert.Equal(18, Items[2].Quality);
        }
    }
}
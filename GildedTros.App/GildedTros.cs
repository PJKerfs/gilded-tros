using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        public enum Categorie
        {
            NormalItem,
            LegendaryItem,
            BackstagePasses,
            Wine,
            SmellyItems
        }

        //Create categories so they can de identified easier.
        List<string> LegendaryItemsList = new List<string> { "B-DAWG Keychain" };
        List<string> BackstagePassesList = new List<string> { "Backstage passes for Re:factor", "Backstage passes for HAXX" };
        List<string> WineList = new List<string> { "Good Wine" };
        List<string> SmellyItemsList = new List<string> { "Duplicate Code", "Long Methods", "Ugly Variable Names" };

        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Categorie c = GetCategoryOfItem(Items[i]);

                switch (c)
                {
                    case Categorie.NormalItem:
                        Items[i] = HandleNormalItem(Items[i]);
                        break;
                    case Categorie.LegendaryItem:
                        Items[i] = HandleLegendaryItem(Items[i]);
                        break;
                    case Categorie.BackstagePasses:
                        Items[i] = HandleBackstagePassItem(Items[i]);
                        break;
                    case Categorie.Wine:
                        Items[i] = HandleWineItem(Items[i]);
                        break;
                    case Categorie.SmellyItems:
                        Items[i] = HandleSmellyItem(Items[i]);
                        break;
                }



                //     if (Items[i].Name != "Good Wine"
                //         && Items[i].Name != "Backstage passes for Re:factor"
                //         && Items[i].Name != "Backstage passes for HAXX")
                //     {
                //         if (Items[i].Quality > 0)
                //         {
                //             if (Items[i].Name != "B-DAWG Keychain")
                //             {
                //                 Items[i].Quality = Items[i].Quality - 1;
                //             }
                //         }
                //     }
                //     else
                //     {
                //         if (Items[i].Quality < 50)
                //         {
                //             Items[i].Quality = Items[i].Quality + 1; //Improved quality of backstage passes and good wine.

                //             if (Items[i].Name == "Backstage passes for Re:factor"
                //             || Items[i].Name == "Backstage passes for HAXX")
                //             {
                //                 if (Items[i].SellIn < 11)
                //                 {
                //                     if (Items[i].Quality < 50)
                //                     {
                //                         Items[i].Quality = Items[i].Quality + 1;
                //                     }
                //                 }

                //                 if (Items[i].SellIn < 6)
                //                 {
                //                     if (Items[i].Quality < 50)
                //                     {
                //                         Items[i].Quality = Items[i].Quality + 1;
                //                     }
                //                 }
                //             }
                //         }
                //     }

                //     if (Items[i].Name != "B-DAWG Keychain")
                //     {
                //         Items[i].SellIn = Items[i].SellIn - 1;
                //     }

                //     if (Items[i].SellIn < 0)
                //     {
                //         if (Items[i].Name != "Good Wine")
                //         {
                //             if (Items[i].Name != "Backstage passes for Re:factor"
                //                 && Items[i].Name != "Backstage passes for HAXX")
                //             {
                //                 if (Items[i].Quality > 0)
                //                 {
                //                     if (Items[i].Name != "B-DAWG Keychain")
                //                     {
                //                         Items[i].Quality = Items[i].Quality - 1;
                //                     }
                //                 }
                //             }
                //             else
                //             {
                //                 Items[i].Quality = Items[i].Quality - Items[i].Quality; //Resets Quality to 0.
                //             }
                //         }
                //         else
                //         {
                //             if (Items[i].Quality < 50)
                //             {
                //                 Items[i].Quality = Items[i].Quality + 1; //Improved Good Wine quality
                //             }
                //         }
                //     }
            }
        }

        private Item HandleNormalItem(Item item)
        {
            if (IsPastSellByDate(item))
            {
                item = ChangeQualityOfItemByAmount(item, -2);
            }
            else
            {
                item = ChangeQualityOfItemByAmount(item, -1);
            }
            item = DecreaseSellInBy1(item);
            return item;
        }

        private Item HandleSmellyItem(Item item)
        {
            throw new NotImplementedException();
        }

        private Item HandleWineItem(Item item)
        {
            throw new NotImplementedException();
        }

        private Item HandleBackstagePassItem(Item item)
        {
            throw new NotImplementedException();
        }

        private Item HandleLegendaryItem(Item item)
        {
            throw new NotImplementedException();
        }

        private Item ChangeQualityOfItemByAmount(Item item, int amount = 1)
        {
            if (item.Quality < 50)
            {
                item.Quality += amount;
                if (item.Quality < 0)
                    item.Quality = 0;
            }
            return item;
        }

        private Item DecreaseSellInBy1(Item item)
        {
            if (item.SellIn >= 0)
            {
                item.SellIn--;
            }
            return item;
        }

        private Item ResetQuality(Item item)
        {
            item.Quality = 0;
            return item;
        }

        private Categorie GetCategoryOfItem(Item item)
        {
            if (LegendaryItemsList.Contains(item.Name))
            {
                return Categorie.LegendaryItem;
            }
            if (WineList.Contains(item.Name))
            {
                return Categorie.Wine;
            }
            if (BackstagePassesList.Contains(item.Name))
            {
                return Categorie.BackstagePasses;
            }
            if (SmellyItemsList.Contains(item.Name))
            {
                return Categorie.SmellyItems;
            }
            return Categorie.NormalItem;
        }

        private bool IsPastSellByDate(Item item)
        {
            return item.SellIn <= 0;
        }
    }
}

using System.Collections.Generic;

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
                        HandleNormalItem(Items[i]);
                        break;
                    case Categorie.LegendaryItem:
                        HandleLegendaryItem(Items[i]);
                        break;
                    case Categorie.BackstagePasses:
                        HandleBackstagePassItem(Items[i]);
                        break;
                    case Categorie.Wine:
                        HandleWineItem(Items[i]);
                        break;
                    case Categorie.SmellyItems:
                        HandleSmellyItem(Items[i]);
                        break;
                }
            }
        }

        private void HandleNormalItem(Item item)
        {
            ChangeQualityOfItemByAmount(item, IsPastSellByDate(item) ? -2 : -1);
            DecreaseSellInBy1(item);
        }

        private void HandleSmellyItem(Item item)
        {
            ChangeQualityOfItemByAmount(item, IsPastSellByDate(item) ? -4 : -2);
            DecreaseSellInBy1(item);
        }

        private void HandleWineItem(Item item)
        {
            ChangeQualityOfItemByAmount(item, IsPastSellByDate(item) ? 2 : 1);
            DecreaseSellInBy1(item);
        }

        private void HandleBackstagePassItem(Item item)
        {
            if (IsPastSellByDate(item))
            {
                ResetQuality(item);
                DecreaseSellInBy1(item);
            }
            else
            {
                ChangeQualityOfItemByAmount(item, item.SellIn <= 5 ? 3 : item.SellIn <= 10 ? 2 : 1);
                DecreaseSellInBy1(item);
            }
        }

        private void HandleLegendaryItem(Item item)
        {
            //Do Nothing, might change in the future.
            return;
        }

        //Legendary items do not change quality after a day, so they won't use this function, otherwise we would need to test it.
        private void ChangeQualityOfItemByAmount(Item item, int amount = 1)
        {
            if (item.Quality < 50)
            {
                item.Quality += amount;
                if (item.Quality < 0)
                    item.Quality = 0;
                if (item.Quality > 50)
                    item.Quality = 50;
            }
        }

        private void DecreaseSellInBy1(Item item)
        {
            item.SellIn--;
        }

        private void ResetQuality(Item item)
        {
            item.Quality = 0;
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

using System;
using System.Linq;

namespace ShopifyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateOverall();
            Line();
            CalculateMostPurchasingUser();
            Line();
            CalculateMostSaleBySeller();
            Line();
            CalculateMostSalePerDay();
            Line();
            CalculateMostSalePerSellerPerDay();
            Line();

        }
        private static void CalculateMostSalePerSellerPerDay()
        {
            var sellerSales = DataSource.Orders.GroupBy(p => new {
                date = p.date.ToShortDateString(),
                p.shop_id
            }).Select(p => new
            {
                date = p.Key.date,
                seller = p.Key.shop_id,
                orderAmount = p.Where(d=> d.shop_id == p.Key.shop_id).Sum(q => q.order_amount),
                numberOfItems = p.Where(d => d.shop_id == p.Key.shop_id).Sum(q => q.total_items),
            });
            var mostPurchasingUser = sellerSales.OrderByDescending(p => p.orderAmount).Take(1).Single();
            Print("Most Sale was by Seller:", mostPurchasingUser.seller, ",", "Most Sale was in Date:", mostPurchasingUser.date, ",", "Amount:", mostPurchasingUser.orderAmount, ",", "Number of Items:", mostPurchasingUser.numberOfItems);
        }

        private static void CalculateMostSalePerDay()
        {
            var sellerSales = DataSource.Orders.GroupBy(p =>  p.date.ToShortDateString()).Select(p => new
            {
                date = p.Key,
                orderAmount = p.Sum(q => q.order_amount),
                numberOfItems = p.Sum(q => q.total_items),
            });
            var mostPurchasingUser = sellerSales.OrderByDescending(p => p.orderAmount).Take(1).Single();
            Print("Most Sale was in Date:", mostPurchasingUser.date, ",", "Amount:", mostPurchasingUser.orderAmount, ",", "Number of Items:", mostPurchasingUser.numberOfItems);
        }

        private static void CalculateMostSaleBySeller()
        {
            var sellerSales = DataSource.Orders.GroupBy(p => p.shop_id).Select(p => new
            {
                shopId = p.Key,
                orderAmount = p.Sum(q => q.order_amount),
                numberOfItems = p.Sum(q => q.total_items),
            });
            var mostPurchasingUser = sellerSales.OrderByDescending(p => p.orderAmount).Take(1).Single();
            Print("Most Sale by Seller Id:", mostPurchasingUser.shopId, ",", "Amount:", mostPurchasingUser.orderAmount, ",", "Number of Items:", mostPurchasingUser.numberOfItems);
        }

        private static void CalculateMostPurchasingUser()
        {
            var purchasePerUser = DataSource.Orders.GroupBy(p => p.user_id).Select(p => new
            {
                userId = p.Key,
                orderAmount = p.Sum(q => q.order_amount),
                numberOfItems = p.Sum(q => q.total_items),
            });
            var mostPurchasingUser = purchasePerUser.OrderByDescending(p => p.orderAmount).Take(1).Single();
            Print("Most Purchasing User Id:", mostPurchasingUser.userId, ",", "Amount:", mostPurchasingUser.orderAmount, ",", "Number of Items:", mostPurchasingUser.numberOfItems);
        }

        private static void CalculateOverall()
        {
            double totalAmount = DataSource.Orders.Sum(p => p.order_amount);
            var orderCount = DataSource.Orders.Count();
            double orderItemCount = DataSource.Orders.Sum(p => p.total_items);
            Print("Total Amount:", totalAmount);
            Print("Total Order Count:", orderCount);
            Print("Total Order Item Count:", orderItemCount);

            double aov = Math.Round(totalAmount / orderItemCount, 2);
            Print("AOV:", aov);
        }

        private static void Print(params object[] arg)
        {
            Console.WriteLine(string.Join(" ", arg.Select(p => p.ToString())));
        }

        private static void Line()
        {
            Print("==================================");
        }
    }
}

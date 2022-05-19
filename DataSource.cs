using ShopifyTest.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ShopifyTest
{
    public class DataSource
    {
        public static List<Order> Orders = JsonSerializer.Deserialize<List<Order>>(Resources.Order);
    }

    public class Order
    {
        public int order_id { get; set; }
        public int shop_id { get; set; }
        public int user_id { get; set; }
        public int order_amount { get; set; }
        public int total_items { get; set; }
        public string payment_method { get; set; }
        public string created_at { get; set; }
        public DateTime date { get { return DateTime.Parse(this.created_at); } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAndInventory.Classes
{
    class Formula
    {
        public static double getSellingPrice(double cost, double markup)
        {
            return (cost * (markup / 100)) + cost;
        }

        public static double getMarkup(double cost, double revenue)
        {
            return ((revenue - cost) / cost) * 100;
        }



    }
}

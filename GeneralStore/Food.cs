﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum FoodType
    {
        Persishable = 0,
        NonPerishable = 0
    }
    public class Food : Product
    {
        public FoodType TypeOfFood { get; set; }
        public Food()
        {

        }

        public Food(string name, int quantity, float tax, FoodType typeofdrink) : base(name, quantity)
        {
            TypeOfFood = typeofdrink;
            TaxPercent = (float)TypeOfFood / 100;
        }

        public Food(string name, int quantity, float tax, FoodType typeofdrink, float price) : base(name, quantity, price)
        {
            TypeOfFood = typeofdrink;
            TaxPercent = (float)TypeOfFood / 100;
        }

        public override bool CanSell()
        {
            if(TypeOfFood == FoodType.Persishable && (DateTime.Now - DatePurchased).TotalDays>7)
            {
                return false;
            }
            return true;
        }

        public override void DisplayProduct()
        {
            base.DisplayProduct();
            Console.WriteLine($"Type of Food: {(FoodType)TypeOfFood}\n");
        }
    }
}

﻿namespace S09_E01.Entities
{
    internal class Product
    {
        public string? Name { get; set; }
        public double price { get; set; }

        public Product()
        {
        }

        public Product(string? name, double price)
        {
            Name = name;
            this.price = price;
        }
    }
}
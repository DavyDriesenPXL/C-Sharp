using System;
using System.Collections.Generic;
using System.Text;

namespace Week_9_Broodjesservice
{ 
    public class Broodje
    {
        public string Name { get; set; }
        private string _type;
        private static readonly string[] _allowedTypes = { "warm", "koud", "veggie", "speciaal" };
        public string Type
        {
            get { return _type; }
            set {
                if (!_allowedTypes.Contains(value))
                {
                    throw new ArgumentException ("Verkeerd type");
                }
                _type = value; }
        }
        public decimal Price { get; set; }
        public  Broodje(string name, string type, decimal price)
        {
            Name = name;
            Type = type;
            Price = price;
        }
    }
}

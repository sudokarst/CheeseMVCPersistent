using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseCategory
    {
        public string Name { set; get; }
        public int ID { set; get; }

        public IList<Cheese> Cheeses { set; get; }
    }
}

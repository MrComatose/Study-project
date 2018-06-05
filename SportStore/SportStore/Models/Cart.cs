
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel.DataAnnotations;
namespace SportStore.Models
{
    public class Cart
    {
        private List<CartLine> LineCollection = new List<CartLine>();
        public virtual void AddItem(Product product, int quentity)
        {
            CartLine line = LineCollection.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();
            if (line == null)
            {
                LineCollection.Add(new CartLine { Product = product, Quentity = quentity });
            }
            else line.Quentity += quentity;
        }
        public virtual void RemoveLine(Product product)
        {
            LineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public virtual decimal ComputTotalValue() => LineCollection.Sum(p => p.GetCost());
        public virtual void Clear() => LineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => LineCollection;
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "Min 1 and max 10."),MinLength(1),MaxLength(10)]
        public int Quentity { get; set; }
        public virtual decimal GetCost() => Product.Price * Quentity;

    }
}


using System.Collections.Generic;
using SportStore.Models;

namespace SportStore.Models.ViewModels
{
    public class ProductListVIewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo Page_info { get; set; }
        public string CurrentCategory { get; set; }
    }
}

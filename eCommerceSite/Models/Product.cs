using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    /// <summary>
    /// A salable product
    /// </summary>
    public class Product
    {
        // Suffix Id will make it automatically a Primary Key, if int it will be an Identity column
        // Use [Key] to force it to be a Primary Key
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// The consumer facing name of the product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The retail price of the product as US currency
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The category the product belongs to. Ex. Electronics, Hardware, etc.
        /// </summary>
        public string Category { get; set; }
    }
}

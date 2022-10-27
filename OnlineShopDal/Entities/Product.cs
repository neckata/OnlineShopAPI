using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopDal.Entities
{
    public class Product : IEntity<Guid>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}

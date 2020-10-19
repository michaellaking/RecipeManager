using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace RecipeManager.Models
{
    public class Recipe
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Make Time")]
        public string Time { get; set; }
        [Required]
        public string Servings { get; set; }
        [Required]
        [Display(Name ="Short Description")]
        public string Description { get; set; }
        [Required]
        public byte[] Photo { get; set; }
        [Required]
        [Display(Name = "Visible to anyone?")]
        public bool IsPublic { get; set; }

        public bool IsFeatured { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        [Required]
        [Display(Name = "Average Rating")]
        public double RatingAverage { get; set; }
        [Required]
        [Display(Name = "Rating Count")]
        public int RatingCount { get; set; }
        [Required]
        public string UploaderName { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }
        public List<Category> Categories { get; set; }
    }
}


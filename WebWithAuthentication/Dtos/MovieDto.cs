using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebWithAuthentication.Models;

namespace WebWithAuthentication.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        //        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        //        [DataType(DataType.Date)]     use validate on view
        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        //        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

    }
}
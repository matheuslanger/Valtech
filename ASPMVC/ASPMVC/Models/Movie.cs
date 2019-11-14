using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Newtonsoft.Json;
using RestSharp.Validation;
using TMDbLib.Objects.General;

namespace ASPMVC.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public long RemoteId { get; set; }

        [JsonProperty("original_title", Required = Required.Always)]
        [Display(Name = "Title")]
        public string Original_title { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Release_date { get; set; }

        [JsonProperty("original_language", Required = Required.Always)]
        [Display(Name = "Original Language")]
        public string Original_Language { get; set; }

        [JsonProperty("overview", Required = Required.Always)]
        [Display(Name = "Overview")]
        public string Description { get; set; }

        [JsonProperty("budget", Required = Required.Always)]
        public long Budget { get; set; }

        [JsonProperty("vote_average", Required = Required.Always)]
        [Display(Name = "Rating")]
        public double Vote_average { get; set; }

        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

    }
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
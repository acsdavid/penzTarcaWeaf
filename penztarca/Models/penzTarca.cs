using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace penztarca.Models
{
    public class penzTarca
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsDone { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
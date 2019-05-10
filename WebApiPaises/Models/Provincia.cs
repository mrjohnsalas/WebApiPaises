using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiPaises.Models
{
    public class Provincia
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PaisId { get; set; }

        [JsonIgnore]
        public Pais Pais { get; set; }
    }
}

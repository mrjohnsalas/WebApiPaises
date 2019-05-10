using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPaises.Models
{
    public class Pais
    {
        public Pais()
        {
            Provincias = new List<Provincia>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Provincia> Provincias { get; set; }
    }
}

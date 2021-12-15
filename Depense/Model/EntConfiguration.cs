using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Depense.Model
{
    public class EntConfiguration
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UtilisateurId { get; set; }
        public bool Switch1 { get; set; }
        public bool Switch2 { get; set; }
        public bool Switch3 { get; set; }
        public Double DegreLatitude { get; set; }
        public Double DegreLongitude { get; set; }
    }
}


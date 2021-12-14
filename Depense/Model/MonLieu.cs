using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Depense.Model
{
    public class MonLieu
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Categorie { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Depense.Model
{
    class UtilisateurLieu
    {
        public int Id { get; set; }
        public string AdresseCourriel { get; set; }
        public string MotDePasse { get; set; }
        public List<MonLieu> ListeDesLieux { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.Model
{
    public class Participant
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int IdPrezentare { get; set; }

        // Constructor
        public Participant(int id, string nume, string email, string telefon, int idPrezentare)
        {
            Id = id;
            Nume = nume;
            Email = email;
            Telefon = telefon;
            IdPrezentare = idPrezentare;
        }

        public Participant()
        {
        }
    }
}

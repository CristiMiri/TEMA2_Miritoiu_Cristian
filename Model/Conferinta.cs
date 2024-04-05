using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.Model
{
    public class Conferinta
    {
        private int id;
        private String titlu;
        private String locatie;
        private String data;
        private List<Utilizator> participanti;
        private List<Prezentare> prezentari;

        public Conferinta(int id, string titlu, string locatie, string data, List<Utilizator> participanti, List<Prezentare> prezentari)
        {
            this.id = id;
            this.titlu = titlu;
            this.locatie = locatie;
            this.data = data;
            this.participanti = participanti;
            this.prezentari = prezentari;
        }

        public Conferinta(Conferinta conferinta)
        {
            this.id = conferinta.id;
            this.titlu = conferinta.titlu;
            this.locatie = conferinta.locatie;
            this.data = conferinta.data;
            this.participanti = conferinta.participanti;
            this.prezentari = conferinta.prezentari;
        }

        public Conferinta()
        {
            this.id = 0;
            this.titlu = "";
            this.locatie = "";
            this.data = "";
            this.participanti = new List<Utilizator>();
            this.prezentari = new List<Prezentare>();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Titlu
        {
            get { return titlu; }
            set { titlu = value; }
        }

        public String Locatie
        {
            get { return locatie; }
            set { locatie = value; }
        }

        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public List<Utilizator> Participanti
        {
            get { return participanti; }
            set { participanti = value; }
        }

        public List<Prezentare> Prezentari
        {
            get { return prezentari; }
            set { prezentari = value; }
        }

    }
}

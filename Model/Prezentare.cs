using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.Model
{
    public enum Sectiune
    {
        STIINTE,
        TEHNOLOGIE,
        MEDICINA,
        ARTA,
        SPORT,
        TOATE, 
    }

    public class Prezentare
    {
        private int id;
        private String titlu;
        private String autor;
        private String descriere;
        private String data;
        private String ora;
        private Sectiune sectiune;
        private int id_conferinta;

        public Prezentare(int id, string titlu, string autor, string descriere, string data, string ora, Sectiune sectiune, int id_conferinta)
        {
            this.id = id;
            this.titlu = titlu;
            this.autor = autor;
            this.descriere = descriere;
            this.data = data;
            this.ora = ora;
            this.sectiune = sectiune;
            this.id_conferinta = id_conferinta;


        }


        public Prezentare(Prezentare prezentare)
        {
            this.id = prezentare.id;
            this.titlu = prezentare.titlu;
            this.autor = prezentare.autor;
            this.descriere = prezentare.descriere;
            this.data = prezentare.data;
            this.ora = prezentare.ora;
            this.sectiune = prezentare.sectiune;
            this.id_conferinta = prezentare.id_conferinta;

        }

        public Prezentare()
        {
            this.id = 0;
            this.titlu = "";
            this.autor = "";
            this.descriere = "";
            this.data = "";
            this.ora = "";
            this.sectiune = Sectiune.STIINTE;
            this.id_conferinta = 0;
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
        public String Autor
        {
            get { return autor; }
            set { autor = value; }
        }
        public String Descriere
        {
            get { return descriere; }
            set { descriere = value; }
        }
        public String Data
        {
            get { return data; }
            set { data = value; }
        }
        public String Ora
        {
            get { return ora; }
            set { ora = value; }
        }
        public Sectiune Sectiune
        {
            get { return sectiune; }
            set { sectiune = value; }
        }
        public int Id_conferinta
        {
            get { return this.id_conferinta; }
            set { this.id_conferinta = value; }
        }
    }
}

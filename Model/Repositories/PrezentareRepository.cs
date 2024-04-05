using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TEMA1_PS.Model.Repositories;


namespace PS_TEMA2.Model.Repositories
{
    public class PrezentareRepository
    {
        private Repository repository;
        private DataTable prezentariTable;
        /* Prezentare = private int id;
                        private String titlu;
                        private String autor;
                        private String descriere;
                        private String data;
                        private String ora; 
                        private Sectiune sectiune;
                        private int id_conferinta
                        */
        public PrezentareRepository()
        {
            repository = new Repository();
        }

        public Repository Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public void PrezentareTable()
        {
            string query = "SELECT * FROM prezentari";
            prezentariTable = repository.ExecuteQuery(query);
            if (prezentariTable != null || prezentariTable.Rows.Count != 0)
            {
                this.prezentariTable = prezentariTable;
            }
        }

        public Prezentare rowToPrezentare(DataRow row)
        {
            Prezentare prezentare = new Prezentare();
            prezentare.Id = Convert.ToInt32(row["id"]);
            prezentare.Titlu = row["titlu"].ToString();
            prezentare.Autor = row["autor"].ToString();
            prezentare.Descriere = row["descriere"].ToString();
            prezentare.Data = ((DateTime)row["data"]).ToShortDateString();
            prezentare.Ora = row["ora"].ToString();
            //Sectiune  enum STIINTE,TEHNOLOGIE,MEDICINA,ARTA,SPORT
            prezentare.Sectiune = (Sectiune)Enum.Parse(typeof(Sectiune), row["sectiune"].ToString());
            prezentare.Id_conferinta = Convert.ToInt32(row["id_conferinta"]);           
            return prezentare;
        }

        public DataTable PrezentariTable
        {
            get { return prezentariTable; }
            set { prezentariTable = value; }
        }

        //CRUD
        public bool AddPrezentare(Prezentare prezentare)
        {
            string query = "INSERT INTO prezentari (titlu, autor, descriere, data, ora, sectiune,id_conferinta) VALUES ('" +
                    prezentare.Titlu + "', '" +
                    prezentare.Autor + "', '" +
                    prezentare.Descriere + "', '" +
                    prezentare.Data + "', '" +
                    prezentare.Ora + "', '" +
                    prezentare.Sectiune + "', '" +
                    prezentare.Id_conferinta + "')";
            return repository.ExecuteNonQuery(query);
        }

        public List<Prezentare> GetPrezentari()
        {
            PrezentareTable();
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in prezentariTable.Rows)
            {
                prezentari.Add(rowToPrezentare(row));
            }
            return prezentari;
        }

        public Prezentare GetPrezentarebyId(int id)
        {
            PrezentareTable();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (Convert.ToInt32(row["id"]) == id)
                {
                    return rowToPrezentare(row);
                }
            }
            return null;
        }

        public bool UpdatePrezentare(Prezentare prezentare)
        {
            string query = "UPDATE prezentari SET " +
                "titlu = '" + prezentare.Titlu + "', " +
                "autor = '" + prezentare.Autor + "', " +
                "descriere = '" + prezentare.Descriere + "', " +
                "data = '" + prezentare.Data + "', " +
                "ora = '" + prezentare.Ora + "'," +
                "sectiune = '" + prezentare.Sectiune + "'," +
                "id_conferinta = '" + prezentare.Id_conferinta + "'" +
                "WHERE id = " + prezentare.Id;
            return repository.ExecuteNonQuery(query);
        }

        public bool DeletePrezentare(int id)
        {
            string query = "DELETE FROM prezentari WHERE id = " + id;
            return repository.ExecuteNonQuery(query);
        }

        //Filters

        public List<Prezentare> getPrezentariBySectiune(Sectiune sectiune)
        {
            PrezentareTable();
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["sectiune"].ToString().Equals(sectiune.ToString()))
                {
                    prezentari.Add(rowToPrezentare(row));
                }
            }
            return prezentari;
        }

        public List<Prezentare> GetPrezentarebySectiune(Sectiune sectiune)
        {
            PrezentareTable();
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["sectiune"].ToString().Equals(sectiune.ToString()))
                {
                    prezentari.Add(rowToPrezentare(row));
                }
            }
            return prezentari;
        }

        public Prezentare GetPrezentarebyTitlu(string titlu)
        {
            PrezentareTable();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["titlu"].ToString().Equals(titlu))
                {
                    return rowToPrezentare(row);
                }
            }
            return null;
        }
    }
}

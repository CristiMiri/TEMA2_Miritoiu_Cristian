using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMA1_PS.Model.Repositories;


namespace PS_TEMA2.Model.Repositories
{
    public class ParticipantiRepository
    {
        private Repository repository;
        private UtilizatorRepository utilizatorRepository;
        private PrezentareRepository prezentareRepository;
        /*
         *  id SERIAL PRIMARY KEY,
            nume VARCHAR(255),
            email VARCHAR(255),
            telefon VARCHAR(20),
            id_prezentare INTEGER REFERENCES Prezentari(id),
            UNIQUE(email, id_prezentare)*/

        public ParticipantiRepository()
        {
            repository = new Repository();
            utilizatorRepository = new UtilizatorRepository();
            prezentareRepository = new PrezentareRepository();
        }

        private Participant rowToParticipant(DataRow row)
        {
            Participant participant = new Participant();
            participant.Id = Convert.ToInt32(row["id"]);
            participant.Nume = row["nume"].ToString();
            participant.Email = row["email"].ToString();
            participant.Telefon = row["telefon"].ToString();
            participant.IdPrezentare = Convert.ToInt32(row["id_prezentare"]);
            return participant;
        }

        public DataTable Participanti()
        {
            string query = "SELECT * FROM participanti";
            DataTable participantiTable = repository.ExecuteQuery(query);
            return participantiTable;
        }

        public List<Participant> GetParticipanti()
        {
            DataTable participantiTable = Participanti();
            if (participantiTable != null || participantiTable.Rows.Count > 0)
            {
                List<Participant> participanti = new List<Participant>();
                foreach (DataRow row in participantiTable.Rows)
                {
                    participanti.Add(rowToParticipant(row));
                }
                return participanti;
            }
            return null;
        }
        
        public List<Participant> GetParticipantibyPrezentare(Prezentare prezentare)
        {
            string query = "SELECT * FROM participanti WHERE id_prezentare = " + prezentare.Id;
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable != null || participantiTable.Rows.Count > 0)
            {
                List<Participant> participanti = new List<Participant>();
                foreach (DataRow row in participantiTable.Rows)
                {
                    participanti.Add(rowToParticipant(row));
                }
                return participanti;
            }
            return null;
        }

        public bool addParticipant(Participant participant)
        {
            string nonQuery = "INSERT INTO participanti (nume, email, telefon, id_prezentare) VALUES ('" +
                participant.Nume + "', '" +
                participant.Email + "', '" +
                participant.Telefon + "', " +
                participant.IdPrezentare + ")";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool deleteParticipant(Participant participant)
        {
            string nonQuery = "DELETE FROM participanti WHERE id = " + participant.Id;
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool updateParticipant(Participant participant)
        {
            string nonQuery = "UPDATE participanti SET nume = '" + participant.Nume + "', email = '" + participant.Email + "', telefon = '" + participant.Telefon + "', id_prezentare = " + participant.IdPrezentare + " WHERE id = " + participant.Id;
            return repository.ExecuteNonQuery(nonQuery);
        }

        public List<Participant> GetParticipantibySectiune(Sectiune sectiune)
        {
            List<Prezentare> prezentari = prezentareRepository.GetPrezentarebySectiune(sectiune);
            List<Participant> participanti = new List<Participant>();
            foreach (Prezentare prezentare in prezentari)
            {
                List<Participant> participantiPrezentare = GetParticipantibyPrezentare(prezentare);
                if (participantiPrezentare != null)
                {
                    participanti.AddRange(participantiPrezentare);
                }
            }
            return participanti;

        }

    }
}

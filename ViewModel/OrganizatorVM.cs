using PS_TEMA2.Model;
using PS_TEMA2.Model.Repositories;
using PS_TEMA2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PS_TEMA2.ViewModel
{
    internal class OrganizatorVM : INotifyPropertyChanged
    {
        //Data consistency
        private ParticipantiRepository participantRepository;
        private PrezentareRepository prezentareRepository;

        //Data containers
        public List<Participant> listaParticipanti;
        public List<Prezentare> listaPrezentari;
        public Participant participantSelectat;
        public Prezentare prezentareSelectata;
        public List<Sectiune> listaSectiuni = Enum.GetValues(typeof(Sectiune)).Cast<Sectiune>().ToList();
        public List<string> fileFormats = new List<string> { "csv", "json", "xml", "doc" };
        public Sectiune sectiuneSelectataforParticipanti;
        public Sectiune sectiuneSelectataforPrezentari;


        //Commands
        private OrganizatorCommands filterParticipanti;
        private OrganizatorCommands filterPrezentari;
        private OrganizatorCommands saveListToCsv;
        public OrganizatorVM()
        {
            participantSelectat = new Participant();
            prezentareSelectata = new Prezentare();
            participantRepository = new ParticipantiRepository();
            prezentareRepository = new PrezentareRepository();
            listaParticipanti = participantRepository.GetParticipanti();
            Console.WriteLine(listaParticipanti.Count);
            listaPrezentari = prezentareRepository.GetPrezentari();
            this.filterParticipanti = new OrganizatorCommands(SearchParticipanti);
            this.filterPrezentari = new OrganizatorCommands(SearchPrezentari);
            this.saveListToCsv = new OrganizatorCommands(SaveDoc);

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        //Getters and setters
        public List<Participant> ListaParticipanti
        {
            get { return listaParticipanti; }
            set
            {
                listaParticipanti = value;
                OnPropertyChanged("ListaParticipanti");
            }
        }
        public List<Prezentare> ListaPrezentari
        {
            get { return listaPrezentari; }
            set
            {
                listaPrezentari = value;
                OnPropertyChanged("ListaPrezentari");
            }
        }

        public Participant ParticipantSelectat
        {
            get { return participantSelectat; }
            set
            {
                participantSelectat = value;
                OnPropertyChanged("ParticipantSelectat");
            }
        }

        public Prezentare PrezentareSelectata
        {
            get { return prezentareSelectata; }
            set
            {
                prezentareSelectata = value;
                OnPropertyChanged("PrezentareSelectata");
            }
        }
        public List<Sectiune> ListaSectiuni
        {
            get { return listaSectiuni; }
            set
            {
                listaSectiuni = value;
                OnPropertyChanged("ListaSectiuni");
            }
        }

        public Sectiune SectiuneSelectataforParticipanti
        {
            get { return sectiuneSelectataforParticipanti; }
            set
            {
                sectiuneSelectataforParticipanti = value;
                OnPropertyChanged("SectiuneSelectataforParticipanti");
            }
        }

        public Sectiune SectiuneSelectataforPrezentari
        {
            get { return sectiuneSelectataforPrezentari; }
            set
            {
                sectiuneSelectataforPrezentari = value;
                OnPropertyChanged("SectiuneSelectataforPrezentari");
            }
        }

        public List<string> FileFormats
        {
            get { return fileFormats; }
        }

        //Commands getters
        public OrganizatorCommands FilterParticipanti
        {
            get { return filterParticipanti; }
        }
        public OrganizatorCommands FilterPrezentari
        {
            get { return filterPrezentari; }
        }
        public OrganizatorCommands SaveListToCsv
        {
            get { return saveListToCsv; }
        }

        //Commands Implementation
        public void SearchParticipanti()
        {
            try
            {
                if (sectiuneSelectataforParticipanti != null)
                {
                    listaParticipanti = participantRepository.GetParticipantibySectiune(sectiuneSelectataforParticipanti);
                    OnPropertyChanged("ListaParticipanti");
                }
                if (sectiuneSelectataforParticipanti == Sectiune.TOATE)
                {
                    listaParticipanti = participantRepository.GetParticipanti();
                    OnPropertyChanged("ListaParticipanti");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Va rog alegeti o sectiune!");
            }
        }

        public void SearchPrezentari()
        {
            try
            {
                if (sectiuneSelectataforPrezentari != null)
                {
                    listaPrezentari = prezentareRepository.getPrezentariBySectiune(sectiuneSelectataforPrezentari);
                    OnPropertyChanged("ListaPrezentari");
                }
                if (sectiuneSelectataforPrezentari == Sectiune.TOATE)
                {
                    listaPrezentari = prezentareRepository.GetPrezentari();
                    OnPropertyChanged("ListaPrezentari");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Va rog alegeti o sectiune!");
            }
        }

        public void Save()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.csv";
            StringBuilder csvBuilder = new StringBuilder();
            // Add the header
            csvBuilder.AppendLine("ID,Titlu,Autor,Descriere,Data,Ora,Sectiune,ID Conferinta");

            foreach (var prezentare in ListaPrezentari)
            {
                // Convert enum to string
                string sectiuneName = Enum.GetName(typeof(Sectiune), prezentare.Sectiune);

                // Escape commas in strings
                string descriere = prezentare.Descriere.Contains(",") ? $"\"{prezentare.Descriere}\"" : prezentare.Descriere;
                string titlu = prezentare.Titlu.Contains(",") ? $"\"{prezentare.Titlu}\"" : prezentare.Titlu;

                // Append each property to the CSV line
                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                         prezentare.Id,
                                         titlu,
                                         prezentare.Autor,
                                         descriere,
                                         prezentare.Data,
                                         prezentare.Ora,
                                         sectiuneName,
                                         prezentare.Id_conferinta);

                csvBuilder.AppendLine(line);
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, csvBuilder.ToString());
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }

        public void SaveJson()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(listaPrezentari, options);
            File.WriteAllText(filePath, json);
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        public void SaveXml()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.xml";
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Prezentare>));
            using (var writer = new StreamWriter(filePath))
            {
                xml.Serialize(writer, listaPrezentari);
            }
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }

        public void SaveDoc()
        {
            /*
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.doc";
            StringBuilder docBuilder = new StringBuilder();
            docBuilder.AppendLine("ID\tTitlu\tAutor\tDescriere\tData\tOra\tSectiune\tID Conferinta");

            foreach (var prezentare in ListaPrezentari)
            {
                // Convert enum to string
                string sectiuneName = Enum.GetName(typeof(Sectiune), prezentare.Sectiune);

                // Escape commas in strings
                string descriere = prezentare.Descriere.Contains(",") ? $"\"{prezentare.Descriere}\"" : prezentare.Descriere;
                string titlu = prezentare.Titlu.Contains(",") ? $"\"{prezentare.Titlu}\"" : prezentare.Titlu;

                // Append each property to the CSV line
                var line = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                                                            prezentare.Id,
                                                                                                    titlu,
                                                                                                                                            prezentare.Autor,
                                                                                                                                                                                    descriere,
                                                                                                                                                                                                                            prezentare.Data,
                                                                                                                                                                                                                                                                    prezentare.Ora,
                                                                                                                                                                                                                                                                                                            sectiuneName,
                                                                                                                                                                                                                                                                                                                                                    prezentare.Id_conferinta);

                docBuilder.AppendLine(line);
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, docBuilder.ToString());
            */
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.doc";
            StringBuilder sb = new StringBuilder();

            // Add a header
            sb.AppendLine("ID\tTitlu\tAutor\tDescriere\tData\tOra\tSectiune\tID Conferinta");

            // Add data rows
            foreach (var prez in listaPrezentari)
            {
                sb.AppendLine($"{prez.Id}\t{prez.Titlu}\t{prez.Autor}\t{prez.Descriere}" +
                    $"\t{prez.Data}\t{prez.Ora}\t{prez.Sectiune}\t{prez.Id_conferinta}");
            }

            // Write to a .doc file
            File.WriteAllText(filePath, sb.ToString());
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
    }
}

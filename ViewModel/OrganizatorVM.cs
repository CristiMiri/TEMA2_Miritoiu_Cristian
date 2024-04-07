using PS_TEMA2.Model;
using PS_TEMA2.Model.Repositories;
using PS_TEMA2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
        public List<string> fileFormats = new List<string> { "Csv", "Json", "Xml", "Doc" };
        public string selectedFileFormat;
        public Sectiune sectiuneSelectataforParticipanti;
        public Sectiune sectiuneSelectataforPrezentari;


        //Commands
        //Participant
        private OrganizatorCommands createParticipantCommand;
        private OrganizatorCommands updateParticipantCommand;
        private OrganizatorCommands deleteParticipantCommand;
        private OrganizatorCommands filterParticipanti;
        private OrganizatorCommands acceptParticipantCommand;
        private OrganizatorCommands rejectParticipantCommand;
        //Prezentare
        private OrganizatorCommands createPrezentareCommand;
        private OrganizatorCommands updatePrezentareCommand;
        private OrganizatorCommands deletePrezentareCommand;
        private OrganizatorCommands filterPrezentari;
        //Save
        private OrganizatorCommands saveFile;
        private Action<string> changeView;
        private OrganizatorCommands backCommand;

        //Constructor
        public OrganizatorVM()
        {
            participantSelectat = new Participant();
            prezentareSelectata = new Prezentare();
            participantRepository = new ParticipantiRepository();
            prezentareRepository = new PrezentareRepository();
            listaParticipanti = participantRepository.GetParticipanti();            
            listaPrezentari = prezentareRepository.GetPrezentari();
            //Participant
            this.createParticipantCommand = new OrganizatorCommands(CreateParticipant);
            this.updateParticipantCommand = new OrganizatorCommands(UpdateParticipant);
            this.deleteParticipantCommand = new OrganizatorCommands(DeleteParticipant);
            this.filterParticipanti = new OrganizatorCommands(SearchParticipanti);
            this.acceptParticipantCommand = new OrganizatorCommands(AcceptParticipant);
            this.rejectParticipantCommand = new OrganizatorCommands(RejectParticipant);

            //Prezentare
            this.createPrezentareCommand = new OrganizatorCommands(CreatePrezentare);
            this.updatePrezentareCommand = new OrganizatorCommands(UpdatePrezentare);
            this.deletePrezentareCommand = new OrganizatorCommands(DeletePrezentare);
            this.filterPrezentari = new OrganizatorCommands(SearchPrezentari);

            //Save
            this.saveFile = new OrganizatorCommands(Save);
            this.backCommand = new OrganizatorCommands(back);

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
        public Participant ParticipantSelectat
        {
            get { return participantSelectat; }
            set
            {
                participantSelectat = value;
                OnPropertyChanged("ParticipantSelectat");
            }
        }
        public List<Participant> ListaParticipanti
        {
            get { return listaParticipanti; }
            set
            {
                listaParticipanti = value;
                OnPropertyChanged("ListaParticipanti");
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

        
        public Prezentare PrezentareSelectata
        {
            get { return prezentareSelectata; }
            set
            {
                prezentareSelectata = value;
                OnPropertyChanged("PrezentareSelectata");
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
        public Sectiune SectiuneSelectataforPrezentari
        {
            get { return sectiuneSelectataforPrezentari; }
            set
            {
                sectiuneSelectataforPrezentari = value;
                OnPropertyChanged("SectiuneSelectataforPrezentari");
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
        public List<string> FileFormats
        {
            get { return fileFormats; }
        }
        public string SelectedFileFormat
        {
            get { return selectedFileFormat; }
            set
            {
                selectedFileFormat = value;
                OnPropertyChanged("SelectedFileFormat");
            }
        }
        //Commands getters
        //Participant
        public OrganizatorCommands CreateParticipantCommand
        {
            get { return createParticipantCommand; }
        }
        public OrganizatorCommands UpdateParticipantCommand
        {
            get { return updateParticipantCommand; }
        }
        public OrganizatorCommands DeleteParticipantCommand
        {
            get { return deleteParticipantCommand; }
        }
        public OrganizatorCommands FilterParticipanti
        {
            get { return filterParticipanti; }
        }
        public OrganizatorCommands AcceptParticipantCommand
        {
            get { return acceptParticipantCommand; }
        }
        public OrganizatorCommands RejectParticipantCommand
        {
            get { return rejectParticipantCommand; }
        }

        //Prezentare
        public OrganizatorCommands CreatePrezentareCommand
        {
            get { return createPrezentareCommand; }
        }
        public OrganizatorCommands UpdatePrezentareCommand
        {
            get { return updatePrezentareCommand; }
        }
        public OrganizatorCommands DeletePrezentareCommand
        {
            get { return deletePrezentareCommand; }
        }
        public OrganizatorCommands FilterPrezentari
        {
            get { return filterPrezentari; }
        }
        
        //Save
        public OrganizatorCommands SaveFile
        {
            get { return saveFile; }

        }
        public Action<string> ChangeView
        {
            get { return changeView; }
            set { changeView = value; }
        }
        public OrganizatorCommands BackCommand
        {
            get { return backCommand; }
        }

        //Commands Implementation
        //CRUD operations for Participant
        public void CreateParticipant()
        {
            try
            {
                bool result = participantRepository.addParticipant(participantSelectat);
                listaParticipanti = participantRepository.GetParticipanti();
                OnPropertyChanged("ListaParticipanti");
                clearParticipantFields();
                if (result)
                {
                       MessageBox.Show("Participantul a fost adaugat cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Participantul nu a putut fi adaugat!");
            }
        }
        public void UpdateParticipant()
        {
            try
            {
                bool result = participantRepository.updateParticipant(participantSelectat);
                listaParticipanti = participantRepository.GetParticipanti();
                OnPropertyChanged("ListaParticipanti");
                clearParticipantFields();
                if (result)
                {
                    MessageBox.Show("Participantul a fost actualizat cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Participantul nu a putut fi actualizat!");
            }
        }
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
        public void DeleteParticipant()
        {
            try
            {
                bool result = participantRepository.deleteParticipant(participantSelectat);
                listaParticipanti = participantRepository.GetParticipanti();
                OnPropertyChanged("ListaParticipanti");
                clearParticipantFields();
                if (result)
                {
                    MessageBox.Show("Participantul a fost sters cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Participantul nu a putut fi sters!");
            }
        }
        public void AcceptParticipant()
        {
            string fromAddress = Environment.GetEnvironmentVariable("EmailUsername");
            string fromPassword = Environment.GetEnvironmentVariable("EmailSTMP");            
            string subject = "Participare acceptata";
            string body = "Felicitari! Participarea dumneavoastra a fost acceptata! Va asteptam la eveniment!";
            //string toAddress = "cristianmiritoiu6@gmail.com";
            string toAddress = participantSelectat.Email;
            SendMail(toAddress, subject, body, fromAddress, fromPassword);
            
        }
        public void RejectParticipant()
        {
            string fromAddress = Environment.GetEnvironmentVariable("EmailUsername");
            string fromPassword = Environment.GetEnvironmentVariable("EmailSTMP");
            string subject = "Participare respinsa";
            string body = "Ne pare rau, dar participarea dumneavoastra a fost respinsa!";
            //string toAddress = "cristianmiritoiu6@gmail.com";
            string toAddress = participantSelectat.Email;
            SendMail(toAddress, subject, body, fromAddress, fromPassword);
        }

        private void SendMail(string toAddress, string subject, string body, string fromAddress, string fromPassword)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,                    
                    Credentials = new NetworkCredential(fromAddress, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(fromAddress, toAddress, subject, body);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send the email. Error: " + ex.Message);
            }
        }
        
        private void clearParticipantFields()
        {
            participantSelectat = new Participant();
            OnPropertyChanged("ParticipantSelectat");
        }

        //CRUD operations for Prezentare
        public void CreatePrezentare()
        {
            try
            {
                bool result = prezentareRepository.AddPrezentare(prezentareSelectata);
                listaPrezentari = prezentareRepository.GetPrezentari();
                OnPropertyChanged("ListaPrezentari");
                clearPrezentareFields();
                if (result)
                {
                    MessageBox.Show("Prezentarea a fost adaugata cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Prezentarea nu a putut fi adaugata!");
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
        public void UpdatePrezentare()
        {
            try
            {
                bool result = prezentareRepository.UpdatePrezentare(prezentareSelectata);
                listaPrezentari = prezentareRepository.GetPrezentari();
                OnPropertyChanged("ListaPrezentari");
                clearPrezentareFields();
                if (result)
                {
                    MessageBox.Show("Prezentarea a fost actualizata cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Prezentarea nu a putut fi actualizata!");
            }
        }
        public void DeletePrezentare()
        {
            try
            {
                bool result = prezentareRepository.DeletePrezentare(prezentareSelectata.Id);
                listaPrezentari = prezentareRepository.GetPrezentari();
                OnPropertyChanged("ListaPrezentari");
                clearPrezentareFields();
                if (result)
                {
                    MessageBox.Show("Prezentarea a fost stearsa cu succes!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Prezentarea nu a putut fi stearsa!");
            }
        }
        private void clearPrezentareFields()
        {
            prezentareSelectata = new Prezentare();
            OnPropertyChanged("PrezentareSelectata");
        }

        //Save file
        public void Save()
        {
            if (String.IsNullOrEmpty(selectedFileFormat))   
            {
                MessageBox.Show("Va rog alegeti un format de fisier!");
                return;
            }
            if (selectedFileFormat == "Csv")
            {
                SaveCsv();
            }
            if (selectedFileFormat == "Json")
            {
                SaveJson();
            }
            if (selectedFileFormat == "Xml")
            {
                SaveXml();
            }
            if (selectedFileFormat == "Doc")
            {
                SaveDoc();
            }
        }
        private void SaveCsv()
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
        private void SaveJson()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(listaPrezentari, options);
            File.WriteAllText(filePath, json);
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        private void SaveXml()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.xml";
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Prezentare>));
            using (var writer = new StreamWriter(filePath))
            {
                xml.Serialize(writer, listaPrezentari);
            }
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        private void SaveDoc()
        {
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

        private void back()
        {
            changeView("Home");
        }
    }
}

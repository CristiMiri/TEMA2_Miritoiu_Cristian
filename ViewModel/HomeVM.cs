﻿using PS_TEMA2.Model;
using PS_TEMA2.Model.Repositories;
using PS_TEMA2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//TODO: Add button to link to login page and Page Header
namespace PS_TEMA2.ViewModel
{
    internal class HomeVM : INotifyPropertyChanged
    {
        //Data consistency
        private PrezentareRepository prezentareRepository;
        private ParticipantiRepository participantiRepository;

        //Data containers
        public Participant participant;  
        public List<Prezentare> listaprezentari;      
        public List<Sectiune> listaSectiuni = Enum.GetValues(typeof(Sectiune)).Cast<Sectiune>().ToList();
        public Sectiune sectiuneSelectata;
        private Dictionary<int, string> comboPrezentari= new Dictionary<int, string>();
        //Commands              
        private HomeCommands searchPrezentare;
        private HomeCommands createParticipant;
        public HomeVM()
        {
            
            prezentareRepository = new PrezentareRepository();
            participantiRepository = new ParticipantiRepository();
            listaprezentari = prezentareRepository.GetPrezentari();
            FillComboPrezentari();
            participant = new Participant();
            searchPrezentare = new HomeCommands(Search);
            createParticipant = new HomeCommands(Create);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        //getters and setters containers
        public List<Prezentare> ListaPrezentari
        {
            get { return listaprezentari; }
            set
            {
                listaprezentari = value;
                OnPropertyChanged("ListaPrezentari");
            }
        }
        public List<Sectiune> ListaSectiuni
        {
            get { return listaSectiuni; }
        }
        public Sectiune SectiuneSelectata
        {
            get { return sectiuneSelectata; }
            set
            {
                sectiuneSelectata = value;
                OnPropertyChanged("SectiuneSelectata");
            }
        }
        public Participant Participant
        {
            get { return participant; }
            set
            {
                participant = value;
                OnPropertyChanged("Participant");
            }
        }
        public Dictionary<int, string> ComboPrezentari
        {
            get { return comboPrezentari; }
            set
            {
                comboPrezentari = value;
                OnPropertyChanged("ComboPrezentari");
            }
        }
        //getters and setters commands
        public HomeCommands SearchPrezentare
        {
            get { return searchPrezentare; }
        }
        public HomeCommands CreateParticipant
        {
            get { return createParticipant; }
        }

        //Commands implementation
        public void Create()
        {
            try
            {
                bool result = participantiRepository.addParticipant(participant);                
                Console.WriteLine(participant.Nume);
                Console.WriteLine(participant.Email);
                Console.WriteLine(participant.Telefon);
                Console.WriteLine(participant.IdPrezentare);
                if (result)
                {
                    MessageBox.Show("Participant adaugat cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la adaugarea participantului!");
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la adaugarea prezentarii!");
            }
        }

        public void Read()
        {

        }

        public void Search()
        {
            try
            {
                if (sectiuneSelectata != null)
                {
                    listaprezentari = prezentareRepository.getPrezentariBySectiune(sectiuneSelectata);
                    OnPropertyChanged("ListaPrezentari");
                }
                if (sectiuneSelectata == Sectiune.TOATE)
                {
                    listaprezentari = prezentareRepository.GetPrezentari();
                    OnPropertyChanged("ListaPrezentari");
                }
                
            }catch(Exception e)
            {
                MessageBox.Show("Va rog alegeti o sectiune!");
            }
        }

        //Fill combo box with prezentari Title and Id index
        private void FillComboPrezentari()
        {
            foreach (Prezentare prezentare in listaprezentari)
            {
                comboPrezentari.Add(prezentare.Id, prezentare.Titlu);
            }
        }
    }
}

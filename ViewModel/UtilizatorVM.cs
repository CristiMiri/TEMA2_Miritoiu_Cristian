using PS_TEMA2.Model;
using PS_TEMA2.Model.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.ViewModel
{
    internal class UtilizatorVM : INotifyPropertyChanged
    {
        //Data consistency
        private PrezentareRepository prezentareRepository;
        private ConferintaRepository conferintaRepository;

        //Data containers
        public List<Prezentare> listaprezentari;
        public List<Conferinta> listaConferinte;
        public object? listaCompleta;
        public UtilizatorVM()
        {
            prezentareRepository = new PrezentareRepository();
            conferintaRepository = new ConferintaRepository();
            listaprezentari = prezentareRepository.GetPrezentari();
            listaConferinte = conferintaRepository.GetConferinte();

            FillListaCompleta();
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
            }
        }

        public List<Conferinta> ListaConferinte
        {
            get { return listaConferinte; }
            set
            {
                listaConferinte = value;
            }
        }

        public object? ListaCompleta
        {
            get { return listaCompleta; }
            set
            {
                listaCompleta = value;
            }
        }

        //Fill the list with all the data from the other two lists
        private void FillListaCompleta()
        {
            ListaCompleta = (from conferinta in listaConferinte
                             join prezentare in listaprezentari on conferinta.Id equals prezentare.Id_conferinta
                             orderby prezentare.Id
                             select new
                             {
                                 ConferintaId = conferinta.Id,
                                 ConferintaTitlu = conferinta.Titlu,
                                 ConferintaLocatie = conferinta.Locatie,
                                 ConferintaData = conferinta.Data,
                                 PrezentareId = prezentare.Id,
                                 PrezentareTitlu = prezentare.Titlu,
                                 PrezentareAutor = prezentare.Autor,
                                 PrezentareDescriere = prezentare.Descriere,
                                 PrezentareData = prezentare.Data,
                                 PrezentareOra = prezentare.Ora,
                                 PrezentareSectiune = prezentare.Sectiune,
                                 PrezentareConferintaId = prezentare.Id_conferinta,
                             }).ToList();
            OnPropertyChanged("ListaCompleta");
        }

    }
}

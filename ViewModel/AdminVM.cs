using PS_TEMA2.Model;
using PS_TEMA2.Model.Repositories;
using PS_TEMA2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PS_TEMA2.ViewModel
{
    class AdminVM : INotifyPropertyChanged
    {
        //Data consistency
        private UtilizatorRepository utilizatorRepository;

        //Data containers
        public List<Utilizator> listaUtilizatori;
        public Utilizator utilizatorSelectat;
        public List<UserType> listaUserType = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();

        //Commands
        private AdminCommands createUtilizator;
        private AdminCommands updateUtilizator;
        private AdminCommands deleteUtilizator;
        private AdminCommands selectUtilizator;
        private AdminCommands bacKCommand;
        private Action<string> changeView;
        public AdminVM()
        {
            utilizatorSelectat = new Utilizator();
            utilizatorRepository = new UtilizatorRepository();
            listaUtilizatori = utilizatorRepository.GetUtilizatori();
            this.createUtilizator = new AdminCommands(Create);
            this.updateUtilizator = new AdminCommands(Update);
            this.deleteUtilizator = new AdminCommands(Delete);
            this.selectUtilizator = new AdminCommands(Select);
            this.bacKCommand = new AdminCommands(Back);
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
        public List<Utilizator> ListaUtilizatori
        {
            get { return listaUtilizatori; }
            set
            {
                listaUtilizatori = value;
                OnPropertyChanged("ListaUtilizatori");
            }
        }

        public Utilizator UtilizatorSelectat
        {
            get { return utilizatorSelectat; }
            set
            {
                utilizatorSelectat = value;
                OnPropertyChanged("UtilizatorSelectat");
            }
        }

        public List<UserType> ListaUserType
        {
            get { return listaUserType; }
        }

        //Commands getters
        public AdminCommands CreateUtilizator
        {
            get { return createUtilizator; }
        }

        public AdminCommands UpdateUtilizator
        {
            get { return updateUtilizator; }
        }

        public AdminCommands DeleteUtilizator
        {
            get { return deleteUtilizator; }
        }

        public AdminCommands SelectUtilizator
        {
            get { return selectUtilizator; }
        }

        public AdminCommands BackCommand
        {
            get { return bacKCommand; }
        }

        public Action<string> ChangeView
        {
            get { return changeView; }
            set { changeView = value; }
        }   
        //Commands implementation
        public void Create()
        {
            try
            {
                Console.WriteLine(utilizatorSelectat.ToString());
                bool result = utilizatorRepository.addUtilizator(utilizatorSelectat);
                listaUtilizatori = utilizatorRepository.GetUtilizatori();
                ClearUtilizatorFields();
                OnPropertyChanged("ListaUtilizatori");
                if (result)
                {
                    MessageBox.Show("Utilizator adaugat cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la adaugarea utilizatorului!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Eroare la adaugarea Utilizatorului!");
            }
        }

        public void Update()
        {
            try
            {
                Console.WriteLine(utilizatorSelectat.ToString());
                bool result = utilizatorRepository.updateUtilizator(utilizatorSelectat);
                listaUtilizatori = utilizatorRepository.GetUtilizatori();
                ClearUtilizatorFields();
                OnPropertyChanged("ListaUtilizatori");
                if (result)
                {
                    MessageBox.Show("Utilizator modificat cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la modificarea utilizatorului!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Eroare la modificarea Utilizatorului!");
            }
        }

        public void Delete()
        {
            try
            {
                Console.WriteLine(utilizatorSelectat.ToString());
                bool result = utilizatorRepository.deleteUtilizator(utilizatorSelectat.Id);
                listaUtilizatori = utilizatorRepository.GetUtilizatori();
                ClearUtilizatorFields();
                OnPropertyChanged("ListaUtilizatori");
                if (result)
                {
                    MessageBox.Show("Utilizator sters cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la stergerea utilizatorului!");
                }

            }
            catch (Exception e)
            {                
                Console.WriteLine(e.Message);
                MessageBox.Show("Eroare la stergerea Utilizatorului!");
            }
        }

        public void Select()
        {
            try
            {
                Console.WriteLine(utilizatorSelectat.ToString());
                OnPropertyChanged("UtilizatorSelectat");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Eroare la selectarea Utilizatorului!");
            }
        }

        private void ClearUtilizatorFields()
        {
            utilizatorSelectat = new Utilizator();
            OnPropertyChanged("UtilizatorSelectat");
        }

        private void Back()
        {
            changeView("Home");
        }
    }

}

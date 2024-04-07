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
    internal class LoginVM : INotifyPropertyChanged
    {
        //Data consistency
        private UtilizatorRepository utilizatorRepository;

        //Data containers
        public string email;
        public string parola;

        //Commands
        private LoginCommands loginCommand;
        private LoginCommands backCommand;
        private Action<string> changeView;
        public LoginVM()
        {
            utilizatorRepository = new UtilizatorRepository();
            this.loginCommand = new LoginCommands(Login);
            this.backCommand = new LoginCommands(Back);
            email = "";
            parola = "";
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
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Parola
        {
            get { return parola; }
            set
            {
                parola = value;
                OnPropertyChanged("Parola");
            }
        }

        //Commands getters
        public LoginCommands LoginCommand
        {
            get { return loginCommand; }
        }
        public LoginCommands BackCommand
        {
            get { return backCommand; }
        }
        public Action<string> ChangeView
        {
            get { return changeView; }
            set { changeView = value; }
        }

        //Commands implementations
        private void Login()
        {
            Utilizator utilizator = validData();
            Utilizator utilizatorLogat = utilizatorRepository.
                GetUtilizatorbyEmailandParola(email, parola);

            Console.WriteLine(utilizatorLogat);

            try
            {
                if (utilizator != null)
                {
                    switch (utilizatorLogat.UserType)
                    {
                        case UserType.ADMINISTRATOR:
                            changeView("Admin");
                            break;
                        case UserType.PARTICIPANT:
                            changeView("Utilizator");
                            break;
                        case UserType.ORGANIZATOR:
                            changeView("Organizator");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid username or password", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back()
        {
            changeView("Home");
        }

        private Utilizator validData()
        {
            if (email.Length < 3 && !email.Contains("@") && email.Length > 30)
            {                
                MessageBox.Show("Invalid email", "Invalid email", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else if (parola.Length < 3)
            {
                MessageBox.Show("Password too short", "Password too short", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return new Utilizator(email, parola);
        }
    }
}

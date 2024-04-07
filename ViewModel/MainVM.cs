using Npgsql.Replication;
using PS_TEMA2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.ViewModel
{
    internal class MainVM : INotifyPropertyChanged
    {
        private object currentViewModel;
        private bool isHeaderVisible = true; // Assuming default visibility is Visible

        private MainCommands loginCommand;
        public MainVM()
        {
            //change view to Login
            loginCommand = new MainCommands(ChageView);
        }

        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public bool IsHeaderVisible
        {
            get => isHeaderVisible;
            set
            {
                isHeaderVisible = value;
                OnPropertyChanged("IsHeaderVisible");
            }
        }
        public MainCommands LoginCommand
        {
            get { return loginCommand; }
        }

        public void ChageView(string view)
        {
            if (!string.IsNullOrEmpty(view))
            {
                   switch (view)
                {
                    case "Admin":
                        AdminVM adminVM = new AdminVM();
                        adminVM.ChangeView = ChageView;
                        CurrentViewModel = adminVM;
                        IsHeaderVisible = false;
                        break;
                    case "Home":
                        HomeVM homeVM = new HomeVM();
                        CurrentViewModel = homeVM;
                        IsHeaderVisible = true;
                        break;
                    case "Login":
                        LoginVM loginVM = new LoginVM();
                        loginVM.ChangeView = ChageView;
                        CurrentViewModel = loginVM;
                        IsHeaderVisible = false;
                        break;
                    case "Organizator":
                        OrganizatorVM organizatorVM = new OrganizatorVM();
                        organizatorVM.ChangeView = ChageView;
                        CurrentViewModel = organizatorVM;
                        IsHeaderVisible = false;
                        break;
                    case "Utilizator":
                        UtilizatorVM utilizatorVM = new UtilizatorVM();
                        utilizatorVM.ChangeView = ChageView;
                        CurrentViewModel = utilizatorVM;
                        IsHeaderVisible = false;
                        break;
                    
                    default:
                        break;
                }
            }
            OnPropertyChanged("CurrentViewModel");
        }
    }
}

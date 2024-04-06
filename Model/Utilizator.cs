using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA2.Model
{
    public enum UserType
    {
        PARTICIPANT,
        ORGANIZATOR,
        ADMINISTRATOR
    }

    public class Utilizator
    {
        private int id;
        private String nume;
        private String email;
        private String parola;
        private UserType userType;
        private String telefon;
        

        public Utilizator(int id, string nume, string email, string parola, UserType userType, string telefon)
        {
            this.id = id;
            this.nume = nume;
            this.email = email;
            this.parola = parola;
            this.userType = userType;
            this.telefon = telefon;
        }

        public Utilizator(Utilizator utilizator)
        {
            this.id = utilizator.id;
            this.nume = utilizator.nume;
            this.email = utilizator.email;
            this.parola = utilizator.parola;
            this.userType = utilizator.userType;
            this.telefon = utilizator.telefon;
        }

        public Utilizator()
        {
            this.id = 0;
            this.nume = "";
            this.email = "";
            this.parola = "";
            this.userType = UserType.PARTICIPANT;
            this.telefon = "";
        }

        public Utilizator(string email, string password)
        {
            this.email = email;
            this.parola = password;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public String Nume
        {
            get { return nume; }
            set { nume = value; }
        }
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        public String Parola
        {
            get { return parola; }
            set { parola = value; }
        }
        public UserType UserType
        {
            get { return userType; }
            set { userType = value; }
        }
        public String Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }

        public override string ToString()
        {
            return "Utilizatorul cu id-ul " + id + " si numele " + nume + " are email-ul " + email + " si parola " + parola + " si este de tipul " + userType + " si are numarul de telefon " + telefon + ".";
        }
    }
}

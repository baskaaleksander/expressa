using Misc;
using System;
using static Misc.addons;
using System.Text;
using System.Linq.Expressions;

namespace expressa
{
    internal class Program
    {
        const string bazaPaczek = "bazapaczek.csv";
        const string bazaUzytkownikow = "bazauzytkownikow.csv";
        const string logo = @"                                                                               
                                                                               
  /$$$$$$  /$$   /$$  /$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$$ /$$$$$$$  /$$$$$$ 
 /$$__  $$|  $$ /$$/ /$$__  $$ /$$__  $$ /$$__  $$ /$$_____//$$_____/ |____  $$
| $$$$$$$$ \  $$$$/ | $$  \ $$| $$  \__/| $$$$$$$$|  $$$$$$|  $$$$$$   /$$$$$$$
| $$_____/  >$$  $$ | $$  | $$| $$      | $$_____/ \____  $$\____  $$ /$$__  $$
|  $$$$$$$ /$$/\  $$| $$$$$$$/| $$      |  $$$$$$$ /$$$$$$$//$$$$$$$/|  $$$$$$$
 \_______/|__/  \__/| $$____/ |__/       \_______/|_______/|_______/  \_______/
                    | $$                                                       
                    | $$                                                       
                    |__/                                                       ";
        public int loginNrTelefonu;
        static List<danePaczka> danePaczek = new();
        static List<daneUzytkownikow> bazaUzytkownikcy = new();

        struct daneUzytkownikow
        {
            public int numerTelefonuUzytkownika;
            public int saldoUzytkownika;
            public int statusKonta;
            public string hasloUzytkownika;

            public void pokazDaneUzytkownika()
            {
                Console.WriteLine($"Numer telefonu użytkownika: {numerTelefonuUzytkownika} Saldo użytkownika: {saldoUzytkownika} Status konta: {statusKonta}");
            }
            public string generujDaneUzytkownika()
            {
                return $"{numerTelefonuUzytkownika};{saldoUzytkownika};{statusKonta};{hasloUzytkownika}";

            }

            public void zarejestrujUzytkownika()
            {
                numerTelefonuUzytkownika = addons.podajNumerTelefonu("Podaj swój numer telefonu");
                saldoUzytkownika = 0;
                statusKonta = 0;
                string przedZabezpieczeniem = addons.wprowadzString("Podaj swoje hasło", false);
                hasloUzytkownika = addons.Encrypt(przedZabezpieczeniem);
            }

            public void zmienSaldoUzytkownika()
            {
                Console.WriteLine($"Aktualne saldo użytkownika to {saldoUzytkownika}PLN");
                saldoUzytkownika = addons.wprowadzInt("Podaj nowe saldo użytkownika");
            }
            public void doladujKontoUzytkownika()
            {
                Console.WriteLine($"Aktualne saldo użytkownika to {saldoUzytkownika}PLN");
                saldoUzytkownika += addons.wprowadzInt("Wprowadz kwote doladowania");
            }
            public void zmienUprawnieniaUzytkownika()
            {
                Console.WriteLine($"Aktualne uprawnienia użytkownika to {statusKonta}");
                statusKonta = addons.wprowadzInt("Podaj nowe uprawnienia użytkownika");
            }

            public void wpiszDaneUzytkownika()
            {
                numerTelefonuUzytkownika = addons.podajNumerTelefonu("Podaj numer telefonu użytkownika");
                saldoUzytkownika = addons.wprowadzInt("Podaj saldo użytkownika");
                statusKonta = addons.wprowadzInt("Podaj status konta użytkownika");
                hasloUzytkownika = addons.wprowadzString("Podaj hasło użytkownika", false);
            }

            public void parsujDaneUzytkownika(string linia)
            {
                var daneUzytkownika = linia.Split(';');

                int.TryParse(daneUzytkownika[0], out numerTelefonuUzytkownika);
                int.TryParse(daneUzytkownika[1], out saldoUzytkownika);
                int.TryParse(daneUzytkownika[2], out statusKonta);
                hasloUzytkownika = daneUzytkownika[3];
            }
        }


        struct danePaczka
        {
            public string imieNadawcy;
            public string nazwiskoNadawcy;
            public int numerNadawcy;
            public int kodNadawcy;
            public string miastoNadawcy;
            public string ulicaNadawcy;
            public int nrDomuNadawcy;
            public string imieOdbiorcy;
            public string nazwiskoOdbiorcy;
            public int numerOdbiorcy;
            public int kodOdbiorcy;
            public string miastoOdbiorcy;
            public string ulicaOdbiorcy;
            public int nrDomuOdbiorcy;
            public long numerPaczki;
            public int status;

            public void pokazDanePaczki()
            {
                Console.WriteLine($"Imie nadawcy: {imieNadawcy} Nazwisko nadawcy: {nazwiskoNadawcy} Numer nadawcy: {numerNadawcy} Kod pocztowy nadawcy: {kodNadawcy} Miasto nadawcy: {miastoNadawcy} Ulica nadawcy: {ulicaNadawcy} Nr domu nadawcy {nrDomuNadawcy} Imie odbiorcy: {imieOdbiorcy} Nazwisko odbiorcy: {nazwiskoOdbiorcy} Numer odbiorcy: {numerOdbiorcy} Kod pocztowy odbiorcy: {kodOdbiorcy} Miasto odbiorcy: {miastoOdbiorcy} Ulica odbiorcy: {ulicaOdbiorcy} Nr domu odbiorcy {nrDomuOdbiorcy} Numer paczki: {numerPaczki} Status: {status}\n");
            }
            public string generujDanePaczki()
            {
                return $"{imieNadawcy};{nazwiskoNadawcy};{numerNadawcy};{kodNadawcy};{miastoNadawcy};{ulicaNadawcy};{nrDomuNadawcy};{imieOdbiorcy};{nazwiskoOdbiorcy};{numerOdbiorcy};{kodOdbiorcy};{miastoOdbiorcy};{ulicaOdbiorcy};{nrDomuOdbiorcy};{numerPaczki};{status}";
            }
            public void wpiszDane()
            {
                imieNadawcy = addons.wprowadzString("Podaj imie nadawcy: ", false);
                nazwiskoNadawcy = addons.wprowadzString("Podaj nazwisko nadawcy: ", false);
                numerNadawcy = addons.podajNumerTelefonu("Podaj numer telefonu nadawcy: ");
                kodNadawcy = addons.podajKodPocztowy("Podaj kod pocztowy nadawcy: ");
                miastoNadawcy = addons.wprowadzString("Podaj miasto nadawcy: ", false);
                ulicaNadawcy = addons.wprowadzString("Podaj ulice nadawcy: ", false);
                nrDomuNadawcy = addons.wprowadzInt("Podaj numer domu nadawcy: ");
                imieOdbiorcy = addons.wprowadzString("Podaj imie odbiorcy: ", false);
                nazwiskoOdbiorcy = addons.wprowadzString("Podaj nazwisko odbiorcy: ", false);
                numerOdbiorcy = addons.podajNumerTelefonu("Podaj numer telefonu odbiorcy: ");
                kodOdbiorcy = addons.podajKodPocztowy("Podaj kod pocztowy odbiorcy: ");
                miastoOdbiorcy = addons.wprowadzString("Podaj miasto odbiorcy: ", false);
                ulicaOdbiorcy = addons.wprowadzString("Podaj ulice odbiorcy: ", false);
                nrDomuOdbiorcy = addons.wprowadzInt("Podaj numer domu odbiorcy: ");
            }
            public void odebrane()
            {
                status = 2;
            }
            public void dostarczone()
            {
                status = 3;
            }
            public void zmienDaneNadawcy()
            {

                Console.WriteLine($"Aktualne imię nadawcy to {imieNadawcy}");
                imieNadawcy = addons.wprowadzString("Podaj nowe imię nadawcy: ", false);
                Console.WriteLine($"Aktualne nazwisko nadawcy to {nazwiskoNadawcy}");
                nazwiskoNadawcy = addons.wprowadzString("Podaj nowe nazwisko nadawcy: ", false);
                Console.WriteLine($"Aktualny numer telefonu nadawcy to {numerNadawcy}");
                numerNadawcy = addons.podajNumerTelefonu("Podaj nowy numer telefonu nadawcy: ");
                Console.WriteLine($"Aktualny kod pocztowy nadawcy to {kodNadawcy}");
                kodNadawcy = addons.podajKodPocztowy("Podaj nowy kod pocztowy nadawcy: ");
                Console.WriteLine($"Aktualne miasto nadawcy to {miastoNadawcy}");
                miastoNadawcy = addons.wprowadzString("Podaj nowe miasto nadawcy: ", false);
                Console.WriteLine($"Aktualna ulica nadawcy to {ulicaNadawcy}");
                ulicaNadawcy = addons.wprowadzString("Podaj nową ulicę nadawcy: ", false);
                Console.WriteLine($"Aktualny numer domu nadawcy to {nrDomuNadawcy}");
                nrDomuNadawcy = addons.wprowadzInt("Podaj nowy numer domu nadawcy: ");
            }
            public void zmienDaneOdbiorcy()
            {
                Console.WriteLine($"Aktualne imię odbiorcy to {imieOdbiorcy}");
                imieOdbiorcy = addons.wprowadzString("Podaj nowe imię odbiorcy: ", false);
                Console.WriteLine($"Aktualne nazwisko odbiorcy to {nazwiskoOdbiorcy}");
                nazwiskoOdbiorcy = addons.wprowadzString("Podaj nowe nazwisko odbiorcy: ", false);
                Console.WriteLine($"Aktualny numer telefonu odbiorcy to {numerOdbiorcy}");
                numerOdbiorcy = addons.podajNumerTelefonu("Podaj nowy numer telefonu odbior: ");
                Console.WriteLine($"Aktualny kod pocztowy odbiorcy to {kodOdbiorcy}");
                kodOdbiorcy = addons.podajKodPocztowy("Podaj nowy kod pocztowy odbiorcy: ");
                Console.WriteLine($"Aktualne miasto odbiorcy to {miastoOdbiorcy}");
                miastoOdbiorcy = addons.wprowadzString("Podaj nowe miasto odbiorcy: ", false);
                Console.WriteLine($"Aktualna ulica odbiorcy to {ulicaOdbiorcy}");
                ulicaOdbiorcy = addons.wprowadzString("Podaj nową ulicę odbiorcy: ", false);
                Console.WriteLine($"Aktualny numer domu odbiorcy to {nrDomuOdbiorcy}");
                nrDomuOdbiorcy = addons.wprowadzInt("Podaj nowy numer domu odbiorcy: ");

            }
            public void parsujDanePaczki(string linia)
            {
                var danePaczki = linia.Split(';');

                imieNadawcy = danePaczki[0];
                nazwiskoNadawcy = danePaczki[1];
                int.TryParse(danePaczki[2], out numerNadawcy);
                int.TryParse(danePaczki[3], out kodNadawcy);
                miastoNadawcy = danePaczki[4];
                ulicaNadawcy = danePaczki[5];
                int.TryParse(danePaczki[6], out nrDomuNadawcy);
                imieOdbiorcy = danePaczki[7];
                nazwiskoOdbiorcy = danePaczki[8];
                int.TryParse(danePaczki[9], out numerOdbiorcy);
                int.TryParse(danePaczki[10], out kodOdbiorcy);
                miastoOdbiorcy = danePaczki[11];
                ulicaOdbiorcy = danePaczki[12];
                int.TryParse(danePaczki[13], out nrDomuOdbiorcy);
                long.TryParse(danePaczki[14], out numerPaczki);
                int.TryParse(danePaczki[15], out status);
            }

        }
        static void wczytajDane()
        {
            danePaczek.Clear();
            bazaUzytkownikcy.Clear();
            if (File.Exists(bazaPaczek) == true)
            {
                using var plik = new StreamReader(bazaPaczek);

                while (plik.EndOfStream == false)
                {
                    var linia = plik.ReadLine();

                    danePaczka danePaczka = new danePaczka();
                    danePaczka.parsujDanePaczki(linia);

                    danePaczek.Add(danePaczka);

                }

            }
            else
            {
                Console.WriteLine("Nie znaleziono pliku z bazą użytkowników");

            }
            if (File.Exists(bazaUzytkownikow) == true)
            {
                using var plikUzytkownikow = new StreamReader(bazaUzytkownikow);

                while (plikUzytkownikow.EndOfStream == false)
                {
                    var linia = plikUzytkownikow.ReadLine();

                    daneUzytkownikow daneUzytkownikow = new daneUzytkownikow();
                    daneUzytkownikow.parsujDaneUzytkownika(linia);

                    bazaUzytkownikcy.Add(daneUzytkownikow);

                }

            }
            else
            {
                Console.WriteLine("Nie znaleziono pliku z bazą użytkowników");
            }
        }
        static void zapiszDane()
        {
            using var plikPaczki = new StreamWriter(bazaPaczek);
            using var plikUzytkownicy = new StreamWriter(bazaUzytkownikow);

            foreach (var paczka in danePaczek)
            {
                plikPaczki.WriteLine(paczka.generujDanePaczki());
            }
            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                plikUzytkownicy.WriteLine(uzytkownik.generujDaneUzytkownika());
            }
        }

        static void dodajPaczke()
        {
            danePaczka paczka = new danePaczka();
            paczka.wpiszDane();
            paczka.status = 1;
            paczka.numerPaczki = addons.generujNumerPaczki();
            danePaczek.Add(paczka);
        }

        static void wyswietlPaczki()
        {
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                Console.Write($"{index} ");
                paczka.pokazDanePaczki();
                index++;
            }
        }
        static void wyswietlPaczkiStatusPierwszy()
        {
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                if (paczka.status == 1) {
                    Console.Write($"{index} ");
                    paczka.pokazDanePaczki();
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie masz paczek do odebrania");
            }
        }

        static void wyswietlPaczkiStatusDrugi()
        {
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                if (paczka.status == 2)
                {
                    Console.Write($"{index} ");
                    paczka.pokazDanePaczki();
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie masz paczek do doręczenia");
            }
        }
        static void nadaneDo(int login)
        {
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                if (paczka.numerOdbiorcy == login)
                {
                    Console.Write($"{index} ");
                    paczka.pokazDanePaczki();
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie ma paczek nadanych do ciebie");
            }

        }
        static void nadanePrzez(int login)
        {
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                if (paczka.numerNadawcy == login)
                {
                    Console.Write($"{index} ");
                    paczka.pokazDanePaczki();
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie ma paczek nadanych przez ciebie");
            }

        }

        static void usunPaczke()
        {
            wyswietlPaczki();
            int index = addons.wprowadzInt("Podaj numer paczki do usunięcia");
            index--;
            if (index >= 0 && index < danePaczek.Count)
            {
                danePaczek.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Nie ma takiej paczki");
            }
        }
        static void poprawPaczke()
        {
            if (danePaczek.Count > 0)
            {
                wyswietlPaczki();

                int index = addons.wprowadzInt("Podaj numer paczki do poprawy", 0, danePaczek.Count);

                if (index > 0)
                {
                    index--;
                    danePaczka paczka = danePaczek[index];
                    bool czyZmienicNadawca = addons.pytanie("Czy chcesz zmienić dane nadawcy?");
                    if (czyZmienicNadawca == true)
                    {
                        paczka.zmienDaneNadawcy();
                    }
                    bool czyZmienicOdbiorca = addons.pytanie("Czy chcesz zmienić dane odbiorcy");
                    if (czyZmienicOdbiorca == true)
                    {
                        paczka.zmienDaneOdbiorcy();
                    }
                    danePaczek[index] = paczka;
                }
            }
        }
        static void listauzytkownikow()
        {
            Console.WriteLine("Lista użytkowników: ");
            if (bazaUzytkownikcy.Count == 0)
            {
                Console.WriteLine("Brak użytkowników");
            }
            else
            {
                int index = 1;
                foreach (var uzytkownik in bazaUzytkownikcy)
                {
                    Console.Write($"{index} ");
                    uzytkownik.pokazDaneUzytkownika();
                    index++;
                }

            }
        }
        static void zmiensaldo()
        {
            Console.WriteLine("Zmień saldo użytkownika ");


            if (bazaUzytkownikcy.Count > 0)
            {
                int numerDoZmiany = addons.podajNumerTelefonu("Podaj numer telefonu użytkownika, którego saldo chcesz zmienić");
                int index = 0;
                foreach (var uzytkownik in bazaUzytkownikcy)
                {

                    if (uzytkownik.numerTelefonuUzytkownika == numerDoZmiany)
                    {
                        break;
                    }
                    else { index++; }
                    
                }
                daneUzytkownikow daneUzytkownikow = bazaUzytkownikcy[index];
                daneUzytkownikow.zmienSaldoUzytkownika();
                bazaUzytkownikcy[index] = daneUzytkownikow;

            }
            else
            {
                Console.WriteLine("Brak użytkowników do zmiany salda");
            }
        }
        static void doladujKonto(int login)
        {
            int index = 0;
            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                if (uzytkownik.numerTelefonuUzytkownika == login)
                {
                    break;
                }
                else { index++; }
            }
            daneUzytkownikow daneUzytkownikow = bazaUzytkownikcy[index];
            daneUzytkownikow.doladujKontoUzytkownika();
            bazaUzytkownikcy[index] = daneUzytkownikow;

        }
        static void nadajPaczke(int login)
        {
            int index = 0;
            danePaczka paczka = new danePaczka();

            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                if (uzytkownik.numerTelefonuUzytkownika == login)
                {
                    break;
                }
                else { index++; }
            }
            daneUzytkownikow daneUzytkownikow = bazaUzytkownikcy[index];
            if (daneUzytkownikow.saldoUzytkownika > 12)
            {
                paczka.wpiszDane();
                paczka.status = 1;
                paczka.numerPaczki = addons.generujNumerPaczki();
                danePaczek.Add(paczka);
                daneUzytkownikow.saldoUzytkownika -= 12;
            }
            else
            {
                Console.WriteLine("Brak środków na koncie");
            }
            bazaUzytkownikcy[index] = daneUzytkownikow;

        }
        static int wyswietlStatusPaczki(long numerPaczki)
        {
            foreach (var paczka in danePaczek)
            {
                if (paczka.numerPaczki == numerPaczki)
                {
                    return paczka.status;
                }
            }
            return 10;
        }

        static void sprawdzStatusPaczki()
        {
            long numerPaczki = addons.wprowadzLong("Podaj numer paczki: ");
            int status = wyswietlStatusPaczki(numerPaczki);
            if (status == 1)
            {
                Console.WriteLine("Paczka została nadana");
            }
            else if (status == 2)
            {
                Console.WriteLine("Paczka odebrana przez kuriera");
            }
            else if (status == 3)
            {
                Console.WriteLine("Paczka doręczona");
            }
            else
            {
                Console.WriteLine("Nie znaleziono paczki");
            }
        }

        static void zmienuprawnienia()
        {
            Console.WriteLine("Zmień saldo użytkownika ");


            if (bazaUzytkownikcy.Count > 0)
            {
                int numerDoZmiany = addons.podajNumerTelefonu("Podaj numer telefonu użytkownika, którego saldo chcesz zmienić");
                int index = 0;
                foreach (var uzytkownik in bazaUzytkownikcy)
                {

                    if (uzytkownik.numerTelefonuUzytkownika == numerDoZmiany)
                    {
                        break;
                    }
                    index++;
                }
                daneUzytkownikow daneUzytkownikow = bazaUzytkownikcy[index];
                daneUzytkownikow.zmienUprawnieniaUzytkownika();
                bazaUzytkownikcy[index] = daneUzytkownikow;

            }
            else
            {
                Console.WriteLine("Brak użytkowników do zmiany uprawnien");
            }
        }

        static void usunuzywtkownika()
        {
            Console.WriteLine("Usuń użytkownika: ");
            if (bazaUzytkownikcy.Count == 0)
            {
                Console.WriteLine("Brak użytkowników ");
                return;
            }

            int numerDoZmiany = addons.podajNumerTelefonu("Podaj numer telefonu użytkownika, którego chcesz usunąć ");
            int index = 0;
            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                index++;
                if (uzytkownik.numerTelefonuUzytkownika == numerDoZmiany)
                {
                    bazaUzytkownikcy.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine($"Nie znaleziono użytkownika o numerze telefonu {numerDoZmiany}");
                }
            }
        }


        static void nowyUser()
        {
            daneUzytkownikow uzytkownik = new daneUzytkownikow();
            uzytkownik.zarejestrujUzytkownika();
            bazaUzytkownikcy.Add(uzytkownik);
        }

        static void odbierzPaczki()
        {

            List<string> listaOpcji = new List<string>();

            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            int index = 1;
            foreach (var paczka in danePaczek)
            {
                if (paczka.status == 1)
                {
                    Console.Write($"{index} ");
                    paczka.pokazDanePaczki();
                    listaOpcji.Add($"{paczka.numerPaczki.ToString()} - {paczka.ulicaNadawcy} - {paczka.miastoNadawcy}");
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie masz paczek do odebrania");
                return;
            }
            string[] options = listaOpcji.ToArray();
            
            Checkbox c3 = new Checkbox("Które paczki chcesz odebrać z centrali?", true, false, options);
            var res3 = c3.Select();

            List<long> listaNumerow = new List<long>();

            foreach (var checkboxReturn in res3)
            {
                listaNumerow.Add(long.Parse(checkboxReturn.Option.Substring(0, 15)));
            }
            foreach (var item in listaNumerow)
            {


                foreach (var paczki in danePaczek)
                {
                    index = 0;
                    if (paczki.numerPaczki == item)
                    {
                        index++;
                    }
                }
                danePaczka paczka = danePaczek[index];
                paczka.odebrane();
                danePaczek[index] = paczka;
                Console.WriteLine(danePaczek[index].status);

            }
        }
        static void dostarczPaczki()
        {

            List<string> listaOpcji = new List<string>();

            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            int index = 1;
            foreach (var paczki in danePaczek)
            {
                if (paczki.status == 2)
                {
                    Console.Write($"{index} ");
                    paczki.pokazDanePaczki();
                    listaOpcji.Add($"{paczki.numerPaczki.ToString()} - {paczki.ulicaNadawcy} - {paczki.miastoNadawcy}");
                    index++;
                }
            }
            if (index == 1)
            {
                Console.WriteLine("Nie masz paczek do odebrania");
                return;
            }
            string[] options = listaOpcji.ToArray();
            
            Checkbox c3 = new Checkbox("Którą paczke chcesz dostarczyć?", false, false, options);
            var res3 = c3.Select();

            List<long> listaNumerow = new List<long>();

            foreach (var numer in res3)
            {
                listaNumerow.Add(long.Parse(numer.Option.Substring(0, 15)));
            }

            long[] dupa = listaNumerow.ToArray();
            int indexx = -1;
            foreach (var paczki in danePaczek)
            {
                
                if (paczki.numerPaczki == dupa[0])
                {
                    break;
                }

                else { index++; }

            }
            danePaczka paczka = danePaczek[indexx];
            paczka.dostarczone();
            danePaczek[indexx] = paczka;
            Console.WriteLine(danePaczek[indexx].status);


        }



        static void menedzeruz()
        {
            bool pytanie = true;
            while (pytanie) { 
                addons.Menu uzytkownicy = new addons.Menu(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White);
                uzytkownicy.dodaj(new addons.pozycjaMenu(0, "Zmień saldo ", ConsoleKey.F1));
                uzytkownicy.dodaj(new addons.pozycjaMenu(1, "Nadaj uprawnienia", ConsoleKey.F2));
                uzytkownicy.dodaj(new addons.pozycjaMenu(2, "Lista uzytkownikow ", ConsoleKey.F3));
                uzytkownicy.dodaj(new addons.pozycjaMenu(3, "Usuń użytkownika ", ConsoleKey.F4));
                uzytkownicy.dodaj(new addons.pozycjaMenu(4, "Cofnij do menu ", ConsoleKey.Escape));

                switch(uzytkownicy.wybierz())
                {
                    case 0: 
                        zmiensaldo();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 1: 
                        zmienuprawnienia();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 2: 
                        listauzytkownikow();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 3: 
                        usunuzywtkownika();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 4:  
                        menuAdmin();
                        break;
                }
            }
        }
        static void menuUzytkownik(int login)
        {
            bool pytanie = true;
            while (pytanie)
            {
                Console.Clear();
                Console.WriteLine(logo);
                addons.Menu uzytkownikMenu = new addons.Menu(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White);
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(0, "Wyslij paczke", ConsoleKey.F1));
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(1, "Sprawdz status paczki", ConsoleKey.F2));
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(2, "Lista nadanych do ciebie paczek", ConsoleKey.F3));
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(3, "Lista wysłanych przez ciebie paczek", ConsoleKey.F4));
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(4, "Doladuj konto", ConsoleKey.F5));
                uzytkownikMenu.dodaj(new addons.pozycjaMenu(5, "Wyjście z programu", ConsoleKey.Escape));
                switch (uzytkownikMenu.wybierz())
                {
                    case 0:
                        nadajPaczke(login);
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 1:
                        sprawdzStatusPaczki();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 2:
                        nadaneDo(login);
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 3:
                        nadanePrzez(login);
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 4:
                        doladujKonto(login);
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 5:
                        Console.WriteLine("Do widzenia!");
                        break;
                }
            }
        }
        static void menuKurier()
        {
            bool pytanie = true;
            while (pytanie) { 
            Console.Clear();
            Console.WriteLine(logo);
            addons.Menu kurierMenu = new addons.Menu(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White);
            kurierMenu.dodaj(new addons.pozycjaMenu(0, "Odbierz paczki", ConsoleKey.F1));
            kurierMenu.dodaj(new addons.pozycjaMenu(1, "Doręcz paczkę", ConsoleKey.F2));
            kurierMenu.dodaj(new addons.pozycjaMenu(2, "Wyjście z programu", ConsoleKey.Escape));
            switch (kurierMenu.wybierz())
            {
                case 0:
                    odbierzPaczki();
                    pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                    break;

                case 1:
                    dostarczPaczki();
                    pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                    break;

                case 2:
                    break;
            }
            }
            
        }
        static void menuAdmin()
        {
            bool pytanie = true;
            while (pytanie) { 
                Console.Clear();
                Console.WriteLine(logo);
                addons.Menu menu = new addons.Menu(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.White);
                menu.dodaj(new addons.pozycjaMenu(0, "Dodaj paczkę", ConsoleKey.F1));
                menu.dodaj(new addons.pozycjaMenu(1, "Zmien dane paczki", ConsoleKey.F2));
                menu.dodaj(new addons.pozycjaMenu(2, "Usun paczke", ConsoleKey.F3));
                menu.dodaj(new addons.pozycjaMenu(3, "Menedzer uzytkownikow", ConsoleKey.F4));
                menu.dodaj(new addons.pozycjaMenu(4, "Wyjście z programu", ConsoleKey.Escape));

                switch (menu.wybierz())
                {
                    case 0:
                        dodajPaczke();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;

                    case 1:
                        poprawPaczke();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;

                    case 2:
                        usunPaczke();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 3:
                        menedzeruz();
                        pytanie = addons.pytanie("Chcesz wykonać kolejną akcję?");
                        break;
                    case 4:
                        break;
                }
            }

        }
        static int podajStatusKonta(int numerTelefonuUzytkownika)
        {
            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                if (uzytkownik.numerTelefonuUzytkownika == numerTelefonuUzytkownika)
                {
                    return uzytkownik.statusKonta;
                }
            }
            return 10;
        }

        static void logowanie()
        {
            bool brakUzytkownika = false;
            Console.WriteLine(logo);
            int login = addons.podajNumerTelefonu("Podaj numer telefonu: ");
            foreach (var uzytkownik in bazaUzytkownikcy)
            {
                Console.Write("Podaj hasło: ");
                string password = Console.ReadLine();
                if (uzytkownik.numerTelefonuUzytkownika == login)
                {
                    if (uzytkownik.hasloUzytkownika == addons.Encrypt(password))
                    {
                        Console.WriteLine("Witaj");
                        int status = podajStatusKonta(login);
                        if (status == 0)
                        {
                            menuUzytkownik(login);
                            break;
                        }
                        else if (status == 1)
                        {
                            menuKurier();
                            break;
                        }
                        else
                        {
                            menuAdmin();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("bledne haslo");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Nie znaleziono użytkownika");

                    if (addons.pytanie("Czy chcesz się zarejestrować?"))
                    {
                        nowyUser();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Do widzenia!");
                        break;
                    }
                }

            }




        }




        static void Main(string[] args)
        {
            wczytajDane();
            logowanie();
            zapiszDane();
        }
    }
}

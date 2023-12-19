using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Misc
{
    public class addons
    {
        public struct pozycjaMenu
        {
            public int id;
            public string tekst;
            public ConsoleKey przycisk;
            public int dlugoscPozycjiMenu;



            public pozycjaMenu(int Id, string tekst, ConsoleKey przycisk)
            {
                this.id = Id;
                this.tekst = $"[{przycisk}] - {tekst}";
                this.przycisk = przycisk;
                this.dlugoscPozycjiMenu = this.tekst.Length;
            }

            public void wyswietl(ConsoleColor kolorTla, ConsoleColor kolorCzcionki, int maksymalnaDlugosc)
            {
                Console.BackgroundColor = kolorTla;
                Console.ForegroundColor = kolorCzcionki;
                Console.Write(tekst);

                if (dlugoscPozycjiMenu < maksymalnaDlugosc)
                {
                    for (int i = dlugoscPozycjiMenu; i < maksymalnaDlugosc; i++)
                        Console.Write(" ");
                }

                Console.ResetColor();
            }
        }

        public struct Menu
        {
            public List<pozycjaMenu> Pozycje = new List<pozycjaMenu>();
            public int maksymalnaDlugoscMenu;
            public ConsoleColor kolorTla;
            public ConsoleColor kolorCzcionki;
            public ConsoleColor kolorTlaZaznaczony;
            public ConsoleColor kolorCzcionkiZaznaczony;
            public int aktualnaPozycjaMenu;

            public Menu(ConsoleColor kolorTla, ConsoleColor kolorCzcionki, ConsoleColor kolorTlaZaznaczony,
                        ConsoleColor kolorCzcionkiZaznaczony)
            {
                this.kolorTla = kolorTla;
                this.kolorCzcionki = kolorCzcionki;
                this.kolorTlaZaznaczony = kolorTlaZaznaczony;
                this.kolorCzcionkiZaznaczony = kolorCzcionkiZaznaczony;
                maksymalnaDlugoscMenu = 0;
                aktualnaPozycjaMenu = 0;
            }

            public void dodaj(pozycjaMenu pozycja)
            {
                Pozycje.Add(pozycja);

                if (pozycja.dlugoscPozycjiMenu > maksymalnaDlugoscMenu)
                {
                    maksymalnaDlugoscMenu = pozycja.dlugoscPozycjiMenu;
                }
            }

            public void wyswietl(int Left, int Top)
            {
                int aktualnaPozycja = 0;

                foreach (var pozycja in Pozycje)
                {
                    Console.SetCursorPosition(Left, Top + aktualnaPozycja);
                    if (aktualnaPozycja == aktualnaPozycjaMenu)
                    {
                        pozycja.wyswietl(kolorTlaZaznaczony, kolorCzcionkiZaznaczony, maksymalnaDlugoscMenu);
                    }
                    else
                    {
                        pozycja.wyswietl(kolorTla, kolorCzcionki, maksymalnaDlugoscMenu);
                    }
                    aktualnaPozycja++;
                }
            }

            public int wybierz()
            {
                var startRysowania = Console.GetCursorPosition();

                Console.CursorVisible = false;

                while (true)
                {
                    wyswietl(startRysowania.Left, startRysowania.Top);

                    var przycisk = Console.ReadKey(true).Key;

                    if (przycisk == ConsoleKey.DownArrow && aktualnaPozycjaMenu < Pozycje.Count - 1)
                    {
                        aktualnaPozycjaMenu++;
                    }
                    else if (przycisk == ConsoleKey.UpArrow && aktualnaPozycjaMenu > 0)
                    {
                        aktualnaPozycjaMenu--;
                    }
                    else if (przycisk == ConsoleKey.Enter)
                    {
                        Console.CursorVisible = true;
                        return Pozycje[aktualnaPozycjaMenu].id;
                    }

                    foreach (var pozycja in Pozycje)
                    {
                        if (pozycja.przycisk == przycisk)
                        {
                            Console.CursorVisible = true;
                            return pozycja.id;
                        }
                    }
                }
            }

        }
        // @author LarsVomMars
        // https://github.com/LarsVomMars/Checkboxes
        public class Checkbox
        {
            private List<CheckboxOptions> _options;
            private int _hoveredIndex;
            private int _selectedIndex;
            private string _displayText;
            private bool _multiSelect;
            private bool _required;
            private bool _error;
            private ConsoleKey _key;
            private ConsoleKey _prevKey;


            public Checkbox(string displayText, bool multiMode, bool required, params string[] options)
            {
                _multiSelect = multiMode;
                _required = required;
                Init(displayText, options);
            }

            private void Init(string dt, string[] options)
            {
                _hoveredIndex = 0;
                _selectedIndex = -1;
                _error = false;
                _displayText = dt;

                _options = new List<CheckboxOptions>();

                for (int i = 0; i < options.Length; i++)
                    _options.Add(new CheckboxOptions(options[i], false, i == _hoveredIndex, i));
            }

            private CheckboxReturn[] ReturnData()
            {
                List<CheckboxReturn> l = new List<CheckboxReturn>();
                foreach (var option in _options)
                {
                    if (option.Selected) l.Add(option.GetData());
                }

                return l.ToArray();
            }

            public void Show()
            {
                Console.Clear();
                Console.WriteLine(_displayText);

                foreach (var option in _options)
                {
                    Console.ForegroundColor = option.Selected
                        ? (option.Hovered ? ConsoleColor.Green : ConsoleColor.DarkGreen)
                        : (option.Hovered ? ConsoleColor.White : ConsoleColor.DarkGray);

                    Console.WriteLine((option.Selected ? "[*]  " : "[ ]  ") + $"{option.Option}");
                }

                Console.ResetColor();
                Console.WriteLine("↑ - góra  |  ↓ - dół  |  Spacja - wybieranie  |  Enter - zatwierdzenie");
                if (_error) Console.WriteLine("\nAt least one item has to be selected!");
            }

            public CheckboxReturn[] Select()
            {
                Show();
                bool end = false;
                while (!end)
                {
                    _key = Console.KeyAvailable ? Console.ReadKey(true).Key : ConsoleKey.D9;
                    if (_key == _prevKey) continue;
                    _options[_hoveredIndex].Hovered = false;

                    switch (_key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            _hoveredIndex = _hoveredIndex - 1 >= 0 ? _hoveredIndex - 1 : _options.Count - 1;
                            break;

                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            _hoveredIndex = _hoveredIndex + 1 < _options.Count ? _hoveredIndex + 1 : 0;
                            break;

                        case ConsoleKey.Spacebar:
                            _options[_hoveredIndex].Selected = !_options[_hoveredIndex].Selected;
                            if (!_multiSelect)
                            {
                                if (_selectedIndex > -1 && _hoveredIndex != _selectedIndex)
                                    _options[_selectedIndex].Selected = false;
                                _selectedIndex = _hoveredIndex;
                            }

                            _error = false;
                            break;

                        case ConsoleKey.Enter:
                            if (_required)
                            {
                                foreach (var option in _options)
                                {
                                    if (!option.Selected) continue;
                                    end = true;
                                    break;
                                }

                                if (!end) _error = true;
                            }
                            else end = true;

                            break;
                    }

                    _options[_hoveredIndex].Hovered = true;
                    Show();
                    _prevKey = _key;
                }

                return ReturnData();
            }
        }

        public class CheckboxOptions
        {
            private readonly int _index;
            private readonly string _option;

            public CheckboxOptions(string option, bool selected, bool hovered, int index)
            {
                _option = option;
                Selected = selected;
                Hovered = hovered;
                _index = index;
            }

            public bool Selected { get; set; }

            public bool Hovered { get; set; }

            public string Option => _option;

            public CheckboxReturn GetData()
            {
                return new CheckboxReturn(_index, _option);
            }
        }

        public class CheckboxReturn
        {
            private int _index;
            private string _option;

            public CheckboxReturn(int index, string option)
            {
                _index = index;
                _option = option;
            }

            public int Index => _index;

            public string Option => _option;
        }


        // encoding
        public static string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));

        }


        // decoding
        public static string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));

        }

        // encrypt
        public static byte[] Encrypt(byte[] strData)
        {
        String strPermutation = "ouiveyxaqtd";
        Int32 bytePermutation1 = 0x19;
        Int32 bytePermutation2 = 0x59;
        Int32 bytePermutation3 = 0x17;
        Int32 bytePermutation4 = 0x41;
        PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { (byte)bytePermutation1,
                         (byte)bytePermutation2,
                         (byte)bytePermutation3,
                         (byte)bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        public static byte[] Decrypt(byte[] strData)
        {
            String strPermutation = "ouiveyxaqtd";
            Int32 bytePermutation1 = 0x19;
            Int32 bytePermutation2 = 0x59;
            Int32 bytePermutation3 = 0x17;
            Int32 bytePermutation4 = 0x41;
            PasswordDeriveBytes passbytes =
                new PasswordDeriveBytes(strPermutation,
                new byte[] { (byte)bytePermutation1,
                         (byte)bytePermutation2,
                         (byte)bytePermutation3,
                         (byte)bytePermutation4
                });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
        #region essentials
        public static int podajNumerTelefonu(string text)
        {
            while (true)
            {
                try
                {
                    Console.Write(text);
                    int wynik = int.Parse(Console.ReadLine());
                    int length = wynik.ToString().Length;
                    if (length == 9)
                    {
                        return wynik;
                    }
                    else
                    {
                        throw new Exception("Niepoprawnie wprowadzony numer telefonu (wymagany fortmat to XXXXXXXXX)");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static int podajKodPocztowy(string text)
        {
            while (true)
            {
                try
                {
                    Console.Write(text);
                    int wynik = int.Parse(Console.ReadLine());
                    int length = wynik.ToString().Length;
                    if (length == 5)
                    {
                        return wynik;
                    }
                    else
                    {
                        throw new Exception("Niepoprawnie wprowadzony kod pocztowy (wymagany fortmat to XXXXX)");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static int wprowadzInt(string tekst, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(tekst);
                    int wynik = int.Parse(Console.ReadLine());

                    if (wynik >= min && wynik <= max)
                    {
                        return wynik;
                    }
                    else
                    {
                        throw new Exception("Niepoprawny zakres wartosci!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        public static double wprowadzDouble(string tekst, double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(tekst);
                    double wynik = double.Parse(Console.ReadLine());

                    if (wynik >= min && wynik <= max)
                    {
                        return wynik;
                    }
                    else
                    {
                        throw new Exception("Niepoprawny zakres wartosci!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        public static long wprowadzLong(string tekst, long min = long.MinValue, long max = long.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(tekst);
                    long wynik = long.Parse(Console.ReadLine());

                    if (wynik >= min && wynik <= max)
                    {
                        return wynik;
                    }
                    else
                    {
                        throw new Exception("Niepoprawny zakres wartosci!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }   
        public static bool pytanie(string tresc)
        {
            Console.WriteLine($"{tresc} (T/N)");

            while (true)
            {
                    var przycisk = Console.ReadKey(true);

                    if (przycisk.Key == ConsoleKey.N)
                    {
                        return false;
                    }
                    else if (przycisk.Key == ConsoleKey.T)
                    {
                        return true;
                    }
                    
            }
        }

        private static bool czyNieLicbza(string s)
        {
            if (!double.TryParse(s, out _) || !int.TryParse(s, out _))
            {
                return true;
            }

            return false;

        }
        public static long generujNumerPaczki()
        {
            Random random = new Random();
            const long minValue = 1000000000000000;
            const long maxValue = 9999999999999999; 

            long randomValue = (long)(random.NextDouble() * (maxValue - minValue + 1)) + minValue;

            return randomValue;
        }
        public static string wprowadzString(string tresc, bool czyMozeBycPusty)
        {
            while (true)
            {
                try
                {
                    Console.Write(tresc);
                    var wartosc = Console.ReadLine();

                    if (czyMozeBycPusty || !string.IsNullOrEmpty(wartosc))
                    {
                        bool allGood = czyNieLicbza(wartosc);
                        if (allGood)
                        {
                            return wartosc;
                        }
                        else
                        {
                            throw new Exception("To mają być litery!!");
                        }
                    }
                    else
                    {
                        throw new Exception("Wartość nie może być pusta!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
#endregion
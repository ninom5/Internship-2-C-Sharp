int globalId = 0;
Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>> UserDict = new Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>>();
var bankAccount = new Dictionary<string, double>
{
    { "tekuci", 100.00},
    { "ziro", 0.00},
    { "prepaid", 0.00}
};

var bankAccountMinus = new Dictionary<string, double>
{
    { "tekuci", -100.00},
    { "ziro", -10.00},
    { "prepaid", 0.00}
};

DateTime user1BDay = new DateTime(1987, 06, 24);
UserDict.Add(globalId++, Tuple.Create("Lionel", "Messi", user1BDay, bankAccount));

DateTime user2BDay = new DateTime(1985, 02, 05);
UserDict.Add(globalId++, Tuple.Create("Cristiano", "Ronaldo", user2BDay, bankAccount));

DateTime user3BDay = new DateTime(1981, 10, 03);
UserDict.Add(globalId++, Tuple.Create("Zlatan", "Ibrahimovic", user3BDay, bankAccount));

DateTime user4BDay = new DateTime(1984, 05, 11);
UserDict.Add(globalId++, Tuple.Create("Andres", "Iniesta", user4BDay, bankAccount));

DateTime user5BDay = new DateTime(1977, 02, 24);
UserDict.Add(globalId++, Tuple.Create("Floyd", "Mayweather", user5BDay, bankAccountMinus));

do
{
    Console.WriteLine("1 - Korisnici\n2 - Računi\n3 - Izlaz iz aplikacije");

    char option = Console.ReadKey().KeyChar; //char, tako da se u slucaju unosa slova program ne prekine


    switch (option)
    {
        case '1':
            Console.WriteLine("\n");
            Users();
            break;
        case '2':
            Console.WriteLine("\n");
            Accounts();
            break;
        case '3':
            return;
        default:
            Console.WriteLine("\nKrivi unos, odaberite jednu od opcija");
            break;
    }
} while (true);
    

void Users()
{
    char option;
    do
    {
        Console.WriteLine("1. Korisnici");
        Console.WriteLine("\t1 - Unos novog korisnika");
        Console.WriteLine("\t2 - Brisanje korisnika\n \t\t a) po id-u \n \t\t b) po imenu i prezimenu");
        Console.WriteLine("\t3 - Uređivanje korisnika \n \t\t a) po id-u");
        Console.WriteLine("\t4 - Pregled korisnika \n \t\t ");
        option = Console.ReadKey().KeyChar;

        switch (option)
        {
            case '1':
                CreateNewUser();
                break;
            case '2':
                Console.WriteLine("\t a) po id-u \n \t b) po imenu i prezimenu");
                bool isValid = true;
                do
                {
                    char choose = Console.ReadKey().KeyChar;
                    if (choose == 'a')
                        DeleteUser('a');
                    else if (choose == 'b')
                        DeleteUser('b');
                    else
                    {
                        Console.WriteLine("Krivi unos. Birajte opet");
                        isValid = false;
                    }
                } while (!isValid);
                break;
            case '3':
                EditUser();
                break;
            case '4':
                ShowOptions();
                break;
            default:
                Console.WriteLine("Krivi unos. Odaberite ponovno");
                break;
        }
    } while(option < '1' || option > '4'); //dok nije odabrana opcija 1 do 4

}
//popravljeni ShowOptions(), CreateNewUser() i valjda Accounts() ostale triba provjerit
void Accounts()
{
    
    while (true)
    {
        bool isFound = false;
        var user = default(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>>);
        Console.WriteLine("Unesite ime i prezime osobe koju zelite odabrati");
        string[] nameAndSurname = Console.ReadLine().Trim().Split(" ");

        if (nameAndSurname.Length != 2)
        {
            Console.WriteLine("Unesite ispravan format imena i prezimena. Ime razmak prezime");
            continue;
        }

        foreach (var entry in UserDict)
        {
            if (entry.Value.Item1 == nameAndSurname[0] && entry.Value.Item2 == nameAndSurname[1])
            {
                user = entry;
                isFound = true;
                break;
            }
        }
        if(!isFound)
        {
            bool isValid = false;
            do
            {
                Console.WriteLine($"Korisnik {nameAndSurname[0]} {nameAndSurname[1]} nije pronađen. \nAko zelite opet upisati ime pritisnite 1 ako se zelite vratiti pritisnite 0");
                char option = Console.ReadKey().KeyChar;
                if (option == '1')
                    continue;
                else if (option == '0')
                    return;
                else
                {
                    Console.WriteLine("Krivi unos. Birajte opet");
                    isValid = true;
                }
            } while (isValid);
        }
        //var accounts = user.Value.Item4;
        bool isPicked = false;
        while(!isPicked)
        {
            Console.WriteLine($"\n1 - Pregled računa: \n\t a) Tekuci \n\t b) Ziro \n\t c) Prepaid \nOdaberite jedan od racuna");
            string selectedAccount;
            char chooseAccount = Console.ReadKey().KeyChar;
            switch (chooseAccount)
            {
                case 'a':
                    selectedAccount = "tekuci";
                    isPicked = true;
                    break;
                case 'b':
                    selectedAccount = "ziro";
                    isPicked = true;
                    break;
                case 'c':
                    selectedAccount = "prepaid";
                    isPicked = true;
                    break;
                default:
                    Console.WriteLine("Krivi unos. Odaberite opet");
                    break;
            }
        }
        Console.WriteLine("\n1 - Unos nove transakcije \n\t a) trenutno izvršena transakcija \n\t b) ranije izvršena transakcija");
        Console.WriteLine("2 - Brisanje transakcije");
        Console.WriteLine("\t a) po id-u \n\t b)ispod unesenog iznosa \n\t c) iznad unesenog iznosa \n\t d) svih prihoda \n\t e) svih rashoda \n\t f) svih transakcija za odabranu kategoriju");
        Console.WriteLine("3 - Uređivanje transakcije \n\t a) po id-u");
        Console.WriteLine("4 - Pregled transakcija");
        Console.WriteLine("\t a) sve transakcije kako su spremljene \n\t b) sve transakcije sortirane po iznosu uzlazno \n\t c) sve transakcije sortirane po iznosu silazno " +
            "\n\t d) sve transakcije sortirane po opisu abecedno \n\t e) sve transakcije sortirane po datumu uzlazno \n\t f) sve transakcije sortirane po datumu silazno" +
            "\n\t g) svi prihodi \n\t h) svi rashodi \n\t i) sve transakcije za odabranu kategoriju \n\t j) sve transakcije za odabrani tip i kategoriju");
        Console.WriteLine("5 - Financijsko izvješće \n\t a) trenutno stanje računa \n\t b) broj ukupnih transakcija \n\t c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu \n\t " +
            "d) postotak udjela rashoda za odabranu kategoriju \n\t e) prosječni iznos transakcija za odabrani mjesec i godinu \n\t f) prosječni iznos transakcije za odabranu kategoriju");

        Console.WriteLine("0 - izlaz iz aplikacije");
    }
}

void ShowOptions()
{
    bool isCorrectOption = false;
    while (!isCorrectOption)
    {
        Console.WriteLine("Odaberite koje korisnike želite prikazati: \n\t\t a) ispis svih korisnika abecedno po prezimenu \n \t\t b) ispis svih onih s više od 30 godina \n \t\t c) ispis svih s bar jednim računom u minusu.");
        char filterOption = Console.ReadKey().KeyChar;
        switch (filterOption)
        {
            case 'a':
                PrintBySurname();
                isCorrectOption = true;
                break;
            case 'b':
                PrintUsersOverThirty();
                isCorrectOption = true;
                break;
            case 'c':
                PrintUsersInMinus();
                isCorrectOption = true;
                break;
            default:
                Console.WriteLine("Krivi unos. Odaberite ponovno");
                isCorrectOption = false;
                break;
        }
    }
}

void CreateNewUser()
{
    try
    {
        DateTime dateOfBirth;
        Console.WriteLine("Enter name");
        string name = Console.ReadLine();
        Console.WriteLine("Enter surname");
        string surname = Console.ReadLine();
        bool isValid = true;
        do
        {
            Console.WriteLine("Enter date of birth in format dd/MM/yyyy");
            string date = Console.ReadLine();
            if (!(DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateOfBirth)))
            {
                Console.WriteLine("Invalid date format");
                isValid = false;
            }
            else
                isValid = true;
        } while (!isValid);

        int id = globalId++;

        
        UserDict.Add(id, Tuple.Create(name, surname, dateOfBirth, bankAccount));
        Console.WriteLine($"User successfully created. Name: {name}, surname: {surname}, date of birth: {dateOfBirth}, ID: {id}");

    }catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");    
    } 
}

void DeleteUser(char option)
{
    if (option == 'a')
    {
        while (true)
        {
            Console.WriteLine("Unesite id korisnika kojeg želite izbrisati");
            int idToDelete;
            bool isValid = int.TryParse(Console.ReadLine(), out idToDelete);
            if (!isValid)
            {
                Console.WriteLine("Krivi unos biraj opet");
                continue;
            }
            if (!UserDict.ContainsKey(idToDelete))
            {
                Console.WriteLine("Ne postoji korisnik s unesenim ID-om");
                continue;
            }
            string userName = UserDict[idToDelete].Item2;
            var userSurname = UserDict[idToDelete].Item3;
            UserDict.Remove(idToDelete);
            Console.WriteLine($"Korisnik {userName + " " + userSurname + " s ID: " + idToDelete} uspješno izbrisan");
            break;
        }
    }
    else
    {
        while (true)
        {

            Console.WriteLine("Unesite ime i prezime osobe koju zelite izbrisati");
            string[] nameAndSurname = Console.ReadLine().Trim().Split(" ");
                
            if (nameAndSurname.Length != 2)
            {
                Console.WriteLine("Unesite ispravan format imena i prezimena. Ime razmak prezime");
                continue;
            }

            foreach (var entry in UserDict)
            {
                if (entry.Value.Item1 == nameAndSurname[0] && entry.Value.Item2 == nameAndSurname[1])
                {
                    UserDict.Remove(entry.Key);
                    Console.WriteLine($"Korisnik {nameAndSurname[0]} {nameAndSurname[1]} je uspješno izbrisan");
                    return;
                }
            }

            Console.WriteLine($"Korisnik {nameAndSurname[0]} {nameAndSurname[1]} nije pronađen");
        }
    }
}

void EditUser()
{
    while (true)
    {
        Console.WriteLine("Unesite id korisnika kojeg želite promijeniti");
        int idToEdit;
        bool isValid = int.TryParse(Console.ReadLine(), out idToEdit);
        if (!isValid)
        {
            Console.WriteLine("Krivi unos biraj opet");
            continue;
        }
        if (!UserDict.ContainsKey(idToEdit))
        {
            Console.WriteLine("Ne postoji korisnik s unesenim ID-om");
            continue;
        }
        Console.WriteLine($"Pronađen korisnik {UserDict[idToEdit].Item1 + " " + UserDict[idToEdit].Item2 + " rođen " + UserDict[idToEdit].Item3}");

        var bankAccounts = UserDict[idToEdit].Item4;

        Console.WriteLine("Unesite u koje ime želite promijeniti. Ako ne želite promijeniti unesite isto ime");
        var newName = Console.ReadLine().Trim();     
        if (newName.Length == 0)
        {
            Console.WriteLine("Ime ne može biti prazno, unesite opet");
            continue;
        }

        Console.WriteLine("Unesite u koje prezime želite promijeniti. Ako ne želite promijeniti unesite isto prezime");
        var newSurname = Console.ReadLine().Trim();

        if (newSurname.Length == 0)
        {
            Console.WriteLine("Prezime ne može biti prazno, unesite opet");
            continue;
        }

        bool isValidDate = true;
        DateTime newDateOfBirth;
        do
        {
            Console.WriteLine("Unesite novi datum rođenja (ako ne želite promijeniti, ostavite prazno)");
            string date = Console.ReadLine().Trim();

            if (date.Length == 0)
            {
                newDateOfBirth = UserDict[idToEdit].Item3;
                isValidDate = false;
            }
            else if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out newDateOfBirth))
            {
                isValidDate = false;
            }
            else
                Console.WriteLine("Unesen neispravan format, unesite opet");
        }while(isValidDate);
        
        UserDict[idToEdit] = Tuple.Create(newName, newSurname, newDateOfBirth, bankAccounts);
        Console.WriteLine("Korisnik uspješno ažuriran");
        break;
    }
}

void PrintBySurname()
{
    var sortedBySurname = UserDict.OrderBy(element => element.Value.Item2);
    Console.WriteLine("Korisnici sortirani po prezimenu");
    foreach(var dictElement in sortedBySurname )
    {
        Console.WriteLine($"{dictElement.Value.Item2 + " " + dictElement.Value.Item1 + " ID " + dictElement.Key + " date of birth: " + dictElement.Value.Item3} ");
    }
}

void PrintUsersOverThirty()
{
    Console.WriteLine("Korisnici s više od 30 godina");
    foreach(var dictElement in UserDict)
    {
        int userAge = CalculateAge(dictElement);
        if(userAge >= 30)
            Console.WriteLine($"ID: {dictElement.Key + " " + dictElement.Value.Item1 + " " + dictElement.Value.Item2 + " date of birth: " + dictElement.Value.Item3 + " godine: " + userAge} ");
    }
}

int CalculateAge(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> dictElement)
{
    DateTime currentDate = DateTime.Now;
    int age = currentDate.Year - dictElement.Value.Item3.Year;
    if (currentDate.Month < dictElement.Value.Item3.Month)
        age--;
    else if (currentDate.Month == dictElement.Value.Item3.Month)
    {
        if(currentDate.Day < dictElement.Value.Item3.Day)
            age--;
    }

    return age;
}

void PrintUsersInMinus()
{
    foreach (var dictElement in UserDict)
    {
        var element = dictElement.Value.Item4;
        foreach(var elem in element)
        {
            if(elem.Value < 0)
            {
                Console.WriteLine($"ID: {dictElement.Key + " " + dictElement.Value.Item1 + " " + dictElement.Value.Item2 + " " + dictElement.Value.Item3 + " " + elem.Key + " račun u minusu: " + elem.Value}");
            }
        }
    }
}
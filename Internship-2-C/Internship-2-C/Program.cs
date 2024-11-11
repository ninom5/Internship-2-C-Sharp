int globalId = 0;
Dictionary<int, Tuple<string, string, DateTime>> UserDict = new Dictionary<int, Tuple<string, string, DateTime>>();

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

void Accounts()
{
    Console.WriteLine("Unesite ime i prezime korisnika");
    var user = Console.ReadLine();
    //choose user account

    Console.WriteLine("\n1 - Pregled računa: ");//choose type of account(tekuci ziro itd)

    Console.WriteLine("1 - Unos nove transakcije \n\t a) trenutno izvršena transakcija \n\t b) ranije izvršena transakcija");
    Console.WriteLine("2 - Brisanje transakcije");
    Console.WriteLine("\t a) po id-u \n\t b)ispod unesenog iznosa \n\t c) iznad unesenog iznosa \n\t d) svih prihoda \n\t e) svih rashoda \n\t svih transakcija za odabranu kategoriju");
    Console.WriteLine("3 - Uređivanje transakcije \n\t a) po id-u");
    Console.WriteLine("4 - Pregled transakcija");
    Console.WriteLine("\t a) sve transakcije kako su spremljene \n\t b) sve transakcije sortirane po iznosu uzlazno \n\t c) sve transakcije sortirane po iznosu silazno " +
        "\n\t d) sve transakcije sortirane po opisu abecedno \n\t e) sve transakcije sortirane po datumu uzlazno \n\t f) sve transakcije sortirane po datumu silazno" +
        "\n\t g) svi prihodi \n\t svi rashodi \n\t i) sve transakcije za odabranu kategoriju \n\t j) sve transakcije za odabrani tip i kategoriju");
    Console.WriteLine("5 - Financijsko izvješće \n\t a) trenutno stanje računa \n\t b) broj ukupnih transakcija \n\t c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu \n\t " +
        "d) postotak udjela rashoda za odabranu kategoriju \n\t e) prosječni iznos transakcija za odabrani mjesec i godinu \n\t f) prosječni iznos transakcije za odabranu kategoriju");
    
    Console.WriteLine("0 - izlaz iz aplikacije");
}

static void ShowOptions()
{
    bool isCorrectOption = true;
    do
    {
        Console.WriteLine("Odaberite koje korisnike želite prikazati: a) ispis svih korisnika abecedno po prezimenu \n \t\t b) ispis svih onih s više od 30 godina \n \t\t c) ispis svih s bar jednim računom u minusu.");
        char filterOption = Console.ReadKey().KeyChar;
        switch (filterOption)
        {
            case 'a':
                //PrintBySurname();
                break;
            case 'b':
                //PrintUsersOverThirty();
                break;
            case 'c':
                //PrintUsersInMinus();
                break;
            default:
                Console.WriteLine("Krivi unos. Odaberite ponovno");
                isCorrectOption = false;
                break;
        }
    }while(!isCorrectOption);
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
        } while (!isValid);

        int id = globalId++;
        UserDict.Add(id, Tuple.Create(name, surname, dateOfBirth));
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
            UserDict.Remove(idToDelete);
            Console.WriteLine($"Korisnik s {idToDelete} ID-om uspješno izbrisan");
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
        Console.WriteLine($"Pronađen korisnik {UserDict[idToEdit].Item1 + " " + UserDict[idToEdit].Item2 + "rođen " + UserDict[idToEdit].Item3}");

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
        
        UserDict[idToEdit] = new Tuple<string, string, DateTime>(newName, newSurname, newDateOfBirth);
        Console.WriteLine("Korisnik uspješno ažuriran");
        break;
    }
}
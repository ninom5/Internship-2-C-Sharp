using System.Reflection;
using System.Xml;

Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>> UserDict = new Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>>();
Dictionary<int, Tuple<int, double, string, string, string, string, DateTime>> TransactionDict = new Dictionary<int, Tuple<int, double, string, string, string, string, DateTime>>();

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

var income = new Dictionary<int, string>
{
    { 1, "plaća" },
    { 2, "honorar" },
    { 3, "poklon" },
    { 4, "investicije" },
    { 5, "penzija" },
    { 6, "bonus" },
    { 7, "stipendija" }
};

var expense = new Dictionary<int, string>
{
    { 1, "hrana" },
    { 2, "najamnina" },
    { 3, "osiguranje" },
    { 4, "odjeća" },
    { 5, "donacije" },
    { 6, "restoran" },
    { 7, "kladionica" }
};

int globalId = 0;
int transactionId = 0;
DateTime dateTimeNow = DateTime.Now;

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

TransactionDict.Add(transactionId++, Tuple.Create(0, 1092.22, "tekuci", "dobit od investicije", "prihod", income[4], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(1, 300.00, "tekuci", "plaćanje najamnine", "rashod", expense[2], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(2, 30.00, "tekuci", "poklon za rodendan", "prihod", income[3], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(3, 340.00, "tekuci", "odijelo", "rashod", expense[4], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 1500.00, "tekuci", "mjesecna penzija", "prihod", income[5], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(0, 180.91, "tekuci", "zivotno osiguranje", "rashod", expense[3], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 180.91, "ziro", "drzavna stipendija", "prihod", income[7], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 180.91, "prepaid", "igre na sreću", "rashod", expense[7], dateTimeNow));
//UpdateBankAccounts(); /*za updateat ove dodane transakcije*/

do
{
    Console.WriteLine("\n1 - Korisnici\n2 - Računi\n3 - Izlaz iz aplikacije");

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
                    {  
                        DeleteUser('a');
                        return;
                    }
                    else if (choose == 'b')
                    { 
                        DeleteUser('b');
                        return;
                    }
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
//popravljeni ShowOptions(), CreateNewUser(), DeleteUser(), EditUser()(već radilo) i valjda Accounts() ostale triba provjerit
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
                    Accounts()
                        ;
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
        do
        {
            string selectedAccount;
            Console.WriteLine($"\n1 - Pregled računa: \n\t a) Tekuci \n\t b) Ziro \n\t c) Prepaid \nOdaberite jedan od racuna");

            char chooseAccount = Console.ReadKey().KeyChar;
            switch (chooseAccount) //provjeirt za ovo za krivi unos
            {
                case 'a':
                    selectedAccount = "tekuci";
                    TransactionMenu(user, selectedAccount);
                    return;
                case 'b':
                    selectedAccount = "ziro";
                    TransactionMenu(user, selectedAccount);
                    return;
                case 'c':
                    selectedAccount = "prepaid";
                    TransactionMenu(user, selectedAccount);
                    return;
                default:
                    Console.WriteLine("Krivi unos. Odaberite opet");
                    isPicked = true;
                    break;
            }
        } while (isPicked);
    }
}

void TransactionMenu(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
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

    char option = Console.ReadKey().KeyChar;
    switch(option)
    {
        case '1':
            EnterNewTransaction(user, typeOfAccount);
            return;
        case '2':
            ChooseHowToDeleteTransactions(user, typeOfAccount);
            return;
        
    }

}

void ChooseHowToDeleteTransactions(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("\t a) po id-u \n\t b)ispod unesenog iznosa \n\t c) iznad unesenog iznosa \n\t d) svih prihoda \n\t e) svih rashoda \n\t f) svih transakcija za odabranu kategoriju");
    char option = Console.ReadKey().KeyChar;

    switch (option)
    {
        case 'a':
            DeleteById(user);
            return;

    }

}

void DeleteById(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user) //event dodat provjeru koji je račun odabran
{
    bool isDeleted = false;
    do
    {
        Console.WriteLine("Unesite ID transkacije koju želite izbrisati");
        var ID = Console.ReadLine();
        int idToDelete;
        if (int.TryParse(ID, out idToDelete))
        {
            if (TransactionDict.ContainsKey(idToDelete) && user.Key == idToDelete)
            {
                TransactionDict.Remove(idToDelete);
                Console.WriteLine($"Transakcija s ID {idToDelete} uspjesno izbrisana.");
                isDeleted = true;
            }
            else
            {
                Console.WriteLine("Ne postoji transakcija s unesenim IDom za odabranog korisnika");
            }
        }
        else
        {
            Console.WriteLine("Krivi unos. Unesite opet");
            isDeleted = false;
        }
    } while (!isDeleted);
}
void EnterNewTransaction(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    DateTime dateAndTimeOfTransaction;
    Console.WriteLine("\n\t a) trenutno izvršena transakcija \n\t b) ranije izvršena transakcija");
    char option;
    do
    {
        option = Console.ReadKey().KeyChar;

        if (option == 'a')
        {
            dateAndTimeOfTransaction = DateTime.Now;
            FillDataOfNewTransaction(dateTimeNow, user, typeOfAccount);
        }
        else if (option == 'b')
        {
            bool isValid = true;
            do
            {
                Console.WriteLine("Unesite datum i vrijeme transakcije u formatu dd/MM/yyyy hh:mm");
                string date = Console.ReadLine();
                if (DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out dateAndTimeOfTransaction))
                {
                    FillDataOfNewTransaction(dateAndTimeOfTransaction, user, typeOfAccount);
                    return;
                }
                else
                {
                    Console.WriteLine("Unesen ne ispravan format. Unesite u obliku: dd/MM/yyyy hh:mm");
                    isValid = false;
                }
            } while (!isValid);
        }

    } while (option != 'a' && option != 'b');
}
//ponovni unos u slucaju krivog, odnosno provjera svega je li negativan iznos je li prazan opis itd.
void FillDataOfNewTransaction(DateTime dateTimeNow, KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    int userId = user.Key;
    int num = 1;

    Console.WriteLine("Unesite tip transakcije(prihod, rashod)");
    string typeOfTransaction = Console.ReadLine();
    string choosenCategory = "";
    if (typeOfTransaction == "prihod")
    {
        Console.WriteLine("Odaberite broj koji predstavlja vašu kategoriju prihoda");
        foreach (var category in income)
        {
            Console.WriteLine($"{num++ + " - " + category.Value}");
        }
        char categoryOption = Console.ReadKey().KeyChar;
        switch (categoryOption)
        {
            case '1':
                choosenCategory = income[1];
                break;
            case '2':
                choosenCategory = income[2];
                break;
            case '3':
                choosenCategory = income[3];
                break;
            case '4':
                choosenCategory = income[4];
                break;
            case '5':
                choosenCategory = income[5];
                break;
            case '6':
                choosenCategory = income[6];
                break;
            case '7':
                choosenCategory = income[7];
                break;
            default:
                Console.WriteLine("Krivi unos. Birajte opet");
                break;
        }
    }
    else if(typeOfTransaction == "rashod")
    {
        Console.WriteLine("Odaberite broj koji predstavlja vašu kategoriju rashoda");
        foreach (var catergory in expense)
        {
            Console.WriteLine($"{num++ + " - " + catergory.Value}");
        }
        char categoryOption = Console.ReadKey().KeyChar;
        switch (categoryOption)
        {
            case '1':
                choosenCategory = expense[1];
                break;
            case '2':
                choosenCategory = expense[2];
                break;
            case '3':
                choosenCategory = expense[3];
                break;
            case '4':
                choosenCategory = expense[4];
                break;
            case '5':
                choosenCategory = expense[5];
                break;
            case '6':
                choosenCategory = expense[6];
                break;
            case '7':
                choosenCategory = expense[7];
                break;
            default:
                Console.WriteLine("Krivi unos. Birajte opet");
                break;
        }
    }
    else
        Console.WriteLine("Kirivi unos. Birajte opet");

    Console.WriteLine("Unesite iznos transakcije");
    string amnt = Console.ReadLine();
    double amount;
    double.TryParse(amnt, out amount);
    Console.WriteLine("Unesite opis transakcije");
    string descriptionOfTransaction = Console.ReadLine();

    TransactionDict.Add(transactionId++, Tuple.Create(userId, amount, typeOfAccount, descriptionOfTransaction, typeOfTransaction, choosenCategory, dateTimeNow));
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
                return;
            case 'b':
                PrintUsersOverThirty();
                return;
            case 'c':
                PrintUsersInMinus();
                return;
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
            return;
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
            return;
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

void UpdateBankAccounts()
{
    foreach(var transaction in TransactionDict)
    {
        int userId = transaction.Value.Item1;
        double amount = transaction.Value.Item2;
        string accType = transaction.Value.Item3;
        string transType = transaction.Value.Item5;

        if (transType == "prihod")
            UserDict[userId].Item4[accType] += amount;
        else if(transType == "rashod")
            UserDict[userId].Item4[accType] -= amount;

        //ne triba provjera postoji li korisnik s tim IDem, osigurano prije poziva funkcije da postoji korisnik s tim id
    }
}
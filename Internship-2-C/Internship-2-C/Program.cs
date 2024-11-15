﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>> UserDict = new Dictionary<int, Tuple<string, string, DateTime, Dictionary<string, double>>>();
Dictionary<int, Tuple<int, double, string, string, string, string, DateTime>> TransactionDict = new Dictionary<int, Tuple<int, double, string, string, string, string, DateTime>>();
HashSet<int> processedTransactions = new HashSet<int>();
HashSet<int> updateId = new HashSet<int>();
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
    { 7, "stipendija" },
    {8, "interni prijenos" },
    {9, "externi prijenos" }
};

var expense = new Dictionary<int, string>
{
    { 1, "hrana" },
    { 2, "najamnina" },
    { 3, "osiguranje" },
    { 4, "odjeća" },
    { 5, "donacije" },
    { 6, "restoran" },
    { 7, "kladionica" },
    {8, "interni prijenos" },
    {9, "externi prijenos" }
};

int dummy = -1;
int globalId = 0;
int transactionId = 0;
DateTime dateTimeNow = DateTime.Now;

DateTime user1BDay = new DateTime(1987, 06, 24);
UserDict.Add(globalId++, Tuple.Create("Lionel", "Messi", user1BDay, new Dictionary<string, double>(bankAccount)));

DateTime user2BDay = new DateTime(1985, 02, 05);
UserDict.Add(globalId++, Tuple.Create("Cristiano", "Ronaldo", user2BDay, new Dictionary<string, double>(bankAccount)));

DateTime user3BDay = new DateTime(1981, 10, 03);
UserDict.Add(globalId++, Tuple.Create("Zlatan", "Ibrahimovic", user3BDay, new Dictionary<string, double>(bankAccount)));

DateTime user4BDay = new DateTime(1984, 05, 11);
UserDict.Add(globalId++, Tuple.Create("Andres", "Iniesta", user4BDay, new Dictionary<string, double>(bankAccount)));

DateTime user5BDay = new DateTime(1977, 02, 24);
UserDict.Add(globalId++, Tuple.Create("Floyd", "Mayweather", user5BDay, new Dictionary<string, double>(bankAccountMinus)));

TransactionDict.Add(transactionId++, Tuple.Create(1, 300.00, "tekuci", "plaćanje najamnine", "rashod", expense[2], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(0, 1092.22, "tekuci", "dobit od investicije", "prihod", income[4], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(0, 180.91, "ziro", "drzavna stipendija", "prihod", income[7], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(0, 190.91, "ziro", "večera", "rashod", expense[6], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(0, 180.91, "tekuci", "zivotno osiguranje", "rashod", expense[3], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(1, 245.12, "tekuci", "placa", "prihod", income[1], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(2, 30.00, "tekuci", "poklon za rodendan", "prihod", income[3], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(3, 340.00, "tekuci", "odijelo", "rashod", expense[4], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 1500.00, "tekuci", "mjesecna penzija", "prihod", income[5], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 180.91, "ziro", "drzavna stipendija", "prihod", income[7], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 180.91, "prepaid", "igre na sreću", "rashod", expense[7], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 10000.98, "prepaid", "huhu", "prihod", expense[7], dateTimeNow));
TransactionDict.Add(transactionId++, Tuple.Create(4, 10070.00, "tekuci", "huhu", "prihod", income[7], dateTimeNow));

UpdateBankAccounts(); /*za updateat ove dodane transakcije*/

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
        Console.WriteLine("\t4 - Pregled korisnika \n" +
            "\t5 - Početni zaslon");
        option = Console.ReadKey().KeyChar;

        switch (option)
        {
            case '1':
                CreateNewUser();
                break;
            case '2':
                Console.WriteLine("\t a) po id-u \n \t b) po imenu i prezimenu \n0 - pocetni zaslon");
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
                    else if (choose == '0')
                        return;
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
            case '5':
                return;
            default:
                Console.WriteLine("Krivi unos. Odaberite ponovno");
                break;
        }
    } while(option < '1' || option > '4'); //dok nije odabrana opcija 1 do 4

}
//popravljeni ShowOptions(), CreateNewUser(), DeleteUser(), EditUser()(već radilo) i valjda Accounts()
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
                { 
                    Accounts();
                    return;
                }
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
            Console.WriteLine($"\n1 - Pregled računa: \n\t a) Tekuci \n\t b) Ziro \n\t c) Prepaid \n2 - Početni zaslon \nOdaberite jedan od racuna(a, b, c ili 2 za vracanje na početni zaslon)");

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
                case '2':
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
    Console.WriteLine("\t a) po id-u \n\t b) ispod unesenog iznosa \n\t c) iznad unesenog iznosa \n\t d) svih prihoda \n\t e) svih rashoda \n\t f) svih transakcija za odabranu kategoriju");
    Console.WriteLine("3 - Uređivanje transakcije \n\t a) po id-u");
    Console.WriteLine("4 - Pregled transakcija");
    Console.WriteLine("\t a) sve transakcije kako su spremljene \n\t b) sve transakcije sortirane po iznosu uzlazno \n\t c) sve transakcije sortirane po iznosu silazno " +
        "\n\t d) sve transakcije sortirane po opisu abecedno \n\t e) sve transakcije sortirane po datumu uzlazno \n\t f) sve transakcije sortirane po datumu silazno" +
        "\n\t g) svi prihodi \n\t h) svi rashodi \n\t i) sve transakcije za odabranu kategoriju \n\t j) sve transakcije za odabrani tip i kategoriju");
    Console.WriteLine("5 - Financijsko izvješće \n\t a) trenutno stanje računa \n\t b) broj ukupnih transakcija \n\t c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu \n\t " +
        "d) postotak udjela rashoda za odabranu kategoriju \n\t e) prosječni iznos transakcija za odabrani mjesec i godinu \n\t f) prosječni iznos transakcije za odabranu kategoriju " +
        "\n6 - slanje novca \n\t a) interno \n\t b) eksterno");
    Console.WriteLine("0 - pocetni zaslon");

    char option = Console.ReadKey().KeyChar;
    switch(option)
    {
        case '1':
            EnterNewTransaction(user, typeOfAccount);
            return;
        case '2':
            ChooseHowToDeleteTransactions(user, typeOfAccount);
            return;
        case '3':
            EditTransactions(user, typeOfAccount);
            return;
        case '4':
            ShowTransactionMenu(user, typeOfAccount);
            return;
        case '5':
            FinanceReportMenu(user, typeOfAccount);
            return;
        case '6':
            SendMoneyMenu(user, typeOfAccount);
            return;
        case '0':
            return;
        default:
            Console.WriteLine("Krivi unos birajte opet");
            TransactionMenu(user, typeOfAccount);
            return;
    }//napravljena provjera je li isti racun kao odabrani(tekuci, ziro, prepaid) za sve funkcije osim za brisanje

} 
                                                                    /*Bonus*/
/*---------------------------------------------------------------------------------------------------------------------------------------*/

void SendMoneyMenu(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("\n\t a) interno \n\t b) eksterno \n0 - pocetni zaslon");
    char option = Console.ReadKey().KeyChar;
    switch (option)
    {
        case 'a':
            SendMoneyIntern(user, typeOfAccount);
            return;
        case 'b':
            SendMoneyExtern(user, typeOfAccount);
            return;
        case '0':
            return;
        default:
            Console.WriteLine("Krivi unos birajte opet");
            SendMoneyMenu(user, typeOfAccount);
            return;
    }
}

void SendMoneyIntern(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    string source = typeOfAccount;
    string destination = "";
    Console.WriteLine($"Korisnik: {user.Value.Item1 + " " + user.Value.Item2}, odabrani racun: {source}." +
        $"Odaberite na koji racun zelite prebaciti sredstva");
    Dictionary<string, string[]> accountDestinationOptions = new Dictionary<string, string[]>
    {
        { "tekuci", new string[] { "ziro", "prepaid" } },
        { "ziro", new string[] { "tekuci", "prepaid" } },
        { "prepaid", new string[] { "tekuci", "ziro" } }
    };

    Console.WriteLine($"1 - {accountDestinationOptions[source][0]}" +
        $"\n2 - {accountDestinationOptions[source][1]}");

    char option = Console.ReadKey().KeyChar;
    if (option == '1')
        destination = accountDestinationOptions[source][0];
    else if(option == '2')
        destination = accountDestinationOptions[source][1];
    else
    {
        Console.WriteLine("Krivi unos. Odaberite opet");
        SendMoneyIntern(user, typeOfAccount);
        return;
    }

    Console.WriteLine("Unesite iznos koji zelite poslati");
    var amount = Console.ReadLine();
    double amountToSend;
    if(double.TryParse(amount, out amountToSend) && amountToSend > 0)
    {
        if (user.Value.Item4[source] >= amountToSend)
        {
            user.Value.Item4[source] -= amountToSend;
            user.Value.Item4[source] = Math.Round(user.Value.Item4[source], 2);
            user.Value.Item4[destination] += amountToSend;
            user.Value.Item4[destination] = Math.Round(user.Value.Item4[destination], 2);

            int transactionIdSender = TransactionDict.Count + 1;
            DateTime transactionDate = DateTime.Now;
            string description = $"Prijenos s {source} na {destination}";
            string type = "rashod";

            TransactionDict[transactionIdSender] = new Tuple<int, double, string, string, string, string, DateTime>(
                user.Key,
                amountToSend,
                source,
                description,
                type,
                expense[8],
                transactionDate
            );

            int transactionIdReciever = TransactionDict.Count + 1;
            string typeReciever = "prihod";

            TransactionDict[transactionIdReciever] = new Tuple<int, double, string, string, string, string, DateTime>(
                user.Key,
                amountToSend,
                destination,
                description,
                typeReciever,
                income[8],
                transactionDate
            );

            Console.WriteLine($"Transakcija uspješno izvršena. Poslano {amountToSend} sa {source} na {destination}.");

        }
        else
        {
            Console.WriteLine("Nemate dovoljno sredstava za poslati");
        }
    }
    else
    {
        Console.WriteLine("Krivi unos. Iznos mora biti pozitivan");
    }

}

void SendMoneyExtern(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Unesite id korisnika kojemu želite poslati novac");
    var id = Console.ReadLine();
    int idToSend;
    if(int.TryParse(id, out idToSend) && UserDict.ContainsKey(idToSend))
    {
        Console.WriteLine("Odaberite na koji racun zelite prenijeti sredstva.\n " +
            "\n1 - tekuci \n2 - ziro \n3 - prepaid");
        string[] typesOfAccount = { "tekuci", "ziro", "prepaid" };
        string destination = "";
        bool validDestination = false;
        while (!validDestination)
        {
            var option = Console.ReadLine();
            if (option == "1" || option == "2" || option == "3")
            {
                destination = typesOfAccount[int.Parse(option) - 1];
                validDestination = true;
                Console.WriteLine($"Odabrali ste {destination} racun.");
            }
            else
            {
                Console.WriteLine("Krivi unos, odaberite opet.");
            }
        }

        Console.WriteLine("Unesite iznos koji zelite poslati");
        var amount = Console.ReadLine();
        double amountToSend;
        if (double.TryParse(amount, out amountToSend) && amountToSend > 0)
        {
            var userReciever = UserDict[idToSend];

            var source = typeOfAccount;
            if (user.Value.Item4[source] >= amountToSend)
            {
                user.Value.Item4[source] -= amountToSend;
                user.Value.Item4[source] = Math.Round(user.Value.Item4[source], 2);
                
                userReciever.Item4[destination] += amountToSend;
                userReciever.Item4[destination] = Math.Round(userReciever.Item4[destination], 2);

                int transactionIdSender = TransactionDict.Count + 1;
                DateTime transactionDate = DateTime.Now;
                string description = $"Prijenos s {source} racun korisnika s ID: {user.Key}, ime i prezime: {user.Value.Item1 + " " + user.Value.Item2} na {destination} racun korisnika s ID: {idToSend}, ime i prezime: { userReciever.Item1 + " " + userReciever.Item2}";
                string type = "rashod";

                TransactionDict[transactionIdSender] = new Tuple<int, double, string, string, string, string, DateTime>(
                    user.Key,
                    amountToSend,
                    source,
                    description,
                    type,
                    expense[9],
                    transactionDate
                );

                int transactionIdReciever = TransactionDict.Count + 1;
                string typeReciever = "prihod";
                TransactionDict[transactionIdReciever] = new Tuple<int, double, string, string, string, string, DateTime>(
                    idToSend,
                    amountToSend,
                    destination,
                    description,
                    typeReciever,
                    income[9],
                    transactionDate
                );

                Console.WriteLine($"Transakcija uspješno izvršena. Poslano {amountToSend} sa {source} na {destination}  racun korisnika s ID: {idToSend}, ime i prezime: {userReciever.Item1 + " " + userReciever.Item2}");

            }
            else
            {
                Console.WriteLine("Nemate dovoljno sredstava za poslati");
            }
        }
        else
        {
            Console.WriteLine("Krivi unos. Iznos mora biti pozitivan");
        }

    }
    else
    {
        Console.WriteLine("Unijeli ste krivi id, unesite opet");
        SendMoneyExtern(user, typeOfAccount);
        return;
    }
}

/*--------------------------------------------------------------------------------------------------------------------------------------*/
void FinanceReportMenu(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("\n\t a) trenutno stanje računa \n\t b) broj ukupnih transakcija \n\t c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu \n\t" +
        " d) postotak udjela rashoda za odabranu kategoriju \n\t e) prosječni iznos transakcija za odabrani mjesec i godinu \n\t f) prosječni iznos transakcije za odabranu kategoriju \n 0 - pocetni zaslon");

    char option = Console.ReadKey().KeyChar;
    switch(option)
    {
        case 'a':
            AccountBalance(user, typeOfAccount);
            return;
        case 'b':
            NumberOfTransactions(user, typeOfAccount);
            return;
        case 'c':
            TotalIncomeAndExpense(user, typeOfAccount);
            return;
        case 'd':
            PercentageOfExpense(user, typeOfAccount);
            return;
        case 'e':
            AverageAmountMonthYear(user, typeOfAccount);
            return;
        case 'f':
            AverageAmountCategory(user, typeOfAccount);
            return;
        case '0':
            return;
        default:
            Console.WriteLine("Krivi unos odaberite opet");
            FinanceReportMenu(user, typeOfAccount);
            return;
    }
}
void AverageAmountCategory(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Odaberite kategoriju ");
    var category = Console.ReadLine();
    if (!expense.ContainsValue(category) && !income.ContainsValue(category))
    {
        Console.WriteLine("Kriva kategorija. Unesite ispravnu");
        AverageAmountCategory(user, typeOfAccount);
        return;
    }
    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && transaction.Value.Item6 == category)
        .ToList();

    if (!sortedTransactions.Any())
    {
        Console.WriteLine($"Nisu pronadene transakcije za odabranu kategoriju");
        return;
    }

    int numOfTransaction = 0;
    double categoryAmount = 0.0;
    foreach (var transaction in sortedTransactions)
    {
        categoryAmount += transaction.Value.Item2;
        numOfTransaction++;
    }
    Console.WriteLine($"Ukupni broj transakcija za odabranu kategoriju je {numOfTransaction}, ukupan iznos je: { categoryAmount}. Proječni iznos je: { categoryAmount/numOfTransaction }");

}
void AverageAmountMonthYear(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Unesite mjesec(broj mjeseca):");
    var monthString = Console.ReadLine();
    Console.WriteLine("Unesite godinu:");
    var yearString = Console.ReadLine();

    if (!int.TryParse(monthString, out int month) || !int.TryParse(yearString, out int year))
    {
        Console.WriteLine("Krivi unos za mjesec ili godinu.");
        return;
    }

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key &&
                             transaction.Value.Item3 == typeOfAccount &&
                             transaction.Value.Item7.Month == month &&
                             transaction.Value.Item7.Year == year)
        .ToList();

    if (!sortedTransactions.Any())
    {
        Console.WriteLine($"Nisu pronadene transakcija za korisnika {user.Value.Item1} u {month} mjesecu {year}.");
        return;
    }

    int numOfTransactions = 0;
    double amount = 0.0;
    foreach (var transaction in sortedTransactions)
    {
        amount += transaction.Value.Item2;
        numOfTransactions++;
    }
    Console.WriteLine($"Ukupno transakcija u odabranome mjesecu i godini: { numOfTransactions }, ukupni iznos: { amount }. Prosjecni iznos po transakciji iznosi: { amount/numOfTransactions } ");
}

void PercentageOfExpense(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Odaberite kategoriju za rashode");
    var category = Console.ReadLine();
    if(!expense.ContainsValue(category))
    {
        Console.WriteLine("Kriva kategorija. Unesite ispravnu");
        PercentageOfExpense(user, typeOfAccount);
        return;
    }
    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && transaction.Value.Item5 == "rashod")
        .ToList();

    if (!sortedTransactions.Any())
    {
        Console.WriteLine($"Nisu pronadene transakcija za korisnika za rashode");
        return;
    }

    double totalExpense = 0.0;
    double categoryExpense = 0.0;
    foreach (var transaction in sortedTransactions)
    {
        
        if(transaction.Value.Item6 == category)
        {
            categoryExpense += transaction.Value.Item2;
        }
        totalExpense += transaction.Value.Item2;
    }
    Console.WriteLine($"Postotak rashoda za odabranu kategoriju({category}) iznosi: { categoryExpense/totalExpense * 100} %");
}
void TotalIncomeAndExpense(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Unesite mjesec(broj mjeseca):");
    var monthString = Console.ReadLine();
    Console.WriteLine("Unesite godinu:");
    var yearString = Console.ReadLine();

    if (!int.TryParse(monthString, out int month) || !int.TryParse(yearString, out int year))
    {
        Console.WriteLine("Krivi unos za mjesec ili godinu.");
        return;
    }

    double totalIncome = 0.0;
    double totalExpense = 0.0;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key &&
                             transaction.Value.Item3 == typeOfAccount &&
                             transaction.Value.Item7.Month == month &&
                             transaction.Value.Item7.Year == year)
        .ToList();

    if (!sortedTransactions.Any())
    {
        Console.WriteLine($"Nisu pronadene transakcija za korisnika {user.Value.Item1} u {month} mjesecu {year}.");
        return;
    }

    foreach (var transaction in sortedTransactions)
    {
        double amount = transaction.Value.Item2;
        string transactionType = transaction.Value.Item5;

        if (transactionType == "prihod")
            totalIncome += amount;
        
        else if (transactionType == "rashod")
            totalExpense += amount;  
    }

    Console.WriteLine($"Ukupni prihod za mjesec: {month} i godinu: {year} je: {totalIncome}.");
    Console.WriteLine($"Ukupni rashod za mjesec: {month} i godinu: {year} je: {totalExpense}.");
}

void NumberOfTransactions(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    var numOfTotalTransactions = 0;
    foreach (var transaction in TransactionDict)
    {
        if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
            numOfTotalTransactions++;
    }
    Console.WriteLine($"Broj ukupnih transakcija korisnika: {numOfTotalTransactions}");
}

void AccountBalance(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine($"Stanje za {typeOfAccount} račun korisnika {user.Value.Item1 + " " + user.Value.Item2} iznosi: {user.Value.Item4[typeOfAccount]}");
}
void ShowTransactionMenu(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("\t a) sve transakcije kako su spremljene \n\t b) sve transakcije sortirane po iznosu uzlazno \n\t c) sve transakcije sortirane po iznosu silazno " +
        "\n\t d) sve transakcije sortirane po opisu abecedno \n\t e) sve transakcije sortirane po datumu uzlazno \n\t f) sve transakcije sortirane po datumu silazno" +
        "\n\t g) svi prihodi \n\t h) svi rashodi \n\t i) sve transakcije za odabranu kategoriju \n\t j) sve transakcije za odabrani tip i kategoriju. \n Za vracanje u prethodni izbornik odaberite 0");

    
    bool isValid = true;
    do
    {
        char option = Console.ReadKey().KeyChar;
        switch (option)
        {
            case 'a':
                ShowAllTransactions(user, typeOfAccount);
                return;
            case 'b':
                ShowAllTransactionsSortedAscendingByAmount(user, typeOfAccount);
                return;
            case 'c':
                ShowAllTransactionsSortedDescending(user, typeOfAccount);
                return;
            case 'd':
                ShowAllTransactionsSortedAlphabeticallyByDescription(user, typeOfAccount);
                return;
            case 'e':
                ShowAllTransactionsSortedAscendingByDate(user, typeOfAccount);
                return;
            case 'f':
                ShowAllTransactionsDescendingByDate(user, typeOfAccount);
                return;
            case 'g':
                ShowAllIncomes(user, typeOfAccount);
                return;
            case 'h':
                ShowAllExpenses(user, typeOfAccount);
                return;
            case 'i':
                ShowAllTransactionsCategory(user, typeOfAccount);
                return;
            case 'j':
                ShowAllTransactionsCategoryAndType(user, typeOfAccount);
                return;
            case '0':
                TransactionMenu(user, typeOfAccount);
                return;
            default:
                Console.WriteLine("\nKrivi unos. Unesite opet");
                isValid = false;
                break;
        }
    } while (!isValid);
}

void ShowAllTransactionsCategoryAndType(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Odaberite tip(rashod, prihod)");
    var type = Console.ReadLine();
    if(type != "prihod" && type != "rashod")
    {
        Console.WriteLine("Krivi unos, odaberite ispravan tip");
        ShowAllTransactionsCategoryAndType(user, typeOfAccount);
        return;
    }
    Console.WriteLine("Odaberite kategoriju");
    var category = Console.ReadLine();
    if (type == "prihod" && income.ContainsValue(category))
    {
        bool isFound = false;

        var sortedTransactions = TransactionDict
            .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && type == transaction.Value.Item5 && category == transaction.Value.Item6);

        Console.WriteLine("\nSve transakcije s prihodima i odabranom kategorijom:");
        foreach (var transaction in sortedTransactions)
        {
            var Values = transaction.Value;
            Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                    Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
            isFound = true;
        }

        if (!isFound)
        {
            Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
        }
    }
    else if(type == "rashod" && expense.ContainsValue(category))
    {
        bool isFound = false;

        var sortedTransactions = TransactionDict
            .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && type == transaction.Value.Item5 && category == transaction.Value.Item6);

        Console.WriteLine("\nSve transakcije s rashodima i odabranom kategorijom:");
        foreach (var transaction in sortedTransactions)
        {
            var Values = transaction.Value;
            Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                    Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
            isFound = true;
        }

        if (!isFound)
        {
            Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
        }
    }
    else
    {
        Console.WriteLine("Kategorija ne postoji, unesite opet");
        ShowAllTransactionsCategory(user, typeOfAccount);
        return;
    }
}

void ShowAllTransactionsCategory(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Odaberite kategoriju");
    var category = Console.ReadLine();
    if(income.ContainsValue(category) || expense.ContainsValue(category))
    {
        bool isFound = false;

        var sortedTransactions = TransactionDict
            .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && category == transaction.Value.Item6);

        Console.WriteLine("\nSve transakcije s odabranom kategorijom:");
        foreach (var transaction in sortedTransactions)
        {
            var Values = transaction.Value;
            Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                    Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
            isFound = true;
        }

        if (!isFound)
        {
            Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
        }
    }
    else
    {
        Console.WriteLine("Kategorija ne postoji, unesite opet");
        ShowAllTransactionsCategory(user, typeOfAccount);
        return;
    }
}
void ShowAllExpenses(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && "rashod" == transaction.Value.Item5);

    Console.WriteLine("\nSve transakcije s rashodima:");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllIncomes(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && "prihod" == transaction.Value.Item5);

    Console.WriteLine("\nSve transakcije s prihodima:");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllTransactionsDescendingByDate(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        .OrderBy(transaction => transaction.Value.Item7);

    Console.WriteLine("\nTransakcije sortirani po datumu silazno: ");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllTransactionsSortedAscendingByDate(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        .OrderBy(transaction => transaction.Value.Item7);

    Console.WriteLine("\nTransakcije sortirani po datumu uzlazno: ");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllTransactionsSortedAlphabeticallyByDescription(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        .OrderBy(transaction => transaction.Value.Item4);

    Console.WriteLine("\nTransakcije sortirani abecedno po opisu: ");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllTransactionsSortedDescending(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        .OrderByDescending(transaction => transaction.Value.Item2);

    Console.WriteLine("\nTransakcije sortirani silazno po iznosu: ");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}
void ShowAllTransactionsSortedAscendingByAmount(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;

    var sortedTransactions = TransactionDict
        .Where(transaction => transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        .OrderBy(transaction => transaction.Value.Item2);

    Console.WriteLine("\nTransakcije sortirani uzlazno po iznosu: ");
    foreach (var transaction in sortedTransactions)
    {
        var Values = transaction.Value;
        Console.WriteLine($"Transaction ID: {transaction.Key + ", ID korisnika: " + Values.Item1 + ", iznos: " + Values.Item2 + ", Tip racuna: " + Values.Item3 + ", opis transakcije: " + Values.Item4 + ",\n tip transakcije: " +
                Values.Item5 + ", kategorija: " + Values.Item6 + ", datum i vrijeme transakcije: " + Values.Item7} ");
        isFound = true;
    }

    if (!isFound)
    {
        Console.WriteLine("Nema pronađenih transakcija za odabranu vrstu računa i korisnika");
    }
}

void ShowAllTransactions(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isFound = false;
    foreach(var transaction in TransactionDict)
    {
        if(transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount)
        { 
            var Values = transaction.Value;
            Console.WriteLine($"ID: {transaction.Key} Tip: {Values.Item5} - Iznos: {Values.Item2} - Opis: {Values.Item4} - Kategorija: {Values.Item6} - Datum i vrijeme: {Values.Item7}");
            isFound = true;
        }
    }
    if(!isFound)
        Console.WriteLine("Nije pronađena transakcija za odabranu vrstu računa i korisnika");
}

void EditTransactions(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("Unesite ID transakcije koju želite izmjeniti");
    var id = Console.ReadLine();
    int idToEdit;

    if (!int.TryParse(id, out idToEdit) || !TransactionDict.ContainsKey(idToEdit))
    {
        Console.WriteLine("Krivi unos ili transakcija s unesenim ID ne postoji. Unesite opet.");
        EditTransactions(user, typeOfAccount);
        return;
    }
    
    var currTuple = TransactionDict[idToEdit];
    if(user.Key != currTuple.Item1)
    {
        Console.WriteLine("Odabrani id ne pripada korisniku");
        EditTransactions(user, typeOfAccount);
        return;
    }
    if(currTuple.Item3 != typeOfAccount)
    {
        Console.WriteLine("Odabrani ID ne spada u ovu vrstu računa");
        EditTransactions(user, typeOfAccount);
        return;
    }
    //Console.WriteLine("Unesite novi ID transakcije, ostavite prazno za isto:");
    //var newIdInput = Console.ReadLine();
    int currId = currTuple.Item1;

    //if (newIdInput.Length != 0 && int.TryParse(newIdInput, out int parsedNewId))
    //{ 
    //    currId = parsedNewId;
    //    if (TransactionDict.ContainsKey(currId))
    //    {
    //        Console.WriteLine("Id vec postoji, unesite novi");
    //        EditTransactions(user, typeOfAccount);
    //        return;
    //    }
    //}

    Console.WriteLine("Unesite novi iznos transakcije, ostavite prazno za isto:");
    var amountInput = Console.ReadLine();
    double currAmount = currTuple.Item2;
    if(amountInput.Length != 0)
    {
        if (double.TryParse(amountInput, out double parsedAmount) && parsedAmount >= 0)
            currAmount = parsedAmount;
        else
        {
            Console.WriteLine("Krivi unos iznosa, unesite opet");
            EditTransactions(user, typeOfAccount);
            return;
        }
    }
    if (amountInput.Length == 0)
        currAmount = currTuple.Item2;

    Console.WriteLine("Unesite novi tip računa(ziro, tekuci, prepaid), ostavite prazno za isto:");
    var newTypeAccount = Console.ReadLine();
    if(newTypeAccount != "ziro" && newTypeAccount != "tekuci" && newTypeAccount != "prepaid" && newTypeAccount.Length != 0)
    {
        Console.WriteLine("Krivi unos odaberite ziro tekuci ili prepaid");
        EditTransactions(user, typeOfAccount);
        return;
    }
    if (newTypeAccount.Length == 0)
        newTypeAccount = currTuple.Item3;

    Console.WriteLine("Unesite novi opis transakcije, ostavite prazno za isto:");
    var newDescription = Console.ReadLine();
    if (newDescription.Length == 0)
        newDescription = currTuple.Item4;

    Console.WriteLine("Unesite novi tip transakcije(prihod, rashod), ostavite prazno za isto:");
    var newTypeOfTransaction = Console.ReadLine();
    if(newTypeOfTransaction != "prihod" && newTypeOfTransaction != "rashod" && newTypeOfTransaction.Length != 0)
    {
        Console.WriteLine("Krivi unos, unesite opet");
        EditTransactions(user, typeOfAccount);
        return;
    }
    if(newTypeOfTransaction.Length == 0)
        newTypeOfTransaction = currTuple.Item5;

    Console.WriteLine("Unesite novu kategoriju transakcije za prihod: plaća, honorar, poklon, investicija, penzija, bonus, stipendija; " +
        "za rashod: hrana, najamnina, osiguranje, odjeća, donacije, restoran, kladionica");
    var newCategory = Console.ReadLine();
    if(newTypeOfTransaction == "prihod")
    {
        if(!income.ContainsValue(newCategory) && newCategory.Length != 0)
        { 
            Console.WriteLine("Niste odabrali ispravnu kategoriju za prihode");
            EditTransactions(user, typeOfAccount);
            return;
        }
    }
    else if(newTypeOfTransaction == "rashod")
    {
        if(!expense.ContainsValue(newCategory) && newCategory.Length != 0)
        { 
            Console.WriteLine("Niste odabrali ispravnu kategoriju za rashode");
            EditTransactions(user, typeOfAccount);
            return;
        }
    }

    Console.WriteLine("Unesite novi datum i vrijeme u formatu: dd/MM/yyyy HH:mm, ostavite prazno za isto:");
    string dateInput = Console.ReadLine().Trim();
    DateTime newDateTime = currTuple.Item7;

    if (dateInput.Length != 0)
    {
        if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            newDateTime = parsedDate;
        }
        else
        {
            Console.WriteLine("Neispravan format datuma i vremena. Pokušajte ponovo.");
            EditTransactions(user, typeOfAccount);
            return;
        }
    }

    var newTransaction = new Tuple<int, double, string, string, string, string, DateTime>(
        currId,
        currAmount,
        newTypeAccount,
        newDescription,
        newTypeOfTransaction,
        newCategory,
        newDateTime
    );
    Console.WriteLine("Zelite li stvarno urediti transakciju: y/n");
    char confirmation = Console.ReadKey().KeyChar;
    while(confirmation != 'y' && confirmation != 'n')
    {
        Console.WriteLine("Krivi unos, unesite opet");
        confirmation = Console.ReadKey().KeyChar;
    }
    if(confirmation == 'y')
    {
        TransactionDict[idToEdit] = newTransaction;
        Console.WriteLine("Transakcija uspjesno uređena");
        Console.WriteLine($"Ažurirani podaci o transakciji: \nID transakcije: {idToEdit} Iznos: {newTransaction.Item2} Tip računa: {newTransaction.Item3} " +
        $"Opis transakcije: {newTransaction.Item4} Tip transakcije: {newTransaction.Item5} Kategorija: {newTransaction.Item6} Datum i vrijeme: {newTransaction.Item7}");
        updateId.Add(idToEdit);
        UpdateBankAccounts();
    }
    else
    {
        Console.WriteLine("Uredivanje transakcije prekinuto");
        return;
    }
}

void ChooseHowToDeleteTransactions(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    Console.WriteLine("\t a) po id-u \n\t b) ispod unesenog iznosa \n\t c) iznad unesenog iznosa \n\t d) svih prihoda \n\t e) svih rashoda \n\t f) svih transakcija za odabranu kategoriju \n 0 - pocetni zaslon");
    char option = Console.ReadKey().KeyChar;

    switch (option)
    {
        case 'a':
            DeleteById(user, typeOfAccount);
            return;
        case 'b':
            DeleteEveryTranscUnderAmount(user, typeOfAccount);
            return;
        case 'c':
            DeleteEveryTranscOverAmount(user, typeOfAccount);
            return;
        case 'd':
            DeleteEveryIncome(user, typeOfAccount);
            return;
        case 'e':
            DeleteEveryExpense(user, typeOfAccount);
            return;
        case 'f':
            DeleteEveryTransactionForChoosenCategory(user, typeOfAccount);
            return;
        case '0':
            return;
        default:
            Console.WriteLine("Krivi unos. Unesite opet");
            ChooseHowToDeleteTransactions(user, typeOfAccount);
            break;
    }

}

void DeleteEveryTransactionForChoosenCategory(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isDeleted = false;
    Console.WriteLine("Unesite kategoriju za koju će se sve transakcije s tom kategorijom izbrisati: ");
    var category = Console.ReadLine();
    if(!expense.ContainsValue(category) && !income.ContainsValue(category))
    {
        Console.WriteLine("Ne postoji unesena kategorija. Unesite opet");
        DeleteEveryTransactionForChoosenCategory(user, typeOfAccount);
        return;
    }

    char confirmation = Confirmation();
    if (confirmation == 'y')
    {
        foreach (var transaction in TransactionDict)
        {
            if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount &&  transaction.Value.Item6 == category)
            {
                double amountOfTrans = transaction.Value.Item2;
                string accountType = transaction.Value.Item3;
                string transactionType = transaction.Value.Item5;

                if (transactionType == "rashod")
                    UserDict[user.Key].Item4[accountType] += amountOfTrans;

                else if (transactionType == "prihod")
                    UserDict[user.Key].Item4[accountType] -= amountOfTrans;

                int id = transaction.Key;
                TransactionDict.Remove(id);
                Console.WriteLine($"Transakcija s ID {id} uspješno izbrisana");
                isDeleted = true;
            }
        }
        if (!isDeleted)
            Console.WriteLine($"Ne postoji transakcija za odabranog korisnika s kategorijom {category}");
    }
    else
    {
        Console.WriteLine("Brisanje transakcije prekinuto");
        return;
    }
}

void DeleteEveryExpense(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    char confirmation = Confirmation();
    if(confirmation == 'y')
    {   
        bool isDeleted = false;
    
        foreach (var transaction in TransactionDict)
        {
            if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && transaction.Value.Item5 == "rashod")
            {
                double amountOfTrans = transaction.Value.Item2;
                string accountType = transaction.Value.Item3;
                string transactionType = transaction.Value.Item5;

                if (transactionType == "rashod")
                    UserDict[user.Key].Item4[accountType] += amountOfTrans;

                else if (transactionType == "prihod")
                    UserDict[user.Key].Item4[accountType] -= amountOfTrans;

                int id = transaction.Key;
                TransactionDict.Remove(id);
                Console.WriteLine($"Transakcija s ID {id} uspješno izbrisana");
                isDeleted = true;
            }
        }
        if (!isDeleted)
            Console.WriteLine("Ne postoje transakcije odabranog korisnika s rashodima");
    }
    else
    {
        Console.WriteLine("Brisanje prekinuto");
        return;
    }
}
void DeleteEveryIncome(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    char confirmation = Confirmation();
    if (confirmation == 'y')
    {
        bool isDeleted = false;
        foreach (var transaction in TransactionDict)
        {
            if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && transaction.Value.Item5 == "prihod")
            {
                double amountOfTrans = transaction.Value.Item2;
                string accountType = transaction.Value.Item3;
                string transactionType = transaction.Value.Item5;

                if (transactionType == "rashod")
                    UserDict[user.Key].Item4[accountType] += amountOfTrans;

                else if (transactionType == "prihod")
                    UserDict[user.Key].Item4[accountType] -= amountOfTrans;

                int id = transaction.Key;
                TransactionDict.Remove(id);
                Console.WriteLine($"Transakcija s ID {id} uspješno izbrisana");
                isDeleted = true;
            }
        }
        if (!isDeleted)
            Console.WriteLine("Ne postoje transakcije odabranog korisnika s prihodima");
    }
    else
    {
        Console.WriteLine("Brisanje prekinuto");
        return;
    }
}

void DeleteEveryTranscOverAmount(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isDeleted = false;
    Console.WriteLine("Unesite iznos iznad koje će se sve transakcije izbrisati");
    var amount = Console.ReadLine();
    double amountToDelete;
    if (double.TryParse(amount, out amountToDelete))
    {
        char confirmation = Confirmation();
        if(confirmation == 'y')
        {
            foreach (var transaction in TransactionDict)
            {
                double currentAmount = transaction.Value.Item2;
                if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && currentAmount > amountToDelete)
                {
                    double amountOfTrans = transaction.Value.Item2;
                    string accountType = transaction.Value.Item3;
                    string transactionType = transaction.Value.Item5;

                    if (transactionType == "rashod")
                        UserDict[user.Key].Item4[accountType] += amountOfTrans;

                    else if (transactionType == "prihod")
                        UserDict[user.Key].Item4[accountType] -= amountOfTrans;
                    int id = transaction.Key;
                    TransactionDict.Remove(id);
                    Console.WriteLine($"Transakcija s ID {id} uspješno izbrisana. Iznos koji je bio: {currentAmount}");
                    isDeleted = true;
                }
            }
            if (!isDeleted)
            {
                Console.WriteLine("Ne postoji transakcija za odabranog korisnika s iznosom većim od unesenog");
            }
        }
        else
        {
            Console.WriteLine("Brisanje prekinuto");
            return;
        }
    }
    else
    {
        Console.WriteLine("krivi unos. Unesite opet");
        DeleteEveryTranscUnderAmount(user, typeOfAccount);
    }
}
void DeleteEveryTranscUnderAmount(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isDeleted = false;
    Console.WriteLine("Unesite iznos ispod koje će se sve transakcije izbrisati");
    var amount = Console.ReadLine();
    double amountToDelete;
    if (double.TryParse(amount, out amountToDelete))
    {
        char confirmation = Confirmation();
        if (confirmation == 'y')
        {
            foreach (var transaction in TransactionDict)
            {
                double currentAmount = transaction.Value.Item2;
                if (transaction.Value.Item1 == user.Key && transaction.Value.Item3 == typeOfAccount && currentAmount < amountToDelete)
                {
                    double amountOfTrans = transaction.Value.Item2;
                    string accountType = transaction.Value.Item3;
                    string transactionType = transaction.Value.Item5;

                    if (transactionType == "rashod")
                        UserDict[user.Key].Item4[accountType] += amountOfTrans;

                    else if (transactionType == "prihod")
                        UserDict[user.Key].Item4[accountType] -= amountOfTrans;

                    UserDict[user.Key].Item4[accountType] = Math.Round(UserDict[user.Key].Item4[accountType], 2);
                    int id = transaction.Key;
                    TransactionDict.Remove(id);
                    Console.WriteLine($"Transakcija s ID {id} uspješno izbrisana. Iznos koji je bio: {currentAmount}");
                    isDeleted = true;
                }
            }
            if (!isDeleted)
            {
                Console.WriteLine("Ne postoji transakcija za odabranog korisnika s iznosom manjim od unesenog");
            }
        }
        else
        {
            Console.WriteLine("Brisanje prekinuto");
            return;
        }
    }
    else
    {
        Console.WriteLine("krivi unos. Unesite opet");
        DeleteEveryTranscUnderAmount(user, typeOfAccount);
    }
}

void DeleteById(KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    bool isDeleted = false;
    do
    {
        Console.WriteLine("Unesite ID transkacije koju želite izbrisati");
        var ID = Console.ReadLine();
        int idToDelete;
        if (int.TryParse(ID, out idToDelete))
        {
            if (TransactionDict.TryGetValue(idToDelete, out var transaction) && transaction.Item1 == user.Key && transaction.Item3 == typeOfAccount)
            {
                char confirmation = Confirmation();
                if (confirmation == 'y')
                {
                    double amount = transaction.Item2;
                    string accountType = transaction.Item3;
                    string transactionType = transaction.Item5;

                    if (transactionType == "rashod")
                        UserDict[user.Key].Item4[accountType] += amount;
                    
                    else if (transactionType == "prihod")
                        UserDict[user.Key].Item4[accountType] -= amount;
                    
                    UserDict[user.Key].Item4[accountType] = Math.Round(UserDict[user.Key].Item4[accountType], 2);
                    TransactionDict.Remove(idToDelete);
                    Console.WriteLine($"Transakcija s ID {idToDelete} uspjesno izbrisana.");
                    isDeleted = true;
                    processedTransactions.Remove(idToDelete);
                }
                else
                {
                    Console.WriteLine("Brisanje prekinuto");
                    return;
                }
            }
            else
                Console.WriteLine("Ne postoji transakcija s unesenim ID-om za odabranog korisnika");
            
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
    char option;

    Console.WriteLine("\n\t a) trenutno izvršena transakcija \n\t b) ranije izvršena transakcija \n 0 - pocetni zaslon");

    while (true)
    {
        option = Console.ReadKey().KeyChar;

        if (option == 'a')
        {
            dateAndTimeOfTransaction = DateTime.Now;
            FillDataOfNewTransaction(dateAndTimeOfTransaction, user, typeOfAccount);
            break;
        }
        else if (option == 'b')
        {
            bool isValid = true;
            do
            {
                Console.WriteLine("\nUnesite datum i vrijeme transakcije u formatu dd/MM/yyyy hh:mm");
                string date = Console.ReadLine();

                if (DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out dateAndTimeOfTransaction))
                {
                    FillDataOfNewTransaction(dateAndTimeOfTransaction, user, typeOfAccount);
                    return;
                }
                else
                {
                    Console.WriteLine("Unesen neispravan format. Unesite u obliku: dd/MM/yyyy hh:mm");
                    isValid = false;
                }

            } while (!isValid);
            break;
        }
        else if (option == '0')
            return;
        else
        {
            Console.WriteLine("Krivi unos, unesite opet.");
        }
    }
}

//ponovni unos u slucaju krivog, odnosno provjera svega je li negativan iznos je li prazan opis itd.-- obavljeno
void FillDataOfNewTransaction(DateTime dateTimeNow, KeyValuePair<int, Tuple<string, string, DateTime, Dictionary<string, double>>> user, string typeOfAccount)
{
    int userId = user.Key;
    int num = 1;

    Console.WriteLine("Unesite tip transakcije(prihod, rashod)");
    string typeOfTransaction = Console.ReadLine();
    string choosenCategory = "";
    if (typeOfTransaction == "prihod")
    {
       choosenCategory = ChooseCategory(income);
    }
    else if(typeOfTransaction == "rashod")
        choosenCategory = ChooseCategory(expense);
    else
    { 
        Console.WriteLine("Krivi unos. Birajte opet");
        FillDataOfNewTransaction(dateTimeNow, user, typeOfAccount);
        return;
    }

    Console.WriteLine("Unesite iznos transakcije");
    string amnt = Console.ReadLine();
    double amount;
    if(!double.TryParse(amnt, out amount) || amount < 0)
    {
        Console.WriteLine("Unijeli ste pogrešan iznos(iznos ne može biti negativan)");
        FillDataOfNewTransaction(dateTimeNow, user, typeOfAccount);
        return;
    }
    Console.WriteLine("Unesite opis transakcije");
    string descriptionOfTransaction = Console.ReadLine();
    if(descriptionOfTransaction.Length == 0)
    {
        Console.WriteLine("Opis ne moze biti prezan. Unesite opet");
        FillDataOfNewTransaction(dateTimeNow, user, typeOfAccount);
        return;
    }
    TransactionDict.Add(transactionId++, Tuple.Create(userId, amount, typeOfAccount, descriptionOfTransaction, typeOfTransaction, choosenCategory, dateTimeNow));
    Console.WriteLine("Transakcija uspjesno dodana");
    UpdateBankAccounts();
}

string ChooseCategory(Dictionary<int, string> categoryDictionary)
{
    int num = 1;

    foreach (var category in categoryDictionary)
    {
        Console.WriteLine($"{num++} - {category.Value}");
    }

    Console.WriteLine("Odaberite broj koji predstavlja vašu kategoriju:");
    char categoryOption = Console.ReadKey().KeyChar;
    int selectedOption;

    if (!int.TryParse(categoryOption.ToString(), out selectedOption) || !categoryDictionary.ContainsKey(selectedOption))
    {
        Console.WriteLine("Krivi unos. Birajte opet.");
        ChooseCategory(categoryDictionary);
        return "";
    }

    return categoryDictionary[selectedOption];
}
void ShowOptions()
{
    bool isCorrectOption = false;
    while (!isCorrectOption)
    {
        Console.WriteLine("Odaberite koje korisnike želite prikazati: \n\t\t a) ispis svih korisnika abecedno po prezimenu \n \t\t b) ispis svih onih s više od 30 godina \n \t\t c) ispis svih s bar jednim računom u minusu." +
            "\n0 - pocetni zaslon");
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
            case '0':
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
        Console.WriteLine("Unesite ime");
        string name = Console.ReadLine();
        if(name.Length == 0)
        {
            Console.WriteLine("Ime ne moze biti prazno. Unesite opet");
            CreateNewUser();
            return;
        }
        Console.WriteLine("Enter surname");
        string surname = Console.ReadLine();
        if (surname.Length == 0)
        {
            Console.WriteLine("Prezime ne moze biti prazno. Unesite opet");
            CreateNewUser();
            return;
        }
        bool isValid = true;
        do
        {
            Console.WriteLine("Unesite datum u formatu dd/MM/yyyy");
            string date = Console.ReadLine();
            if (!(DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateOfBirth)))
            {
                Console.WriteLine("Krivi format datuma");
                isValid = false;
            }
            else
                isValid = true;
        } while (!isValid);

        int id = globalId++;

        
        UserDict.Add(id, Tuple.Create(name, surname, dateOfBirth, bankAccount));
        Console.WriteLine($"Korisnik uspješno kreiran. Ime: {name}, prezime: {surname}, datum rodenja: {dateOfBirth}, ID: {id}");

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

        Console.WriteLine("Unesite u koje ime želite promijeniti. Ako ne želite promijeniti ostavite prazno");
        var newName = Console.ReadLine().Trim();     
        if (newName.Length == 0)
        {
            newName = UserDict[idToEdit].Item1;
        }

        Console.WriteLine("Unesite u koje prezime želite promijeniti. Ako ne želite promijeniti ostavite");
        var newSurname = Console.ReadLine().Trim();

        if (newSurname.Length == 0)
        {
            newSurname = UserDict[idToEdit].Item2;
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
        Console.WriteLine($"Korisnik uspješno ažuriran\n {"Ime: " + newName + ", prezime: " + newSurname + ", datum rodenja: " + newDateOfBirth}");

        break;
    }
}

void PrintBySurname()
{
    var sortedBySurname = UserDict.OrderBy(element => element.Value.Item2);
    Console.WriteLine("Korisnici sortirani po prezimenu");
    foreach(var dictElement in sortedBySurname )
    {
        Console.WriteLine($"{" ID " + dictElement.Key + " - Ime: " + dictElement.Value.Item1 + " - Prezime: " + dictElement.Value.Item2 +  " - Datum rodenja: " + dictElement.Value.Item3} ");
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
    //if (TransactionDict.Count == 0)
    //{
    //    Console.WriteLine("No new transactions to process.");
    //    return;
    //}
    foreach (var transaction in TransactionDict)
    {
        int transactionId = transaction.Key;
        int userId = transaction.Value.Item1;
        double amount = transaction.Value.Item2;
        string accType = transaction.Value.Item3;
        string transType = transaction.Value.Item5;

        if (processedTransactions.Contains(transactionId) && !updateId.Contains(transactionId))
            continue;
        updateId.Remove(transactionId);
        if (transType == "prihod")
            UserDict[userId].Item4[accType] += amount;
        else if (transType == "rashod")
            UserDict[userId].Item4[accType] -= amount;

        UserDict[userId].Item4[accType] = Math.Round(UserDict[userId].Item4[accType], 2);
        //ne triba provjera postoji li korisnik s tim IDem, osigurano prije poziva funkcije da postoji korisnik s tim id
        processedTransactions.Add(transactionId);
    }
}
char Confirmation()
{
    Console.WriteLine("Zelite li stvarno urediti transakciju: y/n");
    char confirmation = Console.ReadKey().KeyChar;
    while (confirmation != 'y' && confirmation != 'n')
    {
        Console.WriteLine("Krivi unos, unesite opet");
        confirmation = Console.ReadKey().KeyChar;
    }
    return confirmation;
}
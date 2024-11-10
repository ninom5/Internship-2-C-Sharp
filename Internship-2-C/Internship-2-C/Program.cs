Console.WriteLine("1 - Korisnici");
Console.WriteLine("2 - Računi");
Console.WriteLine("3 - Izlaz iz aplikacije");

char option = Console.ReadKey().KeyChar; //char, tako da se u slucaju unosa slova program ne prekine

switch(option)
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
        break; //add funct to choose again for wrong input
}    

static void Users()
{
    Console.WriteLine("1. Korisnici");
    Console.WriteLine("\t1 - Unos novog korisnika");
    Console.WriteLine("\t2 - Brisanje korisnika\n \t\t a) po id-u \n \t\t b) po imenu i prezimenu");
    Console.WriteLine("\t3 - Uređivanje korisnika \n \t\t a) po id-u");
    Console.WriteLine("\t4 - Pregled korisnika \n \t\t a) ispis svih korisnika abecedno po prezimenu \n \t\t b) ispis svih onih s više od 30 godina \n \t\t c) ispis svih s bar jednim računom u minusu");


}

static void Accounts()
{
    Console.WriteLine("Unesite ime i prezime korisnika");
    string user = Console.ReadLine();
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
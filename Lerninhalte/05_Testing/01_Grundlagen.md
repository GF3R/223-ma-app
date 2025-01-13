# Testing

Wir vermuten, dass die Funktion book(…) im BookingRepository im Backend anfällig auf Multiuserprobleme sein könnte, darum erstellen wir Unittests um spezifisch die Multiuserfähigkeiten und potentielle Probleme zu testen. Um Multiuserprobleme zu testen, müssen normale Unittests zuerst das Vertrauen erhöhen, dass die Applikation nach Spezifikation funktioniert.

## Aufgabenstellung

Erstellen Sie Integrationstests mit einer guten Test Coverage und erstellen Sie danach Tests, die spezifisch auf die Multiuserprobleme testen.

In der aktuellen Applikation gibt es sehr wenig Logik, daher ist ein Unit-Test nicht sinnvoll. Wir erstellen daher Integrationstests.
Kennen Sie den Unterschied noch? Falls nicht, recherchieren Sie das kurz.

### Teilaufgabe 1: Integrationstests

Erstellen Sie ein eigenes Projekt für «normale» Tests. Erstellen Sie Integrationstests für die Funktionen in Bookingservice. Testen Sie die Funktionen mit verschiedenen Parametern und prüfen Sie, ob die Funktionen das richtige Resultat zurückgeben.

Frischen Sie Ihr Wissen über Testing auf mit den entsprechenden Modulen oder Quellen wie https://xunit.net/docs/getting-started/netcore/visual-studio oder https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test.


erstellen Sie folgendes Grundgerüst:

```csharp

using System;
using Xunit.Abstractions;

namespace LederTest
{
    public class LedgerRepositoryTests
    {
        private readonly ITestOutputHelper output;

        public LedgerRepositoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAllLedgers_ReturnsAllLedgers()
        {
            Assert.True(true, "Test not implemented");
        }
    }    
}

```

Mit «output» können Informationen aus den Tests ausgegeben werden.

Die «Collection» verhindert das parallele Ausführen der Tests Erstellen Sie Unittests nach dem [FIRST](https://medium.com/@tasdikrahman/f-i-r-s-t-principles-of-testing-1a497acda8d6) und dem «Arrange/Act/Assert» Prinzip, so dass Sie eine vernünftige Coverage in den relevanten Klassen imBackend erreichen.
Testen Sie auch, dass Fehlerzustände nicht auftreten können, also beispielsweise nicht gebucht wird, wenn die Buchung grösser ist als die zur Verfügung stehende Balance oder dass ein Ledger nicht geändert wird bei falscher ID. Testen Sie auch Edge-Cases, wie wenn beispielsweise der Genaue Betrag auf einem Ledger gebucht wird, so dass die Balance nachher 0 ist. Da wir für die Multiusertests die «echte» Datenbank verwenden, Mocken wir die Datenbank nicht. In einer echten Applikation würden Frameworks wie «Moq» für diese Aufgabe verwendet. 

Die gesuchten Fachleute sind diejenigen, die ChatGPT für die mühsamen Arbeiten verwenden und den Output verstehen und korrigieren können. Um etwas zu verstehen, muss die mühsame Arbeit normalerweise zuerst selbst erledigt werden. Finden Sie also eine gute Mischung zwischen selbst erstellten Unittests und der Hilfe von ChatGPT.

### Teilaufgabe 2: Parallele Unittests

Wir erstellen ein eigenes Projekt für diese Unittests, da sie einige Regeln für Unittests verletzen:

Fast: Sie sind nicht schnell ausgeführt. Sie sollen möglichst viele Bookings durchführen.

Isolated: Sie sind nicht wirklich isoliert, da Sie überprüfen müssen, ob ein Problem aufgetreten ist.

Repeatable: Durch die Parallelität wird nicht bei jedem Durchgang genau der gleiche Ablauf durchgeführt, und das ist auch nicht das Hauptziel.

Erstellen Sie ein eigenes Projekt und nennen Sie es «L-Bank.Concurrent.Test»

Erstellen Sie eine Testklasse nach folgendem Muster: 

```csharp	

using System;
using Xunit.Abstractions;

namespace LBank.Concurrent.Test
{
    public class ConcurrentTests
    {
        private readonly ITestOutputHelper output;

        public ConcurrentTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestBookingParallel()
        {
            const int numberOfBookings = 1000;
            const int users = 10;

            // Implementieren Sie hier die parallelen Buchungen
            Task[] tasks = new Task[users];
            void UserAction(int bookingsCount, decimal startingMoney)
            {
                Random random = new Random();
                for (int i = 0; i < numberOfBookings; i++)
                {
                    // Implementieren Sie hier die parallelen Buchungen
                    // Bestimmen sie zwei zufällige Ledgers
                }
            }

            for (int i = 0; i < users; i++)
            {
                tasks[i] = Task.Run(() => UserAction(numberOfBookings, 1000));
            }

            Task.WaitAll(tasks);
    }
}

```

Wenn beim Testen ein Fehler angezeigt wird, dann können Sie die obigen Hilfen verwenden oder setzen Sie USER_COUNT auf 1.
Das Ausführen dieses Tests kann lange dauern (mehrt als 30 Sekunden). Versuchen Sie, das Programm und die Tests so anzupassen, dass alles nach Vorgaben funktioniert.

Wenn von Beginn weg alles funktioniert, entfernen Sie die Transaktionen und schauen Sie, ob wir sie überhaupt brauchen und ob die Unittests das nachweisen können.


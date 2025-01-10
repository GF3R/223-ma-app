# Die Applikation

Die L-Bank hat jemanden eingestellt, der dieses Modul nicht besucht hat. Die
Person schreibt ein Programm, das Buchungen auf Kontos vornimmt, und alles
funktioniert im Singleuserbetrieb perfekt.

## Aufgabenstellung
Sie erstellen in diesem Auftrag eine Konsolenapplikation, welche die Probleme
von Multiuseranwendungen zeigt.

### Teilaufgabe 1: Repetition SQL
Repetieren Sie SQL. Sie sollten in der Lage sein:
- Eine Tabelle mit Primary- und Foreign Key zu erstellen und zu löschen.
- Daten einzufügen (INSERT).
- Daten zu löschen (DELETE).
- Daten zu ändern (UPDATE).
- Daten über mehrere Tabellen hinweg sortiert und gefiltert abzufragen

(SELECT … FROM … WHERE … ORDER BY …)

Sie können folgende Quellen benutzen:
- Modul 106/(105) [empfohlen]
- Tutorialspoint: https://www.tutorialspoint.com/sql/sql-overview.htm
- W3 schools: https://www.w3schools.com/sql/default.asp
- YouTube

### Teilaufgabe 2: Fragen zum Programm
Es wurde schon ein Grossteil des Programms erstellt. Schauen Sie sich mal das Backend durch.

Die Applikation besteht aus 3 Projekten. 

Cli, Core und DbAccess. Das Web Projekt wird in diesem Auftrag nicht benötigt und kann ignoriert werden.

Im Main Projekt "Cli" befindet sich die Datei Program.cs und das Command Line interface


Im Projekt "Core" befinden sich die Models.

Im Projekt "DbAccess" befinden sich die Datenbankzugriffe, hier finden Sie auch die Klasse DatabaseSeeder.cs, die die Datenbank erstellt und die Testdaten einfügt.

Ebenfalls in diesem Projekt finden Sie die Klasse DatabaseSettings.cs, diese Klasse enthält die Verbindungsstrings zur Datenbank. Diese werden durch die Appsettings befüllt.
```csharp
    public string? ConnectionString { get; set; } // die Verbindung zum Datenbank Server
```

Klasse «Ledger»: Steht für ein Konto.
- id ist der Primary Key.
- name ist der Name des Kontos.
- balance ist der aktuelle Kontostand.

Analysieren Sie das Programm und beantworten Sie folgende Fragen:
1. Wie wird verhindert, dass die Testdaten bei jedem Start wieder eingefügt
werden?
2. Recherchieren Sie, was ein HashSet und was ein ImmutableHashSet ist.
3. Welcher SQL-Befehl zählt alles Geld zusammen?
4. Was bedeutet <<Property>>?
5. Was bedeutet <<Readonly>>?
6. (Schwer) Wieso wird nicht «double» für den Kontostand verwendet?
7. implementieren sie die Methode Book.


Bringen Sie das Programm zum Laufen und sehen Sie sich mit dem «SQL Server
Management Studio» die Tabellen an. (Falls ihrMariaDb verwendet ist eventuell HeidiSQL oder ähnliche Tools einfacher)
Bringen Sie folgenden Ablauf des Programms in die korrekte Reihenfolge:
1. Es werden alle Ledgers (Kontos) abgefragt und ausgegeben.
2. Das Geld in allen Ledgers wird am Ende zusammengezählt und ausgegeben.
3. Wenn die Tabelle «ledgers» leer ist, werden Testdaten geseeded (eingefügt).
4. Das Geld in allen Ledgers wird zu Beginn zusammengezählt und ausgegeben.
5. Hier wäre Platz für Ihr Code.
6. Wenn die Datenbank «l_bank» noch nicht existiert, wird sie erstellt.
7. Wenn die Tabelle «ledgers» noch nicht existiert, wird sie erstellt.

Erstellen Sie für sich eine kurze Referenz, die Sie beim Programmieren
verwenden können, mit einem praktischen Codebeispiel für folgende Dinge:
1. Wie wird in C# eine Tabelle erstellt?
2. Wie wird in C# ein Datensatz eingefügt?
3. Wie wird in C# ein Datensatz geändert?
4. Wie werden in C# mehrere Datensätze abgefragt?
5. Wie wird in C# ein Wert (wie die Summe allen Geldes) abgefragt?

Der Lern- und Arbeitsauftrag ist erfüllt, wenn …
- Sie das Programm bei zum Laufen gebracht haben.
- Sie das Programm studiert haben und den Ablauf sowie die Klassen erklären können.
- Sie zeigen können, wie mit C# DDL2-Befehle an die Datenbank gesendet werden können.
- Sie zeigen können, wie mit C# DML3-Befehle an die Datenbank gesendet werden können.
- Sie zeigen können, wie mit C# Daten in der Datenbank abgefragt werden können.
- Sie zeigen können, wie mit C# Daten in der Datenbank eingefügt und geändert werden können.

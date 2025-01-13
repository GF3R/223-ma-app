# Authentifizierung

Diese Multiuserapp braucht Authentifizierung und Autorisierung.


Authentifizierung ist der Prozess, bei dem die Identität einer Person überprüft wird. 

Autorisierung ist der Prozess, bei dem die Berechtigungen einer Person überprüft werden.

Theoretisch könnte ein Benutzer eingeloggt (Authentifiziert) sein, aber keine Berechtigung (Authorisierung) haben, um auf bestimmte Daten zuzugreifen.

## Aufgabenstellung
Finden Sie heraus, wie die Authentifizierung und die Autorisierung gelöst
wurden.

## Teilaufgabe 1: Authentifizierung
Beantworten Sie anhand des Quellcodes folgende Fragen:

- Findet eine Authentifizierung statt?
- Wo und wie findet die Authentifizierung statt?
- Was für einen Typ von Authentifizierung verwendet dieses Projekt? Welche Technologie wird verwendet?
- Wo werden den Benutzer:innen die Rollen zugewiesen und wo sind sie gespeichert?
- Wie wird sichergestellt, dass sich ein User nicht selbst in eine Rolle hinzufügen kann?

## Teilaufgabe 2: Authentifizierung

Ändern Sie im Quellcode temporär die Rechte auf die Liste aller Ledger so, dass nur Benutzer:innen in der Rolle «Administrators» Zugriff erhalten.

Benutzen Sie JSON-MAN oder POSTMAN und führen Sie folgende Schritte aus:

1. Rufen Sie die Liste aller Ledgers ohne Authentifizierung auf, was passiert?
2. Loggen Sie sich als ein User mit der korrekten Rolle ein und rufen Sie die
Liste aller Ledgers auf. Was passiert?
3. Loggen Sie sich als ein User mit einer Rolle ohne Rechte ein und rufen Sie die Liste aller Ledgers auf. Was passiert?
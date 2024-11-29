# 223-ma-app

## Autoren

Maurus Brunnschweiler, Nino Nussbaumer, Mois Diabate

## Teilaufgabe 1: Analyse

Wo kann es bei diesem Ablauf bei Multiuserapplikationen zu Problemen kommen? Finden Sie mindestens 3 Probleme.

Mögliche Probleme bei einer Multiuserapplikation wären:

    - Concurrent Updates (Gleichzeitige Aktualisierungen): Wenn zwei Transaktionen gleichzeitig ausgeführt werden und gleichzeitig dasselbe Konto aktualisieren, könnte eine Race Condition entstehen, bei der eine der Aktionen nicht korrekt widergespiegelt wird.

    - Deadlocks: Wenn verschiedene Transaktionen gleichzeitig verschiedene Konten blockieren und dann versuchen, auf das jeweils andere Konto zuzugreifen, könnte es zu einem Deadlock kommen.

    - Nicht persistentes Lesen (Dirty Reads): Eine Transaktion könnte den Wert eines Kontos lesen, während eine andere Transaktion es ändert, was zu inkonsistenten Daten führen könnte.

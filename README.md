# Vocab-Owl-Ary
## Ein einfacher Vokabeltrainer für Schüler.

Diese App ist mit .Net Maui erstellt. Es handelt sich um einen einfachen Vokabeltrainer für Windows, Android und iOS. 

Die Datenhaltung erfolgt in einer lokalen SQLite Datenbank. Die eingebenen Daten, Datenquellen und Statistiken werden nirgendwo hin exportiert - die App läuft komplett lokal ohne Internetzugriff. 


## Projektstruktur
Das Projekt ist ein Visual Studio 2022 Projekt. Die Solution enthält 3 Unterprojekte: 
- Das Hauptprojekt: Vokabeltrainer: Eine .Net Maui App, erstellt nach dem MVVM Pattern
- Ein Model-Projekt: Enthält Die DB Context Klassen für die Code-First Generierung des Datenbankschemas.
  - Dieses wird benötigt, da der Generator für die Entity Framework Migrations aktuell noch nicht mit .Net Maui Projekten umgehen kann -> Normales .Net 7.0 Projekt
  - Dieses Projekt enthält nur die konkrete Datenbankanbindung.
- Ein Shared-Projekt, welches die Model-Klassen enthält

## Compile
Die Visual Studio Solution hat außer den referenzierten Nuget Packages keine weiteren Abhängigkeiten. 

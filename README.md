# Web-Dashboard zur Ticketverwaltung

## 📘 Projektbeschreibung
Dieses Projekt ist Teil der Abschlussprüfung zum Fachinformatiker Anwendungsentwicklung (IHK Rheinhessen, Winter 2025).
Es handelt sich um eine ASP.NET Core MVC-Anwendung zur zentralen Darstellung und Verwaltung von Ticketdaten aus Sage und E-Mail.

## ⚙️ Technischer Stack
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8.0.10
- SQL Server Express
- MailKit für IMAP-Mailabruf
- HTML5 / CSS3 / JavaScript

## 🧩 Funktionen
- Login & Benutzerverwaltung
- Personalisierte Ticketübersicht
- Statusänderungen direkt im Dashboard
- Mail-Tickets zuweisen
- Automatische Datenkonsolidierung

## 🗄️ Datenbank
Die Anwendung verwendet eine lokale SQL Server Express-Instanz.
Connection String: siehe `appsettings.json`.

## 🚀 Projektstart
```bash
dotnet build
dotnet ef database update
dotnet run

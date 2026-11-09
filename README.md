# Benaudo Overlay Manager

Application Windows d'overlays performants pour iRacing.

Le développement de l'application a commencé par son écran de connexion et par une architecture d'authentification destinée à être reliée à un futur service commercial.

## Objectifs

- application C# / .NET 10 / WPF ;
- impact minimal sur les performances d'iRacing ;
- overlays Windows indépendants et transparents ;
- mises à jour distribuées par les Releases GitHub ;
- aucune télémétrie envoyée dans le cloud.

## Mises à jour

Les versions publiques seront publiées depuis un tag `vMAJEUR.MINEUR.CORRECTIF`, par exemple `v0.1.0`. GitHub Actions produira l'installateur Windows et les paquets nécessaires aux mises à jour automatiques.

La conception et la procédure complète sont décrites dans [docs/MISES-A-JOUR.md](docs/MISES-A-JOUR.md).

## État du projet

- socle C# / .NET 10 / WPF ;
- écran de connexion français noir et bleu électrique ;
- validation locale des champs et états d'attente/erreur ;
- contrat d'authentification testable ;
- refus sécurisé des connexions tant que le serveur commercial n'est pas configuré.

Voir [l'architecture d'authentification](docs/AUTHENTIFICATION.md).

## Liens

- [Signaler un problème](https://github.com/Benaudo-oss/Benaudo-Overlay-Manager/issues/new/choose)
- [Versions publiées](https://github.com/Benaudo-oss/Benaudo-Overlay-Manager/releases)
- [Politique de sécurité](SECURITY.md)

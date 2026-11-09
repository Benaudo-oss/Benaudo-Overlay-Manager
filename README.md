# Benaudo Overlay Manager

Application Windows d'overlays performants pour iRacing.

> Le développement repart actuellement de zéro. Le dépôt contient d'abord l'infrastructure GitHub, la chaîne de validation et la préparation du système de mises à jour.

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

Le code applicatif n'est pas encore présent. Les workflows de validation ignorent proprement la compilation jusqu'à l'ajout de la solution .NET.

## Liens

- [Signaler un problème](https://github.com/Benaudo-oss/Benaudo-Overlay-Manager/issues/new/choose)
- [Versions publiées](https://github.com/Benaudo-oss/Benaudo-Overlay-Manager/releases)
- [Politique de sécurité](SECURITY.md)

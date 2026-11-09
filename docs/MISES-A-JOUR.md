# Architecture des mises à jour

## Choix retenu

Benaudo Overlay Manager utilisera **Velopack** avec **GitHub Releases** comme source publique. Ce choix fournit :

- un installateur Windows ;
- des mises à jour différentielles quand elles sont disponibles ;
- une vérification du contenu téléchargé par le mécanisme de paquets ;
- une application de la mise à jour au redémarrage, hors du processus WPF ;
- des canaux stables et préversions séparables.

Le client ne devra jamais installer directement un fichier arbitraire depuis une URL. Il consultera uniquement la source GitHub configurée pour `Benaudo-oss/Benaudo-Overlay-Manager`.

## Intégration future dans l'application

Le projet WPF devra référencer le paquet NuGet `Velopack`. Le point d'entrée appellera le bootstrap Velopack avant de créer WPF, afin que l'application d'une mise à jour ne charge pas inutilement l'interface.

Un service applicatif unique devra :

1. vérifier la présence d'une version plus récente au démarrage puis à intervalle raisonnable ;
2. télécharger en arrière-plan sans bloquer l'overlay ;
3. afficher la version et les notes à l'utilisateur ;
4. demander confirmation avant le redémarrage ;
5. ne jamais interrompre une session iRacing pour forcer une installation.

Les erreurs réseau ne doivent pas empêcher l'application de démarrer.

## Publier une version

1. Mettre à jour le numéro de version et les notes.
2. Fusionner les changements validés dans `main`.
3. Créer et pousser un tag conforme, par exemple `v0.1.0`.
4. Le workflow `release.yml` vérifie, teste, publie en `win-x64`, fabrique les paquets Velopack et crée une Release GitHub.
5. Télécharger l'installateur depuis la Release et effectuer un test manuel sur une machine propre avant de déclarer la version stable.

Les tags contenant un suffixe, par exemple `v0.2.0-beta.1`, produisent une préversion GitHub.

## Signature Windows

La chaîne est prête à accueillir une signature de code, mais aucun certificat ne doit être stocké dans le dépôt. Quand un certificat sera disponible, ses informations seront ajoutées sous forme de secrets GitHub et l'étape de signature sera placée avant la publication.

Sans certificat de signature, Windows SmartScreen peut afficher un avertissement même si le fichier provient du dépôt officiel.

## Retour arrière

Une Release ne doit jamais être remplacée silencieusement. En cas de défaut, publier une nouvelle version corrective. Une Release gravement défectueuse peut être marquée comme préversion ou retirée du canal visible, tout en conservant l'historique Git.

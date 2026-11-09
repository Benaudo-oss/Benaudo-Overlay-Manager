# Authentification et vente

## Principe

L'application Windows est un client non fiable : elle ne doit jamais décider seule qu'une licence est valide. L'identité, les droits d'accès, les appareils autorisés et l'état de l'abonnement devront être contrôlés par un service distant exploité par Benaudo.

## État actuel

Le premier écran et le contrat `IAuthenticationService` sont présents. Le service temporaire refuse toutes les connexions avec un message explicite. Il n'existe donc aucun compte de démonstration, mot de passe universel ou contournement local.

Les boutons de création de compte et de récupération dirigent temporairement vers le dépôt GitHub. Ils devront être remplacés par les pages du futur portail client.

## Exigences du futur serveur

- HTTPS uniquement ;
- mots de passe hachés côté serveur avec un algorithme adapté ;
- jetons d'accès courts et jetons de renouvellement révocables ;
- jeton persistant protégé par Windows DPAPI, jamais le mot de passe ;
- limitation des tentatives et protection contre le credential stuffing ;
- vérification de l'adresse e-mail et récupération de compte ;
- journal d'audit des connexions et révocations ;
- gestion des licences, remboursements et appareils côté serveur ;
- période hors ligne limitée et signée, si elle est proposée ;
- suppression de compte et traitement conforme aux obligations de confidentialité applicables.

## Paiement

Les informations bancaires ne devront jamais transiter par l'application ni par notre serveur. Le paiement devra passer par une page hébergée par un prestataire spécialisé. Le serveur Benaudo recevra uniquement des événements signés du prestataire pour attribuer ou retirer les droits.

Le choix du fournisseur d'identité, du prestataire de paiement, des tarifs et des règles de licence reste à faire avant de connecter cet écran à la production.

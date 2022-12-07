Ces étapes peuvent etres exécutés aléatoirement mais il est preferable de suivre cet ordre : 

1) Lancer l'éxécutable "ProxySerivce.exe" qui se trouve dans WebRoutingServer>ProxyService>bin>Debug

2) Lancer l'exécutable "BikingApp.exe" qui se trouve dans WebRoutingServer>BikingApp>bin>Debug

3) Lancer la commande "activemq.bat start" dans votre fichier apache-activemq-5.17.2-bin>bin

4) Executez le main du projet Client qui se trouve ici : ".\BikingApp\Client\src\main\java\main\MessageReceiver.java" .  

------------------------------------------------------------------------------------------------------------------------------------------------------------
 Fonctionnement : 

- Il va vous etre proposé de rentrer une adresse d'origine et de destination : on les entre et les steps du position d'origine vers notre destination seront affichés.

 --------------------------------------------------------------------------------------------------------------------------------------------------------------

- Nous avons réalisé la v1 avec l'activemq  : on affiche tous les steps directement . 

-Nous avons ajouté le serveur proxy et cache.


-------------------------------------------------------------------------------------------------------------------------------------------------------------
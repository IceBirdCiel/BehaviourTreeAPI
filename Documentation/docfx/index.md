# Documentation des Behaviour Tree : Cédric CHOPIN, Etienne GIBIAT, Nicolas RUCHE
## API
Voir l'[API](api/toc.html) de tous les scripts.

## Construire un Behaviour Tree dans Unity

1. **Ajout des scripts**
   L'IA doit posséder son script basique (par exemple Boss) ainsi qu'un script qui va gerer le BehaviourTree (Par exemple BossBT). 
   Le script BossBT doit hériter de BehaviourTree avec comme template le script de l'IA (public class BossBT : BehaviourTree<span><Boss</span>>)

2. **Construction des Nodes principaux**
   
   Il est nécessaire de construire les noeuds principaux de notre BehaviourTree, à savoir le root ainsi que les séquences.
   Pour cela il suffit de faire créer un scriptable object à partir du menu ESGI/Nodes/SELECTOR qui sera notre Selector ROOT.
   Ce noeud sera a ajouté au niveau du Start Node du BT.
   Ensuite, les séquences sont créées en utilisant le menu contextuel. Elles determinent l'ordre d'action que l'on veut obtenir pour notre IA.
   Il est nécessaire de creer les Node Sequence et Selector en dérivant les classes SelectorNode et SequenceNode spécifiques à notre agent.
   
3. **Conditions Node**

   Le conditions node est un noeud permettant de gérer les conditions (Est-ce que le joueur est à portée, est-ce que j'ai assez de vie,etc..). Pour cela il faut créer un script qui hérite de ConditionsNode et qui override la fonction OnUpdate. Elle doit retourner Success si la condition est vérifiée, Failure si non, Running si le node a besoin de plus de temps pour vérifier la condition. Un scriptable object est ensuite à créer à partir de ce script pour être donné dans le Tree.
4. **Actions Node**

    Les actions Node sont des Nodes qui modifient le Monde : position d'un objet par exemple. Il faut hériter de ActionNode. Tant que l'action est en cours la méthode OnUpdate doit renvoyer Running. Si l'action est réussie elle doit renvoyer Success, Failure sinon.
5. **Créer le Tree**
    Dans la scène, on glisse le script Behaviour Tree spécifique à notre objet (Boss par exemple). Il a besoin d'une référence à cet objet. On crée l'arbre en glissant des Nodes créés dans l'éditeur (rappel : se sont des scriptable objects.) dans le Behaviour Tree.
6. **Methodes des Node**
    On peut override certaines méthodes des Node : 

    - OnBeforeExecute : Appelée avant d'utiliser le node dans le tree. Utilisée pour reset les data avant d'executer le node.
    - OnInit : Appelée au tout début; comme un constructeur.
    - OnExecutionEnd : Une fois que le node arrive en Failure ou State.
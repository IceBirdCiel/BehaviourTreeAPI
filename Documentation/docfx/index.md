# API
Go to [API](api/toc.html).

### Construire un Behaviour Tree dans Unity

1. **Ajout des scripts**
   L'IA doit posséder son script basique (par exemple Boss) ainsi qu'un script qui va gerer le BehaviourTree (Par exemple BossBT). 
   Le script BossBT doit hériter de BehaviourTree avec comme template le script de l'IA (public class BossBT : BehaviourTree<span><Boss</span>>)

2. **Construction des Nodes principaux**
   
   Il est nécessaire de construire les noeuds principaux de notre BehaviourTree, à savoir le root ainsi que les séquences.
   Pour cela il suffit de faire créer un scriptable object à partir du menu ESGI/Nodes/SELECTOR qui sera notre Selector ROOT.
   Ce noeud sera a ajouté au niveau du Start Node du BT.
   Ensuite, les séquences sont créées en utilisant le menu contextuel. Elles determinent l'ordre d'action que l'on veut obtenir pour notre IA
   
3. **Conditions Node**
   Le conditions node est un noeud permettant de gérer les conditions (Est-ce que le joueur est à portée, est-ce que j'ai assez de vie,etc..). Pour cela il faut créer un script qui hérite de ConditionsNode et qui override la fonction OnUpdate. Un scriptable object est ensuite créer à partir de ce script pour être donné dans la séquence.
4. **Actions Node**
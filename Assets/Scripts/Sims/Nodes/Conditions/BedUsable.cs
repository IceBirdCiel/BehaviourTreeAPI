using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using PGSauce.Core.PGDebugging;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Conditions/BedUsable")]
public class BedUsable : ConditionNode<SimsController>
{
    public SharedUsableObject sharedUsable;
    private List<UsableObject> usables;
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
        sharedUsable.ResetVariable();
        if(Agent.gameManager.usableObjects.ContainsKey("bed"))
            usables = Agent.gameManager.usableObjects["bed"];
    }

    protected override NodeState OnUpdate()
    {
        foreach(UsableObject usable in usables)
        {
            if (!usable.isInUse)
            {
                sharedUsable.Value = usable;
                return NodeState.Success;
            }
                
        }
        return NodeState.Failure;
    }
}

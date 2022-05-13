using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using PGSauce.Core.PGDebugging;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Conditions/ToiletUsable")]
public class ToiletUsable : ConditionNode<SimsController>
{
    public SharedUsableObject sharedUsable;
    private List<UsableObject> usables;
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
        sharedUsable.ResetVariable();
        if(Agent.gameManager.usableObjects.ContainsKey("toilet"))
            usables = Agent.gameManager.usableObjects["toilet"];
    }

    protected override NodeState OnUpdate()
    {
        PGDebug.Message($"TOILET USABLE UPDATE").Log();
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

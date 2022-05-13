using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using PGSauce.Core.PGDebugging;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Conditions/NeedToUseToilet")]
public class NeedToUseToilet : ConditionNode<SimsController>
{
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
    }

    protected override NodeState OnUpdate()
    {
        Agent.besoins.besoins["Petits_besoins"].isIncreasing = false;
        if(Agent.besoins.besoins["Petits_besoins"].value < 75)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}

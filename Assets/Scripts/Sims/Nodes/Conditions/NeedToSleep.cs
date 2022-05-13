using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using PGSauce.Core.PGDebugging;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Conditions/NeedToSleep")]
public class NeedToSleep : ConditionNode<SimsController>
{
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
    }

    protected override NodeState OnUpdate()
    {
        Agent.besoins.besoins["Energie"].isIncreasing = false;
        if(Agent.besoins.besoins["Energie"].value < 75)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}

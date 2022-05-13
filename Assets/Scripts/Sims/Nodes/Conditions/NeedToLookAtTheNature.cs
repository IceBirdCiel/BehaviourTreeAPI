using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using PGSauce.Core.PGDebugging;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Conditions/NeedToLookAtTheNature")]
public class NeedToLookAtTheNature : ConditionNode<SimsController>
{
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
    }

    protected override NodeState OnUpdate()
    {
        Agent.besoins.besoins["Environnement"].isIncreasing = false;
        if(Agent.besoins.besoins["Environnement"].value < 75)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}

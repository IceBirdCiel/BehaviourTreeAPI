using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Actions/PlayGame")]

public class PlayGame : ActionNode<SimsController>
{
    public SharedUsableObject sharedUsable;
    float duration;
    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();

        sharedUsable.Value.use();
        duration = sharedUsable.Value.timeToUse;
        Agent.besoins.besoins["Distractions"].isIncreasing = true;

    }

    protected override NodeState OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            sharedUsable.Value.reset();
            Agent.besoins.besoins["Distractions"].isIncreasing = false;
            return NodeState.Success;
        }

        return NodeState.Running;

    }
}

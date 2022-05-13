using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Sims/Actions/WalkTo")]
public class WalkTo : ActionNode<SimsController>
{

    private NavMeshAgent navMeshAgent;
    public SharedUsableObject sharedUsable;

    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
        Agent.m_Agent.SetDestination(sharedUsable.Value.transform.position);
    }

    protected override NodeState OnUpdate()
    {

        if (Vector3.Distance(navMeshAgent.transform.position, sharedUsable.Value.transform.position) < 1)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }

}

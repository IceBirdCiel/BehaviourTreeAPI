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
    private Vector3 target;

    public override void OnBeforeExecute()
    {
        base.OnBeforeExecute();
        if (sharedUsable)
        {
            if (sharedUsable.Value != null)
            {
                target = sharedUsable.Value.transform.position;
            }
        }
        else
        {
            target = Agent.getRandomPos();
        }
        Agent.m_Agent.SetDestination(target);
        Agent.walk();
    }

    protected override NodeState OnUpdate()
    {
        if (!Agent.m_Agent)
        {
            Debug.Log(")");
        }
        float dist = Vector3.Distance(Agent.m_Agent.transform.position, target);
        if (dist < 1)
        {
            Agent.idle();
            return NodeState.Success;
        }

        return NodeState.Running;
    }

}

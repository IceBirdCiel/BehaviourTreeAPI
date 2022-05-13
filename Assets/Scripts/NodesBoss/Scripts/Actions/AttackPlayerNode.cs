using ESGI.BehaviourTrees;
using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Actions/Attack Enemy")]
public class AttackPlayerNode : ActionNode<Boss>
{

    
    protected override NodeState OnUpdate()
    {
        Agent.animator.SetBool("Attack", true);

        return NodeState.Success;
    }
}

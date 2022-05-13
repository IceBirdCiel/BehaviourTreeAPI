using ESGI.BehaviourTrees;
using PGSauce.Core.Strings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Conditions/Check boss is dead")]
public class CheckBossIsDeadNode : ConditionNode<Boss>
{
    protected override NodeState OnUpdate()
    {
        if (Agent.isDead)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

}

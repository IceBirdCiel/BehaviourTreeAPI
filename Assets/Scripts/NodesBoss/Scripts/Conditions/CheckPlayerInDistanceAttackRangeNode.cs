using ESGI.BehaviourTrees;
using ESGI.BehaviourTrees.Variables;
using LeTai;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Conditions/Check enemy in Distance Attack Range")]
public class CheckPlayerInDistanceAttackRangeNode : ConditionNode<Boss>
{
    [SerializeField] private SharedTransform target;
    [SerializeField] private float fovDistance = 15f;
    [SerializeField] private GlobalLayerMask enemyMask;

    protected override NodeState OnUpdate()
    {
        var colliders = Physics.OverlapSphere(Agent.transform.position, fovDistance, enemyMask.Value);

        if (colliders.Length > 0)
        {
            target.Value = colliders[0].transform;
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();
        Gizmos.color = Color.blue.WithA(0.6f);
        Gizmos.DrawSphere(Agent.transform.position, fovDistance);
    }
}

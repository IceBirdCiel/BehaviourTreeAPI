﻿using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// Check if the target Enemy is in range. If true, update the SharedEnemy value.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Conditions/Enemy in melee range")]
    public class EnemyInMeleeRangeNode : ConditionNode<Patroller>
    {
        [SerializeField] private float attackRange = 1;
        [SerializeField] private SharedTransform target;
        [SerializeField] private SharedEnemy enemy;

        public override void OnBeforeExecute()
        {
            base.OnBeforeExecute();
            enemy.ResetVariable();
        }

        protected override NodeState OnUpdate()
        {
            if (!target.Value)
            {
                return NodeState.Failure;
            }
            
            if (Vector3.Distance(Agent.transform.position, target.Value.position) <= attackRange)
            {
                enemy.Value = target.Value.gameObject.GetComponentInParent<Enemy>();
                if (!enemy.Value)
                {
                    return NodeState.Failure;
                }
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}
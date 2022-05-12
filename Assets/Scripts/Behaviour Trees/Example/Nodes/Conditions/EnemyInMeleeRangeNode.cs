using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example.Conditions
{
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Conditions/Enemy in melee range")]
    public class EnemyInMeleeRangeNode : ConditionNode<Patroller>
    {
        [SerializeField] private float attackRange = 1;
        [SerializeField] private SharedTransform target;
        [SerializeField] private SharedEnemy enemy;
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
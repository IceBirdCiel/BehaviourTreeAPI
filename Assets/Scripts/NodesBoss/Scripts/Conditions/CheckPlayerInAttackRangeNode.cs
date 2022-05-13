using ESGI.BehaviourTrees.Variables;
using LeTai;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example.Conditions
{
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Conditions/Check enemy in Attack Range")]
    public class CheckPlayerInAttackRangeNode : ConditionNode<Boss>
    {
        [SerializeField] private SharedTransform target;
        [SerializeField] private float fovDistance = 2f;
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
            Gizmos.color = Color.red.WithA(0.6f);
            Gizmos.DrawSphere(Agent.transform.position, fovDistance);
        }
    }
}
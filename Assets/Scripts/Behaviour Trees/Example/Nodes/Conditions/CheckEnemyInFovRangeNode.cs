using ESGI.BehaviourTrees.Variables;
using LeTai;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example.Conditions
{
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Conditions/Check enemy in FOV")]
    public class CheckEnemyInFovRangeNode : ConditionNode<Patroller>
    {
        [SerializeField] private SharedTransform target;
        [SerializeField] private float fovDistance = 6f;
        [SerializeField] private GlobalLayerMask enemyMask;

        protected override NodeState OnUpdate()
        {
            if (target.Value)
            {
                return NodeState.Success;
            }
            
            var colliders = Physics.OverlapSphere(Agent.transform.position, fovDistance, enemyMask.Value);

            if (colliders.Length > 0)
            {
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
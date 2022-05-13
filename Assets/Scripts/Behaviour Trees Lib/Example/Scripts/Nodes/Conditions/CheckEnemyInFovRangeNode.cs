using ESGI.BehaviourTrees.Variables;
using LeTai;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// Check if the target Transform is in range. If true, update the SharedData value.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Conditions/Check enemy in FOV")]
    public class CheckEnemyInFovRangeNode : ConditionNode<Patroller>
    {
        [SerializeField] private SharedTransform target;
        [SerializeField] private float fovDistance = 6f;
        [SerializeField] private GlobalLayerMask enemyMask;

        public override void OnBeforeExecute()
        {
            base.OnBeforeExecute();
            target.ResetVariable();
        }

        protected override NodeState OnUpdate()
        {
            if (target.Value)
            {
                return NodeState.Success;
            }
            
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
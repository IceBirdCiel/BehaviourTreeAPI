using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// Make the patroller move to the target transform
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Actions/Go to target")]
    public class GoToTargetNode : ActionNode<Patroller>
    {
        [SerializeField] private SharedTransform target;

        protected override NodeState OnUpdate()
        {
            var targetT = target.Value;

            if (targetT == null)
            {
                return NodeState.Failure;
            }

            if (Vector3.Distance(Transform.position, targetT.position) > 0.01f)
            {
                var position = targetT.position;
                Transform.position = Vector3.MoveTowards(
                    Transform.position,
                    position,
                    Agent.Speed * Time.deltaTime);
                Transform.LookAt(position);
            }
            else
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }

        private Transform Transform => Agent.transform;
    }
}
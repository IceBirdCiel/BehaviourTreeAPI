using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// The player patrols around waypoints
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Actions/Patrol Node")]
    public class PatrolNode : ActionNode<Patroller>
    {
        [SerializeField]
        private float waitTime = 1f; // in seconds
        
        private int _currentWaypointIndex;
        private float _waitCounter;
        private bool _waiting;
        
        private float Speed => Agent.Speed;

        private Transform Transform => Agent.transform;

        private Transform[] Waypoints => Agent.Waypoints;

        public override void OnBeforeExecute()
        {
            _currentWaypointIndex = 0;
            _waitCounter = 0f;
            _waiting = false;
        }

        protected override NodeState OnUpdate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= waitTime)
                {
                    _waiting = false;
                }
            }
            else
            {
                var wp = Waypoints[_currentWaypointIndex];
                if (Vector3.Distance(Transform.position, wp.position) < 0.01f)
                {
                    Transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % Waypoints.Length;
                }
                else
                {
                    var position = wp.position;
                    Transform.position = Vector3.MoveTowards(
                        Transform.position,
                        position,
                        Speed * Time.deltaTime);
                    Transform.LookAt(position);
                }
            }

            return NodeState.Running;
        }
    }
}
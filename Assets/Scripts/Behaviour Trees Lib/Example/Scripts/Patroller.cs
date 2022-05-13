using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// A character that patrols around waypoints, if an enemy is close : goes attacking it before
    /// patrolling.
    /// </summary>
    public class Patroller : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Transform jumper;
        /// <summary>
        /// The speed of the Patroller
        /// </summary>
        public float Speed => speed;
        /// <summary>
        /// The waypoints of the patroller
        /// </summary>
        public Transform[] Waypoints => waypoints;
        /// <summary>
        /// The transform that jumps when attacking an enemy
        /// </summary>
        public Transform AttackTransform => jumper;
    }
}
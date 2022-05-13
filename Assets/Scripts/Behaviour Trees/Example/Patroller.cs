using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    public class Patroller : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Transform jumper;
        public float Speed => speed;
        public Transform[] Waypoints => waypoints;
        public Transform AttackTransform => jumper;
    }
}
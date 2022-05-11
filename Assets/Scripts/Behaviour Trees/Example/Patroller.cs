using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    public class Patroller : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        [SerializeField] private Transform[] waypoints;
        public float Speed => speed;
        public Transform[] Waypoints => waypoints;
    }
}
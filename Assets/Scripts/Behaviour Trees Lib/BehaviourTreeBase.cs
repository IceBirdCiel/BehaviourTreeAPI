using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// Non generic version of the BT.
    /// </summary>
    public abstract class BehaviourTreeBase : MonoBehaviour
    {
        /// <summary>
        /// Init the BT.
        /// </summary>
        protected virtual void Awake()
        {
        }
        
        /// <summary>
        /// Starts the BT
        /// </summary>
        protected virtual void Start()
        {
        }
        
        /// <summary>
        /// Updates the tree
        /// </summary>
        protected virtual void Update()
        {
            
        }
        
        /// <summary>
        /// Draw each node gizmos
        /// </summary>
        protected virtual void OnDrawGizmos()
        {

        }
    }
}
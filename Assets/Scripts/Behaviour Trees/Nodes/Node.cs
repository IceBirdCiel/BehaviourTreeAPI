using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    [InlineEditor()]
    public abstract class Node<TAgent> : NodeBase where TAgent : MonoBehaviour
    {
        [SerializeField] private List<Node<TAgent>> children;

        private const bool Debug = true;

        /// <summary>
        /// The tree owning this node
        /// </summary>
        public BehaviourTree<TAgent> Tree { get; set; }

        public TAgent Agent => Tree.Agent;

        protected Node<TAgent> Parent { get; set; }
        [ShowInInspector, ReadOnly]
        protected NodeState State { get; private set; }

        public List<Node<TAgent>> Children => children;

        /// <summary>
        /// Called by the BT
        /// </summary>
        /// <param name="behaviourTree"></param>
        public void Init(BehaviourTree<TAgent> behaviourTree)
        {
            Tree = behaviourTree;
            foreach (var child in Children)
            {
                child.Parent = this;
            }
            
            OnInit();
        }

        /// <summary>
        /// Called only once, once the behaviour tree is initialized
        /// </summary>
        protected virtual void OnInit()
        {
            
        }

        /// <summary>
        /// Called before the execution of the node, used to setup data that needs to be reset from the previous execution
        /// </summary>
        public virtual void OnBeforeExecute()
        {
            
        }
        
        public NodeState Update()
        {
            State = OnUpdate();
            PGDebug.SetCondition(Debug).SetContext(this).Message($"Update Node {name}, state is {State}").Log();
            return State;
        }

        /// <summary>
        /// Called each frame during the task execution
        /// </summary>
        /// <returns>The status of the task at the end of the frame</returns>
        protected virtual NodeState OnUpdate()
        {
            return NodeState.Success;
        }

        /// <summary>
        /// Same as <see cref="OnUpdate"/> (OnUpdate), called during physics loop.
        /// </summary>
        public virtual void OnFixedUpdate()
        {
            
        }

        /// <summary>
        /// Called at the end of the execution, on Success or Fail
        /// </summary>
        public virtual void OnExecutionEnd()
        {
            
        }

        /// <summary>
        /// Called when the behaviour tree has finished executing
        /// </summary>
        public virtual void OnBehaviourTreeExecutionEnd()
        {
            
        }

        /// <summary>
        /// Called in Editor to Draw Gizmos
        /// </summary>
        public virtual void DrawGizmos()
        {
            
        }
    }
}

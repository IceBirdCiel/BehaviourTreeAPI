using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

namespace ESGI.BehaviourTrees
{
    [InlineEditor()]
    public abstract class Node<TAgent> : NodeBase where TAgent : MonoBehaviour
    {
        [SerializeField] private List<Node<TAgent>> children;
        
        private const bool Debug = true;
        private BehaviourTree<TAgent> Tree { get; set; }

        /// <summary>
        /// The agent being controlled by this BT.
        /// </summary>
        protected TAgent Agent => Tree.Agent;
        /// <summary>
        /// The current state of the Node.
        /// </summary>
        [ShowInInspector, ReadOnly]
        public NodeState State { get; set; }
        /// <summary>
        /// The children of the Node. The order is important : Left is executed first in the BT. 
        /// </summary>
        public List<Node<TAgent>> Children => children;
        /// <summary>
        /// If the Node has children, returns true if the most right node in the
        /// children list is running.
        /// </summary>
        public bool IsRunningLastChild =>  IsLastChildRunning();
        /// <summary>
        /// If has children, returns it. Else returns the Node itself.
        /// </summary>
        public Node<TAgent> LastChild => Children.Count == 0 ? this : Children.Last();

        /// <summary>
        /// Called by the BT, once at the initialization. Sets the State to NotExecuted.
        /// </summary>
        /// <param name="behaviourTree">The tree owning this Node</param>
        public void Init(BehaviourTree<TAgent> behaviourTree)
        {
            Tree = behaviourTree;

            State = NodeState.NotExecuted;
            
            OnInit();
        }

        /// <summary>
        /// Called only once, once the behaviour tree is initialized.
        /// Override this. Can be considered as the Node constructor.
        /// </summary>
        protected virtual void OnInit()
        {
            
        }

        /// <summary>
        /// Called before the execution of the node, used to setup data that needs to be reset from
        /// the previous execution of the Node. Can be overriden.
        /// </summary>
        public virtual void OnBeforeExecute()
        {
            
        }
        
        /// <summary>
        /// Called to tick the Node. If the State is already Success or Failure does not do anything.
        /// </summary>
        /// <returns>The state of the node at the end of the tick.</returns>
        public NodeState Update()
        {
            if (State == NodeState.Success || State == NodeState.Failure)
            {
                return State;
            }
            
            State = OnUpdate();
            if (State == NodeState.Success || State == NodeState.Failure)
            {
                OnExecutionEnd();
            }
            PGDebug.SetCondition(Debug).SetContext(this).Message($"Update Node {name}, state is {State}").Log();
            return State;
        }

        /// <summary>
        /// Called each frame during the task execution. Override this to define the behaviour
        /// of the node.
        /// </summary>
        /// <returns>The status of the task at the end of the frame</returns>
        protected virtual NodeState OnUpdate()
        {
            return NodeState.Success;
        }

        /// <summary>u
        /// Called at the end of the execution : on Success or Fail.
        /// Override this to do stuff on the Node once we update the next node.
        /// </summary>
        protected virtual void OnExecutionEnd()
        {
            
        }

        /// <summary>
        /// Called in Editor to Draw Gizmos.
        /// </summary>
        public virtual void DrawGizmos()
        {
            
        }
        
        private bool IsLastChildRunning()
        {
            if (Children.Count == 0)
            {
                return true;
            }

            var firstRunning = Children.FirstOrDefault(c => c.State == NodeState.Running);
            var last = Children.Last();
            return firstRunning == last;
        }
    }
}

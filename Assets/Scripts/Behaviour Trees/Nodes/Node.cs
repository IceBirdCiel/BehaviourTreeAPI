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
        public override List<NodeBase> _children => children.Select(x => x as NodeBase).ToList();
        public override NodeState states => State;
        /// <summary>
        /// The tree owning this node
        /// </summary>
        public BehaviourTree<TAgent> Tree { get; set; }

        [ShowInInspector]
        public TAgent Agent => Tree.Agent;

        protected Node<TAgent> Parent { get; set; }
        [ShowInInspector, ReadOnly]
        public NodeState State { get; set; }

        public List<Node<TAgent>> Children => children;
        public bool IsRunningLastChild =>  IsLastChildRunning();
        public Node<TAgent> LastChild => Children.Count == 0 ? this : Children.Last();

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

            State = NodeState.NotExecuted;
            
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
            if (State == NodeState.Success || State == NodeState.Failure)
            {
                return State;
            }
            
            State = OnUpdate();
            if (State == NodeState.Success || State == NodeState.Failure)
            {
                OnExecutionEnd();
            }
            PGDebug.SetCondition(Debug).SetContext(this).Message($"Update Node {name}, name {Tree.name}, state is {State}").Log();
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

        /// <summary>u
        /// Called at the end of the execution, on Success or Fail
        /// </summary>
        protected virtual void OnExecutionEnd()
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

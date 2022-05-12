using System;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public abstract class BehaviourTree<TAgent> : BehaviourTreeBase where TAgent : MonoBehaviour
    {
        [SerializeField] private Node<TAgent> startNode;
        [SerializeField] private TAgent agent; 
        
        private List<Node<TAgent>> _nodes;
        override public NodeBase root => startNode as NodeBase;
        public override NodeBase CurrentNode { get; }

        [ShowInInspector, ReadOnly]
        private NodeState State { get; set; }

        protected void Awake()
        {
            if (Agent == null)
            {
                agent = GetComponentInChildren<TAgent>();
            }

            if (Agent == null)
            {
                PGDebug.Message($"No agent found in {name} !!").LogError();
            }
        }

        protected void Start()
        {
            ActivateBehaviourTree();
        }

        protected void OnDisable()
        {
            DeactivateBehaviourTree();
        }

        protected void OnEnable()
        {
            ActivateBehaviourTree();
        }

        protected void OnDestroy()
        {
            DeactivateBehaviourTree();
        }

        protected void Update()
        {
            if (startNode)
            {
                startNode.Update();
            }
        }

        protected void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (Application.isPlaying && _nodes != null)
            {
                foreach (var node in _nodes)
                {
                    node.DrawGizmos();
                }
            }
#endif
        }

        private void DeactivateBehaviourTree()
        {
        }

        private void ActivateBehaviourTree()
        {
            if (Activated)
            {
                return;
            }

            _nodes = new List<Node<TAgent>>();
            InitNodes(startNode);
            State = NodeState.Running;
        }

        private void InitNodes(Node<TAgent> root)
        {
            root.Init(this);
            _nodes.Add(root);
            foreach (var child in root.Children)
            {
                InitNodes(child);
            }
        }

        public bool Activated => State == NodeState.Running;

        public TAgent Agent => agent;
    }
}
using DG.Tweening;
using PGSauce.Core.Extensions;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Actions/Attack Enemy")]
    public class AttackEnemyNode : ActionNode<Patroller>
    {
        [SerializeField] private SharedEnemy enemy;
        [SerializeField] private Ease jumpEase;
        private bool _init;

        protected override NodeState OnUpdate()
        {
            if (!enemy.Value)
            {
                return NodeState.Success;
            }

            var state = NodeState.Running;

            if (!_init)
            {
                _init = true;
                Agent.transform.DOMoveY(2, .75f / 2).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase).OnComplete(() =>
                {
                    Destroy(enemy.Value);
                    _init = false;
                    state = NodeState.Success;
                });
            }

            return state;
        }
    }
}
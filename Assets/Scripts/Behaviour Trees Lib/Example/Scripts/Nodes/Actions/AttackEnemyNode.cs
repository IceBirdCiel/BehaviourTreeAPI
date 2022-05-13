using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using PGSauce.Core.Extensions;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// Action Node to attack en enemy
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Actions/Attack Enemy")]
    public class AttackEnemyNode : ActionNode<Patroller>
    {
        [SerializeField] private SharedEnemy enemy;
        [SerializeField] private Ease jumpEase;
        private bool _init;
        private Tween _attack;

        protected override void OnInit()
        {
            base.OnInit();
            _init = false;
        }

        public override void OnBeforeExecute()
        {
            base.OnBeforeExecute();
            _init = false;
        }

        protected override NodeState OnUpdate()
        {
            if (!enemy.Value)
            {
                return NodeState.Success;
            }

            if (_attack == null)
            {
                _attack = Agent.AttackTransform.DOLocalMoveY(2, .75f / 2).SetLoops(2, LoopType.Yoyo).SetEase(jumpEase).OnComplete(() =>
                {
                    Destroy(enemy.Value.gameObject);
                    _attack = null;
                });
            }

            const NodeState state = NodeState.Running;
            return state;
        }
    }
}
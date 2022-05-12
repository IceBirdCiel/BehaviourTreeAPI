using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private BehaviourTreeBase tree;
        // Start is called before the first frame update
        void Start()
        {
            if(tree.root is ConditionNodeBase)
            {

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
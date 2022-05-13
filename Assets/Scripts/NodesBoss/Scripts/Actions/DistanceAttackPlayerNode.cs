using ESGI.BehaviourTrees;
using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Actions/Distance Attack Enemy")]
public class DistanceAttackPlayerNode : ActionNode<Boss>
{

    [SerializeField] private SharedTransform target;
    protected override NodeState OnUpdate()
    {
        Agent.animator.SetBool("Attack2", true);
        GameObject massue = Instantiate(Agent.massue);
        massue.transform.position = Agent.transform.position;
        Rigidbody rb = massue.GetComponent<Rigidbody>();
        rb.AddForce(target.Value.position - massue.transform.position);
        return NodeState.Success;
    }
}

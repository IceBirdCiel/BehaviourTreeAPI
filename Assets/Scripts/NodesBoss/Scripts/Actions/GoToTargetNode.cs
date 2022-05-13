using ESGI.BehaviourTrees;
using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Actions/Go to target")]
public class GoToTargetNode : ActionNode<Boss>
{
    [SerializeField] private SharedTransform target;
    private float maxDist = 15;
    private float minDist = 2;
    private Transform Transform => Agent.transform;
    protected override NodeState OnUpdate()
    {
        var vtarget = target.Value;
        if (vtarget == null)
        {
            return NodeState.Failure;
        }
        float dist = Vector3.Distance(Transform.position, vtarget.position);
        if (dist < minDist)
        {
            return NodeState.Success;
        }
        if (dist < maxDist)
        {

            Transform.LookAt(new Vector3(vtarget.position.x, 0, vtarget.transform.position.z));
            Transform.position = Vector3.MoveTowards(
                    Transform.position,
                    vtarget.position,
                    Agent.speed * Time.deltaTime);
            
        }
        return NodeState.Running;
    }
}

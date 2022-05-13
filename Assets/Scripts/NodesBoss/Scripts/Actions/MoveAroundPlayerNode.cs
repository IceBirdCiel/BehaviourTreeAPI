using ESGI.BehaviourTrees;
using ESGI.BehaviourTrees.Variables;
using PGSauce.Core.Strings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = MenuPaths.Nodes + "Examples/Boss/Actions/Move Around Player")]
public class MoveAroundPlayerNode : ActionNode<Boss>
{
    private Transform target => Agent.player.transform;
    private Transform Transform => Agent.transform;
    private float timer = 0;
    protected override NodeState OnUpdate()
    {
        
        timer += Time.deltaTime;
        MoveAroundPlayerLeft(target);
        if (timer < 5)
        {
            return NodeState.Running;
        }
        else if(timer < 10)
        {
            MoveAroundPlayerRight(target);
            return NodeState.Running;
        }
        else
        {
            timer = 0;
        }
        return NodeState.Success;
    }

    void MoveAroundPlayerRight(Transform vtarget)
    {
        var q = Transform.rotation;
        Transform.RotateAround(vtarget.transform.position, Vector3.up, 20 * Time.deltaTime);
        Transform.rotation = q;
    }

    void MoveAroundPlayerLeft(Transform vtarget)
    {
        var q = Transform.rotation;
        Transform.RotateAround(vtarget.transform.position, Vector3.up, -20 * Time.deltaTime);
        Transform.rotation = q;
    }
}

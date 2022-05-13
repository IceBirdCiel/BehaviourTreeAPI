using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayer : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boss" && player.isAttacking)
        {
            other.GetComponent<Boss>().GetAttacked(10);
            player.StopAttack();
        }
    }
}

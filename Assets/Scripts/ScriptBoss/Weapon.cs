using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Boss boss;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && boss.isAttacking)
        {
            other.GetComponent<PlayerController>().GetAttacked(10);
            boss.StopAttack();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    private Boss boss;
    private bool firstAttack = true;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && firstAttack)
        {
            other.GetComponent<PlayerController>().GetAttacked(10);
            firstAttack = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss")
        {
            other.GetComponent<BossScript>().GetAttacked(10);
        }
    }
}

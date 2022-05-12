using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    private PlayerController player;
    private Transform transform;
    private Animator animator;
    public float speed = 2;
    private float Health = 100;
    private float MaxHealth = 100;
    private Slider slider;
    private float minDist = 3;
    private float maxDist = 10;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            LookAtPlayer();
            UpdateHealth();
            if (IsPlayerInRange())
            {
                MoveTowardPlayer();
                AttackPlayer();
            }
            else
                MoveAroundPlayer();
        }

    }
    private void UpdateHealth()
    {
        slider.value = Health / MaxHealth;
        slider.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));

    }

    public void GetAttacked(float value)
    {
        Health -= value;
        if(Health <= 0)
        {
            animator.Play("Dead");
            isDead = true;

        }
    }

    void MoveAroundPlayer()
    {
        var q = transform.rotation;
        transform.RotateAround(player.transform.position, Vector3.forward, 50 * Time.deltaTime);
        transform.rotation = q;
    }

    void AttackPlayer()
    {
        animator.SetBool("Attack", true);
    }
    void Patroll()
    {
        animator.SetBool("Attack", false);
        MoveBackwardPlayer();
    }
    bool IsPlayerInRange()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if ( dist < maxDist && dist > minDist)
            return true;
        else return false;
    }
    void LookAtPlayer()
    {
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
    }
    void MoveTowardPlayer()
    {
        
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void MoveBackwardPlayer()
    {
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        transform.position -= transform.forward * Time.deltaTime * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public PlayerController player;
    private Transform transform;
    public Animator animator;
    public float speed = 2;
    private float Health = 100;
    private float MaxHealth = 100;
    private Slider slider;
    private float minDist = 2;
    private float minDistAttack = 1;
    private float maxDist = 10;
    private float maxDistAttack = 4;
    public bool isDead = false;
    public bool isAttacking = false;
    public GameObject massue;
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
        UpdateHealth();
        if (!isDead)
        {
            LookAtPlayer();
            
            
            /*if (IsPlayerInVisionRange())
            {
                if (isPlayerInAttackRange())
                {
                    AttackPlayer();
                }
                else
                {
                    MoveTowardPlayer();
                }
            }
            else
            {
                MoveAroundPlayerLeft();

            }*/
        }

    }

    public void StartAttack()
    {
        isAttacking = true;
        
    }

    public void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Attack2", false);
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

    void MoveAroundPlayerRight()
    {
        var q = transform.rotation;
        transform.RotateAround(player.transform.position, Vector3.up, 20 * Time.deltaTime);
        transform.rotation = q;
    }

    void MoveAroundPlayerLeft()
    {
        var q = transform.rotation;
        transform.RotateAround(player.transform.position, Vector3.up, -20 * Time.deltaTime);
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
    bool isPlayerInAttackRange()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < maxDistAttack && dist > minDistAttack)
            return true;
        else return false;
    }
    bool IsPlayerInVisionRange()
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

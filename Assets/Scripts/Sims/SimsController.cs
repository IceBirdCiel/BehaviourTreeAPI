using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimsController : MonoBehaviour
{

    [Header("Sims")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0f, 240f)]
    public float RotationSmoothTime = 240f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    //private Queue<Action> actionQueue;
    //public Action currentAction = null;

    private Animator _animator;
    private bool _hasAnimator;

    public NavMeshAgent m_Agent;

    private float _animationBlend;

    public GameManager gameManager;
    public Besoins besoins;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    bool isWalking = false;

    RaycastHit hitInfo = new RaycastHit();

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        besoins = this.gameObject.GetComponent<Besoins>();
        m_Agent = GetComponent<NavMeshAgent>();
        //actionQueue = new Queue<Action>();

        m_Agent.speed = MoveSpeed;
        m_Agent.angularSpeed = RotationSmoothTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_Dest_Index = 0;
        //m_Agent.destination = dest.ToArray()[m_Dest_Index].position;

        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                //actionQueue.Enqueue(new WalkTo(this.gameObject, hitInfo.point));
            }  
        }

        if (isWalking)
        {
            float targetSpeed = false ? SprintSpeed : MoveSpeed;

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
        }

        //if (currentAction == null)
        //{
        //    if (actionQueue.Count > 0)
        //    {
        //        currentAction = actionQueue.Dequeue();
        //        currentAction.Start();
        //    }
        //}
        //else
        //{
        //    currentAction.Update();
        //    if (currentAction.IsFinished()) currentAction = null;
        //}




    //if(Vector3.Distance(transform.position, m_Agent.destination) < 1)
    //{
    //    m_Dest_Index++;
    //    if (m_Dest_Index == 5)
    //        m_Dest_Index = 0;
    //    m_Agent.destination = dest.ToArray()[m_Dest_Index].position;
    //}

    _hasAnimator = TryGetComponent(out _animator);

        

        float inputMagnitude = 1f;

        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }

        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    public void walk()
    {
        isWalking = true;
        
    }

    public void idle()
    {
        isWalking = false;
        _animationBlend = 0;
    }

    public Vector3 getRandomPos()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 18;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 18, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }

    
    

}

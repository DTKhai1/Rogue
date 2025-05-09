using UnityEngine;

public class Slime : MonoBehaviour, IUpdateObserver, IFixedUpdateObserver
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    [SerializeField]
    private Vector2 jumpDir;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public DetectionZone playerDetectionZone;

    private Transform player;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Enemy damageable;

    public enum WalkableDirection { Right, Left }
    [SerializeField]
    private WalkableDirection _walkDirection;

    [SerializeField]
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //Direction flipped
                _walkDirection = value;
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
        }
    }
    public bool _notNearCliff;
    public bool notNextToCliff
    {
        get { return _notNearCliff; }
        private set
        {
            _notNearCliff = value;
            animator.SetBool(AnimationStrings.nextToCliff, !value);
        }
    }

    public bool _foundPlayer = false;
    public bool FoundPlayer
    {
        get { return _foundPlayer; }
        private set
        {
            _foundPlayer = value;
            animator.SetBool(AnimationStrings.foundPlayer, value);
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        UpdateManager.RegisterUpdateObserver(this);
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterUpdateObserver(this);
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }

    public void ObservedUpdate()
    {
        notNextToCliff = cliffDetectionZone.detectedColliders.Count > 0;
        FoundPlayer = playerDetectionZone.detectedColliders.Count > 0;
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
    public void ObservedFixedUpdate()
    {
        if(touchingDirections.IsGrounded)
        if (!damageable.LockVelocity)
        {
            if (FoundPlayer)
            {
                if ((player.transform.position.x > this.transform.position.x && WalkDirection == WalkableDirection.Left) ||
                            (player.transform.position.x < this.transform.position.x && WalkDirection == WalkableDirection.Right))
                {
                    FlipDirection();
                }
            }
            else
            {
                if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
                {
                    FlipDirection();
                }
                if (touchingDirections.IsGrounded && !_notNearCliff)
                {
                    FlipDirection();
                }
            }
            if (CanMove)
            {
                if (FoundPlayer)
                {
                    Vector2 newPos = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), moveSpeed * Time.deltaTime);
                    rb.MovePosition(newPos);
                }
                else
                {
                    rb.velocity = new Vector2(walkDirectionVector.x * moveSpeed, rb.velocity.y);
                }
            }else rb.velocity = new Vector2(0, rb.velocity.y);
        }else rb.velocity = new Vector2(0, rb.velocity.y);

    }
    
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void Jump()
    {
        jumpDir = (player.position - this.transform.position).normalized;
        jumpDir *= jumpForce;
        rb.AddForce(new Vector2(Mathf.Clamp(jumpDir.x, -5f, 5f), Mathf.Clamp(jumpDir.y, 3, 7.5f)), ForceMode2D.Impulse);
    }
}

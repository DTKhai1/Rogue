
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

public class PlayerController : MonoBehaviour, IFixedUpdateObserver
{
    public float walkSpeed = 5f;
    public float dashSpeed = 20f;
    public float airWalkSpeed = 2f;

    [SerializeField]
    private float jumpForce = 8f;
    private int jumpCount;

    [SerializeField]
    private float dashCooldown = 0.5f;
    [SerializeField]
    private float dashingTime = 0.2f;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Player damageable;

    [SerializeField] private LayerMask Interactable;
    GameManager gameManager;
    public float currentMoveSpeed 
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                            return walkSpeed;
                }
                else
                {
                    return 0;
                }
            }else return 0;
            
        }
    }

    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }


    public bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; } private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove {  get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } 
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    

    public Rigidbody2D rb;
    Animator animator;

    public bool canDash = true;
    private bool _isDashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    public void ObservedFixedUpdate()
    {
        if (_isDashing)
            return;
        if (touchingDirections.IsGrounded) jumpCount = 2;
        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight )
        {
            //Face right
            IsFacingRight = true;
        }else if(moveInput.x < 0 && IsFacingRight )
        {
            //Face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        animator.SetTrigger(AnimationStrings.isDashing);
        gameManager.audioManager.PlaySFX(gameManager.audioManager.dash);
        float originalGravity = rb.gravityScale;
        _isDashing = true;
        damageable.isInvisible = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        yield return new WaitForSeconds(dashingTime);
        damageable.isInvisible = false;
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        _isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && jumpCount > 0 && CanMove)
        {
            gameManager.audioManager.PlaySFX(gameManager.audioManager.jump);
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            jumpCount--;
            rb.velocity = new Vector2(0, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gameManager.audioManager.PlaySFX(gameManager.audioManager.attack);
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.hit);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, Vector2.zero ,0, Interactable);
            if(hit == true)
            {
                if(hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gameManager.ChangeState(GameState.Pause);
        }
    }

}

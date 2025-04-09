using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirections : MonoBehaviour, IFixedUpdateObserver
{
    public ContactFilter2D groundCastFilter, wallCastFilter;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.05f;

    Collider2D touchingCol;
    Rigidbody2D rb;
    
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded { get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool isOnPlatform;

    [SerializeField]
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    // Update is called once per frame
    public void ObservedFixedUpdate()
    {
        if(Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            IsGrounded = touchingCol.Cast(Vector2.down, groundCastFilter, groundHits, groundDistance) > 0;
        }else IsGrounded = false;
        IsOnWall = touchingCol.Cast(wallCheckDirection, wallCastFilter, wallHits, wallDistance) > 0;
    }

    
}

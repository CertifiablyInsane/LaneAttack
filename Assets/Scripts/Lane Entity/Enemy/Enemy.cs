using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : LaneEntity
{
    [Header("Enemy Data")]
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected LayerMask playerTeamMask;
    public int moneyOnKill;

    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected AnimatorListener animatorListener;
    // Events
    public delegate void EnemyEvent(Enemy enemy);
    public static event EnemyEvent OnEnemyKilled;

    // Vars
    protected bool _isAttacking = false;
    protected LaneEntity _target;

    protected new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        AssignAnimationIDs();
        AssignAnimationEvents();
    }
    protected LaneEntity CheckForTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackDistance, playerTeamMask);
        if (hit.collider != null && hit.collider.TryGetComponent(out LaneEntity target))
        {
            return target;
        }
        return null;
    }
    protected override void Kill()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
    protected abstract void Move();
    protected abstract IEnumerator Attack();
    protected abstract void AssignAnimationIDs();
    protected abstract void AssignAnimationEvents();
}

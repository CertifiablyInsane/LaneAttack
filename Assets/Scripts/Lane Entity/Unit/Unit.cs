using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Unit : LaneEntity
{
    [Header("Unit Data")]
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected LayerMask enemyTeamMask;

    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected AnimatorListener animatorListener;
    // Events
    public delegate void UnitEvent(Unit unit);
    public static event UnitEvent OnUnitKilled;

    // Vars
    protected bool _isAttacking = false;
    protected LaneEntity _target;

    protected new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }
    protected LaneEntity CheckForTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackDistance, enemyTeamMask);
        if (hit.collider != null && hit.collider.TryGetComponent(out LaneEntity target))
        {
            return target;
        }
        return null;
    }
    protected override void Kill()
    {
        OnUnitKilled?.Invoke(this);
        Destroy(gameObject);
    }
    protected abstract void Move();
}

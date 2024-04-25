using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : LaneEntity
{
    protected LayerMask targetMask;
    protected int attackDamage = 0;
    protected float maxTravelDistance;
    protected LaneEntity _target;

    private Vector2 prevPos;
    private float _travelledDistance = 0;
    protected new void Start()
    {
        base.Start();
        prevPos = transform.position;
    }
    void Update()
    {
        if (GameManager.gamePaused) return;
        _target = CheckForTarget();
        if( _target != null)
        {
            _target.Damage(attackDamage);
            Kill();
        }
        Move();

        if(_travelledDistance > maxTravelDistance)
            Kill();
    }
    public override void Stop()
    {
        rb.velocity = Vector2.zero;
    }
    protected void Move()
    {
        float targetSpeed = -speed;
        // Negative speed as we're moving left
        _speed = LerpSpeed(targetSpeed, _speed);
        rb.velocity = new(_speed, 0);
    
        float distance = Vector2.Distance(prevPos, transform.position);
        _travelledDistance += distance;
        prevPos = transform.position;
    }
    protected override void Kill()
    {
        Destroy(gameObject);
    }
    protected LaneEntity CheckForTarget()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(.25f, .25f), 0, targetMask);
        if (hit != null && hit.TryGetComponent(out LaneEntity target))
        {
            return target;
        }
        return null;
    }
    public void SetData(int damage, float speed, float maxTravelDistance, LayerMask targetMask)
    {
        attackDamage = damage;
        this.speed = speed;
        this.maxTravelDistance = maxTravelDistance;
        this.targetMask = targetMask;
    }
}

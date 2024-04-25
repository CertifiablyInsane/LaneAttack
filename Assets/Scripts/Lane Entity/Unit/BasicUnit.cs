using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicUnit : Unit
{
    private int _animAttacking_B;

    private Coroutine _attackCoroutine;
    private bool _damageFramePassed;
    protected new void Start()
    {
        base.Start();
        AssignAnimationIDs();
        AssignAnimationEvents();
    }
    void Update()
    {
        if (GameManager.gamePaused) return;
        _target = CheckForTarget();
        if (!_isAttacking && _target != null)
        {
            _isAttacking = true;
            animator.SetBool(_animAttacking_B, true);
            _attackCoroutine = StartCoroutine(Attack());
        }
        else if (_isAttacking && _target == null) 
        { 
            _isAttacking = false;
            animator.SetBool(_animAttacking_B, false);
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
        }
        Move();
    }
    public override void Stop()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool(_animAttacking_B, false);
    }
    protected override void Move()
    {
        float targetSpeed = _isAttacking ? 0 : speed;
        // Negative speed as we're moving left
        _speed = LerpSpeed(targetSpeed, _speed);
        rb.velocity = new(_speed, 0);
    }
    protected IEnumerator Attack()
    {
        while (_isAttacking)
        {
            _damageFramePassed = false;
            yield return new WaitUntil(() => _damageFramePassed);
            _target.Damage(attackDamage);
            yield return null;
        }
    }
    private void AssignAnimationIDs()
    {
        _animAttacking_B = Animator.StringToHash("Attacking");
    }
    private void AssignAnimationEvents()
    {
        animatorListener.OnEvent01 += Anim_DamageFramePassed;
    }
    private void Anim_DamageFramePassed()
    {
        _damageFramePassed = true;
    }
}

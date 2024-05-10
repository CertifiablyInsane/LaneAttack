using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodger : Enemy
{
    [SerializeField][Range(0f, 1f)] private float dodgeChance;
    private int _animAttacking_B;

    private Coroutine _attackCoroutine;
    private bool _damageFramePassed;
    private new void Start()
    {
        base.Start();
    }
    void Update()
    {
        if (GameManager.gamePaused) return;
        _target = CheckForTarget();
        if (!_isAttacking && _target != null)
        {
            // Before we get straight to attacking, try to dodge first
            if (!TryDodge())
            {
                // We didn't dodge, so attack instead
                _isAttacking = true;
                animator.SetBool(_animAttacking_B, true);
                _attackCoroutine = StartCoroutine(Attack());
            }
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
    private bool TryDodge()
    {
        if(Random.Range(0f, 1f) <= dodgeChance)
        {
            // Dodge
            bool dodgeUp = false;
            switch(lane)
            {
                case Lane.BOT:
                    dodgeUp = true; break;
                case Lane.TOP:
                    dodgeUp = false; break;
                case Lane.MID:
                    dodgeUp = Random.Range(0, 2) == 0; break;
            }
            ChangeLane(dodgeUp);
            return true;
        }
        // Don't dodge
        return false; 
    }
    public override void Stop()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool(_animAttacking_B, false);
    }
    protected override void Move()
    {
        float targetSpeed = _isAttacking ? 0 : -speed;
        // Negative speed as we're moving left
        _speed = LerpSpeed(targetSpeed, _speed);
        rb.velocity = new(_speed, 0);
    }
    protected override IEnumerator Attack()
    {
        while (_isAttacking)
        {
            _damageFramePassed = false;
            yield return new WaitUntil(() => _damageFramePassed);
            _target.Damage(attackDamage);
            yield return null;
        }
    }
    protected override void AssignAnimationIDs()
    {
        _animAttacking_B = Animator.StringToHash("Attacking");
    }
    protected override void AssignAnimationEvents()
    {
        animatorListener.OnEvent01 += Anim_DamageFramePassed;
    }
    private void Anim_DamageFramePassed()
    {
        _damageFramePassed = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy, IShootsProjectile
{
    private int _animAttacking_B;
    private bool _spawnProjectileFramePassed;

    private Coroutine _attackCoroutine;

    [Header("IShootsProjectile")]
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileTravelDistance;
    public Projectile Projectile { get { return _projectile; } }
    public Transform ProjectileSpawnPoint { get { return _projectileSpawnPoint; } }
    public int ProjectileDamage { get { return _projectileDamage; } }
    public float ProjectileSpeed { get { return _projectileSpeed; } }
    public float ProjectileTravelDistance { get { return _projectileTravelDistance; } }

    protected new void Start()
    {
        base.Start();
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
        float targetSpeed = _isAttacking ? 0 : -speed;
        // Negative speed as we're moving left
        _speed = LerpSpeed(targetSpeed, _speed);
        rb.velocity = new(_speed, 0);
    }
    protected override IEnumerator Attack()
    {
        while (_isAttacking)
        {
            _spawnProjectileFramePassed = false;
            yield return new WaitUntil(() => _spawnProjectileFramePassed);
            ShootProjectile();
            yield return null;
        }
    }
    protected override void AssignAnimationIDs()
    {
        _animAttacking_B = Animator.StringToHash("Attacking");
    }
    protected override void AssignAnimationEvents()
    {
        animatorListener.OnEvent01 += Anim_SpawnProjectileFramePassed;
    }
    private void Anim_SpawnProjectileFramePassed()
    {
        _spawnProjectileFramePassed = true;
    }

    public void ShootProjectile()
    {
        Projectile p = Instantiate(Projectile, _projectileSpawnPoint.position, Quaternion.identity);
        p.SetData(ProjectileDamage, ProjectileSpeed, ProjectileTravelDistance, playerTeamMask);
    }
}

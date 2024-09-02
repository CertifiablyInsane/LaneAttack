using System.Collections;
using UnityEngine;

public class Player : LaneEntity, IUpgradable
{
    [Header("Player Data")]
    [SerializeField] private float attackDistance;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask enemyTeamMask;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorListener animatorListener;

    private InputManager inputManager;

    // Events
    public delegate void PlayerEvent(Player player);
    public static event PlayerEvent OnPlayerKilled;
    // Anim
    private int _animAttacking_B;

    // Vars
    private bool _isAttacking;
    private bool _damageFramePassed;
    private LaneEntity _target;
    private Coroutine _attackCoroutine;

    protected new void Start()
    {
        base.Start();
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        SetLane(Lane.MID);

        SetStats(GameManager.SaveInfo.robotPlayerLevel);
        AssignAnimationIDs();
        AssignAnimationEvents();
    }
    private void OnEnable()
    {
        SwipeDetection.OnSwipe += HandleSwipe;
    }
    private void OnDisable()
    {
        SwipeDetection.OnSwipe -= HandleSwipe;
    }

    private void Update()
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
    private void HandleSwipe(Vector2 dir)
    {
        if (dir.y == 1) ChangeLane(true);
        else if (dir.y == -1) ChangeLane(false);
    }
    private void Move()
    {
        float targetSpeed = inputManager.holdingLeft ? -speed : 0;
        targetSpeed += inputManager.holdingRight ? speed : 0;
        _speed = LerpSpeed(targetSpeed, _speed);
        rb.velocity = new Vector2(_speed, 0);
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

    private LaneEntity CheckForTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackDistance, enemyTeamMask);
        if (hit.collider != null && hit.collider.TryGetComponent(out LaneEntity target))
        {
            return target;
        }
        return null;
    }
    public override void Stop()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool(_animAttacking_B, false);
    }
    protected override void Kill()
    {
        OnPlayerKilled?.Invoke(this);
        gameObject.SetActive(false);
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

    public void SetStats(int level)
    {
        Upgrade stats = UpgradeData.RobotPlayerUpgrades[level];

        Debug.Log("Setting Player to Level " + stats.level);

        maxHealth = (int)stats.health;
        _health = maxHealth;
        speed = stats.speed;
        attackDamage = (int)stats.damage;
        GetComponent<AutoHeal>().SetStats(stats.healDelay, stats.healInterval);
    }
}

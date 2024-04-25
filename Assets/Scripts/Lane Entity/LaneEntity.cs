using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class LaneEntity : MonoBehaviour
{
    [Header("Lane Entity Data")]
    [SerializeField] protected float speed = 2.0f;
    [SerializeField] protected float acceleration = 10.0f;
    [SerializeField] protected int maxHealth = 10;

    [Header("Components")]
    [SerializeField] private GameObject spriteRoot;
    [SerializeField] private HealthBar healthBar;

    // LOCAL (INNER-ENTITY) EVENTS
    public delegate void EntityEvent(float f);
    public EntityEvent OnHurt;

    protected Rigidbody2D rb;

    public Lane lane {  get; private set; }
    protected float _speed;
    protected int _health;

    protected void Start()
    {
        _health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    /**
     *  Damages the entity by damage. Kills if 0 HP
     */
    public void Damage(int damage)
    {
        _health -= damage;
        healthBar.SetValue(_health, maxHealth);
        if(_health <= 0)
        {
            _health = 0;
            Kill();
        }
        OnHurt?.Invoke(damage);
    }
    /**
     *  Heals the entity by heal. Tops off at max HP
     */
    public void Heal(int heal)
    {
        _health += heal;
        if (_health > maxHealth)
            _health = maxHealth;
        healthBar.SetValue(_health, maxHealth);
    }
    /**
     *  Moves the entity one lane up or down
     */
    protected void ChangeLane(bool up)
    {
        if (up && lane == Lane.TOP) return;
        if (!up && lane == Lane.BOT) return;
        lane += up ? 1 : -1;
        SetPositionAfterLaneChange();
    }
    /**
     *  Snaps the entity to the specified lane
     */
    public void SetLane(Lane lane)
    {
        this.lane = lane;
        SetPositionAfterLaneChange();
    }
    /**
     *  Adjusts the world position and sorting layer of the enemy after a lane change
     */
    private void SetPositionAfterLaneChange()
    {
        transform.position = new(transform.position.x, LevelManager.Instance.lanePosition[(int)lane], 0);
        SpriteRenderer[] renderers = spriteRoot.GetComponentsInChildren<SpriteRenderer>();
        string newLayer = Enum.LaneToString(lane);
        healthBar.GetComponent<Canvas>().sortingLayerName = newLayer;
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingLayerName = newLayer;
        }
    }
    /**
     *  A Lerp function for speed
     */
    protected float LerpSpeed(float targetSpeed, float currentSpeed)
    {
        float s = currentSpeed;
        if (Mathf.Abs(targetSpeed - s) > 0.1f)
        {
            s = Mathf.Lerp(s, targetSpeed, Time.deltaTime * acceleration);
            s = Mathf.Clamp(s, -speed, speed);
            return Mathf.Round(s * 1000f) / 1000f;
        }
        // Else
        return targetSpeed;
    }
    public abstract void Stop();
    protected abstract void Kill();
}

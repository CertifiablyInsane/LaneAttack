using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaneEntity))]
/**
 *  This attaches itself to a LaneEntity and heals it after some time outside combat
 */
public class AutoHeal : MonoBehaviour
{
    [SerializeField] private float healDelay;
    [SerializeField] private int healthPerTick;
    [SerializeField] private float tickDelaySeconds;

    private LaneEntity entity;

    private float _delayTimer = 0f;
    private bool _doingLoop = false;

    private void OnEnable()
    {
        entity = GetComponent<LaneEntity>();
        entity.OnHurt += OnEntityHurt;
    }
    private void OnDisable()
    {
        entity.OnHurt -= OnEntityHurt;
    }
    private void Update()
    {
        if (!_doingLoop)
        {
            if(_delayTimer > healDelay)
            {
                _doingLoop = true;
                StartCoroutine(HealLoop());
            }
            else
            {
                _delayTimer += Time.deltaTime;
            }
        }
    }
    private void OnEntityHurt(float amount)
    {
        _delayTimer = 0f; 
        _doingLoop = false;
    }

    private IEnumerator HealLoop()
    {
        while (_doingLoop)
        {
            entity.Heal(healthPerTick);
            yield return new WaitForSeconds(tickDelaySeconds);
        }
    }

    public void SetStats(float healDelay, float tickDelaySeconds)
    {
        this.healDelay = healDelay;
        this.tickDelaySeconds = tickDelaySeconds;
    }
}

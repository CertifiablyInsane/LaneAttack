using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : AdvancedButton
{
    public UnitData data { get; private set; }
    [SerializeField] private Image icon;
    [SerializeField] private Slider cooldownOverlay;
    private bool _onCooldown = false;
    private bool _canAfford = false;
    private float _cooldownTimer = 0f;
    protected new void Start()
    {
        base.Start();
        AddOnClick(OnPressed);
    }
    public void SetData(UnitData data)
    {
        this.data = data;
        icon.sprite = data.icon;
        label.text = data.cost.ToString();
    }
    private void OnEnable()
    {
        LevelManager.OnMoneyChanged += OnMoneyChanged;
    }
    private void OnDisable()
    {
        LevelManager.OnMoneyChanged -= OnMoneyChanged;
    }
    private void OnMoneyChanged(int amount)
    {
        if (enabled && amount < data.cost)
        {
            _canAfford = false;
            Disable();
        }
        else if (!enabled && amount >= data.cost)
        {
            _canAfford = true;
            TryEnable();
        }
    }
    private void TryEnable()
    {
        if (!enabled && !_onCooldown && _canAfford)
            Enable();
    }
    private void OnPressed(AdvancedButton _)
    {
        LevelManager.Instance.SpawnUnit(data);
        StartCoroutine(C_Recharge());
    }
    private IEnumerator C_Recharge()
    {
        Disable();
        _onCooldown = true;
        _cooldownTimer = 0f;
        cooldownOverlay.value = 1.0f;   // Fill completely
        while(_cooldownTimer < data.cooldownTime)
        {
            _cooldownTimer += Time.deltaTime;
            cooldownOverlay.value = Mathf.Lerp(1f, 0f, _cooldownTimer / data.cooldownTime);
            yield return null;
        }
        _onCooldown = false;
        cooldownOverlay.value = 0f;
        TryEnable();
    }
}

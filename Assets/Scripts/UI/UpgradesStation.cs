using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesStation : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] private AdvancedButton backButton;
    [SerializeField] private TextMeshProUGUI pointsDisplay;

    [Header("Unit Selection Buttons - Robot")]
    [SerializeField] private AdvancedButton robotBasicUnitButton;
    [SerializeField] private AdvancedButton robotSwarmUnitButton;
    [SerializeField] private AdvancedButton robotRangedUnitButton;
    [SerializeField] private AdvancedButton robotKnockbackUnitButton;
    [SerializeField] private AdvancedButton robotSupportUnitButton;

    [Header("Unit Selection Buttons - Martian")]
    [SerializeField] private AdvancedButton martianBasicUnitButton;
    [SerializeField] private AdvancedButton martianHeavyUnitButton;
    [SerializeField] private AdvancedButton martianRangedUnitButton;
    [SerializeField] private AdvancedButton martianDodgerUnitButton;
    [SerializeField] private AdvancedButton martianSupportUnitButton;

    private void Start()
    {
        backButton.AddOnClick(BackPressed);
        OnPointsChanged(GameManager.SaveInfo.points);
        HideLockedButtons();
    }
    private void OnEnable()
    {
        GameManager.OnPointsChanged += OnPointsChanged;
    }
    private void OnDisable()
    {
        GameManager.OnPointsChanged -= OnPointsChanged;
    }
    public void HideLockedButtons()
    {
        // Robot Buttons
        if (robotBasicUnitButton != null && !GameManager.SaveInfo.robotBasicUnitUnlocked)
            robotBasicUnitButton.gameObject.SetActive(false);
        if (robotSwarmUnitButton != null && !GameManager.SaveInfo.robotSwarmUnitUnlocked)
            robotSwarmUnitButton.gameObject.SetActive(false);
        if (robotRangedUnitButton != null && !GameManager.SaveInfo.robotRangedUnitUnlocked)
            robotRangedUnitButton.gameObject.SetActive(false);
        if (robotKnockbackUnitButton != null && !GameManager.SaveInfo.robotKnockbackUnitUnlocked)
            robotKnockbackUnitButton.gameObject.SetActive(false);
        if (robotSupportUnitButton != null && !GameManager.SaveInfo.robotSupportUnitUnlocked)
            robotSupportUnitButton.gameObject.SetActive(false);

        // Martan Buttons
        if (martianBasicUnitButton!= null && !GameManager.SaveInfo.martianBasicUnitUnlocked)
            martianBasicUnitButton.gameObject.SetActive(false);
        if (martianHeavyUnitButton != null && !GameManager.SaveInfo.martianHeavyUnitUnlocked)
            martianHeavyUnitButton.gameObject.SetActive(false);
        if (martianRangedUnitButton != null && !GameManager.SaveInfo.martianRangedUnitUnlocked)
            martianRangedUnitButton.gameObject.SetActive(false);
        if (martianDodgerUnitButton != null && !GameManager.SaveInfo.martianDodgerUnitUnlocked)
            martianDodgerUnitButton.gameObject.SetActive(false);
        if (martianSupportUnitButton != null && !GameManager.SaveInfo.martianSupportUnitUnlocked)
            martianSupportUnitButton.gameObject.SetActive(false);
    }
    public void OnPointsChanged(int points)
    {
        pointsDisplay.text = "Points: " + points;
    }


    // Button Events
    public void BackPressed(AdvancedButton _)
    {
        SceneLoader.Instance.LoadScene(Scene.LEVEL_SELECT);
    }
}

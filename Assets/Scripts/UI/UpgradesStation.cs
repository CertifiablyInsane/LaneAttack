using TMPro;
using UnityEngine;

public class UpgradesStation : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] private AdvancedButton backButton;
    [SerializeField] private TextMeshProUGUI pointsDisplay;

    private void Start()
    {
        backButton.AddOnClick(BackPressed);
        OnPointsChanged(GameManager.SaveInfo.points);
    }
    private void OnEnable()
    {
        GameManager.OnPointsChanged += OnPointsChanged;
    }
    private void OnDisable()
    {
        GameManager.OnPointsChanged -= OnPointsChanged;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStation : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] private AdvancedButton backButton;

    private void Start()
    {
        backButton.AddOnClick(BackPressed);
    }

    public void BackPressed(AdvancedButton _)
    {
        SceneLoader.Instance.LoadScene(Scene.LEVEL_SELECT);

    }
}

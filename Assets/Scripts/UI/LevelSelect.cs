using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private LevelData[] levels;
    [Header("Pointers")]
    [SerializeField] private Transform levelSelectButtonContainer;
    [SerializeField] private AdvancedButton backButton;
    [Header("Prefabs")]
    [SerializeField] AdvancedButton levelSelectButton;

    private void Start()
    {
        backButton.AddOnClick(Back);
        GenerateMenu();
    }
    public void GenerateMenu()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            AdvancedButton b = Instantiate(levelSelectButton, levelSelectButtonContainer);
            b.SetText(levels[i].levelName);
            b.SetID(i);
            b.AddOnClick(SelectLevel);

            // Disable if not unlocked yet
            if(GameManager.saveInfo.level >= levels[i].levelNumber)
            {
                b.Enable();
            }
            else
            {
                b.Disable();
            }
        }
    }
    public void SelectLevel(AdvancedButton button)
    {
        GameManager.LoadLevel(levels[button.id]);
    }
    public void Back(AdvancedButton _)
    {
        SceneLoader.Instance.LoadScene("Main Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentUserText;
    [SerializeField] private TextMeshProUGUI versionText;
    private void Start()
    {
        OnChangeUser(GameManager.SaveInfo.username);
        versionText.text = "Build : " + Application.version;
    }
    private void OnEnable()
    {
        GameManager.OnChangedUser += OnChangeUser;
    }
    private void OnDisable()
    {
        GameManager.OnChangedUser -= OnChangeUser;
    }
    private void OnChangeUser(string username)
    {
        currentUserText.text = "User : " + username;
    }
    public void PlayPressed()
    {
        SceneLoader.Instance.LoadScene(Scene.LEVEL_SELECT);
    }
}

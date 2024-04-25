using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentUserText;
    private void Start()
    {
        OnChangeUser(GameManager.saveInfo.username);
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
        currentUserText.text = "User:\n" + username;
    }
    public void PlayPressed()
    {
        SceneLoader.Instance.LoadScene("Level Select");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserSelect : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private AdvancedButton userSelectButton;
    [SerializeField] private ScreenToggle textInputToggle;
    [SerializeField] private TextInput newUserInput;

    private readonly List<AdvancedButton> buttons = new();
    private readonly List<string> fileNames = new();
    private int currentFile = 0;
    public void GenerateMenu()
    {
        foreach(AdvancedButton button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
        fileNames.Clear();
        var info = new DirectoryInfo(Application.dataPath + "/Resources/");
        FileInfo[] files = info.GetFiles("*.json");
        for (int i = 0; i < files.Length; i++)
        {
            AdvancedButton b = Instantiate(userSelectButton, content);
            string name = files[i].Name.Replace(".json", "");
            b.SetText(name);
            b.SetID(i);
            b.AddOnClick(SelectUser);
            buttons.Add(b);
            fileNames.Add(name);

            if(GameManager.saveInfo != null && GameManager.saveInfo.username == name)
            {
                // This is our current file
                currentFile = i;
            }
        }
    }
    public void SelectUser(AdvancedButton b)
    {
        currentFile = b.id;
        // Put a lil' indicator on it :)
    }
    public void ConfirmPressed()
    {
        GameManager.Load(fileNames[currentFile]);
    }
    public void NewUserPressed()
    {
        textInputToggle.SetScreen(0);
        newUserInput.OnTextReceived += NewUserTextReceived; // Subscribe to the text received
    }
    public void NewUserTextReceived(string text)
    {
        newUserInput.OnTextReceived -= NewUserTextReceived; // Unsubscribe to prevent weirdness
        textInputToggle.DisableAll();
        GameManager.CreateNewFile(text);
        GenerateMenu();                                     // Generate the menu again to see the new file.
    }
}

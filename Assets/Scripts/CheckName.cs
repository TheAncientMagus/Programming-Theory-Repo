using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CheckName : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject invalidNameText;
    public GameObject startButton;
    private NameData nameData;

    // Start is called before the first frame update
    void Start()
    {
        nameData = GameObject.Find("Name Data").GetComponent<NameData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ABSTRACTION
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // ABSTRACTION
    public void CheckText()
    {
        if (inputField.text.Length > 12)
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(true);
            startButton.SetActive(false);
        }
        else if (inputField.text.Length < 1)
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(true);
            startButton.SetActive(false);
        }
        else
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(false);
            startButton.SetActive(true);
        }

    }
}

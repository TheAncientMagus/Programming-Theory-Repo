using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InvalidName : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject invalidNameText;
    public NameData nameData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckText()
    {
        if (inputField.text.Length > 12)
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(true);
        }
        else if (inputField.text.Length < 1)
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(true);
        }
        else
        {
            nameData.playerName = inputField.text;
            invalidNameText.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NameData : MonoBehaviour
{
    public static NameData Instance { get; private set; }
    [SerializeField] private string m_playerName = "Player";
    public string playerName
    {
        get { return m_playerName; }
        set
        {
            if (value.Length < 13 && value.Length > 0)
            {
                m_playerName = value;
            }
        }
    }
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

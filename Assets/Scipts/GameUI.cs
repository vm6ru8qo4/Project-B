using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] Text m_MessageText;

    public void ShowMessage (string msg){m_MessageText.text = "Me: " + msg; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

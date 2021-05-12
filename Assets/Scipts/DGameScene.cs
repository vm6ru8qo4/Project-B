using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGameScene : MonoBehaviour
{
    [SerializeField] GameUI m_GameUI;

	DGame m_Game;
	public DGame Game {get {return m_Game;}}

    // Start is called before the first frame update
	void Awake (){

		m_Game = new DGame ();
		m_Game.OnMessageAdded += HandleOnMessageAdded;
		m_Game.MakeGame ();
	}
	void HandleOnMessageAdded (string message){ 
		m_GameUI.ShowMessage (message);
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

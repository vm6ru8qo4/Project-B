using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGame : MonoBehaviour
{
	public delegate void  DMessageEvent (string message);
	public delegate void  DGameEvent (DGame game);
    public event DMessageEvent OnMessageAdded = (m) => {};
    public event DGameEvent OnGameStarted = (g) => {};
	public event DGameEvent OnGameOver = (g) => {};
	public event DGameEvent OnGameFinished = (g) => {};
    [SerializeField] SpriteRenderer[] m_Renderers = new SpriteRenderer[5];
    //Haning grades
    //public int point = 100;
    // Start is called before the first frame update
	public DGame () {

		//MakeGame ();

		Debug.Log ("Try Hanging the item and put in!");
	
	}

	void Finish () {

		Debug.Log ("Thanks for playing the game!");
		UnityEditor.EditorApplication.isPlaying = false;
	}
	public void MakeGame () {


		OnGameStarted(this);
	}

    void ResetColor()
    {
        ChangeColor(Color.white);
    }
    void ChangeColor(Color color)
    {
        foreach(SpriteRenderer r in m_Renderers)
        {
            r.color = color;
        }
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        //Stop();
        //point -= 12;
        Debug.Log("Congrats!");
        ChangeColor(Color.red);
    }
    /*void OnCollisionStay2D(Collision2D collision)
    {
        Stop();
    }
    */
    void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

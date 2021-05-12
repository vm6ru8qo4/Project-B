using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCheck : MonoBehaviour
{
    SpriteRenderer m_TargetRenderer;
    //public event EscapeMessageEvent OnMessageAdded = (m) => {};
    // Start is called before the first frame update
    void Start()
    {
        m_TargetRenderer = this.GetComponent<SpriteRenderer>();
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        m_TargetRenderer.color = Color.red;
        Debug.Log("detected!");
    }
    void OnCollisionExit2D(Collision2D collision){
        m_TargetRenderer.color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivePicker : MonoBehaviour
{
    GameObject m_Picked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //try to pick
            FireRaycast();
        }else if (Input.GetMouseButtonUp(0))
        {
            //try to release
            Recover();
        }
    }
    void FireRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;//report
        if(Physics.Raycast(ray,out hit))//ray should provide origin & direction
        {
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
                m_Picked = hit.collider.gameObject;
            }
        }
    }
    void Recover()
    {
        if (m_Picked != null)
        {
            m_Picked.GetComponent<MeshRenderer>().material.color = Color.white;
            m_Picked = null;
        }
    }
}

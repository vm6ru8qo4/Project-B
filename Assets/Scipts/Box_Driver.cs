using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Driver : MonoBehaviour
{
    const float INCREMENT = 0.1f;
    const float HEIGHT = 14f;
    [SerializeField] GameObject Arm;
    [SerializeField] ConfigurableJoint lift;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector3(0, 0, INCREMENT));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector3(0, 0, -INCREMENT));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Arm.transform.Rotate(new Vector3(0, -INCREMENT, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Arm.transform.Rotate(new Vector3(0, INCREMENT, 0));
        }
        if (Input.GetKey(KeyCode.R))
        {
            var ll = lift.linearLimit;
            ll.limit = Mathf.Clamp(ll.limit - INCREMENT, 1, HEIGHT);
            lift.linearLimit = ll;
        }
        if (Input.GetKey(KeyCode.F))
        {
            var ll = lift.linearLimit;
            ll.limit = Mathf.Clamp(ll.limit + INCREMENT, 0, HEIGHT);
            lift.linearLimit = ll;
        }
    }
}

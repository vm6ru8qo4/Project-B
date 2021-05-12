using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoist : MonoBehaviour
{
    //hook control
    public float m_Velocity = 2f;
    const float Attach_Distance=0.5f;
    const float Linear_Distance=0.1f;
    GameObject m_Detected;
    ConfigurableJoint m_Joint;
    [SerializeField] GameObject m_JointBody;
    [SerializeField] LineRenderer m_Cable;

    //camera control
    [SerializeField] GameObject camera;
    Vector3 m_mousePosition;
    float m_HyAngle=0;
    float m_VxAngle=0;

    // Start is called before the first frame update
    void Start()
    {
        m_mousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
            //forward(z
            this.transform.Translate(0, 0, m_Velocity * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.S))
        {
            //backward
            this.transform.Translate(0, 0, -m_Velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //left shift(x
            this.transform.Translate(-m_Velocity * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //right shift
            this.transform.Translate(m_Velocity * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.R))
        {
            //Up shift(y
            this.transform.Translate(0, m_Velocity * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            //down shift
            this.transform.Translate(0, -m_Velocity * Time.deltaTime, 0);
        }*/
        //mouse tilt control
        if (Input.GetMouseButtonDown(0))
        {
            m_mousePosition = Input.mousePosition;
        }else if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = m_mousePosition - Input.mousePosition;
            m_HyAngle -= mouseDelta.x * 0.1f;
            m_VxAngle = Mathf.Clamp(m_VxAngle - mouseDelta.y * 0.1f, -49f, 49f);
            camera.transform.localEulerAngles = new Vector3(m_VxAngle, m_HyAngle, 0f);
            m_mousePosition = Input.mousePosition;
        }
        //detection
        if (m_Joint == null)
        {
            //free to find something new
            Detects();
        }
        //try to attach something
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttachOrDetaches();
        }
        UpdateCable();
    }
    //try to detect object
    void Detects()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;//report
        if (Physics.Raycast(ray, out hit, Attach_Distance))
        {
            GameObject may = hit.collider.gameObject;
            if (may == m_Detected)
            {//hit the same
                return;
            }
            //try to find something new
            Recover();
            //MeshRenderer r = hit.collider.GetComponent<MeshRenderer>();
            Rigidbody rb = may.GetComponent<Rigidbody>();
            MeshRenderer r = may.GetComponent<MeshRenderer>();
            if (r != null && rb!=null)
            {//detect something new
                r.material.color = Color.yellow;
                m_Detected = may;
            }
        }
        else {//nothing
            Recover();
        }
    }
    //try to release
    void Recover()
    {
        if (m_Detected != null)
        {
            m_Detected.GetComponent<MeshRenderer>().material.color = Color.white;
            m_Detected = null;
        }
    }
    //try to attach or detach
    void AttachOrDetaches()
    {
        if (m_Joint == null)
        {//free to attach
            if (m_Detected != null)
            {//able to attach
                var joint = m_JointBody.AddComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;
                //???講義上的limit code像繞口令迴圈
                var ll = joint.linearLimit;
                ll.limit = Linear_Distance;
                joint.linearLimit=ll;
                var ayl = joint.angularYLimit;
                ayl.limit = 30f;
                joint.angularYLimit = ayl;
                var azl = joint.angularZLimit;
                azl.limit = 30f;
                joint.angularZLimit = azl;
                ///
                //joint.connectedMassScale = 10;
                ///
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f);
                joint.anchor = new Vector3(0f, 0f, 0f);
                //constrain
                joint.connectedBody = m_Detected.GetComponent<Rigidbody>();
                //mark
                m_Joint = joint;
                //pick color
                m_Detected.GetComponent<MeshRenderer>().material.color = Color.green;
                m_Detected = null;//???
            }
        }
        else
        {
            GameObject.Destroy(m_Joint);
            m_Joint = null;
        }
    }
    //line display
    void UpdateCable()
    {
        m_Cable.enabled = m_Joint!=null &&  m_Joint.connectedBody != null;
        if (m_Cable.enabled)
        {
            //m_Cable.SetPosition(0, this.transform.position);
            //m_Cable.SetPosition(1, m_Joint.connectedBody.transform.position);
            m_Cable.SetPosition(0, m_Joint.transform.TransformPoint(new Vector3(0f,-0.3f,0f)));
            m_Cable.SetPosition(1, m_Joint.connectedBody.transform.TransformPoint(m_Joint.connectedAnchor));
        }
    }
}

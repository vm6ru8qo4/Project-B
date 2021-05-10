﻿// Author(s): Kyla NeSmith
// last edited: Dec. 2, 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/solved-nullreferenceexception-object-reference-not-set-to-an-instance-of-an-object.443805/
// https://answers.unity.com/questions/1112497/adding-gameobjects-to-an-existing-array.html
// https://answers.unity.com/questions/261556/how-to-tell-if-my-character-hit-a-collider-of-a-ce.html

public enum MagnetType { PinPoint, Directional}

public class MagneticBehavior : MonoBehaviour
{
    [Tooltip("For pull and push")]
    public float multiplier = 5;
    [Tooltip("Directional works for transform.up direction")]
    public MagnetType magType = MagnetType.PinPoint;

    // name of layer that can have the magnet affect it
    private const string magneticLayer = "Magnetic";
    // list to hold game objects within the collider of the magnet
    private List<GameObject> objs = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {

    }

    public void MagneticPull(float mag)
    {
        foreach (GameObject o in objs)
        {
            // so that objects don't keep moving upwards beyond the centerpoint of the magnet
            if (magType == MagnetType.Directional && o.transform.position.y >= this.transform.position.y)
            {
                return;
            }

            // set dir based on magType:
            // if magType == MagnetType.Pinpoint,
            // dir = this.transform.position - o.transform.position,
            // else, dir = this.transform.up
            Vector3 dir = (magType == MagnetType.PinPoint) ?
                this.transform.position - o.transform.position :
                this.transform.up;

            // just in case, check for rigid body
            Rigidbody rb = o.GetComponent<Rigidbody>();
            if (rb != null)
            {
                o.GetComponent<Rigidbody>().AddForce(dir * mag * multiplier, ForceMode.Force);
            }

            // alternate option:
            //tmp = tmp + dir * multiplier * mag * Time.deltaTime;
            //o.transform.localPosition = tmp;
        }
    }

    public void MagneticPush(float mag)
    {
        foreach (GameObject o in objs)
        {
            // set dir based on magType:
            // if magType == MagnetType.Pinpoint,
            // dir = this.transform.position - o.transform.position,
            // else, dir = this.transform.up
            Vector3 dir = (magType == MagnetType.PinPoint) ?
                this.transform.position - o.transform.position :
                this.transform.up;

            // just in case, check for rigid body
            Rigidbody rb = o.GetComponent<Rigidbody>();
            if (rb != null)
            {
                o.GetComponent<Rigidbody>().AddForce(dir * mag * multiplier * -1, ForceMode.Force);
            }

            // alternate option:
            //tmp = tmp + dir * multiplier * mag * Time.deltaTime;
            //o.transform.localPosition = tmp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(magneticLayer))
        {
            objs.Add(other.gameObject);

            // for debugging purposes
            Debug.Log(other.name + " entered");
            string str = "objs: ";
            foreach (GameObject o in objs)
            {
                str += o.name + " ";
            }
            Debug.Log(str);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(magneticLayer))
        {
            objs.Remove(other.gameObject);

            // for debugging purposes
            Debug.Log(other.name + " exited");
            string str = "objs: ";
            foreach (GameObject o in objs)
            {
                str += o.name + " ";
            }
            Debug.Log(str);
        }
    }
}

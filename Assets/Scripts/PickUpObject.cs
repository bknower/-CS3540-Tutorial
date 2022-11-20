using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject heldObject;
    public float radius = 2f;
    public float distance = 2f;
    public float height = 1f;

    private void Update()
    {
        var t = transform;
        var pressedE = Input.GetKeyDown(KeyCode.E);
        if (heldObject)
        {
            if (pressedE)
            {
                var rigidBody = heldObject.GetComponent<Rigidbody>();
                rigidBody.drag = 1f;
                rigidBody.useGravity = true;
                rigidBody.constraints = RigidbodyConstraints.None;
                heldObject = null;
            }
        }
        else
        {
            if (pressedE)
            {
                var hits = Physics.SphereCastAll(t.position + t.forward, radius, t.forward, radius);
                var hitIndex = Array.FindIndex(hits, hit => hit.transform.tag == "Pickupable");

                if (hitIndex != -1)
                {
                    var hitObject = hits[hitIndex].transform.gameObject;
                    heldObject = hitObject;
                    var rigidBody = heldObject.GetComponent<Rigidbody>();
                    rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                    rigidBody.drag = 25f;
                    rigidBody.useGravity = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        var t = transform;
        var rigidBody = heldObject.GetComponent<Rigidbody>();
        var moveTo = t.position + distance * t.forward + height * t.up;
        var difference = moveTo - heldObject.transform.position;
        rigidBody.AddForce(difference * 500);
        heldObject.transform.rotation = t.rotation;
    }
}

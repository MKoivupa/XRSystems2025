using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)

    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;

                Vector3 deltaPosition = transform.position - previousPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
                Vector3 aroundControllerOrigin = grabbedObject.position - transform.position;
                
                if(otherHand.grabbedObject == grabbedObject){
                    Vector3 sumDeltaPosition = (deltaPosition + otherHand.getDeltaPosition()) * 0.05f;
                    Quaternion sumDeltaRotation = deltaRotation * otherHand.getDeltaRotation();

                    grabbedObject.position = sumDeltaPosition + grabbedObject.position;
                    grabbedObject.position = grabbedObject.position + sumDeltaRotation * aroundControllerOrigin - aroundControllerOrigin;
                    grabbedObject.rotation = sumDeltaRotation * grabbedObject.rotation;
                } else {
                    grabbedObject.position = deltaPosition + grabbedObject.position;
                    grabbedObject.position= grabbedObject.position + deltaRotation * aroundControllerOrigin - aroundControllerOrigin;
                    grabbedObject.rotation = deltaRotation * grabbedObject.rotation;
                }
            }

        }
        // If let go of button, release object
        else if (grabbedObject){
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.useGravity = true; 
            rb.isKinematic = false;

            grabbedObject = null;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;

    }

    public Vector3 getDeltaPosition(){
        return transform.position - previousPosition;
    }

    public Quaternion getDeltaRotation(){
        return transform.rotation * Quaternion.Inverse(previousRotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}

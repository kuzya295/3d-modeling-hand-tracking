using UnityEngine;
using System.Collections;
using Leap;
using System;

using UnityEngine.UI; 

public class MeshDeformerInput : MonoBehaviour
{
    public GameObject guiTextLink;

    bool isOldPoint = false;
    Controller controller;

    void Start()
    {
        controller = new Controller();
    }

    void Update()
    {
        if (controller.IsConnected)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Frame frame = controller.Frame();
        Hand hand = frame.Hands[0];
        InteractionBox interactionBox = frame.InteractionBox;

        Vector normalizedFingerPosition;
        Vector3 fingersPosition;

        normalizedFingerPosition = interactionBox.NormalizePoint(hand.Fingers[1].TipPosition);
        Vector3 fingerPos1 = new Vector3((float)(normalizedFingerPosition.x * 9.6 - 4.8), (float)(normalizedFingerPosition.y * 9.6 + 3.2), (float)(normalizedFingerPosition.z * 6.4 - 3.2));

        normalizedFingerPosition = interactionBox.NormalizePoint(hand.Fingers[0].TipPosition);
        Vector3 fingerPos0 = new Vector3((float)(normalizedFingerPosition.x * 9.6 - 4.8), (float)(normalizedFingerPosition.y * 9.6 + 3.2), (float)(normalizedFingerPosition.z * 6.4 - 3.2));

        if (hand.PinchStrength <= 0.8) {
            isOldPoint = false;
        }

        if (hand.PinchStrength > 0.8)
        {
            var collider = gameObject.GetComponent<Collider>();
            if (collider)
            {
                fingersPosition = fingerPos0 + ((fingerPos1 - fingerPos0) / 2);
                fingersPosition.z = -fingersPosition.z;                
                Vector3 closestPoint = collider.ClosestPoint(fingersPosition);

                fingersPosition = transform.InverseTransformPoint(fingersPosition);
                closestPoint = transform.InverseTransformPoint(closestPoint);
                
                if (Vector3.Distance(fingersPosition, closestPoint) < 0.2)
                {
                    MeshDeformer deformer = collider.GetComponent<MeshDeformer>();
                    if (deformer)
                    {
                        guiTextLink.GetComponent<Text>().text = "Деформация";
                        deformer.AddDeformingForce(closestPoint, fingersPosition, isOldPoint);
                        isOldPoint = true;
                    }
                }

                if (Vector3.Distance(fingersPosition, closestPoint) >= 0.2)
                    isOldPoint = false;
            }
        }
    }
}





//Debug.Log(String.Format("1 {0}, 2 {1}, 3 {2}", closestPoint, closestPoint2, closestPoint3));
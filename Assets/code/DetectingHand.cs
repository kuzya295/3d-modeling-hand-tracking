using UnityEngine;
using System.Collections;
using Leap;
using System;

using UnityEngine.UI; 

public class DetectingHand : MonoBehaviour
{
    public GameObject guiTextLink;
    Controller controller;

    void Start()
    {
        controller = new Controller();
    }

    void Update()
    {
        if (controller.IsConnected)
        {
            Frame frame = controller.Frame();
            Hand hand = frame.Hands[0];

            if (frame.Hands.Count == 1 && hand.IsRight)
            {
                GetComponent<SimpleTransform>().enabled = false;
                GetComponent<MeshDeformer>().enabled = true;
                GetComponent<MeshDeformerInput>().enabled = true;

                guiTextLink.GetComponent<Text>().text = "";
            }

            if ((frame.Hands.Count == 1 && hand.IsLeft) || frame.Hands.Count == 2)
            {
                GetComponent<MeshDeformer>().enabled = false;
                GetComponent<MeshDeformerInput>().enabled = false;
                GetComponent<SimpleTransform>().enabled = true;

                guiTextLink.GetComponent<Text>().text = "";
            }
        }
    }
}

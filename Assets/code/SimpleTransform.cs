using UnityEngine;
using System.Collections;
using Leap;
using System;
using System.Collections.Generic;

using UnityEngine.UI; 

public class SimpleTransform : MonoBehaviour
{
    Controller controller;
    public GameObject guiTextLink;

    void Start()
    {
        controller = new Controller();

        //guiTextLink.GetComponent<Text>().text = "42";
    }

    void Update()
    {
        if (controller.IsConnected)
        {
            Frame frame = controller.Frame();
            Hand hand = frame.Hands[0];

            InteractionBox interactionBox = frame.InteractionBox;
            Vector normalizedPalmPosition = new Vector(interactionBox.NormalizePoint(hand.PalmPosition));
            Vector3 PalmPos = new Vector3((float)(normalizedPalmPosition.x * 9.6 - 4.8), (float)(normalizedPalmPosition.y * 9.6 + 3.2), (float)(normalizedPalmPosition.z * 6.4 - 3.2));

            PalmPos.z = -PalmPos.z;

            if (hand.GrabStrength > 0.8 && frame.Hands.Count != 2 && Vector3.Distance(PalmPos, this.transform.position) < 1)//перемещение
            {
                guiTextLink.GetComponent<Text>().text = "Перемещение";
                this.transform.position = new Vector3(PalmPos.x, PalmPos.y, PalmPos.z);
            }

            if (hand.PinchStrength > 0.7 && frame.Hands.Count != 2 && Vector3.Distance(PalmPos, this.transform.position) > 1.5)//вращение
            {
                guiTextLink.GetComponent<Text>().text = "Вращение";
                Frame previous = controller.Frame(1);
                this.transform.RotateAroundLocal(Vector3.right, hand.Translation(previous).y / 50);
                this.transform.RotateAroundLocal(Vector3.down, hand.Translation(previous).x / 50);
            }

            if (frame.Hands.Count == 2 && hand.GrabStrength > 0.7)//масштабирование
            {
                guiTextLink.GetComponent<Text>().text = "Масштабирование";
                Frame previous = controller.Frame(1);
                if(hand.IsRight)
                     transform.localScale += new Vector3(hand.Translation(previous).x/20, hand.Translation(previous).x/20, hand.Translation(previous).x/20);
                else
                    transform.localScale -= new Vector3(hand.Translation(previous).x / 20, hand.Translation(previous).x / 20, hand.Translation(previous).x / 20);
            }
        }
    }
}

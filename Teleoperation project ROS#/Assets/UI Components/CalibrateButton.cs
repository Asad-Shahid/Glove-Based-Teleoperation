using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateButton : MonoBehaviour
{
    //public Button calibrateButton;
    public void calibrateButtonClicked()
    {
        GameObject.Find("Client - OptiTrack").transform.position = GameObject.Find("Nest").transform.position;
        Debug.Log("button pressed");
    }

}

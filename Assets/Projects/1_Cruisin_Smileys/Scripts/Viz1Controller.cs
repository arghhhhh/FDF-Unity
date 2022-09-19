using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;


public class Viz1Controller : MonoBehaviour
{
    private AudioPeer audioPeer;
    public TextAsset dataFile;
    public TextMeshPro passengerFace;
    public int passengerBand;
    private bool changePassengerFace;
    public TextMeshPro driverFace;
    public int driverBand;
    private bool changeDriverFace;
    private string[] dataLines;
    public float[] bandInfo = new float[8];
    public float[] bandThresholds = new float[8];
    public float bandMultiplier;
    public float cheerThreshold;
    public bool isCheering;

    public Animator passengerAnimator;
    
    

    private void Awake()
    {
        audioPeer = GetComponent<AudioPeer>();
    }
    void Start()
    {
        dataLines = dataFile.text.Split('\n'); //split text file into array
        driverFace.text = dataLines[Random.Range(0, dataLines.Length - 1)];
        passengerFace.text = dataLines[Random.Range(0, dataLines.Length - 1)];

        if (cheerThreshold < bandInfo[passengerBand])
            Debug.LogError("Cheer threshold is too low!");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bandInfo.Length; i++)
        {
            bandInfo[i] = audioPeer._audioBand[i]*bandMultiplier;
        }

        if (bandInfo[driverBand] > bandThresholds[driverBand])
        {
            if (changeDriverFace)
            {
                driverFace.text = dataLines[Random.Range(0, dataLines.Length - 1)];
                changeDriverFace = false;
            }
        }
        else
        {
            changeDriverFace = true;
        }

        if (bandInfo[passengerBand] > bandThresholds[passengerBand])
        {
            if (changePassengerFace)
            {

                passengerFace.text = dataLines[Random.Range(0, dataLines.Length - 1)];
                changePassengerFace = false;
            }
        }
        else
        {
            changePassengerFace = true;
        }

        if (passengerAnimator.GetBool("Cheer"))
        {
            if (passengerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !passengerAnimator.IsInTransition(0))
            {
                passengerAnimator.SetBool("Cheer", false);
                Debug.Log("changed cheer to false");
                isCheering = false;
            }
        }
        else
        {
            if (bandInfo[passengerBand] > cheerThreshold)
            {
                if (passengerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !passengerAnimator.IsInTransition(0))
                {
                    passengerAnimator.SetBool("Cheer", true);
                    Debug.Log("changed cheer to true");
                    isCheering = true;
                }
            }
        }
    }

}

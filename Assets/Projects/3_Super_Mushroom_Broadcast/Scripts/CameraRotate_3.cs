using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate_3 : MonoBehaviour
{
    public AudioPeer audioPeer;
    private Camera cam;

    [SerializeField]
    private float threshold;

    private int prevFrameCount;
    private int currFrame;

    [SerializeField]
    private int rotateFrameDelay;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        currFrame = Time.frameCount;
        if (currFrame > (prevFrameCount + rotateFrameDelay))
        {
            if (audioPeer._audioBand[5] > threshold || audioPeer._audioBand[1] > threshold)
            {
                Debug.Log(audioPeer._audioBand[5]);
                cam.transform.Rotate(0, 0, 90);
                prevFrameCount = Time.frameCount;
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    public AudioPeer audioPeer;
    public Vector3 rotateAxis;
    public Vector3 rotateSpeed;

    float rotateY;
    public bool left;

    public GameObject quad;
    public bool rotateCover;

    [Range(0,7)]
    public int bandNum;

    public int rotateThreshold;

    public TextAsset dataFile;
    public TextMeshPro emoticonText;
    private string[] dataLines;
    void Start()
    {
        dataLines = dataFile.text.Split('\n'); //split text file into array
    }

    void Update()
    {

        rotateY = rotateAxis.y * rotateSpeed.y * Time.deltaTime * audioPeer._audioBandBuffer[bandNum];
        
        if (left)
        {
            //FIGURE OUT BEST WAY TO ONLY ROTATE TEXT AND NOT ROTATE COVER
            transform.Rotate(0, rotateY, 0);
        }
        else
        {
            transform.Rotate(0, -rotateY, 0);
            rotateThreshold = -Mathf.Abs(rotateThreshold);
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.y) > Mathf.Abs(rotateThreshold))
        {
            emoticonText.text = dataLines[Random.Range(0,dataLines.Length-1)];
            Debug.Log("rotated at " + transform.rotation.eulerAngles.y);
            transform.Rotate(0, -rotateThreshold, 0);
        }
    }
}

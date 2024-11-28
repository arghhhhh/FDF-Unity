using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WindowVisualizer : MonoBehaviour
{
    public GameObject[] glass = new GameObject[4];
    [Range(0,7)]
    public int[] glassBands = new int[4];

    [Range(0, 7)]
    public int blendBand;
    [Range(0, 7)]
    public int scaleBand;
    [Range(0, 7)]
    public int zoomBand;

    private AudioPeer audioPeer;
    public float startScale;
    public float intensity;

    public GameObject window;
    public GameObject hinge;

    private bool open;

    public Material cover;

    void Start()
    {
        audioPeer = GetComponent<AudioPeer>();
        LeanTween.rotateAround(window, Vector3.up, -360f, 5.0f).setLoopClamp();
        audioPeer._mixerGroupMaster.audioMixer.SetFloat("CutFrq", 326.0f);
        audioPeer._mixerGroupMaster.audioMixer.SetFloat("Volume", -2.4f);
    }

    void Update()
    {
        //cycle thru each GameObject in glass[]
        for (int i = 0; i < glass.Length; i++)
        {
            float intensityScale = Mathf.Lerp(0f, intensity, audioPeer._audioBandBuffer[glassBands[i]]);
            glass[i].transform.localScale = new Vector3(0.75f, 0.75f, startScale + intensityScale);
        }

        float blend = Mathf.Lerp(0.005f, -0.05f, audioPeer._audioBandBuffer[blendBand]);
        float scale = Mathf.SmoothStep(1f, 0.95f, audioPeer._audioBandBuffer[scaleBand]);

        cover.SetFloat("_BlendOpacity", blend);
        cover.SetFloat("_ScaleXY", scale);

        if (open)
        {
            float zoom = Mathf.Lerp(1f, 1.2f, audioPeer._audioBandBuffer[zoomBand]);
            Camera.main.transform.position = new Vector3(0f, 0f, -5 / zoom);
        }

        if (Input.GetKeyDown("space"))
        {
            OpenWindow();
        }
    }

    //implement keypress function to open window
    void OpenWindow()
    {
        if (!open)
        {
            LeanTween.moveY(hinge, 2.15f, 1.0f);
            LeanTween.moveY(window, -0.9f, 1.0f);
            StartCoroutine(LerpMix(1.0f, false));
        }

        else
        {
            LeanTween.moveY(hinge, 1.1f, 1.0f);
            LeanTween.moveY(window, 0f, 1.0f);
            StartCoroutine(LerpMix(1.0f, true));
        }
            
        open = !open;
    }

    IEnumerator LerpMix(float fadeTime, bool isOpen)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            if (!isOpen)
            {
                audioPeer._mixerGroupMaster.audioMixer.SetFloat("CutFrq", Mathf.Lerp(326f, 22000f, elapsedTime / (fadeTime)));
                audioPeer._mixerGroupMaster.audioMixer.SetFloat("Volume", Mathf.Lerp(-2.4f, 0f, elapsedTime / (fadeTime)));
            }
            else
            {
                audioPeer._mixerGroupMaster.audioMixer.SetFloat("CutFrq", Mathf.Lerp(22000f, 326f, elapsedTime / (fadeTime)));
                audioPeer._mixerGroupMaster.audioMixer.SetFloat("Volume", Mathf.Lerp(0f, -2.4f, elapsedTime / (fadeTime)));
            }
            yield return null;
        }
    }
}

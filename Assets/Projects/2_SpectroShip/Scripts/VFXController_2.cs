using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController_2 : MonoBehaviour
{
    public AudioPeer audioPeer;

    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField, Range(0,7)]
    private int band = 0;

    [SerializeField]
    private float maxSize = 1f;
    [SerializeField]
    private float minAlpha = 0.05f;
    void Update()
    {
        float lerpSize = Mathf.Lerp(0.01f, maxSize, audioPeer._audioBand[band]);
        float lerpAlpha = Mathf.Lerp(minAlpha, 1f, audioPeer._audioBand[band]);
        visualEffect.SetFloat("pSize", lerpSize);
        visualEffect.SetFloat("pAlpha", lerpAlpha);
    }
}

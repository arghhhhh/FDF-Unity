using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TweenTorus_7 : MonoBehaviour
{
    public VolumeProfile volume;
    public AudioPeer audioPeer;
    private Bloom bloom;
    private WhiteBalance wb;
    private Camera cam;
    private bool tweenInCam;
    private bool tweenOutCam;
    [Range(0,7)]
    public int band;

    void Start()
    {
        cam = Camera.main;
        if (volume.TryGet<Bloom>(out var getBloom))
        {
            getBloom.intensity.overrideState = true;
            bloom = getBloom;
        }
        if (volume.TryGet<WhiteBalance>(out var getWB))
        {
            getWB.tint.overrideState = true;
            getWB.temperature.overrideState = true;
            wb = getWB;
        }
        tweenInCam = true;
    }

    void Update()
    {
        if (tweenInCam)
            LeanTween.value(gameObject, TweenInCam, 10f, 120f, 25f).setEase(LeanTweenType.easeInQuad);
        else if (tweenOutCam)
            LeanTween.value(gameObject, TweenOutCam, 120f, 10f, 15f).setEase(LeanTweenType.easeInOutExpo);
        float vol = audioPeer._audioBandBuffer[band];
        vol = Mathf.Lerp(1f, 7f, vol);
        bloom.intensity.value = vol;
        //Debug.Log(audioPeer._audioBandBuffer[band]);
    }

    void TweenInCam(float val, float ratio)
    {
        tweenInCam = false;
        cam.fieldOfView = val;
        //Debug.Log("tweened value:" + val + " percent complete:" + ratio * 100);
        if (ratio == 1f)
            tweenOutCam = true;
    }

    void TweenOutCam(float val, float ratio)
    {
        tweenOutCam = false;
        cam.fieldOfView = val;
        if (ratio == 1f)
            tweenInCam = true;
    }
}

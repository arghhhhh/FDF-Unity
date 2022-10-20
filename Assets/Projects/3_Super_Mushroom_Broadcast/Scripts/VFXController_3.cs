using UnityEngine;
using UnityEngine.VFX;

public class VFXController_3 : MonoBehaviour
{
    public AudioPeer audioPeer;
    private VisualEffect vfx;

    [SerializeField]
    private float minSize = 0.01f;
    [SerializeField]
    private float maxSize = 1f;

    private int duration = 7;
    private int frame = 0;
    private float xPos = 0;
    private float  maxPos = 9.375f;

    [SerializeField]
    [Tooltip("Determines the space between the rows of spawned mushrooms")]
    [Range(2,10)]
    private int spawnDelayMult = 2;
    private int alphaDur;
    private int alpha = 0;

    private void Awake()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
        alphaDur = (duration+1) * spawnDelayMult - 1;
    }

    void Update()
    {
        if (frame > duration) frame = 0;
        if (xPos > maxPos) xPos = 0;
        if (alpha > alphaDur) alpha = 0;

        float size = Mathf.Lerp(minSize, maxSize, audioPeer._audioBand[frame]);
        vfx.SetFloat("pSize", size);
        vfx.SetFloat("pXpos", xPos);
        vfx.SetInt("pSpriteIndex", frame); //set sprite by modifying pSpriteIndex of vfx graph

        if (alpha<=duration) vfx.SetInt("pAlpha", 1);
        else vfx.SetInt("pAlpha", 0);

        frame++;
        xPos += 1.25f; // each block is 1.25 units apart on the x-axis
        alpha++;
    }
}

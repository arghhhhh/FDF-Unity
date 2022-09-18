using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using UnityEngine.U2D;
using Joss.Helpers;

public class VFXController_3 : MonoBehaviour
{
    public AudioPeer audioPeer;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField]
    private float minSize = 0.01f;
    [SerializeField]
    private float maxSize = 1f;

    [SerializeField]
    private float maxPosX;

    //private int duration = 8;

    public Gradient gradient;

    void Update()
    {
        //int t = (int)(Time.frameCount % (duration));
        //Vector3 curPos = new Vector3(Toolbox.Remap(t, 0, duration, 0, maxPosX) + 1.25f, 0, 0);

        //spawnPos.position = curPos;

        //float mapColor = Toolbox.Map(spawnPos.position.x, 0, maxPosX, 0, 1f);
        //if (mapColor > 1) mapColor = 0.99f;
        //Color color = gradient.Evaluate(mapColor);
        //visualEffect.SetVector4("pColor", color);

        float round = Toolbox.RoundToCustom(spawnPos.position.x, 1.25f); //get the position as a fraction of the total band count (8)
        int index = (int)(round * 4 / 5); //convert that position to an index in the band count

        float size = Mathf.Lerp(minSize, maxSize, audioPeer._audioBand[index]);
        visualEffect.SetFloat("pSize", size);

        visualEffect.SetInt("pTexIndex", index); //set sprite by modifying texIndex of vfx graph
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongAnim : MonoBehaviour
{
    public AnimationClip anim;
    void Start()
    {
        // Set the wrap mode of the walk animation to loop
        GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.wrapMode = WrapMode.PingPong;
    }
}

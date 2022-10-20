using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimControl_3 : MonoBehaviour
{
    // Add this in your variables at the top of your object script
    private Animator animator;  //Used to store a reference to the Player's animator component.

    [SerializeField]
    private float delay = 0f;

    public void Start()
    {
        // Put this line of code in your Start method for your object
        animator = GetComponent<Animator>();
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(delay);

        // Use this line of code to set a trigger in your animation section:
        animator.SetTrigger("playerAttack");    // Change playerAttack to whatever trigger you are using
    }

}

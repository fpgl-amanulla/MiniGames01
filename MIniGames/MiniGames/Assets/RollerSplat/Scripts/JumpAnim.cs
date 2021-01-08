using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnim : MonoBehaviour
{
    public static JumpAnim instance;

    private Animator anim;

    private void Start()
    {
        if (instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    public void Jump()
    {
        anim.SetBool("isJumping", true);
    }
}

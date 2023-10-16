using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
      private Animator animator;
    const string Run_anim = "Run";
    const string Idle_Anim = "Idle";
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayCharacterAnimation()
    {
        // Trigger the animation
        animator.SetTrigger("Run");
        animator.SetTrigger("Idle");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger(Run_anim);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger(Run_anim);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger(Run_anim);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger(Run_anim);
        }

        if (Input.GetKeyUp(KeyCode.W)==false)
        {
            animator.SetTrigger(Idle_Anim);
        }
        if (Input.GetKeyUp(KeyCode.A) == false)
        {
            animator.SetTrigger(Idle_Anim);
        }
        if (Input.GetKeyUp(KeyCode.S) == false)
        {
            animator.SetTrigger(Idle_Anim);
        }
        if (Input.GetKeyUp(KeyCode.D) == false)
        {
            animator.SetTrigger(Idle_Anim);
        }
    }
}

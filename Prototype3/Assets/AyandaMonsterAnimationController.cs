using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    sterAnimationController : MonoBehaviour
{

    public void StopTakingDamage()
    {
        Animator myAnim = this.GetComponent<Animator>();
        myAnim.SetBool("takingDamage", false);
    }

    public void StopAttacking()
    {
        Animator myAnim = this.GetComponent<Animator>();
        myAnim.SetBool("attack", false);
    }
}

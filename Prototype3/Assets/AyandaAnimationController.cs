using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyandaAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StopTakingDamage()
    {
        Animator myAnim = this.GetComponent<Animator>();
        myAnim.SetBool("takingDamage", false);
    }

    public void StopAttacking()
    {
        Animator myAnim = this.GetComponent<Animator>();
        myAnim.SetBool("attack", false);
        myAnim.SetBool("defaultAttack", false);
        myAnim.SetBool("specialAttack", false);
        myAnim.SetBool("multiplierAttack", false);
    }

    private void ControlAnimationsTest()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Animator myAnim = this.GetComponent<Animator>();
            myAnim.SetBool("takingDamage", true);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Animator myAnim = this.GetComponent<Animator>();
            myAnim.SetBool("attack", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator myAnim = this.GetComponent<Animator>();
            myAnim.SetBool("dying", true);
        }
    }
}

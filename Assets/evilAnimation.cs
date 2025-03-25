using UnityEngine;

public class evilAnimation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        //set Damaged animation ---------------------------------
        if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isDamaged",true);
        }
        else 
        {
            anim.SetBool("isDamaged",false);
        }
        // ----------------------------------------------------------


        //set Dead animation ---------------------------------------
        if(Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("isDead",true);
            Destroy(this.gameObject,5);
        }
        //----------------------------------------------------------------
    }
}

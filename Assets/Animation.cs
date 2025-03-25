using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        //set Damaged animation ---------------------------------
        if(Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isDamaged",true);
        }
        else 
        {
            anim.SetBool("isDamaged",false);
        }
        // ----------------------------------------------------------


        //set Dead animation ---------------------------------------
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("isDead",true);
            Destroy(this.gameObject,5);
        }
        //----------------------------------------------------------------
    }
}

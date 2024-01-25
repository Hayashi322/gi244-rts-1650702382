using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private Animator anim;
    private Units unit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Units>();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseAnimation(unit); 
    }

    private void ChooseAnimation(Units u)
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);

        switch (u.State)
        {
            case UnitState.Idle:
                anim.SetBool("IsIdle", true);
                break;
            case UnitState.Move:
                anim.SetBool("IsMove", true);
                break;
            case UnitState.Attack:
                anim.SetBool("IsAttack", true);
                break;
        }
    }
}

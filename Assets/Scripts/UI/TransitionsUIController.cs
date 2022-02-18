using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsUIController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TransitionOutForCombatInitalization()
    {
        anim.Play("TransitionOutForCombatInitialization");
    }

    public void TransitionInForCombatStartInitalization()
    {
        anim.Play("TransitionInForCombatInitialization");
    }

    //Called from animation events
    private void StopWaitingOnAnimation()
    {
        UserInterfaceManager.Instance.StopWaitingOnAnimation();
    }
}
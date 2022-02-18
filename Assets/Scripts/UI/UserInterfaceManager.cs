using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;

    [SerializeField] private TransitionsUIController transitionsUI;
    [SerializeField] private OverworldUIController overworldUI;
    [SerializeField] private CombatUIController combatUI;

    private bool waitingOnAnimation = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void StopWaitingOnAnimation()
    {
        waitingOnAnimation = false;
    }

    public void TransitionOutForCombatInitalization()
    {
        waitingOnAnimation = true;

        transitionsUI.TransitionOutForCombatInitalization();

        //while (waitingOnAnimation)
        //{
        //    //chill
        //}
    }


    public void TransitionInForCombatStartInitalization()
    {
        waitingOnAnimation = true;

        transitionsUI.TransitionInForCombatStartInitalization();

        //while (waitingOnAnimation)
        //{
        //    //chill
        //}
    }
}
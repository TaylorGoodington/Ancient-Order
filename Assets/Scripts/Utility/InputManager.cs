using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

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


    private void Update()
    {
        if (MasterControl.Instance.GetCurrentInputState() == InputStates.Overworld)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            ProcessPlayerInput(input);

            //TODO Testing for Combat Setup
            if (Input.GetButtonDown("Submit"))
            {
                MasterControl.Instance.SelectGameData();
                MasterControl.Instance.IntitializeCombat();
            }
        }
        else if (MasterControl.Instance.GetCurrentInputState() == InputStates.Menus)
        {

        }
        else if (MasterControl.Instance.GetCurrentInputState() == InputStates.Dialogue)
        {

        }
    }

    //TODO add conditions for when playerMovementInput gets set
    private void ProcessPlayerInput(Vector2 input)
    {
        MasterControl.Instance.GetOverworldPlayer().RecieveInput(input);
    }
}
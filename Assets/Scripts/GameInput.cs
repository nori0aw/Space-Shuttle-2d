using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    public EventHandler OnPause;
    public static GameInput Instance { private set; get; }

    private bool mobilePlatform = false;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();


        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            mobilePlatform = false;
        }
        else
        {
            mobilePlatform = true;
        }

        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Start()
    {
        if (!mobilePlatform)
        {
            playerInputActions.Player.MoveMobile.Disable();
        }
        else
        {
            playerInputActions.Player.Move.Disable();
        }
        

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovmentVectorNormalized()
    {
        Vector2 inputVector;

        if (mobilePlatform)
        {
            inputVector = playerInputActions.Player.MoveMobile.ReadValue<Vector2>();
        }
        else
        {
            inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        }    
        

        return inputVector.normalized;
    }

  

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
        Instance = null;
    }

    public void DisableUserInput()
    {
        playerInputActions.Player.Disable();
    }

    public void EnableUserInput()
    {
        playerInputActions.Player.Enable();
    }

    public bool IsMobilePlatform()
    {
        return mobilePlatform;
    }
}

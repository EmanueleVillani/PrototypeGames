using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{


    private Controls myControls;


    // Inizializzazione variabili , Movimento , Jump 
    public float valueX;

    public bool jumpInput;

    private void Awake()
    {
        myControls = new Controls();
       

      
    }

    private void OnEnable()
    {
        //Avvia il movimento del Player
        myControls.Player.Move.performed += StartMove;


        //Ferma il movimento del Player
        myControls.Player.Move.canceled += StopMove;


        myControls.Player.Jump.performed += JumpStar;
        myControls.Player.Jump.canceled += JumpStop;



        // Attivo il player ai suoi commandi
        myControls.Player.Enable();

    }
    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;


        myControls.Player.Jump.performed -= JumpStar;
        myControls.Player.Jump.canceled -= JumpStop;


        myControls.Player.Disable();
        //myControls.Disable();

    }

    //Func di Input da avviare il player
    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
        Debug.Log("Cercando di muovermi");
    }

    // Func per far fermare il player dopo aver lasciato i tasti di movimento
    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }
       private void JumpStar(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }

}

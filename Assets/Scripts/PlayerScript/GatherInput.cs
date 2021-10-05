using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{


    private Controls myControls;


    // Inizializzazione variabile  Movimento 
    public float valueX;

    //Inizializzazione variabile Jump 
    public bool jumpInput;
    public bool runInput;


    public bool tryAttack;  //variabile Attacco prova a prendere sarà vero se stiamo premendo i pulsanti e provando 

    private void Awake()
    {
        myControls = new Controls();
    }

    private void OnEnable()
    {
        
        myControls.Player.Move.performed += StartMove;//Avvia il movimento del Player
        myControls.Player.Move.canceled += StopMove; //Ferma il movimento del Player

        myControls.Player.Jump.performed += JumpStar; //Avvia il Salto del Player
        myControls.Player.Jump.canceled += JumpStop; //Stop il Salto del Player
       
        myControls.Player.Run.performed +=RunStart; //Avvia il Salto del Player
        myControls.Player.Run.canceled += RunStop; //Stop il Salto del Player

        myControls.Player.Attack.performed += TryToAttack;  //Avvio l'Attaco del Player
        myControls.Player.Attack.canceled += StopTryAttack;   //cancello l'Attaco del Player

        myControls.Player.Enable();  // Attivo il player ai suoi commandi

    }
    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;


        myControls.Player.Jump.performed -= JumpStar;
        myControls.Player.Jump.canceled -= JumpStop;


        myControls.Player.Attack.performed -= TryToAttack;  
        myControls.Player.Attack.canceled -= StopTryAttack;   

        myControls.Player.Disable();
        myControls.Disable();

    }

    private void DisableControls()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;


        myControls.Player.Jump.performed -= JumpStar;
        myControls.Player.Jump.canceled -= JumpStop;


        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryAttack;

        myControls.Player.Disable();

        valueX = 0;
        myControls.Disable();

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

    private void TryToAttack(InputAction.CallbackContext ctx)//Func Attacco
    {
        tryAttack = true;
    }

    private void StopTryAttack(InputAction.CallbackContext ctx) // Sto Attacco
    {
        tryAttack = false;
    }

    private void RunStart(InputAction.CallbackContext ctx)
    {
        runInput = true;
    }

    private void RunStop(InputAction.CallbackContext ctx)
    {
        runInput = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{


    private Controls myControls;


    // Inizializzazione variabile  Movimento 
    public float valueX;
    public Vector2 mousedelta;
    public Vector2 mouseposition;
    //Inizializzazione variabile Jump 
    public bool jumpInput;
    public bool runInput;
    public bool fireInput;
    public bool torchInput;
    public bool tryAttack;  //variabile Attacco prova a prendere sarà vero se stiamo premendo i pulsanti e provando 
    public bool tryDodge;  

    private void Awake()
    {
        myControls = new Controls();
    }

    private void OnEnable()
    {
        
        myControls.Player.Move.performed += StartMove;//Avvia il movimento del Player
        myControls.Player.Move.canceled += StopMove; //Ferma il movimento del Player

     //  myControls.Player.Jump.performed += JumpStar; //Avvia il Salto del Player
    //  myControls.Player.Jump.canceled += JumpStop; //Stop il Salto del Player
       
       myControls.Player.Run.performed +=RunStart; //Avvia il Salto del Player
       myControls.Player.Run.canceled += RunStop; //Stop il Salto del Player

    //  myControls.Player.Attack.performed += TryToAttack;  //Avvio l'Attaco del Player
    //  myControls.Player.Attack.canceled += StopTryAttack;   //cancello l'Attaco del Player
      
        myControls.Player.MouseDelta.performed += MovingMouse;  //Avvio l'Attaco del Player
        myControls.Player.MousePosition.performed += MousePosition;   //cancello l'Attaco del Player
        
        myControls.Player.Enable();  // Attivo il player ai suoi commandi
        myControls.Enable();
    }
    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        // myControls.Player.Jump.performed -= JumpStar;
        // myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Run.performed -= RunStart; //Avvia il Salto del Player
        myControls.Player.Run.canceled -= RunStop; //Stop il Salto del Player

      //   myControls.Player.Attack.performed -= TryToAttack;  
      //   myControls.Player.Attack.canceled -= StopTryAttack;

        myControls.Player.MouseDelta.performed -= MovingMouse;  //Avvio l'Attaco del Player
        myControls.Player.MousePosition.performed -= MousePosition;   //cancello l'Attaco del Player

        myControls.Player.Disable();
        myControls.Disable();

    }
    public void Update()
    {
        fireInput= myControls.Player.Fire.triggered;
        tryAttack= myControls.Player.Attack.triggered;
        tryDodge = myControls.Player.Dodge.triggered;
        jumpInput = myControls.Player.Jump.triggered;
        torchInput = myControls.Player.Torch.triggered;
     //   runInput= myControls.Player.Run.triggered;
    }

    public void DisableControls()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

     // myControls.Player.Jump.performed -= JumpStar;
     // myControls.Player.Jump.canceled -= JumpStop;

  //    myControls.Player.Attack.performed -= TryToAttack;
   //   myControls.Player.Attack.canceled -= StopTryAttack;

        myControls.Player.Disable();

        valueX = 0;
        myControls.Disable();

    }

    //Func di Input da avviare il player
    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }

    // Func per far fermare il player dopo aver lasciato i tasti di movimento
    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }
  // private void JumpStar(InputAction.CallbackContext ctx)
  // {
  //     jumpInput = true;
  // }
  //
  // private void JumpStop(InputAction.CallbackContext ctx)
  // {
  //     jumpInput = false;
  // }

//  private void TryToAttack(InputAction.CallbackContext ctx)//Func Attacco
//  {
//      tryAttack = true;
//  }
// 
//  private void StopTryAttack(InputAction.CallbackContext ctx) // Sto Attacco
//  {
//      tryAttack = false;
//  }
    private void RunStart(InputAction.CallbackContext ctx)
    {
          runInput = true;
    }

    private void RunStop(InputAction.CallbackContext ctx)
    {
           runInput = false;
    }


    private void MovingMouse(InputAction.CallbackContext ctx)
    {
        mousedelta = ctx.ReadValue<Vector2>();
    }

    private void MousePosition(InputAction.CallbackContext ctx)
    {
        mouseposition = ctx.ReadValue<Vector2>();
    }
}

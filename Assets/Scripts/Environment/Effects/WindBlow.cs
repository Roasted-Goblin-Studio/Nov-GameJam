using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlow : MonoBehaviour
{
    [SerializeField] private bool BlowingHorizontally = true;
    [SerializeField] private bool BlowingRight = false;
    [SerializeField] private float HorizontalWindBlowingForce = 50;

    [SerializeField] private bool BlowingVertically = false;
    [SerializeField] private bool BlowingUp = false;
    [SerializeField] private float VerticalWindBlowingForce = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            Character character = other.GetComponent<Character>();
            if(!character.IsInWind){
                Debug.Log("Entered the wind ...");
                character.IsInWind = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            CharacterMovement characterMovement = other.GetComponent<CharacterMovement>();
            
            if(BlowingHorizontally) characterMovement.HorizontalEnviromentalForceApplied = BlowingRight ? HorizontalWindBlowingForce : -HorizontalWindBlowingForce;
            else if (BlowingVertically) characterMovement.VerticalEnviromentalForceApplied = BlowingUp ? VerticalWindBlowingForce : -VerticalWindBlowingForce;
            else {
                characterMovement.HorizontalEnviromentalForceApplied = 0;
                characterMovement.VerticalEnviromentalForceApplied = 0;
            }
            
            
            
            Debug.Log("In the wind");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {   
            Character character = other.GetComponent<Character>();
            CharacterMovement characterMovement = other.GetComponent<CharacterMovement>();
            if(character.IsInWind){
                Debug.Log("Left the wind ... ");
                character.IsInWind = false;
                characterMovement.HorizontalEnviromentalForceApplied = 0;
                characterMovement.VerticalEnviromentalForceApplied = 0;
            }
        }
        
    }
}

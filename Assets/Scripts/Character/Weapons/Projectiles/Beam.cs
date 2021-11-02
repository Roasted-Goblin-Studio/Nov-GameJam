using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile
{
    [SerializeField] private float _ColliderWidth = 1;
    [SerializeField] private BeamDirections _BeamDirection = BeamDirections.Right;

    private bool _PlayerInBeam = false;
    private bool _EndOfBeam = false;

    public bool EndOfBeam {get => _EndOfBeam; set => _EndOfBeam = value;}
    public BeamDirections BeamDirection {get => _BeamDirection; set => _BeamDirection = value;}
    public enum BeamDirections
    {
        Left,
        Right,
        Up,
        Down
    }


    protected override void Start()
    {
        base.Start();
    }

    protected override void MoveProjectile()
    {
        if(BeamDirection == BeamDirections.Right){
            // Projectile should expand to the right at the acceleration set.
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.localScale += new Vector3(_Acceleration, 0, 0);
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.position += new Vector3(_Acceleration/2,0,0);
            else _ProjectileRigidBody2D.transform.position += new Vector3(_Acceleration,0,0);
        }
        else if(BeamDirection == BeamDirections.Left){
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.localScale += new Vector3(-_Acceleration, 0, 0);
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.position += new Vector3(-_Acceleration/2,0,0);
            else _ProjectileRigidBody2D.transform.position += new Vector3(-_Acceleration,0,0);
        }
        else if(BeamDirection == BeamDirections.Up){
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.localScale += new Vector3(0, _Acceleration, 0);
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.position += new Vector3(0,_Acceleration/2,0);
            else _ProjectileRigidBody2D.transform.position += new Vector3(0,_Acceleration,0);
        }
        else if(BeamDirection == BeamDirections.Down){
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.localScale += new Vector3(0, -_Acceleration, 0);
            if(!EndOfBeam) _ProjectileRigidBody2D.transform.position += new Vector3(0,-_Acceleration/2,0);
            else _ProjectileRigidBody2D.transform.position += new Vector3(0,-_Acceleration,0);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("Player entered Beam");
            if (!_PlayerInBeam) _PlayerInBeam = true;
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("Player is in Beam");
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("Player exited Beam");
            if (_PlayerInBeam) _PlayerInBeam = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] protected float _StartingSpeed = 0f;
    [SerializeField] protected float _Acceleration = 0f;
    [SerializeField] protected float _ProjectileDamage = 1f;
    [SerializeField] protected ProjectileTypes _ProjectileType;

    protected Collider2D _Collider2D;
    protected Rigidbody2D _ProjectileRigidBody2D;
    protected SpriteRenderer _ProjectileSpriteRender;
    protected Vector2 _ProjectileMovement;
    protected ReturnToPool _ProjectileReturnToPool;
    protected Character _ProjectileOwner;
    protected float _TotalSpeed = 0f;
    protected ReturnToPool _ReturnToPool;

    protected LayerIgnore _LayersToIgnore;
    protected TagsToAvoid _TagsToAvoid;

    public Vector2 Direction { get; set; }
    public float Speed {get => _TotalSpeed; set => _TotalSpeed = value;}
    public Character ProjectileOwner { get => _ProjectileOwner; set => _ProjectileOwner = value; }
    public ProjectileTypes ProjectileType {get => _ProjectileType; set=> _ProjectileType = value; }

    public enum ProjectileTypes
    {
        Bullet,
        Beam,
        Falling
    }

    private void Awake() {
        _ProjectileRigidBody2D = GetComponent<Rigidbody2D>();
        _ProjectileSpriteRender = GetComponent<SpriteRenderer>();
        _Collider2D = GetComponent<Collider2D>();
        _LayersToIgnore = GetComponent<LayerIgnore>();
        _TagsToAvoid = GetComponent<TagsToAvoid>();
        _ReturnToPool = GetComponent<ReturnToPool>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetLayerCollisionIgnores();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate(){
        MoveProjectile();
    }

    protected virtual void SetLayerCollisionIgnores(){
        if(_LayersToIgnore == null) return;
        foreach (int item in _LayersToIgnore.LayersToIgnore)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, item);
        }

        
    }

    protected virtual void MoveProjectile(){

    }

    protected virtual void FlipProjectile(){
        _ProjectileSpriteRender.flipX = !_ProjectileSpriteRender.flipX;
    }

    public  virtual void SetDirection(Vector2 newDirection, Quaternion newRotation, bool isFacingRight=true){
        Direction = newDirection;
        if(!isFacingRight){
            FlipProjectile();
        }
        transform.rotation = newRotation;
    }

    public void Reset(){
        _ProjectileSpriteRender.flipX = false;
        Speed = _StartingSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // This will need to improve drastically.
        foreach (var tag in _TagsToAvoid.TagsToAvoidStrings)
        {
            if(other.tag == tag){
                return;
            }
        }
        if(other.tag == "Non Hitable") {
            return;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collider){

    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        
    }
}

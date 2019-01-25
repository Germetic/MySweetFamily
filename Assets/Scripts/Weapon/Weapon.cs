using UnityEngine;

public abstract class Weapon : MonoBehaviour , ICanDoDamage
{
    public string Name;
    public float Damage;
    public float ColdownTime;

    public bool IsAttackingNow;
    public bool IsHandleNow;
    
    public Vector3 OnHandsPosition;
    public Vector3 OnHandsRotation;

    public ParticleSystem CanCatchParts;
    public PlayerWeaponController PlayerWeaponController;

    public abstract void Attack(ICanGetDamage damagedObject);
    public abstract void Throw(Vector3 direction);
    public abstract void ShowFightReadyVFX();

    protected float _coldownFinishTime;

    public virtual void Initialize(PlayerWeaponController owner,string name="Unnamed")
    {
        PlayerWeaponController = owner;
        IsHandleNow = true;
        Name = name;
        if (CanCatchParts != null)
            CanCatchParts.gameObject.SetActive(false);
    }
    public virtual void Miss()
    {
        Rigidbody2D rgb = GetComponent<Rigidbody2D>();
        PlayerWeaponController = null;
        IsHandleNow = false;
        transform.parent = null;
        rgb.isKinematic = false;
        rgb.AddForce(Vector2.up * 15f,ForceMode2D.Impulse);
        GetComponent<Collider2D>().isTrigger = false;
        if(CanCatchParts !=null)
        CanCatchParts.gameObject.SetActive(true);
    }
    public virtual void Catch()
    {
        IsHandleNow = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().isTrigger = true;
        if (CanCatchParts != null)
            CanCatchParts.gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localPosition = OnHandsPosition;
        transform.localEulerAngles = OnHandsRotation;
    }

    protected void SetColdown()
    {   
        _coldownFinishTime = Time.time + ColdownTime;
    }

    protected bool IsColdownedNow()
    {
        return _coldownFinishTime < Time.time ? true : false;
    }

    public Transform GetPosition()
    {
        return PlayerWeaponController.transform;
    }
}

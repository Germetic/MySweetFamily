using UnityEngine;

public abstract class Weapon : MonoBehaviour , ICanDoDamage
{
    public string Name;
    public float Damage;
    public float ColdownTime;

    public bool IsAttackingNow;
    public bool IsHandleNow;
    public bool IsThrowingNow;


    public Vector3 OnHandsPosition;
    public Vector3 OnHandsRotation;

    public ParticleSystem CanCatchParts;
    public PlayerWeaponController PlayerWeaponController;

    public abstract void Attack(ICanGetDamage damagedObject);
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
    public virtual void ThrowTo(Vector2 direction)
    {
        //Miss();
        transform.parent = null;
        Rigidbody2D rgb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = false;
        rgb.isKinematic = false;

        rgb.AddForce(direction * 10f, ForceMode2D.Impulse);
        IsThrowingNow = true;
        Invoke("RemoveParent", 1f);
    }
    public virtual void Miss()
    {
        Rigidbody2D rgb = GetComponent<Rigidbody2D>();
        RemoveParent();
        rgb.isKinematic = false;
        rgb.AddForce(Vector2.up * 15f,ForceMode2D.Impulse);
        GetComponent<Collider2D>().isTrigger = false;
        if(CanCatchParts !=null)
        CanCatchParts.gameObject.SetActive(true);
    }
    protected void RemoveParent()
    {
        if (PlayerWeaponController != null)
            PlayerWeaponController.RemoveWeapon(this);
        PlayerWeaponController = null;
        IsHandleNow = false;
        transform.parent = null;
        IsThrowingNow = false;
    }
    public virtual void Catch()
    {
        IsHandleNow = true;
        IsThrowingNow = false;
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
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsThrowingNow)
        {
            Debug.Log("<color=orange><b> ISTHROWINGNOW </b></color>");
            if (collision.collider.tag == "Damagable")
            {
                Debug.Log("<color=orange><b> collision name :  </b></color>" + collision.gameObject.name);
                ICanGetDamage damagedObject = collision.collider.gameObject.GetComponent<ICanGetDamage>();
                Attack(damagedObject);
                Debug.Log("<color=orange><b> ISTHROWINGNOW COL</b></color>");
            }
        }
    }
}

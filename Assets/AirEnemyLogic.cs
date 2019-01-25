using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyLogic : MonoBehaviour
{
    public float health;
    public ParticleSystem DieVFX;

    public void GetDamage(float damage)
    {
        Debug.Log("<color=orange><b> ENEMY </b></color>" + gameObject.name + "GET DMG " + damage);
        health -= damage;
        if (health < 0)
            Die();
    }
    private void Die()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        DieVFX.Emit(10);
    }
}

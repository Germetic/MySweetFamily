﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public PlayerStatsDisplayer PlayerStatsDisplayer;
    public PlayerWeaponController PlayerWeaponController;
    public float MaxHealth;
    public float CurrentHealth;
    public BodyPart[] BodyParts;
    public ParticleSystem GetDamageVFX;

    private Rigidbody2D rgb2d;
    //JustDelateItLater
    [Space]
    public GameObject HEADIKOBJECT;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        rgb2d = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(float damage,Vector3 VFXposition,Transform damageSource)
    {
        CurrentHealth -= damage;
        GetDamageVFX.transform.position = VFXposition;
        GetDamageVFX.Emit(1);
        if (damage > MaxHealth / 4)
            PlayerWeaponController.MissAllWeapons();
        if (CurrentHealth <= 0)
            Kill();
        PlayerStatsDisplayer.UpdateHealthBar();

        Vector2 directionVector = transform.position - damageSource.position;
        Debug.Log(directionVector.x + " " + directionVector.y + " " + directionVector.magnitude);
        Vector2 direction = new Vector2(0, 0);
        if(directionVector.magnitude != 0)
            direction = directionVector / directionVector.magnitude;
        FoldBack(damage, direction);
        AudioManager.Instance.Play("MenGetDamage");
    }

    public void Kill()
    {
        Debug.Log("<color=red><b> PlayerDie </b></color>");
        if(PlayerWeaponController.IsFirstPlayer)
        {
            GlobalManager.Instance.PlayerTwoScore += 10;
        } else
        {
            GlobalManager.Instance.PlayerOneScore += 10;
        }
        PlayerStatsDisplayer.DisplayAsDead();
        GetComponent<SpriteRenderer>().color = Color.gray;
        HEADIKOBJECT.GetComponent<CircleCollider2D>().isTrigger = false;
        HEADIKOBJECT.GetComponent<Rigidbody2D>().isKinematic = false;
        StartCoroutine(TimerToScore());
    }

    private IEnumerator TimerToScore()
    {
        GlobalManager.Instance.IsScoreShowed = true;
        Camera.main.GetComponent<Animator>().enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        FinalScore.Instance.ShowScore();
    }

    public bool IsCurrentPlayerBodyPart(ICanGetDamage bodyPart)
    {
        bool isCurrentPlayerBodyPart = false;

        foreach(ICanGetDamage part in BodyParts)
        {
            if (part == bodyPart)
            {
                isCurrentPlayerBodyPart = true;
                Debug.Log("<color=orange><b> CURR </b></color>");
            }
                
        }
        
        return isCurrentPlayerBodyPart;
    }

    public void FoldBack(float power,Vector2 direction)
    {
        Debug.Log("<color=orange><b> DIR </b></color>" + direction);
       // rgb2d.AddForce(direction * power * 1000, ForceMode2D.Impulse);
        rgb2d.velocity += new Vector2(direction.x * power * 10000, direction.y + 0.1f * power * 10000);
    }
}

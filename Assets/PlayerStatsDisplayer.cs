using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplayer : MonoBehaviour
{
    public Image HealthImg;
    public PlayerStateController PlayerStateController;

    public void UpdateHealthBar()
    {
        HealthImg.fillAmount = PlayerStateController.CurrentHealth / PlayerStateController.MaxHealth;
    }

    public void DisplayAsDead()
    {
        gameObject.SetActive(false);
    }
}

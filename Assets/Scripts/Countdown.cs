using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public Image BlackScreen;
    public Text CountdownTxt;
    public float Delay = 0.4f;
    public float CrossFadeTime = 1;

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        GlobalManager.Instance.IsCountDownEnded = false;
        BlackScreen.gameObject.SetActive(true);
        CountdownTxt.text = "3";
        CountdownTxt.gameObject.SetActive(false);
        BlackScreen.color = new Color(0, 0, 0, 1);
        StartCoroutine(SetCountdown());
        
    }

    private IEnumerator SetCountdown()
    {
        BlackScreen.CrossFadeAlpha(0, CrossFadeTime, false);
        yield return new WaitForSecondsRealtime(CrossFadeTime);
        CountdownTxt.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(Delay);
        CountdownTxt.text = "2";
        yield return new WaitForSecondsRealtime(Delay);
        CountdownTxt.text = "1";
        yield return new WaitForSecondsRealtime(Delay);
        CountdownTxt.text = "GO";
        yield return new WaitForSecondsRealtime(Delay);
        CountdownTxt.gameObject.SetActive(false);
        GlobalManager.Instance.IsCountDownEnded = true;
        GameEvents.Instance.OnCountdownEnd.Invoke();

    }
    
}

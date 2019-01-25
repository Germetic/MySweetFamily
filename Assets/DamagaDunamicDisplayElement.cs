using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagaDunamicDisplayElement : MonoBehaviour
{
    public Text DamageText;
    public float Dispersion;
    public float High;

    public void Initialize(int damageToDisplay)
    {
        DamageText.CrossFadeAlpha(0f, 1, false);
        DamageText.text = damageToDisplay.ToString();
        StartCoroutine(DecrementScaleAndPosition());
    }

    private IEnumerator DecrementScaleAndPosition()
    {
        Vector3 finalPos = new Vector3(Random.Range(transform.position.x - Dispersion, transform.position.x + Dispersion),transform.position.y + High, transform.position.z);
        float t = 0;
        while(t < 1)
        {
            DamageText.transform.localScale = Vector3.Lerp(DamageText.transform.lossyScale,Vector3.zero,t);
            DamageText.transform.position = Vector3.Lerp(DamageText.transform.position, finalPos, t);
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
            yield return null;
        }
        Destroy(gameObject);
    }
}

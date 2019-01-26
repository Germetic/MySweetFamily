using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public Vector3 ScoreCameraPosition;
    public Transform PlayerOnePlatform;
    public Transform PlayerTwoPlatform;

    public Transform PlayerOneSpawnPoint;
    public Transform PlayerTwoSpawnPoint;

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public float PlatformGrowDelay;

    private float _maxScoreLenght = 8;
    private float _maxScore = 50;


    public void ShowScore()
    {
        PlayerOnePlatform.localScale = new Vector3(PlayerOnePlatform.localScale.x,0.1f, PlayerOnePlatform.localScale.z);
        PlayerTwoPlatform.localScale = new Vector3(PlayerTwoPlatform.localScale.x, 0.1f, PlayerTwoPlatform.localScale.z);
        GlobalManager.Instance.IsScoreShowed = true;
        //GameEvents.Instance.OnGameOver.Invoke();
        StartCoroutine(MoveCamera(ScoreCameraPosition));

    }

    private IEnumerator MoveCamera(Vector3 endPosition)
    {
        Vector3 startPosition = MenuManager.Instance.MainCamera.transform.position;
        while (MenuManager.Instance.MainCamera.transform.position != endPosition)
        {
            MenuManager.Instance.MainCamera.transform.position = Vector3.Lerp(MenuManager.Instance.MainCamera.transform.position, endPosition, MenuManager.Instance.CameraMoveSpeed);
            if(Vector3.Distance(MenuManager.Instance.MainCamera.transform.position, endPosition) < 0.05f)
                MenuManager.Instance.MainCamera.transform.position =endPosition;
            yield return new WaitForFixedUpdate();
        }

        PlayerOne.transform.position = PlayerOneSpawnPoint.position;
        PlayerTwo.transform.position = PlayerTwoSpawnPoint.position;

        StartCoroutine(GrowPlatform());
    }

    private float CalculatePlatformHeight( int score)
    {
        return score * _maxScoreLenght / _maxScore;
    }

    private IEnumerator GrowPlatform()
    {
       
        float _firstPlatformHeight = CalculatePlatformHeight(GlobalManager.Instance.PlayerOneScore);
        float _secondPlatformHeight = CalculatePlatformHeight(GlobalManager.Instance.PlayerTwoScore);
        
        while ( PlayerOnePlatform.localScale.y != _firstPlatformHeight && PlayerTwoPlatform.localScale .y != _secondPlatformHeight)
        {
            Vector3 _newPlatformOneScale = new Vector3(PlayerOnePlatform.localScale.x, _firstPlatformHeight, PlayerOnePlatform.localScale.z);
            Vector3 _newPlatformTwoScale = new Vector3(PlayerTwoPlatform.localScale.x, _secondPlatformHeight, PlayerTwoPlatform.localScale.z);
            PlayerOnePlatform.localScale = Vector3.Lerp(PlayerOnePlatform.localScale, _newPlatformOneScale, PlatformGrowDelay);
            PlayerTwoPlatform.localScale = Vector3.Lerp(PlayerTwoPlatform.localScale, _newPlatformTwoScale, PlatformGrowDelay);
            if (Vector3.Distance(PlayerOnePlatform.localScale, _newPlatformOneScale) < 0.05f)
                PlayerOnePlatform.localScale = _newPlatformOneScale;
            if (Vector3.Distance(PlayerTwoPlatform.localScale, _newPlatformTwoScale) < 0.05f)
                PlayerTwoPlatform.localScale = _newPlatformTwoScale;
            yield return new WaitForFixedUpdate();
        }

    }
}

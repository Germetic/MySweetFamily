using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public static FinalScore Instance;

    public Vector3 ScoreCameraPosition;
    public float ScoreCameraSize = 25;
    public Transform PlayerOnePlatform;
    public Transform PlayerTwoPlatform;

    public Transform PlayerOneSpawnPoint;
    public Transform PlayerTwoSpawnPoint;

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public float PlatformGrowDelay;
    public float ShowScoreTime = 2;

    public float MaxScoreLenght = 8;

    public Camera CurrentCamera;
    private float _maxScore = 50;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


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
        Vector3 startPosition = CurrentCamera.transform.position;
        bool isDone = false;
        while (!isDone)
        {
            CurrentCamera.transform.position = Vector3.Lerp(CurrentCamera.transform.position, endPosition, MenuManager.Instance.CameraMoveSpeed);
            CurrentCamera.orthographicSize = Mathf.Lerp(CurrentCamera.orthographicSize, ScoreCameraSize, MenuManager.Instance.CameraMoveSpeed);
            if (Vector3.Distance(CurrentCamera.transform.position, endPosition) < 0.05f)
                CurrentCamera.transform.position =endPosition;
            if (Mathf.Abs(CurrentCamera.orthographicSize - ScoreCameraSize) < 0.05f)
                CurrentCamera.orthographicSize = ScoreCameraSize;

            if (CurrentCamera.transform.position == endPosition && CurrentCamera.orthographicSize == ScoreCameraSize)
                isDone = true;
            yield return new WaitForFixedUpdate();
        }

        PlayerOne.transform.position = PlayerOneSpawnPoint.position;
        PlayerTwo.transform.position = PlayerTwoSpawnPoint.position;

        StartCoroutine(GrowPlatform());
    }

    private float CalculatePlatformHeight(int score)
    {
        float finalScore = 0.1f;
        if(score > 0)
            finalScore = score * MaxScoreLenght / _maxScore;
        return finalScore;

    }

    private IEnumerator GrowPlatform()
    {
       
        float _firstPlatformHeight = CalculatePlatformHeight(GlobalManager.Instance.PlayerOneScore);
        float _secondPlatformHeight = CalculatePlatformHeight(GlobalManager.Instance.PlayerTwoScore);
        float t = PlatformGrowDelay;
        bool isDone = false;

        while ( !isDone)
        {
            Vector3 _newPlatformOneScale = new Vector3(PlayerOnePlatform.localScale.x, _firstPlatformHeight, PlayerOnePlatform.localScale.z);
            Vector3 _newPlatformTwoScale = new Vector3(PlayerTwoPlatform.localScale.x, _secondPlatformHeight, PlayerTwoPlatform.localScale.z);
            PlayerOnePlatform.localScale = Vector3.Lerp(PlayerOnePlatform.localScale, _newPlatformOneScale, PlatformGrowDelay);
            PlayerTwoPlatform.localScale = Vector3.Lerp(PlayerTwoPlatform.localScale, _newPlatformTwoScale, PlatformGrowDelay );
            if (Vector3.Distance(PlayerOnePlatform.localScale, _newPlatformOneScale) < 0.05f)
                PlayerOnePlatform.localScale = _newPlatformOneScale;
            if (Vector3.Distance(PlayerTwoPlatform.localScale, _newPlatformTwoScale) < 0.05f)
                PlayerTwoPlatform.localScale = _newPlatformTwoScale;
            
            if(PlayerOnePlatform.localScale.y == _firstPlatformHeight && PlayerTwoPlatform.localScale.y == _secondPlatformHeight)
            {
                isDone = true;
            }

            yield return new WaitForFixedUpdate();
        }
        

        yield return new WaitForSecondsRealtime(ShowScoreTime);

        if(GlobalManager.Instance.PlayerOneScore >= GlobalManager.Instance.MaxScore || GlobalManager.Instance.PlayerTwoScore >= GlobalManager.Instance.MaxScore)
        {
            GlobalManager.Instance.CurrentLocation = ScenesNumbers.Menu;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            GlobalManager.Instance.ClearData();
        }
        else
        {
            LoadNewLevel();
        }
        GlobalManager.Instance.IsScoreShowed = false;


    }

    private void LoadNewLevel()
    {
        ScenesNumbers _scene = ScenesNumbers.Menu;
        if (GlobalManager.Instance.CurrentLocation == ScenesNumbers.Kitchen)
        {
            _scene = ScenesNumbers.Hall;
        }
        else
        {
            _scene = ScenesNumbers.Kitchen;
        }
        GlobalManager.Instance.CurrentLocation = _scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)_scene);
    }
}

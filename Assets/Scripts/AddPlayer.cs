using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AddPlayer : MonoBehaviour
{
    

    public bool IsPl1Ready = false;
    public bool IsPl2Ready = false;

    public List<GameObject> PlayerOneModels;
    public List<GameObject> PlayerTwoModels;

    public List<Vector3> PlayerModelSpawnPoint;

    public List<TextMeshPro> PlayerStatusTxt;
    private string _readyTxt = "Ready";
    private string _pressA = "Press A";

    private int _currentPl1 = 0;
    private int _currentPl2 = 0;

    private bool _wasTriggerPlayerOne = false;
    private bool _wasTriggerPlayerTwo = false;
    
    
    private void OnEnable()
    {
        IsPl1Ready = IsPl2Ready = false;
        _currentPl1 = _currentPl2 = 0;
        foreach(GameObject model in PlayerOneModels)
        {
            model.SetActive(false);
        }
        foreach (GameObject model in PlayerTwoModels)
        {
            model.SetActive(false);
        }
        PlayerStatusTxt[0].text = PlayerStatusTxt[1].text = _pressA;
        PlayerOneModels[_currentPl1].SetActive(true);
        PlayerTwoModels[_currentPl2].SetActive(true);

    }

    // Update is called once per frame
    private void Update()
    {
        if (MenuManager.Instance.CurrentMenuState == MenuState.ChoosePlayer)
        {
            if (!IsPl1Ready)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    if (!_wasTriggerPlayerOne)
                        SwitchPlayerModel(true, Input.GetAxis("Vertical"));
                }
                else
                {
                    if (_wasTriggerPlayerOne)
                        _wasTriggerPlayerOne = false;
                }
            }

            if (!IsPl2Ready)
            {
                if (Input.GetAxis("VerticalJ2") != 0)
                {
                    if (!_wasTriggerPlayerTwo)
                        SwitchPlayerModel(false, Input.GetAxis("VerticalJ2"));
                }
                else
                {
                    if (_wasTriggerPlayerTwo)
                        _wasTriggerPlayerTwo = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                SetPlayerReady(true);
            }
            if (Input.GetKeyDown(KeyCode.Joystick2Button2))
            {
                SetPlayerReady(false);
            }
        }
    }

    private void SetPlayerReady(bool isFirstPlayer)
    {
        if(isFirstPlayer)
        {
            IsPl1Ready = !IsPl1Ready;
                PlayerStatusTxt[0].text = IsPl1Ready ? _readyTxt : _pressA;
            GlobalManager.Instance.PlayerOneModel = _currentPl1;
        } else
        {
            IsPl2Ready = !IsPl2Ready;
            PlayerStatusTxt[1].text = IsPl2Ready ? _readyTxt : _pressA;
            GlobalManager.Instance.PlayerTwoModel = _currentPl2;
        }
        CheckIsPlayerReady();
    }

    private void SwitchPlayerModel(bool isFirstPlayer, float axis)
    {
        int _tmpIndex;
        List<GameObject> _tmpList;
        Vector3 _tmpSpawnPoint;
        if (isFirstPlayer)
        {
            _wasTriggerPlayerOne = true;
            _tmpList = PlayerOneModels;
            _tmpIndex = _currentPl1;
            _tmpSpawnPoint = PlayerModelSpawnPoint[0];
        }
        else
        {
            _wasTriggerPlayerTwo = true;
            _tmpList = PlayerTwoModels;
            _tmpIndex = _currentPl2;
            _tmpSpawnPoint = PlayerModelSpawnPoint[1];
        }

        _tmpList[_tmpIndex].SetActive(false);



        if (axis > 0)
        {
            _tmpIndex--;
            if (_tmpIndex < 0)
            {
                _tmpIndex = _tmpList.Count - 1;
            }
        } else
        {
            _tmpIndex++;
            if (_tmpIndex > _tmpList.Count - 1)
            {
                _tmpIndex = 0;
            }
        }

        _tmpList[_tmpIndex].transform.position = _tmpSpawnPoint;
        _tmpList[_tmpIndex].SetActive(true);
        if (isFirstPlayer)
        {
            _currentPl1 = _tmpIndex;
        }
        else
        {
            _currentPl2 = _tmpIndex;
        }
    }

    private void CheckIsPlayerReady()
    {
        if(IsPl1Ready && IsPl2Ready)
        {
            ScenesNumbers _scene = (ScenesNumbers)Random.Range((int)ScenesNumbers.Kitchen, (int)ScenesNumbers.Kitchen);
            GlobalManager.Instance.CurrentLocation = _scene;
            SceneManager.LoadScene((int)_scene);
        }
    }
}

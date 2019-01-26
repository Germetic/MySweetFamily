using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseMenuItem : MonoBehaviour
{

    public List<TextMeshPro> MenuItems;
    public Color DefaultMenuColor;
    public Color SelectedMenuColor;
    private int _currentMenuItem = 0;
    private Coroutine _cameraMove;

    public bool WasTriggerOne = false;

    private void OnEnable()
    {
        _currentMenuItem = 0;
        foreach(TextMeshPro txt in MenuItems)
        {
            txt.color = DefaultMenuColor;
        }
        if(MenuItems.Count > 0)
        {
            MenuItems[_currentMenuItem].color = SelectedMenuColor;
        }
    }

    private void Update()
    {
        if (MenuManager.Instance.CurrentMenuState == MenuState.StartGame)
        {
            if (Input.GetAxis("AllVertical") != 0)
            {
                if(!WasTriggerOne)
                    SwitchMenuButton(Input.GetAxis("AllVertical"));
            } else
            {
                if (WasTriggerOne)
                    WasTriggerOne = false;
            }

            if(Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                ChooseCurrentMenuItem();
            }
        }else
        {
            if(MenuManager.Instance.CurrentMenuState == MenuState.ChoosePlayer)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton10))
                {
                    ChooseCurrentMenuItem();
                }
            }
        }
    }

    private void SwitchMenuButton(float axis)
    {
        WasTriggerOne = true;
        MenuItems[_currentMenuItem].color = DefaultMenuColor;
        if(axis > 0)
        {
            _currentMenuItem--;
            if(_currentMenuItem < 0)
            {
                _currentMenuItem = MenuItems.Count - 1;
            }
        } else
        {
            _currentMenuItem++;
            if(_currentMenuItem > MenuItems.Count -1)
            {
                _currentMenuItem = 0;
            }
        }
        MenuItems[_currentMenuItem].color = SelectedMenuColor;

    }

    private void ChooseCurrentMenuItem()
    {
        switch(_currentMenuItem)
        {
            case 0:
                SelectPlayer();
            break;

            case 1:
                Quit();
            break;
        }
    }

    private void SelectPlayer()
    {
        Debug.Log("SelectPlayer");
        if (MenuManager.Instance.CurrentMenuState == MenuState.ChoosePlayer)
        {
            MenuManager.Instance.CurrentMenuState = MenuState.StartGame;
            if (_cameraMove != null)
            {
                StopCoroutine(_cameraMove);
                _cameraMove = null;
            }

            _cameraMove = StartCoroutine(MoveCamera(MenuManager.Instance.MenuCameraPosition));
        } else
        {
            MenuManager.Instance.CurrentMenuState = MenuState.ChoosePlayer;
            if (_cameraMove != null)
            {
                StopCoroutine(_cameraMove);
                _cameraMove = null;
            }

            _cameraMove = StartCoroutine(MoveCamera(MenuManager.Instance.SelectPlayerCameraPosition));
        }
        //MenuManager.Instance.MainCamera.transform.position = MenuManager.Instance.SelectPlayerCameraPosition;
        
    }

    private IEnumerator MoveCamera(Vector3 endPosition)
    {
        Vector3 startPosition = MenuManager.Instance.MainCamera.transform.position;
        while (MenuManager.Instance.MainCamera.transform.position != endPosition)
        {
            MenuManager.Instance.MainCamera.transform.position = Vector3.Lerp(MenuManager.Instance.MainCamera.transform.position, endPosition, MenuManager.Instance.CameraMoveSpeed);
            yield return new WaitForFixedUpdate();
        }
            _cameraMove = null;
    }

    private void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
}

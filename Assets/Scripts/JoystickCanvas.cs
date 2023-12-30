using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCanvas : MonoBehaviour
{
    public static JoystickCanvas Instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        if (!GameInput.Instance.IsMobilePlatform())
        {
            gameObject.SetActive(false);

        }

    }

    private void OnDestroy()
    {

        Instance = null;
    }




    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
       if (GameInput.Instance.IsMobilePlatform())
        {
            gameObject.SetActive(true);
        }
        
    }



}

   

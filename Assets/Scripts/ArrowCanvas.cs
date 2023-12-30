using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ArrowCanvas : MonoBehaviour
{

    [SerializeField] RectTransform arrow;
    private GameObject gem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGem(GameObject currentGem)
    {
        gem = currentGem;
    }
    // Update is called once per frame
    void Update()
    {
        if (gem)
        {
            Vector3 toPostion = gem.transform.position;
            Vector3 fromPostion = Camera.main.transform.position;
            Vector3 forward = Camera.main.transform.forward;
            fromPostion.z = 0f;

            Vector3 dir = (toPostion - fromPostion).normalized;

            float angel = AngelFromDir(dir);
            arrow.localEulerAngles = new Vector3(0, 0, -angel);

            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(toPostion);
            float borderSize = 100f;

            bool isOffScreen = targetPositionScreenPoint.x < borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y < borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
            //Debug.Log(isOffScreen + "   " + targetPositionScreenPoint);

            if (isOffScreen)
            {
                arrow.gameObject.SetActive(true);
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x < borderSize) { cappedTargetScreenPosition.x = borderSize; }
                if (cappedTargetScreenPosition.x > Screen.width - borderSize) { cappedTargetScreenPosition.x = Screen.width - borderSize; }
                if (cappedTargetScreenPosition.y < borderSize) { cappedTargetScreenPosition.y = borderSize; }
                if (cappedTargetScreenPosition.y > Screen.height - borderSize) { cappedTargetScreenPosition.y = Screen.height - borderSize; }

                Vector3 pointWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
                arrow.position = pointWorldPosition;
                arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y, 0f);
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
        }

        
       
    }

    private float AngelFromDir(Vector3 dir)
    {
        dir = dir.normalized;
        float ang = Mathf.Asin(dir.x) * Mathf.Rad2Deg;

        if (dir.y < 0)
        {
            ang = 180 - ang;
        }
        else
        {
            if(dir.x< 0)
            {
                ang = 360 + ang;
            }
        }
        return ang;
    }
}

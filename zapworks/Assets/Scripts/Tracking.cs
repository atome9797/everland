using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

public class Tracking : MonoBehaviour
{
    //현재 메뉴명
    string _CurrentMenu;

    [SerializeField]
    GameObject _PlaceTrackingObject; //바닥 트래킹
    [SerializeField]
    GameObject _FaceTrackingObject; //얼굴 트래킹
    [SerializeField]
    GameManager _GameManager; //게임 매니저

    [SerializeField]
    GameObject[] _ActiveTrackingType;

    private void Update()
    {
        PlaceTrackingPositioning();
    }

    /// <summary>
    /// 바닥 트래킹 앵커 초기화
    /// </summary>
    public void ResetTrackingAchor()
    {
        _ActiveTrackingType[(int)trackingType.PlaceTracking].GetComponent<ZapparInstantTrackingTarget>().ResetTrackerAnchor();
    }

    /// <summary>
    /// 트래킹시 각 타입별 세팅
    /// </summary>
    public void TrackingTask(string selectMenu)
    {
        _CurrentMenu = selectMenu;

        if(_GameManager._CategoryMap[selectMenu][0].ToString() == "FaceTracking")
        {
        }

    }

    /// <summary>
    /// 트래킹 타입 변경
    /// </summary>
    /// <param name="trackingName"></param>
    public void ActiveType(string trackingName)
    {
        for (int i = 0; i < _ActiveTrackingType.Length; i++)
        {
            _ActiveTrackingType[i].SetActive(false);
            if (_ActiveTrackingType[i].name == trackingName)
            {
                _ActiveTrackingType[i].SetActive(true);

                //게임오브젝트 활성화인 것의 앵커 교체
                ZapparCamera.Instance.AnchorOrigin = _ActiveTrackingType[i].GetComponent<ZapparTrackingTarget>();
            }
        }
    }


    /// <summary>
    /// 바닥 트래킹 초기화
    /// </summary>
    public void Reset3DObject()
    {
        int childCount = _PlaceTrackingObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //바닥 트래킹일때 오브젝트 활성화 및 포지셔닝
    public void PlaceTrackingPositioning()
    {
        if (_GameManager._CategoryMap[_CurrentMenu][0].ToString() == "PlaceTracking" && Input.GetMouseButtonDown(0))
        {
            //1. 오브젝트 활성화
            int childCount = _PlaceTrackingObject.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(false);

                if (_PlaceTrackingObject.transform.GetChild(i).gameObject.name == _CurrentMenu)
                {
                    _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(true);

                    //2. 오브젝트 raycast 포지셔닝
                    //오브젝트 정보를 담을 변수 생성
                    RaycastHit hit;

                    //터치 좌표를 담는 변수
                    Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    //터치한 곳에 ray를 보냄
                    Physics.Raycast(touchray, out hit);

                    if (hit.collider != null)
                    {
                        _PlaceTrackingObject.transform.GetChild(i).transform.position = hit.point;
                    }

                    _PlaceTrackingObject.transform.GetChild(i).GetComponent<Animation>().Play();
                }
            }

        }
    }

}

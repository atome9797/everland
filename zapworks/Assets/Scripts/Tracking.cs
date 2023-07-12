using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

public class Tracking : MonoBehaviour
{
    //���� �޴���
    string _CurrentMenu;

    [SerializeField]
    GameObject _PlaceTrackingObject; //�ٴ� Ʈ��ŷ
    [SerializeField]
    GameObject _FaceTrackingObject; //�� Ʈ��ŷ
    [SerializeField]
    GameManager _GameManager; //���� �Ŵ���

    [SerializeField]
    GameObject[] _ActiveTrackingType;

    private void Update()
    {
        PlaceTrackingPositioning();
    }

    /// <summary>
    /// �ٴ� Ʈ��ŷ ��Ŀ �ʱ�ȭ
    /// </summary>
    public void ResetTrackingAchor()
    {
        _ActiveTrackingType[(int)trackingType.PlaceTracking].GetComponent<ZapparInstantTrackingTarget>().ResetTrackerAnchor();
    }

    /// <summary>
    /// Ʈ��ŷ�� �� Ÿ�Ժ� ����
    /// </summary>
    public void TrackingTask(string selectMenu)
    {
        _CurrentMenu = selectMenu;

        if(_GameManager._CategoryMap[selectMenu][0].ToString() == "FaceTracking")
        {
        }

    }

    /// <summary>
    /// Ʈ��ŷ Ÿ�� ����
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

                //���ӿ�����Ʈ Ȱ��ȭ�� ���� ��Ŀ ��ü
                ZapparCamera.Instance.AnchorOrigin = _ActiveTrackingType[i].GetComponent<ZapparTrackingTarget>();
            }
        }
    }


    /// <summary>
    /// �ٴ� Ʈ��ŷ �ʱ�ȭ
    /// </summary>
    public void Reset3DObject()
    {
        int childCount = _PlaceTrackingObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //�ٴ� Ʈ��ŷ�϶� ������Ʈ Ȱ��ȭ �� �����Ŵ�
    public void PlaceTrackingPositioning()
    {
        if (_GameManager._CategoryMap[_CurrentMenu][0].ToString() == "PlaceTracking" && Input.GetMouseButtonDown(0))
        {
            //1. ������Ʈ Ȱ��ȭ
            int childCount = _PlaceTrackingObject.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(false);

                if (_PlaceTrackingObject.transform.GetChild(i).gameObject.name == _CurrentMenu)
                {
                    _PlaceTrackingObject.transform.GetChild(i).gameObject.SetActive(true);

                    //2. ������Ʈ raycast �����Ŵ�
                    //������Ʈ ������ ���� ���� ����
                    RaycastHit hit;

                    //��ġ ��ǥ�� ��� ����
                    Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    //��ġ�� ���� ray�� ����
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

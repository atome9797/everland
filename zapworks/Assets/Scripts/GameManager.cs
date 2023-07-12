using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

enum trackingType
{
    NoneTracking,
    FaceTracking,
    PlaceTracking
}

public class GameManager : MonoBehaviour
{
    //������ ��Ƶ� �������� ���
    public Dictionary<string, List<string>> _CategoryMap;


    [SerializeField]
    Tracking _Tracking;

    [SerializeField]
    BackgroundManager _BackgroundManager;
    
    [SerializeField]
    ImageVideoPlayer _VideoPlayer;

    [SerializeField]
    ZSNSTest _SnapShotAndShare;

    int test = 0;
    private void Awake()
    {
        _CategoryMap = new Dictionary<string, List<string>>();
    }

    void Start()
    {
        trackingTypeSetting();
    }


    /// <summary>
    /// ĸó ��ư �������� �̺�Ʈ
    /// </summary>
    public void StartCapture()
    {
        _SnapShotAndShare.TakeSnapshot();
    }

    /// <summary>
    /// ī�޶� ��ȯ ��ư �������� �̺�Ʈ
    /// </summary>
    /// <param name="cameraType"></param>
    public void CameraSwap(string cameraType)
    {
        object tmp = (cameraType == "front") ? ZapparCamera.Instance.SwitchToFrontCameraMode() : ZapparCamera.Instance.SwitchToRearCameraMode();
    }

    private void ClearTracking()
    {
        //���� �÷��̾� �ʱ�ȭ
        _VideoPlayer.ResetVideoPlayer();

        //3d ������Ʈ �ʱ�ȭ
        _Tracking.Reset3DObject();

        //�ٴ� Ʈ��ŷ ��Ŀ �ʱ�ȭ
        _Tracking.ResetTrackingAchor();

        //1. Nonetracking ������� ����
        _Tracking.ActiveType("NoneTracking");

        //2. Canvas Background ��Ȱ��ȭ
        _BackgroundManager.BackgroundTypeSetting("");

        //3. Sound Manager �Ҹ� stop
        SoundManager._instance.GetComponent<SoundManager>().PlaySound("");
    }

    /// <summary>
    /// �޴� ��ư �������� �̺�Ʈ
    /// </summary>
    /// <param name="name"></param>
    public void SelectMenuEvent(string name)
    {
        //�ɼ� �ʱ�ȭ
        ClearTracking();

        //Ȱ��ȭ ��ų Ʈ��ŷ ���ӿ�����Ʈ
        _Tracking.ActiveType(_CategoryMap[name][0].ToString());

        _Tracking.TrackingTask(name);

        //������� ���
        SoundManager._instance.GetComponent<SoundManager>().PlaySound(name);

        //��� ����
        _BackgroundManager.BackgroundTypeSetting(name);
    }




    public void trackingTypeSetting()
    {
        _CategoryMap.Add("MonsterCleanup", new List<string> { "FaceTracking" , "asd"});
        _CategoryMap.Add("Parade", new List<string> { "FaceTracking", "asd" });
        _CategoryMap.Add("Fireworks", new List<string> { "NonTracking", "asd" });
        _CategoryMap.Add("LennyAndFriends", new List<string> { "PlaceTracking", "asd" });
        _CategoryMap.Add("PandaWithPose", new List<string> { "PlaceTracking", "asd" });
        _CategoryMap.Add("RedPandaWithPose", new List<string> { "PlaceTracking", "asd" });
        _CategoryMap.Add("WalkingTiger", new List<string> { "PlaceTracking", "asd" });
        _CategoryMap.Add("AmazonFilter", new List<string> { "FaceTracking", "asd" });
        _CategoryMap.Add("TexpressFilter", new List<string> { "FaceTracking", "asd" });
        _CategoryMap.Add("FairyTownFilter", new List<string> { "FaceTracking", "asd" });
    }

    /// <summary>
    /// �׽�Ʈ
    /// </summary>
    void switchTest()
    {
        test = (test + 1) % 4;
        if (test == 0)
        {
            SelectMenuEvent("MonsterCleanup");
        }
        else if (test == 1)
        {
            SelectMenuEvent("Parade");
        }
        else if (test == 2)
        {
            SelectMenuEvent("Fireworks");
        }
        else if (test == 3)
        {
            SelectMenuEvent("TexpressFilter");
        }
    }

}

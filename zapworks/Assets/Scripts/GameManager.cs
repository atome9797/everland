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
    //데이터 담아둘 공간으로 사용
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
    /// 캡처 버튼 눌렀을때 이벤트
    /// </summary>
    public void StartCapture()
    {
        _SnapShotAndShare.TakeSnapshot();
    }

    /// <summary>
    /// 카메라 전환 버튼 눌렀을때 이벤트
    /// </summary>
    /// <param name="cameraType"></param>
    public void CameraSwap(string cameraType)
    {
        object tmp = (cameraType == "front") ? ZapparCamera.Instance.SwitchToFrontCameraMode() : ZapparCamera.Instance.SwitchToRearCameraMode();
    }

    private void ClearTracking()
    {
        //비디오 플레이어 초기화
        _VideoPlayer.ResetVideoPlayer();

        //3d 오브젝트 초기화
        _Tracking.Reset3DObject();

        //바닥 트래킹 앵커 초기화
        _Tracking.ResetTrackingAchor();

        //1. Nonetracking 방식으로 변경
        _Tracking.ActiveType("NoneTracking");

        //2. Canvas Background 비활성화
        _BackgroundManager.BackgroundTypeSetting("");

        //3. Sound Manager 소리 stop
        SoundManager._instance.GetComponent<SoundManager>().PlaySound("");
    }

    /// <summary>
    /// 메뉴 버튼 눌렀을때 이벤트
    /// </summary>
    /// <param name="name"></param>
    public void SelectMenuEvent(string name)
    {
        //옵션 초기화
        ClearTracking();

        //활성화 시킬 트래킹 게임오브젝트
        _Tracking.ActiveType(_CategoryMap[name][0].ToString());

        _Tracking.TrackingTask(name);

        //배경음악 재생
        SoundManager._instance.GetComponent<SoundManager>().PlaySound(name);

        //배경 설정
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
    /// 테스트
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

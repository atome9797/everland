using System.Collections.Generic;
using UnityEngine;


enum BackgroundContentType
{
    video,
    picture,
    none
}

public class BackgroundManager : MonoBehaviour
{

    public GameObject Background;
    private ImageVideoPlayer BackgroundVideoPlayer;

    Dictionary<string, BackgroundContentType> BackgroundMap = new Dictionary<string, BackgroundContentType>();

    // Start is called before the first frame update
    private void Awake()
    {
        BackgroundVideoPlayer = gameObject.GetComponent<ImageVideoPlayer>();
        BackgroundTrackingTypeSetting();
    }

    public void BackgroundTypeSetting(string trackingName)
    {
        Background.SetActive(false);
        SetBackground(trackingName);
    }


    //4. 백그라운드 있으면 true 및 세팅
    public void SetBackground(string trackingName)
    {

        if (trackingName == "")
        {
            //내부적으로 이미지 돌고 있기 때문에 코루틴 종료 시켜야함
            return;
        }

        if(BackgroundMap[trackingName] == BackgroundContentType.video)
        {
            //이미지 재생 
            Background.SetActive(true);
            //시퀀스로 재생하기
            BackgroundVideoPlayer.StartBackgroundVideoPlayer(trackingName);
        }
        else
        {
            //아닐땐 아무것도 실행안함
        }

    }


    public void BackgroundTrackingTypeSetting()
    {
        BackgroundMap.Add("MonsterCleanup", BackgroundContentType.video);
        BackgroundMap.Add("Parade", BackgroundContentType.video);
        BackgroundMap.Add("Fireworks", BackgroundContentType.video);
        BackgroundMap.Add("LennyAndFriends", BackgroundContentType.video);
        BackgroundMap.Add("PandaWithPose", BackgroundContentType.none);
        BackgroundMap.Add("RedPandaWithPose", BackgroundContentType.none);
        BackgroundMap.Add("WalkingTiger", BackgroundContentType.none);
        BackgroundMap.Add("AmazonFilter", BackgroundContentType.video);
        BackgroundMap.Add("TexpressFilter", BackgroundContentType.video);
        BackgroundMap.Add("FairyTownFilter", BackgroundContentType.video);
    }

}

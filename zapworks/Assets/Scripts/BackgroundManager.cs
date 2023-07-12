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


    //4. ��׶��� ������ true �� ����
    public void SetBackground(string trackingName)
    {

        if (trackingName == "")
        {
            //���������� �̹��� ���� �ֱ� ������ �ڷ�ƾ ���� ���Ѿ���
            return;
        }

        if(BackgroundMap[trackingName] == BackgroundContentType.video)
        {
            //�̹��� ��� 
            Background.SetActive(true);
            //�������� ����ϱ�
            BackgroundVideoPlayer.StartBackgroundVideoPlayer(trackingName);
        }
        else
        {
            //�ƴҶ� �ƹ��͵� �������
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

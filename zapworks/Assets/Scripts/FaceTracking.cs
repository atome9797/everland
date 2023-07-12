using UnityEngine;
using Zappar;

enum faceType
{
    facePaint,
    faceAttachment,
    all
}

enum faceRenderingType
{
    video,
    picture
}

enum gifType
{
    face,
    background
}


public class FaceTracking : MonoBehaviour
{

    public Material[] faceMaterials;

    public GameObject[] FaceRenderingType;

    public GameObject [] FaceType;
    public ImageVideoPlayer _FaceVideoPlayer;
    public BackgroundManager _BackgroundManager;

    public GameObject DepthMask;


    public void FaceTrackingTask(string trackingName)
    {
        //�۷��̵�
        if (trackingName == "Parade")
        {
            //4. depth ����ũ off
            DepthMask.SetActive(false);

            //2. type �������ֱ�
            FaceTypeSetting((int)faceType.all);
            FaceRenderingTypeSetting((int)faceRenderingType.video);

            //3. Parade url �ҷ�����
            _FaceVideoPlayer.StartFaceVideoPlayer(trackingName, 8, (int)faceType.facePaint);

            //4. Parade ���� �ҷ�����
            ActiveFaceAttachmentObject(trackingName);
        }
        else if(trackingName == "MonsterCleanup")
        {
            DepthMask.SetActive(true);

            //2. type �������ֱ�
            FaceTypeSetting((int)faceType.faceAttachment);

            //3. Parade url �ҷ�����
            ActiveFaceAttachmentObject(trackingName);
        }
        else if(trackingName == "AmazonFilter")
        {
            DepthMask.SetActive(false);

            //2. type �������ֱ�
            FaceTypeSetting((int)faceType.facePaint);
            FaceRenderingTypeSetting((int)faceRenderingType.picture);

            //3. Parade url �ҷ�����
            FacePaintPictureUrl(trackingName);
        }
        else if(trackingName == "FairyTownFilter")
        {
            DepthMask.SetActive(true);

            FaceTypeSetting((int)faceType.faceAttachment);

            ActiveFaceAttachmentObject(trackingName);
        }
        else if(trackingName == "TexpressFilter")
        {
            DepthMask.SetActive(false);
            FaceTypeSetting((int)faceType.faceAttachment);

            ActiveFaceAttachmentObject(trackingName);


            //���� ���
            _FaceVideoPlayer.StartFaceVideoPlayer(trackingName, 20, (int)faceType.faceAttachment);

        }

        //4. ��׶��� ������ true �� ����
        _BackgroundManager.BackgroundTypeSetting(trackingName);
    }


    //3. face attachment �ش� ������Ʈ�� Ȱ��ȭ ��Ű��
    public void ActiveFaceAttachmentObject(string name)
    {
        int childCount = FaceType[(int)faceType.faceAttachment].transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            FaceType[(int)faceType.faceAttachment].transform.GetChild(i).gameObject.SetActive(false);

            if (FaceType[(int)faceType.faceAttachment].transform.GetChild(i).gameObject.name == name)
            {
                FaceType[(int)faceType.faceAttachment].transform.GetChild(i).gameObject.SetActive(true);

                if(name == "FairyTownFilter")
                {
                    FaceType[(int)faceType.faceAttachment].transform.GetChild(i).GetComponent<Animation>().Play();
                }
            }
        }
    }

    public void FacePaintPictureUrl(string trackingName)
    {
        for(int i = 0; i< faceMaterials.Length;i++)
        {
            if(faceMaterials[i].name == trackingName)
            {
                FaceRenderingType[(int)faceRenderingType.picture].GetComponent<MeshRenderer>().material = faceMaterials[i];
                FaceRenderingType[(int)faceRenderingType.picture].GetComponent<ZapparFaceMeshTarget>().FaceMaterial = faceMaterials[i];
            }
        }
    }

    

    //3. type �������ֱ�
    public void FaceRenderingTypeSetting(int index)
    {
        for (int i = 0; i < 2; i++)
        {
            FaceRenderingType[i].SetActive(false);

            if (index == i)
            {
                FaceRenderingType[i].SetActive(true);
            }
        }
    }

    public void FaceTypeSetting(int index)
    {
        for (int i = 0; i < 2; i++)
        {
            FaceType[i].SetActive(false);

            if (index == i || index == (int)faceType.all)
            {
                FaceType[i].SetActive(true);
            }
        }

    }


}

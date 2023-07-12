using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageVideoPlayer : MonoBehaviour
{
    private Queue<Texture2D> ImageSequence;

    private bool BackgroundImageActived;
    private bool [] faceTypeActived = new bool[2];
    public RawImage  background;
    public Material faceAttachment;
    public Material faceMesh;

    public LoadingManager SpriteBackground;
    Coroutine backgroundTemp;
    Coroutine faceAttachmentTemp;
    Coroutine faceMeshTemp;
    int num = 0;

    private void Awake()
    {
        ImageSequence = new Queue<Texture2D>();
    }

    private void Start()
    {
        //StartCoroutine(BackgroundAnimationImage());
        //StartCoroutine(FaceAttachmentAnimationImage());
        //StartCoroutine(FacePaintAnimationImage());
    }



    public void ResetVideoPlayer()
    {
        BackgroundImageActived = false;

        ImageSequence.Clear();


        for (int i = 0; i < 2; i++)
        {
            faceTypeActived[i] = false;
        }
        //idle �ִϸ��̼��� ����
        //if(background.activeSelf)
            //background.GetComponent<Animator>().SetInteger("Num", 0);


        Color color = background.color;
        color.a = 1f;
        background.color = color;
    }


    
    public void StartBackgroundVideoPlayer(string name)
    {

        //�ִϸ��̼��� ����Ѵ�.
        /*int num;
        switch (name)
        {
            case "AmazonFilter": num = 1; break;
            case "Fireworks": num = 2; break;
            case "FairyTownFilter": num = 3; break;
            case "LennyAndFriends": num = 4; break;
            case "Parade": num = 5; break;
            case "MonsterCleanup": num = 6; break;
            case "TexpressFilter": 
                num = 7; 
                Color color = background.GetComponent<Image>().color;
                color.a = 0.5f;
                background.GetComponent<Image>().color = color;
                break;
            default: num = 0;  break;
        }
        background.GetComponent<Animator>().SetInteger("Num", num);*/

        //�������� �ִϸ��̼� ����ϱ�
        switch (name)
        {
            case "AmazonFilter": num = 0; break;
            case "FairyTownFilter": num = 1; break;
            case "Fireworks": num = 2; break;
            case "LennyAndFriends": num = 3; break;
            case "MonsterCleanup": num = 4; break;
            case "Parade": num = 5; break;
            case "TexpressFilter":
                num = 6;
                Color color = background.color;
                color.a = 0.5f;
                background.color = color;
                break;
            default: num = 0; break;
        }


        SetBackgroundImageActived(true);
    }

    public void StartFaceVideoPlayer(string name, int index, int FaceType)
    {

        for (int i = 0; i < index; i++)
        {
            ImageSequence.Enqueue(Resources.Load<Texture2D>($"Textures/Face/{name}/{name}{i}"));
        }
        faceTypeActived[FaceType] = true;
    }

    

    private void SetBackgroundImageActived(bool check)
    {
        BackgroundImageActived = check;
    }

    private void Update()
    {
        if(BackgroundImageActived && backgroundTemp == null)
        {
            backgroundTemp = StartCoroutine(BackgroundAnimationImage());
        }
        else if (!BackgroundImageActived && backgroundTemp != null)
        {
            //�޸� �ּҸ� �Ҵ��� �����̱� ������ �ڷ�ƾ ������ ���� ����
            StopCoroutine(backgroundTemp);
            backgroundTemp = null;
        }


        if(faceTypeActived[(int)faceType.faceAttachment] && faceAttachmentTemp == null)
        {
            faceAttachmentTemp = StartCoroutine(FaceAttachmentAnimationImage());
        }
        else if(!faceTypeActived[(int)faceType.faceAttachment] && faceAttachmentTemp != null)
        {
            //�޸� �ּҸ� �Ҵ��� �����̱� ������ �ڷ�ƾ ������ ���� ����
            StopCoroutine(faceAttachmentTemp);
            faceAttachmentTemp = null;
        }

        if (faceTypeActived[(int)faceType.facePaint] && faceMeshTemp == null)
        {
            faceMeshTemp = StartCoroutine(FacePaintAnimationImage());
        }
        else if (!faceTypeActived[(int)faceType.facePaint] && faceMeshTemp != null)
        {
            //�޸� �ּҸ� �Ҵ��� �����̱� ������ �ڷ�ƾ ������ ���� ����
            StopCoroutine(faceMeshTemp);
            faceMeshTemp = null;
        }

    }
    


    IEnumerator BackgroundAnimationImage()
    {
        while (true)
        {
            Texture2D texture = SpriteBackground._SpriteBackground[num]._Texture.Dequeue();

            SpriteBackground._SpriteBackground[num]._Texture.Enqueue(texture);
            background.texture = texture;
            yield return new WaitForSeconds(0.06f);
        }
    }


    IEnumerator FaceAttachmentAnimationImage()
    {
        
        while (true)
        {

            Texture2D texture = ImageSequence.Dequeue();

            ImageSequence.Enqueue(texture);
            faceAttachment.SetTexture("_MainTex", texture);
            
            yield return new WaitForSeconds(0.06f);
        }
    }

    IEnumerator FacePaintAnimationImage()
    {

        while (true)
        {
            Texture2D texture = ImageSequence.Dequeue();

            ImageSequence.Enqueue(texture);

            faceMesh.SetTexture("_MainTex", texture);
            

            yield return new WaitForSeconds(0.06f);
        }
    }

}

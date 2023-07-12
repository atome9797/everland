using UnityEngine;
using UnityEngine.Networking;
using Piglet;
using System.Collections;
using System.Collections.Generic;
using System;
using Zappar;

public class LoadingManager : MonoBehaviour
{
    public ZapparCamera camera;

    public GameObject _GameManager;
    public GameObject _Tracking;
    public GameObject _FaceTracking;
    public GameObject _LoadingPage;

    //public sprite
    public SpriteData [] _SpriteBackground;

    public GameObject placeTracking;
    public GameObject faceTracking;

    public string[] objectName = { "FairyTownFilter", "LennyAndFriends", "PandaWithPose", "RedPandaWithPose", "WalkingTiger", "MonsterCleanup", "Parade" };

    public string[] backgroundName = { "AmazonFilter", "FairyTownFilter", "Fireworks", "LennyAndFriends", "MonsterCleanup", "Parade", "TexpressFilter" };

    public int version = 0;

    Coroutine temp1;
    Coroutine []temp2 = new Coroutine[7];
    private bool BackgroundImageActived1 = true;
    private bool [] BackgroundImageActived2 = new bool[7];
    int count = 0;

    private void Start()
    {
        //StartCoroutine(AssetBundleLoad("https://uxstory.github.io/everland/modelweb"));
        //StartCoroutine(AssetBundleLoad2("https://uxstory.github.io/everland/backweb"));

        //QueueSet();

    }

    private void Update()
    {
        if (BackgroundImageActived1 && temp1 == null)
        {
            //ī�޶� enable
            camera.enabled = false;
            _FaceTracking.SetActive(false);
            _GameManager.SetActive(false);
            _Tracking.SetActive(false);
            temp1 = StartCoroutine(AssetBundleLoad("https://uxstory.github.io/everland/model"));
        }
        else if (!BackgroundImageActived1 && temp1 != null)
        {
            StopCoroutine(temp1);
            temp1 = null;
        }

        for(int i = 0; i < backgroundName.Length; i++)
        {
            if (BackgroundImageActived2[i] && temp2[i] == null)
            {
                string LittleName = backgroundName[i].ToLower();
                temp2[i] = StartCoroutine(AssetBundleLoad2($"https://uxstory.github.io/everland/{LittleName}"));
            }
            else if (!BackgroundImageActived2[i] && temp2[i] != null)
            {
                StopCoroutine(temp2[i]);
                temp2[i] = null;
            }
        }

/*
        if (_Task.Count > 0)
        {
            _Task.Peek().MoveNext();
        }*/
    }

    //��� 1
    IEnumerator AssetBundleLoad(string LoadBundleURL)
    {
        yield return new WaitForEndOfFrame();

        // cache ������ AssetBundle�� ���� ���̹Ƿ� ĳ�̽ý����� �غ� �� ������ ��ٸ�
        while (!Caching.ready)
            yield return null;

        // ���¹����� ĳ�ÿ� ������ �ε��ϰ�, ������ �ٿ�ε��Ͽ� ĳ�������� �����մϴ�.
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(LoadBundleURL))
        {
            /*
            yield return www;
            if (www.error != null)
                throw new Exception("WWW �ٿ�ε忡 ������ ������ϴ�.:" + www.error);
            AssetBundle bundle = www.assetBundle;*/

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

                //�۾�
                for (int i = 0; i < objectName.Length; i++)
                {
                    var request = bundle.LoadAsset($"{objectName[i]}3D", typeof(GameObject));
                    yield return request;
                    GameObject obj = Instantiate(request) as GameObject;

                    if (!(objectName[i] == "MonsterCleanup" || objectName[i] == "Parade"))
                    {
                        var Anim = bundle.LoadAsset<AnimationClip>(objectName[i]);

                        AnimationClip AnimClip = Instantiate(Anim);
                        yield return AnimClip;

                        //yield return new WaitForSeconds(1f);
                        obj.GetComponent<Animation>().AddClip(AnimClip, AnimClip.name);
                        obj.GetComponent<Animation>().clip = AnimClip;
                    }

                    int index = obj.name.IndexOf("(Clone)");
                    if (index > 0)
                        obj.name = obj.name.Substring(0, index);
                    int index2 = obj.name.IndexOf("3D");
                    if (index2 > 0)
                        obj.name = obj.name.Substring(0, index2);
                    ObjectSetting(obj);
                }

                bundle.Unload(true);
                www.Dispose();
                //Resources.UnloadUnusedAssets();
                BackgroundImageActived1 = false;
                BackgroundImageActived2[count] = true;
            }


        } // using���� File �� Font ó�� ��ǻ�� ���� �����Ǵ� ���ҽ����� ���� ���� ���� �ڿ��� �ǵ����ټ� �ֵ��� ����� ����  

    }

    public Texture2D CopyTexture(Texture2D texture)
    {
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = previous;

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        // "myTexture2D" now has the same pixels from "texture" and it's readable.

        return myTexture2D;
    }


    IEnumerator AssetBundleLoad2(string LoadBundleURL)
    {
        
        yield return new WaitForEndOfFrame();

        while (!Caching.ready)
            yield return null;


        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(LoadBundleURL))
        {
            /*
            yield return www;
            if (www.error != null)
                throw new Exception("WWW �ٿ�ε忡 ������ ������ϴ�.:" + www.error);

            AssetBundle bundle = www.assetBundle;*/

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

                //�۾�
                _SpriteBackground[count]._Texture.Clear();

                int spriteCount = 0;
                switch (backgroundName[count])
                {
                    case "AmazonFilter": spriteCount = 37; break;
                    case "FairyTownFilter": spriteCount = 45; break;
                    case "Fireworks": spriteCount = 75; break;
                    case "LennyAndFriends": spriteCount = 60; break;
                    case "MonsterCleanup": spriteCount = 55; break;
                    case "Parade": spriteCount = 75; break;
                    case "TexpressFilter": spriteCount = 75; break;
                    default: break;
                }

                for (int j = 1; j <= spriteCount; j++)
                {
                    var abrd = bundle.LoadAsset<Texture2D>($"{backgroundName[count]} ({j})");
                    //yield return abrd;
                    //Texture2D texture = (Texture2D)abrd.asset;

                    Debug.Log(abrd);
                    _SpriteBackground[count]._Texture.Enqueue(abrd);
                }
                

                bundle.Unload(false);
                www.Dispose();
                Resources.UnloadUnusedAssets();

                BackgroundImageActived2[count] = false;
                count +=1;
                if(count < backgroundName.Length)
                {
                    BackgroundImageActived2[count] = true;
                }
                else
                {
                    _FaceTracking.SetActive(true);
                    _LoadingPage.SetActive(false);
                    _GameManager.SetActive(true);
                    _Tracking.SetActive(true);
                    camera.enabled = true;
                    //Zappar.Additional.SNS.ZSaveNShare.UnityAssetLoading();
                    gameObject.SetActive(false);
                }

            }

        }
    }

    /*
    private void QueueSet()
    {
        for (int i = 0; i < objectName.Length; i++)
        {
            _Task.Enqueue(RuntimeGltfImporter.GetImportTask(
            $"https://uxstory.github.io/everland/{objectName[i]}.glb"));
        }

        if (_Task.Count > 0)
        {
            _Task.Peek().OnProgress = OnProgress;
            _Task.Peek().OnCompleted = OnComplete;
        }
        else
        {
            _LoadingPage.SetActive(false);
            Zappar.Additional.SNS.ZSaveNShare.UnityAssetLoading();
        }
    }
    */


    private void ObjectSetting(GameObject importedModel)
    {
        //Debug.Log("Success!");

        switch (importedModel.name)
        {
            case "MonsterCleanup":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "Parade":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "FairyTownFilter":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "LennyAndFriends":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "PandaWithPose":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "RedPandaWithPose":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "WalkingTiger":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            default: break;
        }

        importedModel.SetActive(false);
        //������Ʈ ũ�� �� ��ġ ����
        ObjectPosition(importedModel);
    }

    /// <summary>
    /// Callback that is invoked by the glTF import task
    /// after it has successfully completed.
    /// </summary>
    /// <param name="importedModel">
    /// the root GameObject of the imported glTF model
    /// </param>
   /* private void OnComplete(GameObject importedModel)
    {
        //Debug.Log("Success!");

        switch (importedModel.name)
        {
            case "MonsterCleanup":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "Parade":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "FairyTownFilter":
                importedModel.transform.SetParent(faceTracking.transform);
                break;
            case "LennyAndFriends":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "PandaWithPose":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "RedPandaWithPose":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            case "WalkingTiger":
                importedModel.transform.SetParent(placeTracking.transform);
                break;
            default: break;
        }

        importedModel.SetActive(false);

        //������Ʈ ũ�� �� ��ġ ����
        ObjectPosition(importedModel);

        //Debug.Log("finish " + _Task.Dequeue());

        if (_Task.Count > 0)
        {
            _Task.Peek().OnProgress = OnProgress;
            _Task.Peek().OnCompleted = OnComplete;
        }

        //queue�� ���̻� ������ false
        if (_Task.Count <= 0)
        {
            _LoadingPage.SetActive(false);

            Zappar.Additional.SNS.ZSaveNShare.UnityAssetLoading();
        }
    }*/

    private void OnProgress(GltfImportStep step, int completed, int total)
    {
        //Debug.LogFormat("{0}: {1}/{2}", step, completed, total);
    }


    private void ObjectPosition(GameObject importedModel)
    {
        switch (importedModel.name)
        {
            case "MonsterCleanup":
                SetPosition(-0.06f, 1.51f, 0.76f, importedModel);
                SetScale(0.6f, 0.6f, 0.6f, importedModel);
                SetRotation(-14.62f, 180f, 0f, importedModel);
                break;
            case "Parade":
                SetPosition(0f, 0.6f, 0f, importedModel);
                SetScale(0.8f, 0.8f, 0.8f, importedModel);
                break;
            case "FairyTownFilter":
                SetPosition(0f, -0.09f, -0.34f, importedModel);
                SetScale(1.2f, 1.2f, 1.2f, importedModel);
                break;
            case "LennyAndFriends":
                SetPosition(0, 0, 0, importedModel);
                SetScale(5, 5, 5, importedModel);
                break;
            case "PandaWithPose":
                SetPosition(0, 0, 0, importedModel);
                SetScale(1, 1, 1, importedModel);
                SetRotation(0, 90, 0, importedModel);
                break;
            case "RedPandaWithPose":
                SetPosition(0f, 0f, 0f, importedModel);
                SetScale(120, 120, 120, importedModel);
                break;
            case "WalkingTiger":
                SetPosition(0, 0, 0, importedModel);
                SetScale(50, 50, 50, importedModel);
                break;
            default: break;
        }
    }

    private void SetPosition(float x, float y, float z, GameObject importedModel)
    {
        importedModel.transform.position = new Vector3(x, y, z);
    }

    private void SetScale(float x, float y, float z, GameObject importedModel)
    {
        importedModel.transform.localScale = new Vector3(x, y, z);
    }

    private void SetRotation(float x, float y, float z, GameObject importedModel)
    {
        importedModel.transform.localEulerAngles = new Vector3(x, y, z);
    }

}

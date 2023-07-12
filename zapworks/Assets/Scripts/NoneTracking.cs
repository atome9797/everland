using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NoneTracking : MonoBehaviour
{
    public BackgroundManager _BackgroundManager;

    public void NoneTrackingTask(string trackingName)
    {
        _BackgroundManager.BackgroundTypeSetting(trackingName);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTrackableEventHandler : DefaultTrackableEventHandler
{

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        Singleton<gun>.Instance.StartShooting();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Singleton<gun>.Instance.shooting = false;
    }
}

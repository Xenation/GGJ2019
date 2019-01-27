using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceName : MonoBehaviour
{
    public Text CompName;
    // Start is called before the first frame update
    void Start()
    {
        CompName.text = SystemInfo.deviceName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

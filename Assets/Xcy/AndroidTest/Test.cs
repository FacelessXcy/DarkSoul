using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text infoText;
    string platform = string.Empty;
    string info = string.Empty;

    void Update()
    {
        info = string.Empty;
        //使用预编译的宏命令检测平台
        //Editor模式下目标平台选Android也会触发UNITY_ANDROID的宏
#if UNITY_ANDROID
        platform = "UNITY_ANDROID";
#elif UNITY_EDITOR
        platform = "UNITY_EDITOR";
#endif
        Debug.Log("platform : " + platform);
        info += "platform : " + platform + "\n";
        //使用Application.platform检测平台
        Debug.Log("platform : " + Application.platform);
        info += "platform : " + Application.platform + "\n";
        //获取当前设备分辨率
        Debug.Log("currentResolution : " + Screen.currentResolution);
        info += "currentResolution : " + Screen.currentResolution + "\n";
        //获取当前DPI
        Debug.Log("dpi : " + Screen.dpi);
        info += "dpi : " + Screen.dpi + "\n";
        //获取是否全屏
        Debug.Log("fullScreen : " + Screen.fullScreen);
        info += "fullScreen : " + Screen.fullScreen + "\n";
        //获取游戏窗口的宽高
        Debug.Log("height : " + Screen.height);
        info += "height : " + Screen.height + "\n";
        Debug.Log("width : " + Screen.width);
        info += "width : " + Screen.width + "\n";
        //获取屏幕方向
        Debug.Log("orientation : " + Screen.orientation);
        info += "orientation : " + Screen.orientation + "\n";
        //获取屏幕超时时间（仅在移动端有效）
        Debug.Log("sleepTimeout : " + Screen.sleepTimeout);
        info += "sleepTimeout : " + Screen.sleepTimeout + "\n";
        //任意键按下
        Debug.Log("anyKey : " + Input.anyKey);
        info += "anyKey : " + Input.anyKey + "\n";
        //获取最后一次加速度计报告的加速度
        Debug.Log("acceleration : " + Input.acceleration);
        info += "acceleration : " + Input.acceleration + "\n";
        //获取本帧加速度计报告的次数
        Debug.Log("accelerationEventCount : " + Input.accelerationEventCount);
        info += "accelerationEventCount : " + Input.accelerationEventCount + "\n";
        //获取本帧加速度计的所有报告信息
        for (int i = 0; i < Input.accelerationEvents.Length; i++)
        {
            Debug.Log("accelerationEvents" + i + " : " + Input.accelerationEvents[i].acceleration + " , " + Input.accelerationEvents[i].deltaTime);
            info += "accelerationEvents" + i + " : " + Input.accelerationEvents[i].acceleration + " , " + Input.accelerationEvents[i].deltaTime + "\n";
        }
        //获取电子罗盘向量
        Debug.Log("compass : " + Input.compass.rawVector);
        info += "compass : " + Input.compass.rawVector + "\n";
        //获取设备方向
        Debug.Log("deviceOrientation : " + Input.deviceOrientation);
        info += "deviceOrientation : " + Input.deviceOrientation + "\n";
        //获取陀螺仪重力
        Debug.Log("gyro : " + Input.gyro.gravity);
        info += "gyro : " + Input.gyro.gravity + "\n";
        //获取位置服务状态
        Debug.Log("location : " + Input.location);
        info += "location : " + Input.location.status + "\n";
        //获取设备是否带有鼠标
        Debug.Log("mousePresent : " + Input.mousePresent);
        info += "mousePresent : " + Input.mousePresent + "\n";
        //获取平台是否支持多点触控
        Debug.Log("multiTouchEnabled : " + Input.multiTouchEnabled);
        info += "multiTouchEnabled : " + Input.multiTouchEnabled + "\n";
        //获取是否支持笔触（可检测压感、触摸夹角等）
        Debug.Log("stylusTouchSupported : " + Input.stylusTouchSupported);
        info += "stylusTouchSupported : " + Input.stylusTouchSupported + "\n";
        //获取是否支持压感
        Debug.Log("touchPressureSupported : " + Input.touchPressureSupported);
        info += "touchPressureSupported : " + Input.touchPressureSupported + "\n";
        //获取是否支持触摸
        Debug.Log("touchSupported : " + Input.touchSupported);
        info += "touchSupported : " + Input.touchSupported + "\n";
        if (Input.touchCount > 0)
        {
            //当前帧的触摸数
            Debug.Log("touchCount : " + Input.touchCount);
            info += "touchCount : " + Input.touchCount + "\n";
            for (int i = 0; i < Input.touchCount; i++)
            {
                //获取当前Touch对象的一系列属性
                Debug.Log("touch" + i + "altitudeAngle : " + Input.GetTouch(i).altitudeAngle);
                info += "touch" + i + "altitudeAngle : " + Input.GetTouch(i).altitudeAngle + "\n";
                Debug.Log("touch" + i + "azimuthAngle : " + Input.GetTouch(i).azimuthAngle);
                info += "touch" + i + "azimuthAngle : " + Input.GetTouch(i).azimuthAngle + "\n";
                Debug.Log("touch" + i + "deltaPosition : " + Input.GetTouch(i).deltaPosition);
                info += "touch" + i + "deltaPosition : " + Input.GetTouch(i).deltaPosition + "\n";
                Debug.Log("touch" + i + "deltaTime : " + Input.GetTouch(i).deltaTime);
                info += "touch" + i + "deltaTime : " + Input.GetTouch(i).deltaTime + "\n";
                Debug.Log("touch" + i + "fingerId : " + Input.GetTouch(i).fingerId);
                info += "touch" + i + "fingerId : " + Input.GetTouch(i).fingerId + "\n";
                Debug.Log("touch" + i + "maximumPossiblePressure : " + Input.GetTouch(i).maximumPossiblePressure);
                info += "touch" + i + "maximumPossiblePressure : " + Input.GetTouch(i).maximumPossiblePressure + "\n";
                Debug.Log("touch" + i + "phase : " + Input.GetTouch(i).phase);
                info += "touch" + i + "phase : " + Input.GetTouch(i).phase + "\n";
                Debug.Log("touch" + i + "position : " + Input.GetTouch(i).position);
                info += "touch" + i + "position : " + Input.GetTouch(i).position + "\n";
                Debug.Log("touch" + i + "pressure : " + Input.GetTouch(i).pressure);
                info += "touch" + i + "pressure : " + Input.GetTouch(i).pressure + "\n";
                Debug.Log("touch" + i + "radius : " + Input.GetTouch(i).radius);
                info += "touch" + i + "radius : " + Input.GetTouch(i).radius + "\n";
                Debug.Log("touch" + i + "radiusVariance : " + Input.GetTouch(i).radiusVariance);
                info += "touch" + i + "radiusVariance : " + Input.GetTouch(i).radiusVariance + "\n";
                Debug.Log("touch" + i + "rawPosition : " + Input.GetTouch(i).rawPosition);
                info += "touch" + i + "rawPosition : " + Input.GetTouch(i).rawPosition + "\n";
                Debug.Log("touch" + i + "tapCount : " + Input.GetTouch(i).tapCount);
                info += "touch" + i + "tapCount : " + Input.GetTouch(i).tapCount + "\n";
                Debug.Log("touch" + i + "type : " + Input.GetTouch(i).type);
                info += "touch" + i + "type : " + Input.GetTouch(i).type + "\n";
            }
        }
        infoText.text = info;
    }
}

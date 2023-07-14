using UnityEngine;

public class MovementZone
{
    private static Camera _cam = Camera.main;
    /// <summary>
    /// 
    /// </summary>
    private static float _screen_w = (float)_cam.pixelWidth / _cam.pixelHeight;
    /// <summary>
    /// 
    /// </summary>
    private static float _cameraHeight = _cam.orthographicSize;
    /// <summary>
    /// 
    /// </summary>
    public readonly static Vector2 Bounds = new Vector2(_cameraHeight * _screen_w, _cameraHeight);
}

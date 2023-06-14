using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierMath 
{
    public static Vector3 BezierPoint(Vector3 _point1 , Vector3 _point2, Vector3 _point3, Vector3 _point4 ,float _progress)
    {
        float progress = _progress;
        if(_progress <0 )
        {
            progress = 0;
        }
        else if (_progress >1 )
        {
            progress = 1;
        }   
        Vector3 point12 = Vector3.Lerp(_point1, _point2, progress);
        Vector3 point23 = Vector3.Lerp(_point2, _point3, progress);
        Vector3 point34 = Vector3.Lerp(_point3, _point4, progress);

        Vector3 point1223 = Vector3.Lerp(point12, point23,progress);
        Vector3 point2334 = Vector3.Lerp(point23, point34,progress);
        Vector3 bazierPoint = Vector3.Lerp(point1223, point2334, progress);
        return bazierPoint;
    }
}

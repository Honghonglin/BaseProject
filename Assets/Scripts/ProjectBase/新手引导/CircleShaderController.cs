using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleShaderController : MonoBehaviour
{
    
    /// <summary>
    /// 高亮显示的目标
    /// </summary>
    public Image Target;

    
    /// <summary>
    /// 区域范围缓存
    /// </summary>
    public Vector3[] _corners=new Vector3[4];

    /// <summary>
    /// 镂空区域圆心
    /// </summary>
    public Vector2 _center;

    /// <summary>
    /// 镂空区域半径
    /// </summary>
    private float _radius;

    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material _material;

    /// <summary>
    /// 当前高亮区域的半径
    /// </summary>
    private float _currentRadius;

    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float _shrinkTime=0.5f;

    /// <summary>
    /// 世界坐标向画布坐标转换
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="world"></param>
    /// <returns></returns>
    private Vector2 WorldToCanvasPos(Canvas canvas,Vector3 world){
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }
    

    public void Awake()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //获取高亮区域的四个顶点的世界坐标
        Target.rectTransform.GetWorldCorners(_corners);
        //计算最终高亮显示区域的半径
        _radius = Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]),WorldToCanvasPos(canvas, _corners[3]))/2f;

        //计算高亮显示区域的圆心
        float x = (_corners[0].x+_corners[3].x) / 2f;
        float y = (_corners[0].y + _corners[1].y) / 2f;
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 center = WorldToCanvasPos(canvas, centerWorld);

        //设置遮罩材料的圆心变量
        Vector3 centerMat = new Vector4(center.x,center.y);
        _material = transform.GetComponent<Image>().material;
        _material.SetVector("_Center", centerMat);

        //计算当前高亮区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布区域的四个顶点
            canRectTransform.GetWorldCorners(_corners);
            foreach (var corner in _corners)
            {
                _currentRadius = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corner), center),_currentRadius);
            }
        }
        _material.SetFloat("_Radius", _currentRadius);
    }

    private float _shrinkVelocity = 0f;
    public void Update()
    {
        //从当前半径到目标半径插值显示收缩动画
        float value = Mathf.SmoothDamp(_currentRadius, _radius, ref _shrinkVelocity, _shrinkTime);
        if (!Mathf.Approximately(value, _currentRadius))
        {
            _currentRadius = value;
            _material.SetFloat("_Radius", _currentRadius);
        }
    }
}

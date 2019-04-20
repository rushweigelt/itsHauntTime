using UnityEngine;
 
// Helper Rect extension methods
public static class RectExtensions
{
    public static Vector2 TopLeft(this Rect rect)
    {
        return new Vector2(rect.xMin, rect.yMin);
    }
	public static Vector2 BottomRight(this Rect rect)
    {
        return new Vector2(rect.xMax, rect.yMax);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale)
    {
        return ScaleSizeBy(rect, scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale;
        result.xMax *= scale;
        result.yMin *= scale;
        result.yMax *= scale;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
    {
        return ScaleSizeBy(rect, scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale.x;
        result.xMax *= scale.x;
        result.yMin *= scale.y;
        result.yMax *= scale.y;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }


    public static Rect Scale (Rect rect, Vector2 pivot, Vector2 scale) 
    {
        rect.position = Vector2.Scale (rect.position - pivot, scale) + pivot;
        rect.size = Vector2.Scale (rect.size, scale);
        return rect;
    }
}
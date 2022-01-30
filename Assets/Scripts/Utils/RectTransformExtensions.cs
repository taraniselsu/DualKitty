using UnityEngine;

/// <summary>
/// Contains extensions we're adding to default RectTransform behaviour.
/// </summary>
public static class RectTransformExtensions
{
    // The original code for SetLeft(), etc. is from here:
    // https://answers.unity.com/questions/888257/access-left-right-top-and-bottom-of-recttransform.html

    /// <summary>Sets the value referred to in the inspector as Left.</summary>
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
    /// <summary>Sets the value referred to in the inspector as Right.</summary>
    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
    /// <summary>Sets the value referred to in the inspector as Top.</summary>
    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
    /// <summary>Sets the value referred to in the inspector as Bottom.</summary>
    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}
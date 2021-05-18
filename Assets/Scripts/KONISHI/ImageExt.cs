using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExt
{
    // imageの不透明度を設定
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        c.a = alpha;
        image.color = c;
    }
}

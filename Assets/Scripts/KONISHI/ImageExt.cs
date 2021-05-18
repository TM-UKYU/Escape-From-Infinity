using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExt
{
    // image�̕s�����x��ݒ�
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        c.a = alpha;
        image.color = c;
    }
}

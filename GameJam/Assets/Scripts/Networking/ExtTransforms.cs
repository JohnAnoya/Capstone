using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        foreach (Transform child_ in t)
        {
            if (destroyImmediately)
            {
                MonoBehaviour.DestroyImmediate(child_.gameObject);
            }

            else
            {
                MonoBehaviour.Destroy(child_.gameObject);
            }
        }
    }
}
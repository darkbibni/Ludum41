using UnityEngine;

public enum Orientation
{
    North,
    East,
    South,
    West,
    None
}

public class OrientationUtility : MonoBehaviour {

    public static Vector3[] orientations = {
        Vector3.forward,
        Vector3.right,
        -Vector3.forward,
        -Vector3.right
    };

    public static Orientation GetOrientation(Vector3 dir)
    {
        for (int i = 0; i < orientations.Length; i++)
        {
            if(dir == orientations[i])
            {
                return (Orientation)i;
            }
        }

        return Orientation.None;
    }
}

using UnityEngine;

public class enum_Sides : MonoBehaviour
{
    public enum Sides
    {
        None,
        North,
        West,
        East,
        South
    }

    public enum Direction
    {
        None,
        Horizontal,
        Vertical
    }

    public Sides side;
    public Direction direction;
}

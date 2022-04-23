using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite portrait;
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义跑道数据
public static class Street
{
    // 定义跑道左右边界
    public static float leftSide = -3f;
    public static float rightSide = 3f;
}

// 控制循环生成跑道
public static class SectionController
{
    public static readonly List<Vector2> generatorPoint = new List<Vector2>()
    {
        new Vector2(-2f, -30f), new Vector2(0f, -30f),
        new Vector2(0f, -20f), new Vector2(2f, -20f),
        new Vector2(-2f, -10f), new Vector2(0f, -10f),
        new Vector2(-2f, 0f), new Vector2(2f, 0f),
        new Vector2(0f, 10f), new Vector2(-2f, 10f),
        new Vector2(2f, 20f), new Vector2(0f, 20f),
    };
}

// 障碍物、道具的枚举类
public enum PrefabName
{
    ROCK,
    TREE_STUMP,
}


// 预制体数据结构体
[System.Serializable]
public struct PrefabData
{
    public PrefabName name;

    public int amount;

    public GameObject prefab;

}
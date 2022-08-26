using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ܵ�����
public static class Street
{
    // �����ܵ����ұ߽�
    public static float leftSide = -3f;
    public static float rightSide = 3f;
}

// ����ѭ�������ܵ�
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

// �ϰ�����ߵ�ö����
public enum PrefabName
{
    ROCK,
    TREE_STUMP,
}


// Ԥ�������ݽṹ��
[System.Serializable]
public struct PrefabData
{
    public PrefabName name;

    public int amount;

    public GameObject prefab;

}
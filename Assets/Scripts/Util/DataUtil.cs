using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 单位跑道控制器
public static class SectionController
{
    // 定义跑道左右边界
    public static float leftSide = -3f;
    public static float rightSide = 3f;

    public static float sectionLenght = 60f;// 每个Section的长度

    public static float zDistance = 10f;    // 生成点在Z轴上的默认间隔

    private static float sectionZPosition = sectionLenght;

    // 障碍物、道具等预制体的生成点
    // 注：在Z轴上每隔zDistance个单位长度有一个生成点 每个Section随机抽取一定数量的生成点
    public static readonly List<Vector3> allGneratorPoints = new List<Vector3>();
    // 根据跑道长度和生成点的Z轴间隔来创建生成点数组
    public static void GeneratePoints()
    {
        allGneratorPoints.Clear();
        for (int i = 0; i < sectionLenght / zDistance; i++)
        {
            allGneratorPoints.Add(new Vector3(-2.2f, 0.5f, -sectionLenght / 2 + i * zDistance));
            allGneratorPoints.Add(new Vector3(0, 0.5f, -sectionLenght / 2 + i * zDistance));
            allGneratorPoints.Add(new Vector3(2.2f, 0.5f, -sectionLenght / 2 + i * zDistance));
        }
    }

    // 存储随机挑选的生成点
    public static readonly List<Vector3> someGeneratePoints = new List<Vector3>();
    // 随机获取指定数量的生成点
    public static void GetSomePoints(int pointNum)
    {
        // 判断参数是否合法
        if (pointNum > allGneratorPoints.Count)
        {
            Debug.Log("获取生成点失败：所需的生成点数目过大");
            return;
        }
 
        // 清除原列表中的数据
        someGeneratePoints.Clear();
        // 记录已选择的生成点索引
        HashSet<int> selectedPointIndexes = new HashSet<int>();

        // 用来判断是否成功获取一个索引
        int allPointsAmount = (int)(sectionLenght / zDistance * 3);
        int flag = 0;

        for (int i = 0; i < pointNum; i++)
        {
            flag = 0;
            // 随机生成索引
            int pointIndex = Random.Range(0, allGneratorPoints.Count);
            // 判断该索引是否已经被选中
            while (selectedPointIndexes.Contains(pointIndex) && flag < allPointsAmount)
            {
                pointIndex = ProcessIndex.Wrap(pointIndex + 1, 0, allGneratorPoints.Count);
                flag++;
            }

            // 判断是否成功获取一个索引
            if (flag == allPointsAmount)
                Debug.Log("生成点索引寻找失败！");

            someGeneratePoints.Add(allGneratorPoints[pointIndex]);
            selectedPointIndexes.Add(pointIndex);
        }
    }

    public static List<PrefabName> prefabNames = new List<PrefabName>();

    public static void GenerateSection()
    {
        // 获取指定数量的道具和障碍物生成点
        GetSomePoints(10);

        // 根据获取的生成点生成一个单位的跑道（Section）
        GameObject section =
            PrefabPoolingManager.Instance.GetPrefabInstanceByName(PrefabName.SECTION);
        section.transform.position = new Vector3(0, 0, sectionZPosition);
        section.SetActive(true);
        sectionZPosition += sectionLenght;

        
        foreach (Vector3 item in someGeneratePoints)
        {
            Debug.Log("Generate Point: " + item);


            int index = Random.Range(0, prefabNames.Count);
            PrefabName prefabName = prefabNames[index];
            GameObject obj = PrefabPoolingManager.Instance.GetPrefabInstanceByName(prefabName);

            obj.transform.SetParent(section.transform);
            obj.transform.localPosition = item;
            obj.SetActive(true);
        }
    }

    public static void Initialize()
    {
        GeneratePoints();

        foreach (PrefabName item in System.Enum.GetValues(typeof(PrefabName)))
        {
            if (!item.Equals(PrefabName.SECTION))
            {
                prefabNames.Add(item);
            }
        }
    }
}

// 预制体枚举类
public enum PrefabName
{
    ROCK,           // 岩石
    TREE_STUMP,     // 树桩
    SLIME,          // 粘液怪
    TURTLE_SHELL,   // 乌龟障碍物
    SECTION,        // 一个单位长度的跑道
}

// 预制体数据结构体
[System.Serializable]
public struct PrefabData
{
    public PrefabName name;     // 预制体枚举类型

    public int amount;          // 预制体对应的实例数量

    public GameObject prefab;   // 对应的预制体

}

// 处理数组索引
public static class ProcessIndex
{
    public static int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}
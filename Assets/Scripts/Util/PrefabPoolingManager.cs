using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 该类主要负责生成和管理各种游戏物体
/// 例如：障碍物、道具、金币等
/// </summary>

public class PrefabPoolingManager : MonoBehaviour
{
    // 使用单例模式
    private static PrefabPoolingManager instance;
    public static PrefabPoolingManager Instance { get { return instance; } }

    // 预制体数组 在Inspector面板初始化
    public PrefabData[] prefabs;  

    // 预制体实例字典 用来存储所有的预制体实例
    private Dictionary<PrefabName, List<GameObject>> prefabInstances;

    // 初始化
    private void Awake()
    {
        instance = this;    // 单例模式

        prefabInstances = new Dictionary<PrefabName, List<GameObject>>();   // 创建预制体实例数组

        // 根据每个预制体的信息创建对应数量的预制体实例 并将其添加到预制体实例数组中
        foreach (PrefabData item in prefabs)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int i = 0; i < item.amount; i++)
            {
                GameObject tempInstance = Instantiate(item.prefab, transform);
                tempInstance.SetActive(false);
                tempList.Add(tempInstance);
            }
            // 将预制体实例数组添加到字典中
            prefabInstances.Add(item.name, tempList);
        }
    }

    private void Start()
    {
        //GameObject testPrefab = GetPrefabInstanceByName(PrefabName.TREE_STUMP);
        //Debug.Log(testPrefab);

    }


    // 获取预制体
    public GameObject GetPrefabInstanceByName(PrefabName name)
    {
        // 获取预制体实例列表
        List<GameObject> instanceList = prefabInstances[name];

        // 遍历是否存在可用预制体实例 存在则直接返回该实例
        foreach (GameObject item in instanceList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }

        // 若该列表中不存在可用预制体实例 则新创建实例并将其添加到对应列表中 返回该实例
        PrefabData tempPrefab = GetPrefabDataByName(name);
        GameObject tempInstance = Instantiate(tempPrefab.prefab, transform);

        tempInstance.SetActive(true);
        instanceList.Add(tempInstance);
        tempPrefab.amount++;

        return tempInstance;
    }


    // 根据预制体的枚举类型返回对应的预制体信息
    private PrefabData GetPrefabDataByName(PrefabName name)
    {
        foreach (PrefabData item in prefabs)
        {
            if (item.name == name)
            {
                return item;
            }
        }

        // struct类型不可为null 此处为临时写法 调用此方法应避免出现空指针异常
        return new PrefabData();
    }

}

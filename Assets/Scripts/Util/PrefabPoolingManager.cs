using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ������Ҫ�������ɺ͹��������Ϸ����
/// ���磺�ϰ�����ߡ���ҵ�
/// </summary>

public class PrefabPoolingManager : MonoBehaviour
{
    // ʹ�õ���ģʽ
    private static PrefabPoolingManager instance;
    public static PrefabPoolingManager Instance { get { return instance; } }

    // Ԥ�������� ��Inspector����ʼ��
    public PrefabData[] prefabs;  

    // Ԥ����ʵ���ֵ� �����洢���е�Ԥ����ʵ��
    private Dictionary<PrefabName, List<GameObject>> prefabInstances;

    // ��ʼ��
    private void Awake()
    {
        instance = this;    // ����ģʽ

        prefabInstances = new Dictionary<PrefabName, List<GameObject>>();   // ����Ԥ����ʵ������

        // ����ÿ��Ԥ�������Ϣ������Ӧ������Ԥ����ʵ�� ��������ӵ�Ԥ����ʵ��������
        foreach (PrefabData item in prefabs)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int i = 0; i < item.amount; i++)
            {
                GameObject tempInstance = Instantiate(item.prefab, transform);
                tempInstance.SetActive(false);
                tempList.Add(tempInstance);
            }
            // ��Ԥ����ʵ��������ӵ��ֵ���
            prefabInstances.Add(item.name, tempList);
        }
    }

    private void Start()
    {
        //GameObject testPrefab = GetPrefabInstanceByName(PrefabName.TREE_STUMP);
        //Debug.Log(testPrefab);

    }


    // ��ȡԤ����
    public GameObject GetPrefabInstanceByName(PrefabName name)
    {
        // ��ȡԤ����ʵ���б�
        List<GameObject> instanceList = prefabInstances[name];

        // �����Ƿ���ڿ���Ԥ����ʵ�� ������ֱ�ӷ��ظ�ʵ��
        foreach (GameObject item in instanceList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }

        // �����б��в����ڿ���Ԥ����ʵ�� ���´���ʵ����������ӵ���Ӧ�б��� ���ظ�ʵ��
        PrefabData tempPrefab = GetPrefabDataByName(name);
        GameObject tempInstance = Instantiate(tempPrefab.prefab, transform);

        tempInstance.SetActive(true);
        instanceList.Add(tempInstance);
        tempPrefab.amount++;

        return tempInstance;
    }


    // ����Ԥ�����ö�����ͷ��ض�Ӧ��Ԥ������Ϣ
    private PrefabData GetPrefabDataByName(PrefabName name)
    {
        foreach (PrefabData item in prefabs)
        {
            if (item.name == name)
            {
                return item;
            }
        }

        // struct���Ͳ���Ϊnull �˴�Ϊ��ʱд�� ���ô˷���Ӧ������ֿ�ָ���쳣
        return new PrefabData();
    }

}

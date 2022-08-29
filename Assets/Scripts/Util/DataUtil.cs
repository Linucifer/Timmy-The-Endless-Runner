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
    public static float sectionLenght = 60f;// ÿ��Section�ĳ���

    public static float zDistance = 10f;    // ���ɵ���Z���ϵ�Ĭ�ϼ��

    private static float sectionZPosition = sectionLenght;

    // �ϰ�����ߵ�Ԥ��������ɵ�
    // ע����Z����ÿ��zDistance����λ������һ�����ɵ� ÿ��Section�����ȡһ�����������ɵ�
    public static readonly List<Vector3> allGneratorPoints = new List<Vector3>();
    // �����ܵ����Ⱥ����ɵ��Z�������������ɵ�����
    public static void GeneratePoints()
    {
        allGneratorPoints.Clear();
        for (int i = 0; i < sectionLenght / zDistance; i++)
        {
            allGneratorPoints.Add(new Vector3(-2, 0, -sectionLenght / 2 + i * zDistance));
            allGneratorPoints.Add(new Vector3(0, 0, -sectionLenght / 2 + i * zDistance));
            allGneratorPoints.Add(new Vector3(2, 0, -sectionLenght / 2 + i * zDistance));
        }
    }

    // �洢�����ѡ�����ɵ�
    public static readonly List<Vector3> someGeneratePoints = new List<Vector3>();
    // �����ȡָ�����������ɵ�
    public static void GetSomePoints(int pointNum)
    {
        // �жϲ����Ƿ�Ϸ�
        if (pointNum > allGneratorPoints.Count)
            Debug.Log("��ȡ���ɵ�ʧ�ܣ���������ɵ���Ŀ����");

        // ���ԭ�б��е�����
        someGeneratePoints.Clear();
        // ��¼��ѡ������ɵ�����
        HashSet<int> selectedPointIndexes = new HashSet<int>();

        // �����ж��Ƿ�ɹ���ȡһ������
        int allPointsAmount = (int)(sectionLenght / zDistance * 3);
        int flag = 0;

        for (int i = 0; i < pointNum; i++)
        {
            flag = 0;
            // �����������
            int pointIndex = Random.Range(0, allGneratorPoints.Count);
            // �жϸ������Ƿ��Ѿ���ѡ��
            while (selectedPointIndexes.Contains(pointIndex) && flag < allPointsAmount)
            {
                pointIndex = ProcessIndex.Wrap(pointIndex + 1, 0, allGneratorPoints.Count);
                flag++;
            }

            // �ж��Ƿ�ɹ���ȡһ������
            if (flag == allPointsAmount)
                Debug.Log("���ɵ�����Ѱ��ʧ�ܣ�");

            someGeneratePoints.Add(allGneratorPoints[pointIndex]);
            selectedPointIndexes.Add(pointIndex);
        }
    }

    public static void GenerateSection()
    {
        GetSomePoints(10);
        GameObject section =
            PrefabPoolingManager.Instance.GetPrefabInstanceByName(PrefabName.SECTION);
        section.transform.position = new Vector3(0, 0, sectionZPosition);
        section.SetActive(true);
        sectionZPosition += sectionLenght;

        foreach (Vector3 item in someGeneratePoints)
        {
            GameObject obj = PrefabPoolingManager.Instance.GetPrefabInstanceByName(PrefabName.TURTLE_SHELL);

            obj.transform.SetParent(section.transform);
            obj.transform.localPosition = item;
            obj.SetActive(true);
        }
    }
}

// Ԥ����ö����
public enum PrefabName
{
    ROCK,           // ��ʯ
    TREE_STUMP,     // ��׮
    SECTION,        // һ����λ���ȵ��ܵ�
    SLIME,          // ճҺ��
    TURTLE_SHELL,   // �ڹ��ϰ���
}

// Ԥ�������ݽṹ��
[System.Serializable]
public struct PrefabData
{
    public PrefabName name;     // Ԥ����ö������

    public int amount;          // Ԥ�����Ӧ��ʵ������

    public GameObject prefab;   // ��Ӧ��Ԥ����

}

// ������������
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
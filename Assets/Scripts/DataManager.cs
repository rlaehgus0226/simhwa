using UnityEngine;

public class DataManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public float[] sizeUpExpArray;

    //private void Awake()
    //{
    //    Manager.Instance.SetDataManager(this);
    //}
    public void ReadJSON()
    {
        if (jsonFile != null)
        {
            // JSON ������ �ؽ�Ʈ�� �Ľ�
            LevelDataArray levelDataArray = JsonUtility.FromJson<LevelDataArray>(jsonFile.text);

            // SizeUpEXP ���� �迭�� ����
            sizeUpExpArray = new float[levelDataArray.Sheet1.Length];
            for (int i = 0; i < levelDataArray.Sheet1.Length; i++)
            {
                sizeUpExpArray[i] = levelDataArray.Sheet1[i].SizeUpEXP;
            }
        }
        else
        {
            Debug.LogError("JSON ������ �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}

[System.Serializable]
public class LevelData
{
    public int Level;
    public float SizeUpEXP;
}

[System.Serializable]
public class LevelDataArray
{
    public LevelData[] Sheet1;
}
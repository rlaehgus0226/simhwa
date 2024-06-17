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
            // JSON 파일의 텍스트를 파싱
            LevelDataArray levelDataArray = JsonUtility.FromJson<LevelDataArray>(jsonFile.text);

            // SizeUpEXP 값을 배열에 저장
            sizeUpExpArray = new float[levelDataArray.Sheet1.Length];
            for (int i = 0; i < levelDataArray.Sheet1.Length; i++)
            {
                sizeUpExpArray[i] = levelDataArray.Sheet1[i].SizeUpEXP;
            }
        }
        else
        {
            Debug.LogError("JSON 파일이 할당되지 않았습니다!");
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
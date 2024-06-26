using System;
using System.IO;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class UserData
{
    public string lastLogoutTime;
    public string _gold;
    public string _value;
    public int _sizeLevel;
    public int _moneyLevel;
    public float _autoClickInterval;

    public Vector3 _objSize;
    public Color _objRenderer;

    public UserData(string lastLogoutTime, BigInteger gold, BigInteger value, int sizeLevel, int moneyLevel, float autoClickInterval, Vector3 objSize, Color objRenderer)
    {
        this.lastLogoutTime = lastLogoutTime;
        _gold = gold.ToString();
        _value = value.ToString();
        _sizeLevel = sizeLevel;
        _moneyLevel = moneyLevel;
        _autoClickInterval = autoClickInterval;
        _objSize = objSize;
        _objRenderer = objRenderer;
    }
}

public class SaveSystem : MonoBehaviour
{
    private string filePath;
    [SerializeField] private GameObject square;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userdata.json");

        // 게임 시작 시 데이터 불러오기
        LoadUserData();
    }

    private void OnApplicationQuit()
    {
        // 게임 종료 시 데이터 저장
        SaveUserData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 애플리케이션이 일시 중지될 때 데이터 저장
            SaveUserData();
        }
    }

    public void SaveUserData()
    {
        UserData data = new UserData(DateTime.Now.ToString("o"), Manager.Instance.gold, Manager.Instance.value, Manager.Instance.sizeLevel,
            Manager.Instance.moneyLevel, Manager.Instance.autoClickInterval, square.transform.localScale, square.GetComponent<Renderer>().material.color);

        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
            Debug.Log("데이터 저장 완료: " + filePath);
            Debug.Log(json);
        }
        catch (Exception ex)
        {
            Debug.LogError("데이터 저장 실패: " + ex.Message);
        }
    }

    public void LoadUserData()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                UserData data = JsonUtility.FromJson<UserData>(json);
                Debug.Log("데이터 불러오기 완료: " + filePath);
                Debug.Log("마지막 로그아웃 시간: " + data.lastLogoutTime);

                Manager.Instance.gold = BigInteger.Parse(data._gold);
                Manager.Instance.value = BigInteger.Parse(data._value);
                Manager.Instance.sizeLevel = data._sizeLevel;
                Manager.Instance.moneyLevel = data._moneyLevel;
                Manager.Instance.autoClickInterval = data._autoClickInterval;
                square.transform.localScale = data._objSize;
                square.GetComponent<Renderer>().material.color = data._objRenderer;
                Manager.Instance.UpdateUI();
                Debug.Log(json);
                Debug.Log(Manager.Instance.value);

            }
            else
            {
                Debug.LogWarning("저장된 데이터가 없습니다: " + filePath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("데이터 불러오기 실패: " + ex.Message);
        }
    }
}

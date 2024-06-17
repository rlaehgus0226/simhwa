using System;
using System.IO;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class UserData
{
    public string lastLogoutTime;
    public BigInteger _gold;
    public BigInteger _value;
    public int _sizeLevel;
    public int _moneyLevel;
    public float _autoClickInterval;

    public Vector3 _objSize;
    public Color _objRenderer;
}

public class SaveSystem : MonoBehaviour
{
    private string filePath;
    [SerializeField] private GameObject square;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userdata.json");

        // ���� ���� �� ������ �ҷ�����
        LoadUserData();
    }

    private void OnApplicationQuit()
    {
        // ���� ���� �� ������ ����
        SaveUserData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // ���ø����̼��� �Ͻ� ������ �� ������ ����
            SaveUserData();
        }
    }

    public void SaveUserData()
    {
        UserData data = new UserData
        {
            lastLogoutTime = DateTime.Now.ToString("o"),
            _gold = Manager.Instance.gold,
            _value = Manager.Instance.value,
            _sizeLevel = Manager.Instance.sizeLevel,
            _moneyLevel = Manager.Instance.moneyLevel,
            _autoClickInterval = Manager.Instance.autoClickInterval,
            _objSize = square.transform.localScale,
            _objRenderer = square.GetComponent<Renderer>().material.color,
        };

        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
            Debug.Log("������ ���� �Ϸ�: " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("������ ���� ����: " + ex.Message);
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
                Debug.Log("������ �ҷ����� �Ϸ�: " + filePath);
                Debug.Log("������ �α׾ƿ� �ð�: " + data.lastLogoutTime);

                Manager.Instance.gold = data._gold;
                Manager.Instance.value = data._value;
                Manager.Instance.sizeLevel = data._sizeLevel;
                Manager.Instance.moneyLevel = data._moneyLevel;
                Manager.Instance.autoClickInterval = data._autoClickInterval;
                square.transform.localScale = data._objSize;
                square.GetComponent<Renderer>().material.color = data._objRenderer;
                Manager.Instance.UpdateUI();
            }
            else
            {
                Debug.LogWarning("����� �����Ͱ� �����ϴ�: " + filePath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("������ �ҷ����� ����: " + ex.Message);
        }
    }
}

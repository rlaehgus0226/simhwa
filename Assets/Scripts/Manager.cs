using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Numerics;
public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Manager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(nameof(Manager));
                    instance = obj.AddComponent<Manager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    //public DataManager dataManager;

    //public void SetDataManager(DataManager mgr)
    //{
    //    dataManager = mgr;
    //}

    public TextMeshProUGUI goldTxt;
    [SerializeField] public BigInteger gold = 0;
    public BigInteger value = 1;

    public bool autoClickEnable = false;
    public float autoClickInterval = 5.0f;

    public int sizeLevel = 0;
    public int moneyLevel = 0;
    //이 부분을 csv로 해보고 싶음.
    //public float[] sizeUpgradeExpense = { 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };
    public float[] moneyUpgradeExpense = { 100, 100, 300, 300, 500, 1000, 2000, 3000, 5000 };


    public void DecreaseGold()
    {
        gold -= value;
        if (gold < 0)
        {
            gold = 0;
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        goldTxt.text = FormatBigInteger(gold);
    }

    private ScreenTouch screenTouch;
    private Coroutine autoClickCoroutine;
    public IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickInterval);
            if (screenTouch != null)
            {
                screenTouch.OnClick();
            }
        }
    }
    public void StartAutoClickCoroutine()
    {
        if (autoClickCoroutine != null)
        {
            autoClickCoroutine = StartCoroutine(AutoClickCoroutine());
        }
        autoClickCoroutine = StartCoroutine(AutoClickCoroutine());
    }
    public void StopAutoClickCoroutine()
    {
        if (autoClickCoroutine != null)
        {
            StopCoroutine(autoClickCoroutine);
            autoClickCoroutine = null;
        }
    }    private void Start()
    {
        screenTouch = GetComponent<ScreenTouch>();
    }

    private string FormatBigInteger(BigInteger value)
    {
        if (value < 1000)
        {
            return value.ToString();
        }

        string[] suffixes = { "", "K", "M", "B", "T", "Q", "E", "D", "F" };
        int suffixIndex = 0;

        BigInteger decimalValue = (BigInteger)value;
        while (decimalValue >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            decimalValue /= 1000;
            suffixIndex++;
        }

        return string.Format("{0:N1}{1}", decimalValue, suffixes[suffixIndex]);
    }


    //
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else if (instance != this)
        {
            DontDestroyOnLoad (gameObject);
        }

        gold = 0;
        value = 10000;
    }
}
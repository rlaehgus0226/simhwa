using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class Buttons : MonoBehaviour
{
    public DataManager dataManager;

    public Square square;
    public ParticleSystem effect;
    public Vector3 sizeIncrement = new Vector3(0.1f, 0.1f, 0.1f);

    public void Awake()
    {
        dataManager = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        //Manager.Instance.dataManager.ReadJSON();
        dataManager.ReadJSON();
    }
    public void AutoBtn()
    {
        if (Manager.Instance.autoClickEnable)
        {
            Manager.Instance.StopAutoClickCoroutine();
            Manager.Instance.autoClickEnable = false;
        }
        else
        {
            Manager.Instance.StartAutoClickCoroutine();
            Manager.Instance.autoClickEnable = true;
        }
    }

    public void SizeUpBtn()
    {
        BigInteger cost;
        //if (Manager.Instance.sizeLevel >= Manager.Instance.sizeUpgradeExpense.Length)
        if (Manager.Instance.sizeLevel >= dataManager.sizeUpExpArray.Length)
        {
            //cost = Manager.Instance.sizeUpgradeExpense[Manager.Instance.sizeUpgradeExpense.Length - 1];
            cost = (BigInteger)dataManager.sizeUpExpArray[dataManager.sizeUpExpArray.Length - 1];
        }
        else
        {
            //cost = Manager.Instance.sizeUpgradeExpense[Manager.Instance.sizeLevel];
            cost = (BigInteger)dataManager.sizeUpExpArray[Manager.Instance.sizeLevel];
        }

        if (Manager.Instance.gold >= cost)
        {
            Manager.Instance.gold -= cost;
            Manager.Instance.sizeLevel++;
            Manager.Instance.autoClickInterval -= 0.01f;
            square.transform.localScale += sizeIncrement;
            effect.Play();
            Manager.Instance.UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다");
        }
    }

    public void MoneyUpBtn()
    {
        BigInteger cost;
        if (Manager.Instance.moneyLevel >= Manager.Instance.moneyUpgradeExpense.Length)
        {
            cost = (BigInteger)Manager.Instance.moneyUpgradeExpense[Manager.Instance.moneyUpgradeExpense.Length - 1];
        }
        else
        {
            cost = (BigInteger)Manager.Instance.moneyUpgradeExpense[Manager.Instance.moneyLevel];
        }

        if (Manager.Instance.gold >= cost)
        {
            Manager.Instance.gold -= cost;
            Manager.Instance.moneyLevel++;
            Manager.Instance.autoClickInterval -= 0.001f;
            Manager.Instance.value++;
            effect.Play();
            Manager.Instance.UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다");
        }
    }
}

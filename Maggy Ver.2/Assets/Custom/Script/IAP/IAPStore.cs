using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;
using System;
using UnityEngine.Purchasing.Extension;

public class IAPStore : MonoBehaviour
{
    [Header("Consumable")] 
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject unsuccessPanel;
    [SerializeField] GameObject successPanel;
    
    [Serializable]
    public class SkuDetails
    {
        public string productId;
        public string type;
        public string title;
        public string name;
        public string iconUrl;
        public string description;
        public string price;
        public long price_amount_micros;
        public string price_currency_code;
        public string skuDetailsToken;
    }

    [Serializable]
    public class PayloadData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
    }

    [Serializable]
    public class Payload
    {
        public string json;
        public string signature;
        public List<SkuDetails> skuDetails;
        public PayloadData payloadData;
    }

    [Serializable]
    public class Data
    {
        public string Payload;
        public string Store;
        public string TransactionID;
    }

    public Data data;
    public Payload payload;
    public PayloadData payloadData;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("totalBones").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        unsuccessPanel.SetActive(true);
    }
    
    //Consumable
    public void OnPurchaseCoin100Complete(Product product)
    {
        successPanel.SetActive(true);
        Debug.Log(product.definition.id);
        try
        {
            if (product.hasReceipt)
            {
                string receipt = product.receipt;
                data = JsonUtility.FromJson<Data>(receipt);
                payload = JsonUtility.FromJson<Payload>(data.Payload);
                payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

                int quantity = payloadData.quantity;

                for (int i = 0; i < quantity; i++)
                {
                    AddCoin(100);
                }
            }
        }
        catch (Exception)
        {
            Debug.Log("you are using Fake Store!!!");
            AddCoin(100);
        }
    }
    
    public void OnPurchaseCoin250Complete(Product product)
    {
        successPanel.SetActive(true);
        Debug.Log(product.definition.id);
        try
        {
            if (product.hasReceipt)
            {
                string receipt = product.receipt;
                data = JsonUtility.FromJson<Data>(receipt);
                payload = JsonUtility.FromJson<Payload>(data.Payload);
                payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

                int quantity = payloadData.quantity;

                for (int i = 0; i < quantity; i++)
                {
                    AddCoin(250);
                }
            }
        }
        catch (Exception)
        {
            Debug.Log("you are using Fake Store!!!");
            AddCoin(250);
        }
    }
    
    public void OnPurchaseCoin600Complete(Product product)
    {
        successPanel.SetActive(true);
        Debug.Log(product.definition.id);
        try
        {
            if (product.hasReceipt)
            {
                string receipt = product.receipt;
                data = JsonUtility.FromJson<Data>(receipt);
                payload = JsonUtility.FromJson<Payload>(data.Payload);
                payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

                int quantity = payloadData.quantity;

                for (int i = 0; i < quantity; i++)
                {
                    AddCoin(600);
                }
            }
        }
        catch (Exception)
        {
            Debug.Log("you are using Fake Store!!!");
            AddCoin(600);
        }
    }

    void AddCoin(int num)
    {
        int bones = PlayerPrefs.GetInt("totalBones");
        bones += num;
        PlayerPrefs.SetInt("totalBones",bones);
        coinText.text = bones.ToString();
    }
    
    
}

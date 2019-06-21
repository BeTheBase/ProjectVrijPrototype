using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gameManager;
    ObjectPooler objectPooler;

    Rigidbody rb;

    public int Value;

    private void Start()
    {
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;

    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(150, 300, 150));
        rb.AddTorque(new Vector3(300, 300, 300));
    }

    private void OnMouseOver()
    {
        objectPooler.SpawnFromPool("MoneyCoinShower", transform.position, Quaternion.identity);
        gameManager.Gold += Value;
        gameObject.SetActive(false);
    }
}

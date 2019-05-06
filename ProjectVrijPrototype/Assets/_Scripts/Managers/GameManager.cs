using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 EndPosition;

    private void Awake()
    {
        Instance = this;
    }

    public Text GoldText;
    public Text LivesText;

    public int Gold;
    public int Lives;

    // Start is called before the first frame update
    void Start()
    {
        GoldText.text = "Gold:" + Gold;
        LivesText.text = "Lives:" + Lives;   
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = "Gold:" + Gold;
        LivesText.text = "Lives:" + Lives;
    }
}

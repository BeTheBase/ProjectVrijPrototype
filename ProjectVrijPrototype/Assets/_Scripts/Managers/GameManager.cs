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

    public int Gold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = "Gold:" + Gold;
    }
}

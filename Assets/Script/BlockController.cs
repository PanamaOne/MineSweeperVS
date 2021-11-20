using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マス1個ずつにアタッチするスクリプト

public class BlockController : MonoBehaviour
{
    int m_nID;  // 例外的に0オリジンにする
    int m_widthIndex, m_heightIndex;
    bool m_bIsBomb = false;
    //GameObject m_gameObj;
    //FieldManager m_fieldManager;

    BlockController()
    {
        m_nID = -1;
        m_widthIndex = m_heightIndex = 0;
        m_bIsBomb = false;
    }

    public void SetID(int id)
    {
        m_nID = id;
    }

    public void SetIndex(int widthIndex, int heightIndex)
    {
        m_widthIndex = widthIndex;
        m_heightIndex = heightIndex;
    }

    public void SetBomb(bool flag)
    {
        m_bIsBomb = flag;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchAct()
    {
        Debug.Log("Vertical:" + m_widthIndex + "Horizontal" + m_heightIndex);

        if (m_bIsBomb)
        {
            Debug.Log("ドカン！");


            // ゲームオーバー

        }
        GameObject tmpObj = (GameObject)Resources.Load("GrayPanel");
        Debug.Log(tmpObj.name);
        //GameObject bombObjParent = (GameObject)Resources.Load("MineSweeper");
        Sprite[] sprite = Resources.LoadAll<Sprite>("MineSweeper");
        Debug.Log(sprite[1].name);
        
        //Sprite bombObj = Resources.Load<Sprite>("GameOverBomb");
        //Debug.Log(bombObj.name);

        GameObject gObj = GameObject.Find("GameObject");
        FieldManager fManager = gObj.GetComponent<FieldManager>();
        fManager.SwitchPushedColor(m_heightIndex, m_widthIndex);

        //m_gameObj = GameObject.Find("GameObject");
        //m_fieldManager = m_gameObj.GetComponent<FieldManager>();
        //m_fieldManager.SwitchPushedColor(m_heightIndex, m_widthIndex);

        //GameObject pushedPrefab = (GameObject)Resources.Load("GrayPanelPushed");

    }
}

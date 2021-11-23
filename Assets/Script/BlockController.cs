using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マス1個ずつにアタッチするスクリプト

public class BlockController : MonoBehaviour
{
    int m_nID;  // 例外的に0オリジンにする
    FieldManager.BlockType m_nBlockType;
    int m_widthIndex, m_heightIndex;
    bool m_bIsBomb = false;
    int m_nBombCount;

    BlockController()
    {
        m_nID = -1;
        m_widthIndex = m_heightIndex = 0;
        m_bIsBomb = false;
        m_nBombCount = 0;
    }

    public void SetID(int id)
    {
        m_nID = id;
    }

    public void SetBlockType(FieldManager.BlockType type)
    {
        m_nBlockType = type;
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

    public bool GetBomb()
    {
        return m_bIsBomb;
    }

    public void SetBombCount(int num)
    {
        m_nBombCount = num;
    }

    public int GetBombCount()
    {
        return m_nBombCount;
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
        if (m_bIsBomb)
        {

            GameOver();
            return;
        }

        GameObject gObj = GameObject.Find("FieldManager");
        FieldManager fManager = gObj.GetComponent<FieldManager>();
        fManager.SwitchPushedColor(m_heightIndex, m_widthIndex, m_nBombCount);


    }

    void GameOver()
    {
        Debug.Log("ドカン！");
        GameObject gameObj = GameObject.Find("FieldManager");
        FieldManager fManager = gameObj.GetComponent<FieldManager>();
        fManager.ShowUI(true);
    }
}

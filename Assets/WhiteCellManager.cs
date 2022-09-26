using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCellManager : MonoBehaviour
{
    #region Singleton
    public static WhiteCellManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject enemy;
}

using System;
using UnityEngine;

namespace EndTank {
[Serializable]
public class TankManager
{
    #region Variables
             
    public Transform m_SpawnPoint;

    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;


    private TankMovement m_Movement;
    private TankShooting m_Shooting;
    private GameObject m_CanvasGameObject;
    #endregion

    #region Custom Methods
    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<TankMovement>();
        m_Shooting = m_Instance.GetComponent<TankShooting>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
      
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
    #endregion
}
}
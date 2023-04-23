using Cinemachine;
using UnityEngine;

namespace PII
{
    /// <summary>
    /// Set the cinemachinetargets (which transforms the camera follows)
    /// </summary>
    [RequireComponent(typeof(CinemachineTargetGroup))]
    public class CinemachineTarget : MonoBehaviour
    {
        private CinemachineTargetGroup CinemachineTargetGroup;
        private void Awake()
        {
            CinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
        }
        private void Start()
        {
            SetMachineTargetGroup();
        }
        private void SetMachineTargetGroup()
        {
            CinemachineTargetGroup.Target CinemachineTargetGroup_player = new CinemachineTargetGroup.Target
            {
                weight = 1f,
                radius = 1f,
                target = GameManager.Instance.Player.transform
            };

            CinemachineTargetGroup.Target[] CinemachineTargetGroupArray =
                new CinemachineTargetGroup.Target[]{
                CinemachineTargetGroup_player
            };

            CinemachineTargetGroup.m_Targets = CinemachineTargetGroupArray;


        }
    }
}
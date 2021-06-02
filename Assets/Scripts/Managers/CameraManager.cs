using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ForestValley.Assistants;
using ForestValley.Managers;
using UnityEngine;

namespace ForestValley
{
    public class CameraManager : Singleton<CameraManager>, IManager
    {
        [SerializeField] private Camera mainCameraPrefab;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCameraPrefab;

        public Camera MainCameraInstance { get; private set; }
        public CinemachineVirtualCamera CinemachineVirtualCameraInstance { get; private set; }

        public void Init()
        {
            MainCameraInstance = Instantiate(mainCameraPrefab);
            CinemachineVirtualCameraInstance = Instantiate(cinemachineVirtualCameraPrefab);

            var levelManager = GameManager.gm.FindController<LevelManager>();
            CinemachineVirtualCameraInstance.Follow = levelManager.PlayerFacadeInstance.transform;
        }
    }
}

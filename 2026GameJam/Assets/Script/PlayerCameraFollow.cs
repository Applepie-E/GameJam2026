using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("相机配置（仅需设置一次）")]
    [SerializeField] private float cameraFixedZ = -10f; // 相机固定Z轴位置（需与MainCamera的Z一致）
    private Transform mainCameraTrans; // 主相机Transform缓存（避免重复获取，提升性能）

    // 场景加载时执行一次：获取主相机组件，判空避免报错
    private void Awake()
    {
        // 通过标签获取主相机（确保相机Tag为MainCamera）
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("场景中未找到Tag为MainCamera的相机，请检查相机标签！");
            return;
        }
        mainCameraTrans = mainCamera.transform;
    }

    // 每一帧Update后执行：避免Player位置更新后相机抖动，保证同步性
    private void LateUpdate()
    {
        // 判空：相机未找到则不执行
        if (mainCameraTrans == null) return;

        // 核心：相机位置 = Player位置（X/Y同步） + 固定Z轴（避免视口偏移/遮挡）
        Vector3 targetCamPos = transform.position;
        targetCamPos.z = cameraFixedZ; // 强制固定相机Z轴，仅同步X/Y
        mainCameraTrans.position = targetCamPos;

        // 锁定相机旋转：始终保持默认旋转，避免Player旋转导致画面歪斜
        mainCameraTrans.rotation = Quaternion.identity;
    }
}


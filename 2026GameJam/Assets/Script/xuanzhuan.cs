using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xuanzhuan : MonoBehaviour
{
    [Header("被看向的目标精灵")]
    public Transform target;

    [Header("旋转速度，0=瞬间转向")]
    public float rotateSpeed = 360f;

    [Header("当前实时朝向(只读)")]
    public Vector2 currentFaceDir;

    [Header("当前旋转角度(只读)")]
    public float currentZAngle;

    private void Update()
    {
        target=transform.parent;
        if (target == null) return;

        // 1. 计算指向目标的方向
        Vector2 dir = target.position - transform.position;

        if (dir != Vector2.zero)
        {
            // 2. 转成Z轴旋转角度
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // 3. 平滑/瞬间旋转到目标角度
            if (rotateSpeed <= 0)
            {
                // 瞬间转向
                transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
            else
            {
                // 平滑旋转
                float currentAngle = transform.eulerAngles.z;
                float smoothAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
            }
        }

        // 4. 实时更新：当前朝向向量 & 当前角度
        currentFaceDir = transform.right;
        currentZAngle = transform.eulerAngles.z;
    }
}

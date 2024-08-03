using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowClone : MonoBehaviour
{
    public string cloneTag = "Untagged1";  // Tên thẻ của đối tượng clone
    private Transform clone;  // Biến lưu trữ đối tượng clone

    public float smoothSpeed = 0.125f;  // Tốc độ di chuyển mượt mà của camera
    public Vector3 offset;  // Độ lệch giữa camera và đối tượng clone

    void Update()
    {
        // Tìm đối tượng clone theo thẻ và lấy Transform của nó nếu chưa có
        if (clone == null)
        {
            GameObject cloneObject = GameObject.FindGameObjectWithTag(cloneTag);
            if (cloneObject != null)
            {
                clone = cloneObject.transform;
            }
        }
    }

    void LateUpdate()
    {
        // Nếu đối tượng clone tồn tại, camera sẽ theo dõi nó
        if (clone != null)
        {
            Vector3 desiredPosition = clone.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
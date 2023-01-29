using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil : MonoBehaviour
{
    public float maxoffsetDistance;
    public float recoilAcceration;
    public float weaponrecoilstartspeed;


    private bool recoileffect;
    private bool weaponheadedbacktostartposition;

    private Vector3 offsetPosition;
    private Vector3 recoilSpeed;

    public void addrecoil()
    {
        recoileffect = true;
        weaponheadedbacktostartposition = false;
        recoilSpeed = transform.right * -weaponrecoilstartspeed;

    }
    void Start()
    {
        recoilSpeed = Vector3.zero;
        offsetPosition = Vector3.zero;

        recoileffect = false;
        weaponheadedbacktostartposition = false;
    }

    void Update()
    {
        updaterecoil();
    }
    private void updaterecoil()
    {
        if (recoileffect == false)
        {
            return;
        }
        recoilSpeed += (-offsetPosition.normalized) * recoilAcceration * Time.deltaTime;
        Vector3 newOffsetPosition = offsetPosition + recoilSpeed * Time.deltaTime;
        Vector3 newTransformPosition = transform.position - offsetPosition;

        if (newOffsetPosition.magnitude > maxoffsetDistance)
        {
            recoilSpeed = Vector3.zero;
            weaponheadedbacktostartposition = true;
            newOffsetPosition = offsetPosition.normalized * maxoffsetDistance;
        }
        else if (weaponheadedbacktostartposition == true && newOffsetPosition.magnitude > offsetPosition.magnitude)
        {
            transform.position -= offsetPosition;
            offsetPosition = Vector3.zero;

            recoileffect = false;
            weaponheadedbacktostartposition = false;
            return;
        }
        transform.position = newTransformPosition + newOffsetPosition;
        offsetPosition = newOffsetPosition;
    }
}

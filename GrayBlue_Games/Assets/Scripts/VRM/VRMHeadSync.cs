using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniHumanoid;
using GrayBlue;
using GrayBlue.Rx;

namespace GrayBlue_Games {
    public class VRMHeadSync : GrayBlueGameSceneBase {
        [SerializeField] Animator animator;

        Transform head;
        Peripheral grayBluePeripheral;

        async void Start() {
            // get head
            head = animator.GetBoneTransform(HumanBodyBones.Head);
            // scan
            grayBluePeripheral = await FindFirstGrayBlueAsync();
            // apply rotation
            var baseSensorQuat = Quaternion.identity;
            var baseHeadQuat = head?.rotation ?? Quaternion.identity;
            grayBluePeripheral?
                .ReactiveIMUData()
                .Take(1)
                .Subscribe(x => { baseSensorQuat = Quaternion.Inverse(x.unity.quat); })
                .AddTo(this);
            grayBluePeripheral?
                .ReactiveIMUData()
                .Skip(1)
                .Subscribe(x => {
                    var sensorRelativeRotation = baseSensorQuat * x.unity.quat;
                    // reverse x,y like mirror
                    var euler = sensorRelativeRotation.eulerAngles;
                    head.localRotation = Quaternion.Euler(-euler.x, -euler.y, euler.z);
                })
                .AddTo(this);
            grayBluePeripheral?.ListenEvent();
        }
    }
}

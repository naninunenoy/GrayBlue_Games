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
            var baseQuat = Quaternion.identity;
            grayBluePeripheral?
                .ReactiveIMUData()
                .Take(1)
                .Subscribe(x => { baseQuat = Quaternion.Inverse(x.unity.quat); })
                .AddTo(this);
            grayBluePeripheral?
                .ReactiveIMUData()
                .Skip(1)
                .Subscribe(x => { head.localRotation = baseQuat * x.unity.quat; })
                .AddTo(this);
            grayBluePeripheral?.ListenEvent();
        }
    }
}

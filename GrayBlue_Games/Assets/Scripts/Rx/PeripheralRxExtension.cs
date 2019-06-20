using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GrayBlue.Rx {
    public static class PeripheralRxExtension {
        public static IObservable<Unit> DeviceLostObservable(this Peripheral p) {
            return Observable
                .FromEvent(h => p.DeviceLostEvent += h, h => p.DeviceLostEvent -= h);
        }
        public static IReadOnlyReactiveProperty<Vector3> ReactiveAccel(this Peripheral p) {
            return Observable
                .FromEvent<Vector3>(h => p.AccelUpdateEvent += h, h => p.AccelUpdateEvent -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IReadOnlyReactiveProperty<Vector3> ReactiveGyro(this Peripheral p) {
            return Observable
                .FromEvent<Vector3>(h => p.GyroUpdateEvent += h, h => p.GyroUpdateEvent -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IReadOnlyReactiveProperty<Vector3> ReactiveCompass(this Peripheral p) {
            return Observable
                .FromEvent<Vector3>(h => p.CompassUpdateEvent += h, h => p.CompassUpdateEvent -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IReadOnlyReactiveProperty<Quaternion> ReactiveQuaternion(this Peripheral p) {
            return Observable
                .FromEvent<Quaternion>(h => p.QuaternionUpdateEvent += h, h => p.QuaternionUpdateEvent -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IReadOnlyReactiveProperty<IMUData> ReactiveIMUData(this Peripheral p) {
            return Observable
                .FromEvent<IMUData>(h => p.IMUSensorUpdateEvent += h, h => p.IMUSensorUpdateEvent -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IObservable<ButtonType> ButtonPushObservable(this Peripheral p) {
            return Observable
                .FromEvent<DeviceButton>(h => p.ButtonPushEvent += h, h => p.ButtonPushEvent -= h)
                .Select(x => x.button);
        }
        public static IObservable<ButtonType> ButtonReleaseObservable(this Peripheral p) {
            return Observable
                .FromEvent<DeviceButton>(h => p.ButtonReleaseEvent += h, h => p.ButtonReleaseEvent -= h)
                .Select(x => x.button);
        }
    }
}

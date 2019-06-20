using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GrayBlue;

namespace GrayBlue_Games {
    public abstract class GrayBlueGameSceneBase : MonoBehaviour {
        [SerializeField] protected Button backSceneButton = default;

        protected Central grayBlueCentral = default;

        protected virtual void Awake() {
            // 戻るボタン
            backSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("Top");
            });
            // GrayBlue取得
            grayBlueCentral = Central.Instance;
        }

        protected async virtual Task<Peripheral> FindFirstGrayBlueAsync() {
            if (grayBlueCentral.KnownDevices.Any()) {
                // 接続済みデバイスがある場合はそれを基にして返す
                var knownBleDevice = grayBlueCentral.KnownDevices.First();
                return new Peripheral(knownBleDevice);
            }
            // GrayBlueの有効化
            var valid = await grayBlueCentral.ValidateAsync();
            if (!valid) {
                Debug.Log("GrayBlue validate failed");
                return null;
            }
            // スキャン
            var deviceIds = await grayBlueCentral.ScanAsync();
            // 接続
            if (deviceIds == null || deviceIds.Length < 1) {
                Debug.Log("GrayBlue not found");
                return null;
            }
            Debug.Log($"GrayBlue found {deviceIds.Length} devices");
            return await ConnectAsync(deviceIds[0]);
        }

        protected async Task<Peripheral> ConnectAsync(string id) {
            // Ble connect
            Debug.Log($"GrayBlue try connect. id={id}");
            var ble = new BLEDevice(id);
            var success = await grayBlueCentral.ConnectAsync(id, ble);
            if (!success) {
                Debug.Log($"GrayBlue connect failed. id={id}");
                return null;
            }
            Debug.Log($"GrayBlue connect done. id={id}");
            return new Peripheral(ble);
        }
    }
}

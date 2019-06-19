using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GrayBlue;

namespace GrayBlue_Games {
    public abstract class GrayBlueGameSceneBase : MonoBehaviour {
        [SerializeField] protected Button backSceneButton = default;

        protected Central grayBlue = default;
        protected IList<Peripheral> peripherals = default;

        protected  virtual void Awake() {
            // 戻るボタン
            backSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("Top");
            });
            // GrayBlue取得
            grayBlue = Central.Instance;
            peripherals = new List<Peripheral>();
        }

        protected async virtual Task SetUpGrayBlueAsync() {
            // GrayBlueの有効化
            await grayBlue.ValidateAsync();
            // スキャン
            var deviceIds = await grayBlue.ScanAsync();
            // 接続
            if (deviceIds != null && deviceIds.Length > 0) {
                foreach (var id in deviceIds) {
                    var peripheral = await ConnectAsync(id);
                    if (peripheral != null) {
                        peripherals.Add(peripheral);
                    }
                }
            }
        }

        async Task<Peripheral> ConnectAsync(string id) {
            // Ble connect
            var ble = new BLEDevice(id);
            var success = await grayBlue.ConnectAsync(id, ble);
            if (!success) {
                return null;
            }
            return new Peripheral(ble);
        }
    }
}

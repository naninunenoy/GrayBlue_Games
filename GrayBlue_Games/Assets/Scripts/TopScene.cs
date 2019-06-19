using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GrayBlue;

namespace GrayBlue_Games {
    public class TopScene : MonoBehaviour {
        [SerializeField] Button grayBlueButton;
        [SerializeField] M5StackImage m5StackImage;
        [SerializeField] Button testSceneButton;
        [SerializeField] Button shootingSceneButton;
        Central grayBlue = default;

        void Awake() {
            testSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("BLETest");
            });
            shootingSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("GyroShooting");
            });
            grayBlue = Central.Instance;
        }
    }
}

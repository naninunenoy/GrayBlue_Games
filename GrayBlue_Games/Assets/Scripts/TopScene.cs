using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GrayBlue_Games {
    public class TopScene : MonoBehaviour {
        [SerializeField] Button testSceneButton;
        [SerializeField] Button shootingSceneButton;
        void Start() {
            testSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("BLETest");
            });
            shootingSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("GyroShooting");
            });
        }
    }
}

using UnityEngine;
using System.Collections;

public class SpecialCamera : MonoBehaviour {
    enum Movement { None, Right, Left, Top, Bottom };
    GameObject[] players;
    float originalSize;
    Vector3 originalPos;
    Camera cam;

    public static SpecialCamera getSpecialCamera() {
        return (SpecialCamera) HushPuppy.safeFindComponent("MainCamera", "SpecialCamera");
    }

    void Start() {
        originalPos = this.transform.localPosition;
        cam = this.GetComponent<Camera>();
        originalSize = cam.orthographicSize;
    }

    #region Screen Shake
    public void screenShake_(float power) { StartCoroutine(screenShake(power)); }
    IEnumerator screenShake(float power) {
        if (power < 0.05f) {
            power = 0.1f;
        }

        for (int i = 0; i < 10; i++) {
            yield return new WaitForEndOfFrame();
            this.transform.localPosition = new Vector3(originalPos.x + Mathf.Pow(-1, Random.Range(0, 2)) * Random.Range(power / 4, power / 2),
                                                       originalPos.y + Mathf.Pow(-1, Random.Range(0, 2)) * Random.Range(power / 4, power / 2),
                                                       originalPos.z);
        }

        this.transform.localPosition = originalPos;
    }
    #endregion
}

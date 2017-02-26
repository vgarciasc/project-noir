﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public static class HushPuppy {
    //public static ItemSpawner itemSpawner;
    //public static GameController gameController;

    static HushPuppy() {
        //itemSpawner = (ItemSpawner) safeFindComponent("GameController", "ItemSpawner");
        //gameController = (GameController) safeFindComponent("GameController", "GameController");
        //playerDatabase = (PlayerDatabase)safeFindComponent("PlayerDatabase", "PlayerDatabase");
        //itemSpawnLocations = (SpawnLocations)safeFindComponent("ItemSpawnLocations", "SpawnLocations");
        //playerSpawnLocations = (SpawnLocations)safeFindComponent("PlayerSpawnLocations", "SpawnLocations");

        //canvas = (Canvas)safeFindComponent("Canvas", "Canvas");

        //playerUIContainer = safeFind("PlayerUIContainer");
    }

    public static GameObject safeFind(string name) {
        GameObject go = GameObject.FindGameObjectWithTag(name);
        if (go == null) {
            Debug.Log("'" + name + "' not found.");
            Debug.Break();
        }

        return go;
    }

    public static Component safeComponent(GameObject go, string componentName) {
        Component c = go.GetComponent(componentName);
        if (c == null) {
            Debug.Log("'" + componentName + "' component not found in GameObject '" + go.name + "'.");
            Debug.Break();
        }

        return c;
    }

    public static Component safeFindComponent(string gameObjectName, string componentName) {
        GameObject go = safeFind(gameObjectName);
        if (go == null) {
            Debug.Log("'" + gameObjectName + "' not found.");
            Debug.Break();
            return null;
        }
        Component c = safeComponent(go, componentName);
        return c;
    }

    public static Object safeFindManager(string gameObjectName, string managerName) {
        //todo
        return null;
    }

    public static IEnumerator WaitUntilNFrames(int n) {
        for (int i = 0; i < n; i++)
            yield return new WaitForEndOfFrame();
    }

    //Static methods that should exist in unity
    #region Unity Methods

    public static void destroyChildren(GameObject go) {
        foreach (Transform child in go.transform)
            GameObject.Destroy(child.gameObject);
    }

    public static void switchVisibilityOfChildren(GameObject g, bool active) {
        foreach (Image i in g.GetComponentsInChildren<Image>())
            i.enabled = active;
        foreach (Text t in g.GetComponentsInChildren<Text>())
            t.enabled = active;
    }

    public static void switchRaycastsOfChildren(GameObject g, bool active) {
        foreach (Transform child in g.transform)
            child.GetComponent<CanvasGroup>().blocksRaycasts = active;
    }

    public static void fadeImgIn(GameObject g, float duration) {
        g.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        g.GetComponent<Image>().CrossFadeAlpha(1f, duration, false);
    }

    public static void fadeImgOut(GameObject g, float duration) {
        g.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
        g.GetComponent<Image>().CrossFadeAlpha(0f, duration, false);
    }

    public static void fadeIn(GameObject g, float duration) {
        foreach (Image i in g.GetComponentsInChildren<Image>()) {
            i.canvasRenderer.SetAlpha(0.0f);
            i.CrossFadeAlpha(1f, duration, false);
        }
        foreach (Text t in g.GetComponentsInChildren<Text>()) {
            t.canvasRenderer.SetAlpha(0.0f);
            t.CrossFadeAlpha(1f, duration, false);
        }
    }

    public static void fadeOut(GameObject g, float duration) {
        foreach (Image i in g.GetComponentsInChildren<Image>()) {
            i.canvasRenderer.SetAlpha(1.0f);
            i.CrossFadeAlpha(1f, duration, false);
        }
        foreach (Text t in g.GetComponentsInChildren<Text>()) {
            t.canvasRenderer.SetAlpha(1.0f);
            t.CrossFadeAlpha(1f, duration, false);
        }
    }

    public static void acrobaticSilhouette(GameObject g, bool zh) {
        if (zh) {
            foreach (Image i in g.GetComponentsInChildren<Image>())
                i.color = new Color(0f, 0f, 0f);
        } else {
            foreach (Image i in g.GetComponentsInChildren<Image>())
                i.color = new Color(1f, 1f, 1f);
        }
    }

    public static void acrobaticSilhouetteFade(GameObject g, float duration, bool zh) {
        if (zh) {
            foreach (Image i in g.GetComponentsInChildren<Image>()) {
                i.canvasRenderer.SetAlpha(1.0f);
                i.CrossFadeColor(new Color(0f, 0f, 0f), duration, false, false);
            }
        } else {
            foreach (Image i in g.GetComponentsInChildren<Image>()) {
                i.canvasRenderer.SetAlpha(1.0f);
                i.CrossFadeColor(new Color(1f, 1f, 1f), duration, false, false);
            }
        }
    }

    public static void changeOpacity(GameObject g, float value) {
        foreach (Image aux in g.GetComponentsInChildren<Image>())
            aux.color = new Color(aux.color.r, aux.color.g, aux.color.b, value);

        foreach (Text aux in g.GetComponentsInChildren<Text>())
            aux.color = new Color(aux.color.r, aux.color.g, aux.color.b, value);
    }

    public static Color getColorWithOpacity(Color color, float opacity) {
        if (opacity < 0f || opacity > 1f) return color;
        return new Color(color.r, color.g, color.b, opacity);
    }

    public static Color invertColor(Color color) {
        return (new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a));
    }
    #endregion

    public static Vector2 rotateVector(Vector2 original, float angle_rad) {
        float angle = angle_rad * Mathf.PI / 180f;
        Vector2 aux = new Vector2(original.x * Mathf.Cos(angle) - original.y * Mathf.Sin(angle),
                                original.x * Mathf.Sin(angle) + original.y * Mathf.Cos(angle));
        return aux;
    }
}
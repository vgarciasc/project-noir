﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAnimationScript : MonoBehaviour {
    public void AnimDestroy() {
        Destroy(this.gameObject);
    }
}
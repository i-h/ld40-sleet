using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowable {
    bool GetFocusOn();
    Vector2 GetMoveTarget();
    Vector2 GetCurrentPosition();
}

using System;
using System.Numerics;

namespace RunawaySystems.Pong {
    public class Transform {

        public Transform? Parent;
        public Vector2 Position;


        #region Constructors
        public Transform() {
            Parent = null;
            Position = Vector2.Zero;
        }

        public Transform(Vector2 localPosition) {
            Parent = null;
            Position = localPosition;
        }

        public Transform(Transform parent) {
            Parent = parent;
            Position = Vector2.Zero;
        }

        public Transform(Transform parent, Vector2 localPosition) {
            Parent = parent;
            Position = localPosition;
        }
        #endregion Constructors
    }
}

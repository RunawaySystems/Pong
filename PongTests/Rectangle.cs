using System;
using Xunit;
using RunawaySystems.Pong.Geometry;
using System.Numerics;

namespace PongTests {
    public class Rectangle {

        [Fact]
        public void Overlaps() {
            var a = new RunawaySystems.Pong.Geometry.Rectangle(new Vector2(-0.9f, -0.9f), new Vector2(1f, 1f));
            var b = new RunawaySystems.Pong.Geometry.Rectangle(new Vector2(0f, 0f), new Vector2(1f, 1f));

            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(-0.9f, 0.9f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(0.9f, 0.9f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(0.9f, -0.9f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));
        }

        [Fact]
        public void OverlapsOnEdge() {
            var a = new RunawaySystems.Pong.Geometry.Rectangle(new Vector2(-1f, 0f), new Vector2(1f, 1f));
            var b = new RunawaySystems.Pong.Geometry.Rectangle(new Vector2(0f, 0f), new Vector2(1f, 1f));

            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(0f, 1f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(1f, 0f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));

            a.Position = new Vector2(0f, -1f);
            Assert.True(a.Overlaps(b));
            Assert.True(b.Overlaps(a));
        }
    }
}

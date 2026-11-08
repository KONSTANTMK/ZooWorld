using System;
using System.Numerics;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal static class ScreenBoundsExtensions
    {
        private const float MinInwardShare = 0.3f;
        private const int Inward = 1;
        private const int Outward = -1;
        private const int Inside = 0;

        public static bool Contains(this IScreenBounds bounds, Vector2 position)
        {
            return position.X >= bounds.Min.X && position.X <= bounds.Max.X
                && position.Y >= bounds.Min.Y && position.Y <= bounds.Max.Y;
        }

        public static bool TryReflectInward(
            this IScreenBounds bounds,
            Vector2 position,
            Vector2 direction,
            out Vector2 reflected)
        {
            int inwardX = GetInwardSign(position.X, bounds.Min.X, bounds.Max.X);
            int inwardY = GetInwardSign(position.Y, bounds.Min.Y, bounds.Max.Y);
            bool isReflectedX = TryReflectAxis(direction.X, inwardX, out float x);
            bool isReflectedY = TryReflectAxis(direction.Y, inwardY, out float y);
            bool isReflected = isReflectedX || isReflectedY;
            reflected = isReflected ? Vector2.Normalize(new Vector2(x, y)) : direction;
            return isReflected;
        }

        private static int GetInwardSign(float position, float min, float max)
        {
            if (position < min)
            {
                return Inward;
            }

            return position > max ? Outward : Inside;
        }

        private static bool TryReflectAxis(float direction, int inwardSign, out float reflected)
        {
            reflected = direction;
            if (inwardSign == Inside || direction * inwardSign >= MinInwardShare)
            {
                return false;
            }

            reflected = inwardSign * MathF.Max(MathF.Abs(direction), MinInwardShare);
            return true;
        }
    }
}

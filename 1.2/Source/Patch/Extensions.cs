using System.Collections.Generic;
using UnityEngine;

namespace Reorderer.Patch
{
    internal static class Extensions
    {
        public static Rect Fix(this Rect self) => new Rect(Mathf.Round(self.x), Mathf.Round(self.y), Mathf.Round(self.width), Mathf.Round(self.height));

        public static Rect[] GetHGrid(this Rect self, float padding, params float[] widths)
        {
            var unfixedCount = 0;
            var currentX = self.x;
            var fixedWidths = 0f;

            var rects = new List<Rect> { self };

            for (var index = 0; index < widths.Length; index++)
            {
                var width = widths[index];
                if (width >= 0f) { fixedWidths += width; }
                else { unfixedCount++; }

                if (index != (widths.Length - 1)) { fixedWidths += padding; }
            }

            var unfixedWidth = unfixedCount > 0 ? (self.width - fixedWidths) / unfixedCount : 0f;

            foreach (var width in widths)
            {
                float newWidth;

                if (width >= 0f)
                {
                    newWidth = width;
                    rects.Add(new Rect(currentX, self.y, newWidth, self.height).Fix());
                }
                else
                {
                    newWidth = unfixedWidth;
                    rects.Add(new Rect(currentX, self.y, newWidth, self.height).Fix());
                }

                currentX += newWidth + padding;
            }

            return rects.ToArray();
        }
    }
}

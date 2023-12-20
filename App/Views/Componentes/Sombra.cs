using System;
using Microsoft.Maui.Graphics;

namespace App.Views.Componentes;

public class Sombra : GraphicsView
{
    public Sombra()
    {
        Drawable = new GradientShadowDrawable();
    }

    private class GradientShadowDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var gradient = new LinearGradientPaint
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new[]
                {
                    new PaintGradientStop(0.1f, Color.FromArgb("#33000000")),
                    new PaintGradientStop(1f, Colors.Transparent)
                }
            };

            canvas.SetFillPaint(gradient, dirtyRect);
            canvas.FillRectangle(dirtyRect);
        }
    }
}


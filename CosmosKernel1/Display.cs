using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;

namespace CosmosKernel1 {
	public class Display {
        private static int screenX = 800;
        private static int screenY = 600;
        private static Color[] pixelBuffer = new Color[ (screenX * screenY) + screenX ];
        private static Color[] pixelBufferOld = new Color[ (screenX * screenY) + screenX ];
        private static Canvas canvas = FullScreenCanvas.GetFullScreenCanvas();

        public static void Init() {
            canvas.Mode = new Mode( screenX, screenX, ColorDepth.ColorDepth32 );
        }

        public static void ClearScreen( Color c ) {
            for ( int i = 0, len = pixelBuffer.Length; i < len; i++ ) {
                pixelBuffer[ i ] = c;
            }
        }

        public static void SetPixel( int x, int y, Color c ) {
            if ( x > screenX || y > screenY ) {
                return;
            }

            pixelBuffer[ (x * y) + x ] = c;
        }

        public static void DrawScreen() {
            Pen pen = new Pen( Color.Red );

            for ( int y = 0, h = screenY; y < h; y++ ) {
                for ( int x = 0, w = screenX; x < w; x++ ) {
                    if ( !(pixelBuffer[ (y * x) + x ] == pixelBufferOld[ (y * y) + x ]) ) {
                        pen.Color = pixelBuffer[ (y * screenX) + x ];
                        canvas.DrawPoint( pen, x, y );
                    }
                }
            }

            for ( int i = 0, len = pixelBuffer.Length; i < len; i++ ) {
                pixelBuffer[ i ] = pixelBufferOld[ i ];
            }
        }

        public static void Update() {
            ClearScreen( Color.Blue );
            SetPixel( 1, 1, Color.Black );
            SetPixel( 1, 2, Color.Black );
            SetPixel( 2, 1, Color.Black );
            SetPixel( 2, 2, Color.Black );
            DrawScreen();
        }
    }
}

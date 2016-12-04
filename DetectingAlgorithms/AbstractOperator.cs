using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAlgorithms
{
    public abstract class AbstractOperator : IDetectingAlgorithm
    {

        public string Name { get; protected set; }

        protected Bitmap _source;

        protected int _limit;

        protected int[,] _gX = null;

        protected int[,] _gY = null;

        public AbstractOperator(Bitmap image, int limit)
        {
            this._source = image;
            this._limit = limit;
        }

        public AbstractOperator()
        {
        }

        public Bitmap DetecteEdges(Bitmap mapBitmap, int limit)
        {
            this._source = mapBitmap;
            this._limit = limit;
            return this.Detect(this._source);
        }

        public Bitmap DetecteEdges()
        {
            return this.Detect(this._source);
        }

        protected virtual Bitmap Detect(Bitmap mapBitmap)
        {
            Dictionary<string, int[,]> colorsMap = this.SelectColorsFromPixels(mapBitmap);

            Bitmap resultBitmap = this.FillWithDetectionColor(mapBitmap, colorsMap["red"], colorsMap["green"],
                colorsMap["blue"]);

            return resultBitmap;
        }

        protected Bitmap FillWithDetectionColor(Bitmap map, int[,] redColorMap, int[,] greenColorMap,
            int[,] blueColorMap)
        {
            int limit = this._limit*this._limit;
            int[,] Gx = this._gX;
            int[,] Gy = this._gY;

            Bitmap resultBitmap = map;
            for (int i = 1; i < map.Width - 1; i++)
            {
                for (int j = 1; j < map.Height - 1; j++)
                {
                    var newRx = 0;
                    var newRy = 0;
                    var newGx = 0;
                    var newGy = 0;
                    var newBx = 0;
                    var newBy = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            var rc = redColorMap[i + hw, j + wi];
                            newRx += Gx[wi + 1, hw + 1]*rc;
                            newRy += Gy[wi + 1, hw + 1]*rc;

                            var gc = greenColorMap[i + hw, j + wi];
                            newGx += Gx[wi + 1, hw + 1]*gc;
                            newGy += Gy[wi + 1, hw + 1]*gc;

                            var bc = blueColorMap[i + hw, j + wi];
                            newBx += Gx[wi + 1, hw + 1]*bc;
                            newBy += Gy[wi + 1, hw + 1]*bc;
                        }
                    }
                    if (newRx*newRx + newRy*newRy > limit || newGx*newGx + newGy*newGy > limit ||
                        newBx*newBx + newBy*newBy > limit)
                        resultBitmap.SetPixel(i, j, Color.Black);
                    else
                        resultBitmap.SetPixel(i, j, Color.Transparent);
                }
            }

            return resultBitmap;
        }

        protected Dictionary<string, int[,]> SelectColorsFromPixels(Bitmap map)
        {
            Dictionary<string, int[,]> colorsMap = new Dictionary<string, int[,]>();
            int width = map.Width;
            int height = map.Height;
            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = map.GetPixel(i, j).R;
                    allPixG[i, j] = map.GetPixel(i, j).G;
                    allPixB[i, j] = map.GetPixel(i, j).B;
                }
            }
            colorsMap.Add("red", allPixR);
            colorsMap.Add("green", allPixG);
            colorsMap.Add("blue", allPixB);

            return colorsMap;
        }
    }
}
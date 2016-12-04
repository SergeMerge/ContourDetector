using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAlgorithms
{
    public class SobelOperator : AbstractOperator
    {
        public SobelOperator(Bitmap image, int limit) : base(image, limit)
        {
            this.Init();
        }

        public SobelOperator() : base()
        {
            this.Init();            
        }

        private void Init()
        {
            this.Name = "Sobel operator";
            this._gX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            this._gY = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        }
    }
}

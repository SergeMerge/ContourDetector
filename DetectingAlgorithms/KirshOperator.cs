using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAlgorithms
{
    public class KirshOperator: AbstractOperator
    {
        public KirshOperator(Bitmap image, int limit) : base(image, limit)
        {
            this.Init();
        }

        public KirshOperator() : base()
        {
            this.Init();
        }

        private void Init()
        {
            this.Name = "Kirsh operator";
            this._gX = new int[,] { { -3, -3, 5 }, { -3, -3, 5 }, { -3, -3, 5 } };
            this._gY = new int[,] { { -3, -3, -3 }, { -3, -3, -3 }, { 5, 5, 5 } };
        }
    }
}

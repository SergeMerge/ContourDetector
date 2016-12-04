using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAlgorithms
{
    public class PrewittOperator: AbstractOperator
    {
        public PrewittOperator(Bitmap image, int limit) : base(image, limit)
        {
            this.Init();
        }

        public PrewittOperator() : base()
        {
            this.Init();
        }

        private void Init()
        {
            this.Name = "Prewitt operator";
            this._gX = new int[,] { { -1, 0, 1 }, { -1, 0, 2 }, { -1, 0, 1 } };
            this._gY = new int[,] { { -1, -1, -1 }, { 0, 0, 0 }, { -1, -1, -1 } };
        }
    }
}

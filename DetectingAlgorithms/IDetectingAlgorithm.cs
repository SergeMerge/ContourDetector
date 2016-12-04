using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DetectingAlgorithms
{
    public interface IDetectingAlgorithm
    {
        Bitmap DetecteEdges(Bitmap source, int limit);
    }
}

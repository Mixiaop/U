using System.Collections.Generic;

namespace U.Segmentation.Jieba.FinalSeg
{
    public interface IFinalSeg
    {
        IEnumerable<string> Cut(string sentence);
    }
}

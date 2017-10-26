using System.Collections.Generic;
using System.Linq;

namespace U.Segmentation
{
    public class Constants
    {
        public static string RESOURCE_PATH = "/Config/U/Segmentation/Resources/";
        public static readonly double MinProb = -3.14e100;

        public static readonly List<string> NounPos = new List<string>() { "n", "ng", "nr", "nrfg", "nrt", "ns", "nt", "nz" };
        public static readonly List<string> VerbPos = new List<string>() { "v", "vd", "vg", "vi", "vn", "vq" };
        public static readonly List<string> NounAndVerbPos = NounPos.Union(VerbPos).ToList();
        public static readonly List<string> IdiomPos = new List<string>() { "i" };
    }
}

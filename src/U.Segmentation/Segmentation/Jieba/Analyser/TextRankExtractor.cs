using System.Collections.Generic;
using System.Linq;
using U.Segmentation.Jieba.PosSeg;

namespace U.Segmentation.Jieba.Analyser
{
    /// <summary>
    /// TextRank算法提取器
    /// TextRank 算法是一种用于文本的基于图的排序算法。
    /// 其基本思想来源于谷歌的 PageRank算法, 通过把文本分割成若干组成单元(单词、句子)并建立图模型, 利用投票机制对文本中的重要成分进行排序, 
    /// 仅利用单篇文档本身的信息即可实现关键词提取、文摘。和 LDA、HMM 等模型不同, TextRank不需要事先对多篇文档进行学习训练, 因其简洁有效而得到广泛应用。
    /// </summary>
    public class TextRankExtractor : KeywordExtractor
    {
        private static readonly IEnumerable<string> DefaultPosFilter = new List<string>()
        {
            "n", "ng", "nr", "nrfg", "nrt", "ns", "nt", "nz", "v", "vd", "vg", "vi", "vn", "vq"
        };
        private JiebaSettings _settings = U.UPrimeEngine.Instance.Resolve<JiebaSettings>();

        private JiebaSegmenter Segmenter { get; set; }
        private PosSegmenter PosSegmenter { get; set; }

        public int Span { get; set; }

        public bool PairFilter(Pair wp)
        {
            return DefaultPosFilter.Contains(wp.Flag)
                   && wp.Word.Trim().Length >= 2
                   && !StopWords.Contains(wp.Word.ToLower());
        }

        public TextRankExtractor()
        {
            Span = 5;

            Segmenter = new JiebaSegmenter();
            PosSegmenter = new PosSegmenter(Segmenter);
            SetStopWords(_settings.StopWordsFileName.GetResourcePath());
            if (StopWords.IsEmpty())
            {
                StopWords.UnionWith(DefaultStopWords);
            }
        }

        public override IEnumerable<string> ExtractTags(string text, int count = 20, IEnumerable<string> allowPos = null)
        {
            var rank = ExtractTagRank(text, allowPos);
            if (count <= 0) { count = 20; }
            return rank.OrderByDescending(p => p.Value).Select(p => p.Key).Take(count);
        }

        public override IEnumerable<WordWeightPair> ExtractTagsWithWeight(string text, int count = 20, IEnumerable<string> allowPos = null)
        {
            var rank = ExtractTagRank(text, allowPos);
            if (count <= 0) { count = 20; }
            return rank.OrderByDescending(p => p.Value).Select(p => new WordWeightPair()
            {
                Word = p.Key,
                Weight = p.Value
            }).Take(count);
        }

        #region Private Helpers

        private IDictionary<string, double> ExtractTagRank(string text, IEnumerable<string> allowPos)
        {
            if (allowPos.IsEmpty())
            {
                allowPos = DefaultPosFilter;
            }

            var g = new UndirectWeightedGraph();
            var cm = new Dictionary<string, int>();
            var words = PosSegmenter.Cut(text).ToList();

            for (var i = 0; i < words.Count(); i++)
            {
                var wp = words[i];
                if (PairFilter(wp))
                {
                    for (var j = i + 1; j < i + Span; j++)
                    {
                        if (j >= words.Count)
                        {
                            break;
                        }
                        if (!PairFilter(words[j]))
                        {
                            continue;
                        }

                        // TODO: better separator.
                        var key = wp.Word + "$" + words[j].Word;
                        if (!cm.ContainsKey(key))
                        {
                            cm[key] = 0;
                        }
                        cm[key] += 1;
                    }
                }
            }

            foreach (var p in cm)
            {
                var terms = p.Key.Split('$');
                g.AddEdge(terms[0], terms[1], p.Value);
            }

            return g.Rank();
        }

        #endregion
    }
}

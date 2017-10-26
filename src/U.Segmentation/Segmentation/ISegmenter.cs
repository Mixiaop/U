using System.Collections.Generic;

namespace U.Segmentation
{
    /// <summary>
    /// 分词器接口定义
    /// </summary>
    public interface ISegmenter : U.Dependency.ITransientDependency
    {
        /// <summary>
        /// The main function that segments an entire sentence that contains 
        /// Chinese characters into seperated words.
        /// </summary>
        /// <param name="text">The string to be segmented.</param>
        /// <param name="cutAll">Specify segmentation pattern. True for full pattern, False for accurate pattern.</param>
        /// <param name="hmm">Whether to use the Hidden Markov Model.</param>
        /// <returns></returns>
        IEnumerable<string> Cut(string text, bool cutAll = false, bool hmm = true);

        IEnumerable<string> CutForSearch(string text, bool cutAll = false, bool hmm = true);

        IEnumerable<Token> Tokenize(string text, TokenizerMode mode = TokenizerMode.Default, bool hmm = true);

        #region Extend Main Dict
        void LoadUserDict(string fileName);

        void AddWord(string word, int freq = 0, string tag = null);

        void DeleteWord(string word);
        #endregion

    }

    public enum TokenizerMode
    {
        Default,
        Search
    }
}

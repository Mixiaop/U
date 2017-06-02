
namespace U.FakeMvc.Routes
{
    /// <summary>
    /// url重置规则
    /// </summary>
    public class RewriterRule
    {
        public RewriterRule() { }
        public RewriterRule(string lookFor, string sendTo, int order = 0) {
            LookFor = lookFor;
            SendTo = sendTo;
            Order = order;
        }

        public string LookFor { get; set; }

        public string SendTo { get; set; }

        public int Order { get; set; }
    }
}

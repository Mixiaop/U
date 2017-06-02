
namespace U.FakeMvc.Routes
{
    public class Route
    {
        public Route() {
        }

        public Route(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public string RewriterRuleLookFor { get; set; }

        public string RewriterRuleSendTo { get; set; }
    }
}

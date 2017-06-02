
namespace U.FakeMvc.Models
{
    public class PagingModel : ModelBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string PagingHTML { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Runtime.Caching;
using U.Domain.Events;
using U.Application.Services.Events;
namespace U.Tests.Web
{
    public partial class JavascriptTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var store = new Store();
            var a = (U.Domain.Entities.IEntity)store;
        }
    }

    /// <summary>
    /// 缓存事件订阅者，用于实体【增删改】后触发处理事件消息（移除查询的缓存键）
    /// </summary>
    public class CacheEventConsumer :
        //Stores
        IConsumer<EntityUpdated<Store>>
    {
        private readonly ICacheManager _cacheManager;
        public CacheEventConsumer()
        {
            _cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
        }

        #region Stores
        public void HandleEvent(EntityUpdated<Store> eventMessage)
        {

        }


        #endregion
    }

    public class Store : U.Domain.Entities.Auditing.FullAuditedEntity
    {
        public string Name { get; set; }
    }

}
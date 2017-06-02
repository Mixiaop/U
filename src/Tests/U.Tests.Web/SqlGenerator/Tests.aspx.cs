using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.SqlGenerator;
using U.SqlGenerator.Attributes;
namespace U.Tests.Web.SqlGenerator
{
    public partial class Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var generator = UPrimeEngine.Instance.Resolve<ISqlGenerator<User>>();
            SqlQuery sql = generator.GetSelectAll(x => x.RoleId == 1, p => p.UserRole);

            Response.Write(sql.Sql);
        }
    }

    [Table("Users")]
    public class User
    {
        [Key, Identity]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }


        public int RoleId { get; set; }

        [LeftJoin("UserRoles", "RoleId", "Id")]
        public UserRole UserRole { get; set; }

        public DateTime CreationTime { get; set; }
    }

    [Table("UserRoles")]
    public class UserRole
    {
        [Key, Identity]
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
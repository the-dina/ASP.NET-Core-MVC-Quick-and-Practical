using System;
using System.Collections.Generic;

namespace EZCourse.Models.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            UserPermission = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}

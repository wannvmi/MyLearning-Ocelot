using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Entities
{
    public class User : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the provider name.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the provider subject identifier.
        /// </summary>
        public string ProviderSubjectId { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets if the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 是否删除 0：未删除 1:删除
        /// </summary>
        public int IsDelete { get; set; } = 0;

    }
}



using System.ComponentModel.DataAnnotations;

namespace FakeUserApi.Models
{
    /// <summary>
    ///  FakeUser
    /// </summary>
    public class FakeUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lastname
        /// </summary>
        public string Lastname { get; set; }
        ///<summary>
        ///lohin
        ///</summary>
        public string Login { get; set; }
        ///<summary>
        ///Email
        ///</summary>
        public string Email { get; set; }
        ///<summary>
        ///HashPass
        ///</summary>
        public string HashPass  { get; set; }
    }
}

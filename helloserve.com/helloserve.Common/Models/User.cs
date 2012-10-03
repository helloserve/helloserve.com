using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace helloserve.Common
{
    [Table("User")]
    public class User : BaseEntity<User>, IEntity
    {
        [Key, Required, DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int UserID { get; internal set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public bool ReceiveUpdates { get; set; }
        public bool Administrator { get; set; }
        public Guid ActivationToken { get; set; }
        public bool Activated { get; set; }

        [NotMapped]
        public string EmailMd5Hash
        {
            get
            {
                MD5 md5 = MD5.Create();
                byte[] bytes = Encoding.Default.GetBytes(EmailAddress.Trim().ToLower());
                bytes = md5.ComputeHash(bytes);

                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < bytes.Length; i++)
                {
                    sBuilder.Append(bytes[i].ToString("x2"));
                }

                return sBuilder.ToString();  // Return the hexadecimal string.
            }
        }

        public string ResetPassword()
        {
            return UserRepo.ResetPassword(this);
        }

        #region IENTITY

        public bool IsNew()
        {
            return UserID == 0;
        }

        public void Save(bool skipValidation = false)
        {
            base.Save(this, skipValidation);
        }

        #endregion

        public void Delete()
        {
            base.Delete(this);
        }
    }
}

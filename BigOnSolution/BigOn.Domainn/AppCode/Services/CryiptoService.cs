using BigOn.Domain.AppCode.Extentions;
using Microsoft.Extensions.Options;

namespace BigOn.Domain.AppCode.Services
{
    public class CryiptoService
    {
        private readonly CryiptoServiceOptions option;

        public CryiptoService(IOptions<CryiptoServiceOptions>option)
        {
            this.option = option.Value;
        }
        public string ToMd5(string value)
        {
            
            return value.ToMd5(option.SaltKey);
        }

        public  string Encrypt( string value, bool appliedUrlEncode)   //123
        {
            return value.Encrypt(option.SymmetricKey, appliedUrlEncode);
        }

        public string Decrypt(string value)   //123
        {
            return value.Decrypt(option.SymmetricKey);
        }
    }
   
    public class CryiptoServiceOptions
    {
        public string SaltKey { get; set; }
        public string SymmetricKey { get; set; }

    }
}

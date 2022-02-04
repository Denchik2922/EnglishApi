using System.Collections.Generic;

namespace EnglishApi.Dto.AuthDtos
{
    public class ExternalLoginsDto
    {
        public string ReturnUrl { get; set; }
        public IList<ProviderDto> ExternalLogins { get; set; }
    }
}

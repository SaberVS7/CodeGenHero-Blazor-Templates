namespace $safeprojectname$.Services
{
    using Microsoft.Extensions.Logging;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial interface IWebApiDataServiceBase
    {
        string IsServiceOnlineRelativeUrl { get; set; }

        ILogger Log { get; set; }

        Task<bool> IsServiceOnlineAsync();
    }
}
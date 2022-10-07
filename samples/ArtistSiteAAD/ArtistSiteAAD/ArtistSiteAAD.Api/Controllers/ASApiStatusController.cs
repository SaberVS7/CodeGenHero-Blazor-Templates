// <auto-generated> - Template:APIStatusController, Version:2021.11.12, Id:021b5127-262b-45fb-a3a7-7388b2edfca9
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArtistSiteAAD.Repository.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ArtistSiteAAD.Api.Infrastructure;

namespace ArtistSiteAAD.Api.Controllers
{
	[ApiController]
	public partial class ASApiStatusController : ASBaseApiController
	{
		public ASApiStatusController(ILogger<ASApiStatusController> logger,
			IServiceProvider serviceProvider,
			IHttpContextAccessor httpContextAccessor,
			LinkGenerator linkGenerator,
			IASRepository repository)
			: base(logger, serviceProvider, httpContextAccessor, linkGenerator, repository)
		{
		}

		[HttpGet]
		    [VersionedActionConstraint(allowedVersion: 1, order: 100)]
		    public async Task<IActionResult> Get()
		    {
			    try
			    {
				    var version = this.GetType().Assembly.GetName().Version;
				    return Ok(version);
			    }
			    catch (Exception ex)
			    {
				    Log.LogError(exception: ex, message: ex.Message);

				    var retVal = StatusCode(StatusCodes.Status500InternalServerError,
					    value: System.Diagnostics.Debugger.IsAttached ? ex : null);
				    return retVal;
			    }
		    }

	}
}
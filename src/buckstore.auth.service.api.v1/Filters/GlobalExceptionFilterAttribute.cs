using System;
using buckstore.auth.service.api.v1.Filters.ErrorsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace buckstore.auth.service.api.v1.Filters
{
	public class GlobalExceptionFilterAttribute : Attribute, IExceptionFilter
	{
		public GlobalExceptionFilterAttribute() { }

		public void OnException(ExceptionContext context)
		{
			context.Result = new BadRequestObjectResult(
				new DefaultError(false, 
					new ErrorsResponse[]
					{
						new ErrorsResponse(Environment.GetEnvironmentVariable("GlobalErrorCode"),
							Environment.GetEnvironmentVariable("GlobalErrorMessage"),
							DateTime.Now)
					}
				)
			);
		}
	}
}
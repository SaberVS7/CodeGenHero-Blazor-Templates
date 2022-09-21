// <auto-generated> - Template:AdminEditViewModel, Version:2021.11.12, Id:17ae856a-a589-40c0-a5be-1579b0714385
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ArtistSite.App.Services;
using ArtistSite.App.Shared;
using ArtistSite.Shared.Constants;
using ArtistSite.Shared.DTO;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArtistSite.App.Pages
{
	[Authorize(Roles = Consts.ROLE_ADMIN_OR_USER)]
	public partial class ASNewsItemEditViewModel : AdminPageBase
	{
		public NewsItem NewsItem { get; set; } = new NewsItem();

		[Inject]
		public IWebApiDataServiceAS WebApiDataServiceAS { get; set; }

		[Parameter]
		public int NewsItemId { get; set; }

		protected override async Task OnInitializedAsync()
		{
				await base.OnInitializedAsync();

				List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>()
				{
						new BreadcrumbItem("Home", "/"),
						new BreadcrumbItem("List of NewsItems", "/admin/newsItems"),
						new BreadcrumbItem("Edit NewsItem", $"/admin/newsitemedit/{NewsItemId}", disabled: true)
				};

				NavigationService.SetBreadcrumbs(breadcrumbs);
		}

		protected async override Task OnParametersSetAsync()
		{
				IsReady = false;
				await SetSavedAsync(false);

				try
				{
						if (NewsItemId == 0) // A new item is being created - opportunity to populate initial/default state
						{
								// Define entity defaults
								NewsItem = new NewsItem { };
						}
						else
						{
								if (NewsItem == null || NewsItem.NewsItemId != NewsItemId)
								{
										var result = await WebApiDataServiceAS.GetNewsItemAsync(NewsItemId);
										if (result.IsSuccessStatusCode)
										{
												var newsItem = result.Data;
												// Admins and other approved user claims (Add below) only
												if (!User.IsInRole(Consts.ROLE_ADMIN))
												{
														NavigationManager.NavigateTo($"/Authorization/AccessDenied");
												}
												else
												{
														NewsItem = newsItem;
												}
										}
								}
						}
				}
				finally
				{
						IsReady = true;
				}
		}

		protected async Task OnValidSubmit()
		{
				await SetSavedAsync(false);

				ClearNoneValues();

					if (NewsItemId == 0) // A new item is being created - opportunity to populate initial/default state
				{
						var result = await WebApiDataServiceAS.CreateNewsItemAsync(NewsItem);
						if (result.IsSuccessStatusCode)
						{
								NewsItem = result.Data;
								StatusClass = "alert-success";
								Message = "New item added successfully.";
								await SetSavedAsync(true);
						}
						else
						{
								StatusClass = "alert-danger";
								Message = "Something went wrong adding the new item. Please try again.";
								await SetSavedAsync(false);
						}
				}
				else
				{
						var result = await WebApiDataServiceAS.UpdateNewsItemAsync(NewsItem);
						if (result.IsSuccessStatusCode)
						{
								StatusClass = "alert-success";
								Message = "NewsItem updated successfully.";
								await SetSavedAsync(true);
						}
						else
						{
								StatusClass = "alert-danger";
								Message = "Something went wrong updating the new item. Please try again.";
								await SetSavedAsync(false);
						}
				}
		}

		protected void ReturnToList()
		{
				NavigationManager.NavigateTo("/admin/newsitems");
		}

        protected async Task SetSavedAsync(bool value)
        {
            Saved = value;
            if (value == true)
            {
                await JsRuntime.InvokeVoidAsync("blazorExtensions.ScrollToTop");
            }
        }

        private void ClearNoneValues()
        {
            // Add handling for null values here
        }
	}
}

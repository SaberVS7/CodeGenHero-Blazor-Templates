// <auto-generated> - Template:AdminListPageViewModel, Version:2021.11.12, Id:b76e62ec-fe5b-47c6-bb58-fb58ed7399e5
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ArtistSite.App.Components;
using ArtistSite.App.Services;
using ArtistSite.App.Shared;
using ArtistSite.Shared.Constants;
using ArtistSite.Shared.DataService;
using ArtistSite.Shared.DTO;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums = ArtistSite.Shared.Constants.Enums;

namespace ArtistSite.App.Pages
{
	[Authorize(Roles = Consts.ROLE_ADMIN_OR_USER)]
	public partial class ASArtworkCategoryXrefsListViewModel : AdminPageBase
	{
		public ASArtworkCategoryXrefsListViewModel()
		{
		}

		public IList<ArtworkCategoryXref> ArtworkCategoryXrefs { get; set; } = new List<ArtworkCategoryXref>();

		[Inject]
		public IWebApiDataServiceAS WebApiDataServiceAS { get; set; }

		protected bool Bordered { get; set; } = false;
		protected bool Dense { get; set; } = false;
		protected bool Hover { get; set; } = true;
		protected bool Striped { get; set; } = true;

		[Inject]
		protected ILocalHttpClientService LocalHttpClientService { get; set; }

		protected string SearchString1 { get; set; } = "";

		protected ArtworkCategoryXref SelectedArtworkCategoryXref { get; set; }

		[Inject]
		private IDialogService DialogService { get; set; }

		protected async Task ConfirmDeleteAsync(ArtworkCategoryXref item)
		{
				var parameters = new DialogParameters();
				parameters.Add("ContentText", $"Are you sure you want to delete this?");
				parameters.Add("ButtonText", "Yes");
				parameters.Add("Color", Color.Success);

				var result = await DialogService.Show<ConfirmationDialog>("Confirm", parameters).Result;
				if (!result.Cancelled)
				{
						await DeleteArtworkCategoryXrefAsync(item.ArtworkId, item.CategoryId);
				}
		}

		protected async Task DeleteArtworkCategoryXrefAsync(int artworkId, int categoryId)
		{
				var result = await WebApiDataServiceAS.DeleteArtworkCategoryXrefAsync(artworkId, categoryId);
				if (result.IsSuccessStatusCode)
				{

                    StatusClass = "alert-success";
                    Message = "Deleted successfully";
                    await SetSavedAsync(true);
				}
				else
				{

                    StatusClass = "alert-danger";
                    Message = "Something went wrong deleting the item. Please try again.";
                    await SetSavedAsync(false);
				}
		}

		protected bool FilterArtworkCategoryXref1(ArtworkCategoryXref item) => FilterFunc(item, SearchString1);

		protected bool FilterFunc(ArtworkCategoryXref item, string searchString)
		{
				if (string.IsNullOrWhiteSpace(searchString))
					return true;

				searchString = searchString.Trim();
				// Replace with the property you intend a search to work against
				var ArtworkIdString = item.ArtworkId.ToString();
				if (!string.IsNullOrWhiteSpace(ArtworkIdString) && ArtworkIdString.Contains(searchString, StringComparison.OrdinalIgnoreCase))
					return true;

				// Replace with the property you intend a search to work against
				var CategoryIdString = item.CategoryId.ToString();
				if (!string.IsNullOrWhiteSpace(CategoryIdString) && CategoryIdString.Contains(searchString, StringComparison.OrdinalIgnoreCase))
					return true;

				return false;
		}

		protected override async Task OnInitializedAsync()
		{
				await base.OnInitializedAsync();
				IsReady = false;
				await SetSavedAsync(false);

				try
				{
						List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>()
						{
								new BreadcrumbItem("Home", "/"),
								new BreadcrumbItem("List of ArtworkCategoryXrefs", "/admin/artworkCategoryXrefs", disabled: true)
						};

						NavigationService.SetBreadcrumbs(breadcrumbs);
						if (ArtworkCategoryXrefs == null || !ArtworkCategoryXrefs.Any())
						{
								if (User != null && User.Identity.IsAuthenticated)
								{
										// Add your filtering logic in here, by adding FilterCriterion to the filterCriteria list
										var filterCriteria = new List<IFilterCriterion>();
										ArtworkCategoryXrefs = await WebApiDataServiceAS.GetAllPagesArtworkCategoryXrefsAsync();
								}
						}
				}
				finally
				{
						IsReady = true;
				}
		}

		protected void ReturnToList()
		{
				NavigationManager.NavigateTo("/admin/artworkCategoryXrefs");
		}
		
        protected async Task SetSavedAsync(bool value)
        {
            Saved = value;
            if (value == true)
            {
                await JsRuntime.InvokeVoidAsync("blazorExtensions.ScrollToTop");
            }
        }

	}
}
// <auto-generated> - Template:DTO, Version:2021.11.12, Id:c97fab8d-db03-4f94-9c85-14d1f9b41aa7
using System;
using System.Collections.Generic;

namespace ArtistSiteAAD.Shared.DTO
{
	public partial class ArtworkCategoryXref
{
		public ArtworkCategoryXref()
		{
				InitializePartial();
		}

		// Primary Key
		public int ArtworkId { get; set; }

		// Primary Key
		public int CategoryId { get; set; }

		public int DisplayOrder { get; set; }

		partial void InitializePartial();
	}
}
// <auto-generated> - Template:DTO, Version:2021.11.12, Id:c97fab8d-db03-4f94-9c85-14d1f9b41aa7
using System;
using System.Collections.Generic;

namespace ArtistSite.Shared.DTO
{
	public partial class Medium
{
		public Medium()
		{
				InitializePartial();
		}

		// Primary Key
		public int MediumId { get; set; }

		public string Code { get; set; }

		public int DisplayOrder { get; set; }

		public bool IsActive { get; set; }

		public string Name { get; set; }

		partial void InitializePartial();
	}
}
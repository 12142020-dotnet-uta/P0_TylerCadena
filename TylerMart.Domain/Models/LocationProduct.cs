using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Location-Product Pair Model
	/// </summary>
	[Table("LocationProducts")]
	public class LocationProduct : Model, IValidatableObject {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int LocationProductID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		public int LocationID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Location Location { get; private set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> primary key
		/// </summary>
		public int ProductID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Product Product { get; private set; }
		/// <summary>
		/// Get Location-Product Pairs's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public override int GetID() => LocationProductID;
		/// <summary>
		/// Generates Location-Product Pair array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Location-Product Pairs
		/// </returns>
		public static LocationProduct[] GenerateSeededData() {
			Location[] locations = Location.GenerateSeededData();
			Product[] products = Product.GenerateSeededData();
			LocationProduct[] locationProducts = new LocationProduct[] {
				new LocationProduct() {
					LocationProductID = 1,
					LocationID = locations[0].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 2,
					LocationID = locations[0].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 3,
					LocationID = locations[0].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 4,
					LocationID = locations[0].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 5,
					LocationID = locations[1].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 6,
					LocationID = locations[1].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 7,
					LocationID = locations[1].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 8,
					LocationID = locations[1].LocationID,
					ProductID = products[1].ProductID
				}
			};
			return locationProducts;
		}
		/// <summary>
		/// Validates LocationProduct.LocationID and LocationProduct.ProductID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (LocationID <= 0) {
				yield return new ValidationResult(
					"LocationProduct.LocationID cannot be less than or equal to zero!",
					new[] { nameof(LocationID), nameof(LocationProduct) }
				);
			}
			if (ProductID <= 0) {
				yield return new ValidationResult(
					"LocationProduct.ProductID cannot be less than or equal to zero!",
					new[] { nameof(ProductID), nameof(LocationProduct) }
				);
			}
		}
	}
}

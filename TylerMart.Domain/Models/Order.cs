using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order Model
	/// </summary>
	[Table("Orders")]
	public class Order : Model, IValidatableObject {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int OrderID { get; set; }
		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Order has been completed
		/// </summary>
		public bool Complete { get; set; } = false;
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> primary key
		/// </summary>
		[ForeignKey("Customer")]
		public int CustomerID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Customer Customer { get; private set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		[ForeignKey("Location")]
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
		/// Navigation list of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual List<OrderProduct> OrderProducts { get; private set; }
		/// <summary>
		/// Get Order's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public override int GetID() => OrderID;
		/// <summary>
		/// Generates Order array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Orders
		/// </returns>
		public static Order[] GenerateSeededData() {
			Customer[] customers = Customer.GenerateSeededData();
			Location[] locations = Location.GenerateSeededData();
			Order[] orders = new Order[] {
				new Order() {
					OrderID = 1,
					Complete = true,
					CustomerID = customers[0].CustomerID,
					LocationID = locations[0].LocationID
				},
				new Order() {
					OrderID = 2,
					Complete = true,
					CustomerID = customers[1].CustomerID,
					LocationID = locations[1].LocationID
				}
			};
			return orders;
		}
		/// <summary>
		/// Validates Order.CustomerID and Order.LocationID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (CustomerID <= 0) {
				yield return new ValidationResult(
					"Order.CustomerID cannot be less than or equal to zero!",
					new[] { nameof(CustomerID), nameof(Order) }
				);
			}
			if (LocationID <= 0) {
				yield return new ValidationResult(
					"Order.LocationID cannot be less than or equal to zero!",
					new[] { nameof(LocationID), nameof(Order) }
				);
			}
		}
	}
}

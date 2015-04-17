using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data.Fiscalization {
	public partial class ItemEntity {
		public double PriceDelta {
			get { return this.TotalPrice - this.Price; }
		}

		protected bool Equals(ItemEntity other) {
			return string.Equals(Code, other.Code) && string.Equals(Name, other.Name) &&
				   Amount.Equals(other.Amount) && Price.Equals(other.Price) &&
				   Discount.Equals(other.Discount) &&
				   DiscountAmount.Equals(other.DiscountAmount) &&
				   TotalPrice.Equals(other.TotalPrice) &&
				   TotalPriceValue.Equals(other.TotalPriceValue) && Type == other.Type &&
				   Equals(AccountEntity, other.AccountEntity);
		}

		public override int GetHashCode() {
			unchecked {
				int hashCode = (Code != null ? Code.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Amount.GetHashCode();
				hashCode = (hashCode * 397) ^ Price.GetHashCode();
				hashCode = (hashCode * 397) ^ Discount.GetHashCode();
				hashCode = (hashCode * 397) ^ DiscountAmount.GetHashCode();
				hashCode = (hashCode * 397) ^ TotalPrice.GetHashCode();
				hashCode = (hashCode * 397) ^ TotalPriceValue.GetHashCode();
				hashCode = (hashCode * 397) ^ (int)Type;
				hashCode = (hashCode * 397) ^ (AccountEntity != null ? AccountEntity.GetHashCode() : 0);
				return hashCode;
			}
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ItemEntity)obj);
		}
	}
}

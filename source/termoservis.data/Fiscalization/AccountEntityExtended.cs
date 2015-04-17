using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data.Fiscalization {
	public partial class AccountEntity {
		public bool IsCancellationAccount {
			get { return this.CancellationAccountDate.HasValue && !this.CancellationAccountDate.Value.Year.Equals(9999); }
		}

		public double ItemsPriceSum {
			get { return this.ItemEntities.Sum(item => item.Price); }
		}

		public double ItemsPriceDeltaSum {
			get { return this.ItemEntities.Sum(item => item.PriceDelta); }
		}

		public double ItemsPriceTotalSum {
			get { return this.ItemEntities.Sum(item => item.TotalPrice); }
		}


		protected bool Equals(AccountEntity other) {
			return string.Equals(StoreName, other.StoreName) &&
				   string.Equals(TreasuryName, other.TreasuryName) && Number == other.Number &&
				   Date.Equals(other.Date);
		}

		public override int GetHashCode() {
			unchecked {
				int hashCode = (StoreName != null ? StoreName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (TreasuryName != null ? TreasuryName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Number;
				hashCode = (hashCode * 397) ^ Date.GetHashCode();
				return hashCode;
			}
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((AccountEntity)obj);
		}


		public override string ToString() {
			return String.Format("{0}/{1}/{2}",
				this.Number, this.StoreName, this.TreasuryName);
		}
	}
}

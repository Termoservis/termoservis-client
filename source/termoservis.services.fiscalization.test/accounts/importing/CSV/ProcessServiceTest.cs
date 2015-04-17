using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Termoservis.Services.Fiscalization.Accounts;
using Termoservis.Services.Fiscalization.Accounts.Importing.CSV;

namespace Termoservis.Services.Fiscalization.Test.Accounts.Importing.CSV {
	/// <summary>
	/// Summary description for ProcessServiceTest
	/// </summary>
	[TestClass]
	public class ProcessServiceTest {
		public ProcessServiceTest() {
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get {
				return this.testContextInstance;
			}
			set {
				this.testContextInstance = value;
			}
		}

		#region Additional test attributes

		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//

		#endregion

		[TestMethod]
		public async void ProcessAccountsTestMethod() {
			// Test 0
			try {
				var result0 = await ProcessService.ProcessAccounts(null, null);
				Debug.Assert(result0 != null);
				Debug.Assert(!result0.Any());
			}
			catch (Exception ex) {
				Debug.Fail(ex.Message);
			}

			// Test 1
			try {
				var result1 = await ProcessService.ProcessAccounts("", "");
				Debug.Assert(result1 != null);
				Debug.Assert(!result1.Any());
			}
			catch (Exception ex) {
				Debug.Fail(ex.Message);
			}

			// Test 2
			try {
				var result2 = await ProcessService.ProcessAccounts("\"SERVIS1\";\"1\";1;\"2013-07-08 17:30:00.000971\";;;;\"\";;\"\";;;;;;\"G-Gotovina\"", ";";
				Debug.Assert(result2 != null);
				Debug.Assert(result2.Any());
				Debug.Assert(result2.Count() == 1);
				Debug.Assert(result2.FirstOrDefault() == new Account() {
					StoreName = "SERVIS1",
					TreasuryNumber = "1",
					AccountNumber = 1,
					AccountDate = new DateTime(2013, 07, 08, 17, 30, 0),
					PaymentMethod = "G-Gotovina"
				});
			}
			catch (Exception ex) {
				Debug.Fail(ex.Message);
			}
		}
	}
}

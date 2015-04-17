using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Orflow {
	public class Updater {
		private const string RemoteUpdatePath = @"http://localhost/TermoservisServer/";
		private const string RemoteUpdateInfoFilePath = @"UpdateInfo.xml";
		private const string LocalPath = @"";

		public WindowStart Owner { get; private set; }

		private Thread updateThread;
		private List<UpdateInfo> infos; 
		

		public Updater(WindowStart owner) {
			this.Owner = owner;
		}


		public void BeginUpdate() {
			this.updateThread = new Thread(this.UpdateAsync);
			this.updateThread.Start();
		}

		private void UpdateAsync() {
			// Fetching update info state
			this.Owner.SetCurrentProgressStateAsync("Fetching update info");

			this.GetUpdateInfo();

			// Checking local files state
			this.Owner.SetCurrentProgressStateAsync("Checking local files");
			this.Owner.SetOverallProgressAsync(0.02d);

			this.GetLocalFilesInfo();
			System.Threading.Thread.Sleep(1000);

			// Download needed files state
			this.Owner.SetCurrentProgressStateAsync("Downloading");
			this.Owner.SetCurrentProgressInderteminateAsync(false);
			this.Owner.SetCurrentProgressAsync(0);
			this.Owner.IsIndicatorActive = true;
			this.Owner.SetOverallProgressAsync(0.05d);

			System.Threading.Thread.Sleep(1000);

			//Apply needed files
			System.Threading.Thread.Sleep(1000);


			// Update finished
			this.Owner.SetCurrentProgressAsync(1d);
			this.Owner.IsIndicatorActive = false;
			this.Owner.SetCurrentProgressStateAsync("All up to date");
			this.Owner.SetOverallProgressAsync(1d);
		}

		private void GetLocalFilesInfo() {
			FileVersionInfo fileInfo = this.GetApplicationInfo("Orflow.exe");
			System.Diagnostics.Debug.WriteLine(fileInfo);
		}

		private FileVersionInfo GetApplicationInfo(string relativePath) {
			string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? String.Empty;
			string absolutePath = Path.Combine(executableDirectory, relativePath);

			if (File.Exists(absolutePath)) 
				return FileVersionInfo.GetVersionInfo(absolutePath);
			else {
				System.Diagnostics.Debug.WriteLine("File not found: " + absolutePath);
				return null;
			}
		}

		private void GetUpdateInfo() {
			// Initialize variables
			this.infos = new List<UpdateInfo>();

			// Creating Uri path
			Uri updateInfoPath = new Uri(RemoteUpdatePath + RemoteUpdateInfoFilePath, UriKind.Absolute);

			// Load document from Uri and assign root
			XElement root = XElement.Load(updateInfoPath.ToString());

			// Populate UpdateInfo object from recieved data
			IEnumerable<XElement> applications = root.Elements("Application");
			foreach (XElement application in applications) {
				UpdateInfo info = new UpdateInfo();
				
				// Application name
				XAttribute applicationNameElement = application.Attribute("Name");
				if (applicationNameElement != null) 
					info.ApplicationName = applicationNameElement.Value;
				else System.Diagnostics.Debug.WriteLine("ApplicationName not found for application!");

				// Latest version
				XElement versionElement = application.Element("Version");
				if (versionElement != null) {
					// Retrieve versions (major, minor, build, revision) elements
					List<XElement> version = versionElement.Elements().ToList();
					if (version.Count() == 4) {
						// Version integers
						int major = 0, minor = 0, build = 0, revision = 0;

						// Version elements
						XElement versionMajorElement = version.ElementAtOrDefault(0);
						XElement versionMinorElement = version.ElementAtOrDefault(1);
						XElement versionBuildElement = version.ElementAtOrDefault(2);
						XElement versionRevisionElement = version.ElementAtOrDefault(3);
						
						// Parsing
						// Check if element is not null and try to parse
						bool parsedVersionMajor = versionMajorElement != null && Int32.TryParse(versionMajorElement.Value, out major);
						bool parsedVersionMinor = versionMinorElement != null && Int32.TryParse(versionMinorElement.Value, out minor);
						bool parsedVersionBuild = versionBuildElement != null && Int32.TryParse(versionBuildElement.Value, out build);
						bool parsedVersionRevision = versionRevisionElement != null && Int32.TryParse(versionRevisionElement.Value, out revision);

						// If all parsed correctly append parsed values to info object
						if (parsedVersionMajor && parsedVersionMinor && parsedVersionBuild && parsedVersionRevision)
							info.Version = new Version(major, minor, build, revision);
						else System.Diagnostics.Debug.WriteLine("Can't parse Version!");
					}
					else System.Diagnostics.Debug.WriteLine("Version doesn't contain enough information!");
				}
				else System.Diagnostics.Debug.WriteLine("Version not found for application!");

				// Release date
				XElement releaseDateElement = application.Element("Date");
				if (releaseDateElement != null) {
					bool parsed = DateTime.TryParse(releaseDateElement.Value, out info.ReleaseDate);
					if (!parsed) 
						System.Diagnostics.Debug.WriteLine("Can't parse ReleaseDate: " + releaseDateElement.Value);
				}
				else System.Diagnostics.Debug.WriteLine("ReleaseDate not found for application!");

				// Release notes
				XElement changesNotesElement = application.Element("Changes");
				if (changesNotesElement != null)
					info.ReleaseNotes = changesNotesElement.Value;
				else System.Diagnostics.Debug.WriteLine("ChengesNotes not found for application!");

				// Update files path
				XElement updateFilesPathElement = application.Element("Path");
				if (updateFilesPathElement != null) 
					info.UpdateFilesPath = updateFilesPathElement.Value;
				else System.Diagnostics.Debug.WriteLine("UpdateFilesPath not found for application!");


				// Add populated UpdateInfo object to list of infos
				this.infos.Add(info);
				System.Diagnostics.Debug.WriteLine(info.ToString() + "\n");
			}
		}

		public struct UpdateInfo {
			public string ApplicationName;
			public Version Version;
			public DateTime ReleaseDate;
			public string ReleaseNotes;
			public string UpdateFilesPath;

			public override string ToString() {
				return String.Format("{0} ({1})\nPath: {2}\n{3}\n\t{4}", this.ApplicationName, this.Version, this.UpdateFilesPath, this.ReleaseNotes, this.ReleaseDate);
			}
		}
	}
}

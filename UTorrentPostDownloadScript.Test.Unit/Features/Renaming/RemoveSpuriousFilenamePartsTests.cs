using System.Collections.Specialized;
using System.Configuration.Abstractions;
using System.IO.Abstractions;
using Moq;
using NUnit.Framework;
using UTorrentPostDownloadScript.Features.Renaming;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Test.Unit.Features.Renaming
{
    [TestFixture]
    public class RemoveSpuriousFilenamePartsTests
    {
        private RemoveSpuriousFilenameParts _rsfp;
        private AppSettingsExtended _appSettings;
        private Mock<IFileSystem> _mockFileSystem;
        private Mock<DirectoryBase> _mockDirectory;
        private Mock<FileBase> _mockFile;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = new Mock<IFileSystem>();
            _mockDirectory = new Mock<DirectoryBase>();
            _mockFile = new Mock<FileBase>();
            _mockFileSystem.Setup(x => x.Directory).Returns(_mockDirectory.Object);
            _mockFileSystem.Setup(x => x.File).Returns(_mockFile.Object);
            SetupAppSettings();
        }

        private void SetupAppSettings(NameValueCollection nvc = null)
        {
            nvc = nvc ?? new NameValueCollection();
            _appSettings = new AppSettingsExtended(nvc);
            _rsfp = new RemoveSpuriousFilenameParts(_appSettings, _mockFileSystem.Object);
        }

        [Test]
        public void Handle_NoConfiguration_NothingHappens()
        {
            var @params = new UtorrentCommandLineParameters {DirectoryWhereFilesAreSaved = "c:\\something\\torrent"};

            _rsfp.Handle(@params);
        }

        [Test]
        public void Handle_BadPartInConfiguration_BadPartRemovedWithARenameInDirectories()
        {
            const string originalPath = "c:\\something\\[Some Prefix]torrent";
            var @params = new UtorrentCommandLineParameters { DirectoryWhereFilesAreSaved = originalPath };
            var settings = new NameValueCollection {{"RemoveSpuriousFilenameParts::SomePrefix", "[Some Prefix]"}};
            SetupAppSettings(settings);

            _rsfp.Handle(@params);

            _mockDirectory.Verify(x => x.Move(originalPath, "c:\\something\\torrent"));
        }

        [Test]
        public void Handle_BadPartInConfiguration_DirectoryReferenceUpdated()
        {
            var @params = new UtorrentCommandLineParameters { DirectoryWhereFilesAreSaved = "c:\\something\\[Some Prefix]torrent" };
            var settings = new NameValueCollection {{"RemoveSpuriousFilenameParts::SomePrefix", "[Some Prefix]"}};
            SetupAppSettings(settings);

            _rsfp.Handle(@params);

            Assert.That(@params.DirectoryWhereFilesAreSaved, Is.EqualTo("c:\\something\\torrent"));
        }

        [Test]
        public void Handle_BadPartInConfiguration_BadPartRemovedWithARenameInFiles()
        {
            const string originalPath = "c:\\something\\[Some Prefix]torrent.jpg";
            var @params = new UtorrentCommandLineParameters { NameOfDownloadedFileForSingleFileTorrents = originalPath };
            var settings = new NameValueCollection {{"RemoveSpuriousFilenameParts::SomePrefix", "[Some Prefix]"}};
            SetupAppSettings(settings);

            _rsfp.Handle(@params);

            _mockFile.Verify(x => x.Move(originalPath, "c:\\something\\torrent.jpg"));
        }

        [Test]
        public void Handle_BadPartInConfiguration_FileReferenceUpdated()
        {
            var @params = new UtorrentCommandLineParameters { NameOfDownloadedFileForSingleFileTorrents = "c:\\something\\[Some Prefix]torrent.jpg" };
            var settings = new NameValueCollection {{"RemoveSpuriousFilenameParts::SomePrefix", "[Some Prefix]"}};
            SetupAppSettings(settings);

            _rsfp.Handle(@params);

            Assert.That(@params.NameOfDownloadedFileForSingleFileTorrents, Is.EqualTo("c:\\something\\torrent.jpg"));
        }
    }
}

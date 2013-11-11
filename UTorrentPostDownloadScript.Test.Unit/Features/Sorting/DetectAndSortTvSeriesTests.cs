using System.Collections.Specialized;
using System.Configuration.Abstractions;
using System.IO.Abstractions;
using Moq;
using NUnit.Framework;
using UTorrentPostDownloadScript.Features.Sorting;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Test.Unit.Features.Sorting
{
    [TestFixture]
    public class DetectAndSortTvSeriesTests
    {
        private DetectAndSortTvSeries _dasts;
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
            _dasts = new DetectAndSortTvSeries();
        }

        [TestCase("SomeShow.S03E01")]
        [TestCase("SomeShow.S3E1")]
        [TestCase("SomeShow.S3E01")]
        public void Handle_WhenDirectoryIsPatternedLikeATvShow_(string tvShowFormat)
        {
            var originalDir = "c:\\something\\" + tvShowFormat;
            var @params = new UtorrentCommandLineParameters
            {
                DirectoryWhereFilesAreSaved = originalDir
            };

            _dasts.Handle(@params);

            _mockDirectory.Verify(x=>x.Move(originalDir, "c:\\something\\#TV\\SomeShow\\Season 3\\" + tvShowFormat));
        }
    }
}

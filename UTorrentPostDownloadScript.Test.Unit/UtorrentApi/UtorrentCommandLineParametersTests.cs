using NUnit.Framework;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Test.Unit.UtorrentApi
{
    [TestFixture]
    public class UtorrentCommandLineParametersTests
    {
        private UtorrentCommandLineParameters _cliInterpreter;

        [SetUp]
        public void SetUp()
        {
            _cliInterpreter = new UtorrentCommandLineParameters();
        }

        [Test]
        public void ParseArgs_NoArgs_ReturnsEmptyDto()
        {
            var args = _cliInterpreter.Parse(new string[] {});

            Assert.That(args, Is.Not.Null);
        }

        [Test]
        public void ParseArgs_CompleteArgsProvided_ReturnsDto()
        {
            const string cliArgs = "-f %F -d %D -n %N -p %P -l %L -t %T -m %M -i %I -s %S -k single";
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = _cliInterpreter.Parse(argsArray);

            Assert.That(args.NameOfDownloadedFileForSingleFileTorrents, Is.EqualTo("%F"));
            Assert.That(args.DirectoryWhereFilesAreSaved, Is.EqualTo("%D"));
            Assert.That(args.HexEndocdedInfoHash, Is.EqualTo("%I"));
            Assert.That(args.Label, Is.EqualTo("%L"));
            Assert.That(args.StatusMessage, Is.EqualTo("%M"));
            Assert.That(args.TitleOfTorrent, Is.EqualTo("%N"));
        }

        [Test]
        public void ParseArgs_QuotedArgsProvided_ReturnsDtoWithoutQuotes()
        {
            const string cliArgs = "-f \"%F\" -d \"%D\" -n \"%N\" -p %P -l \"%L\" -t \"%T\" -m \"%M\" -i \"%I\" -s %S -k single";
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = _cliInterpreter.Parse(argsArray);

            Assert.That(args.NameOfDownloadedFileForSingleFileTorrents, Is.EqualTo("%F"));
            Assert.That(args.DirectoryWhereFilesAreSaved, Is.EqualTo("%D"));
            Assert.That(args.HexEndocdedInfoHash, Is.EqualTo("%I"));
            Assert.That(args.Label, Is.EqualTo("%L"));
            Assert.That(args.StatusMessage, Is.EqualTo("%M"));
            Assert.That(args.TitleOfTorrent, Is.EqualTo("%N"));
        }

        [Test]
        public void ParseArgs_SingleQuotedArgsProvided_ReturnsDtoWithoutQuotes()
        {
            const string cliArgs = "-f '%F' -d '%D' -n '%N' -p %P -l '%L' -t '%T' -m '%M' -i '%I' -s %S -k single";
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = _cliInterpreter.Parse(argsArray);

            Assert.That(args.NameOfDownloadedFileForSingleFileTorrents, Is.EqualTo("%F"));
            Assert.That(args.DirectoryWhereFilesAreSaved, Is.EqualTo("%D"));
            Assert.That(args.HexEndocdedInfoHash, Is.EqualTo("%I"));
            Assert.That(args.Label, Is.EqualTo("%L"));
            Assert.That(args.StatusMessage, Is.EqualTo("%M"));
            Assert.That(args.TitleOfTorrent, Is.EqualTo("%N"));
        }

        [TestCase(KindOfTorrent.Single, "single")]
        [TestCase(KindOfTorrent.Multi, "multi")]
        public void ParseArgs_CanMapKindOfTorrent_ReturnsDto(KindOfTorrent kind, string cliParam)
        {
            var cliArgs = "-k " + cliParam;
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = _cliInterpreter.Parse(argsArray);

            Assert.That(args.KindOfTorrent, Is.EqualTo(kind));
        }

        [TestCase(StateOfTorrent.Checked)]
        [TestCase(StateOfTorrent.Downloading)]
        [TestCase(StateOfTorrent.DownloadingF)]
        [TestCase(StateOfTorrent.Error)]
        [TestCase(StateOfTorrent.Finished)]
        [TestCase(StateOfTorrent.Paused)]
        [TestCase(StateOfTorrent.Queued)]
        [TestCase(StateOfTorrent.QueuedSeed)]
        [TestCase(StateOfTorrent.Seeding)]
        [TestCase(StateOfTorrent.SeedingF)]
        [TestCase(StateOfTorrent.Stopped)]
        [TestCase(StateOfTorrent.SuperSeedF)]
        [TestCase(StateOfTorrent.SuperSeeding)]
        public void ParseArgs_CanMapStateOfTorrent_ReturnsDto(StateOfTorrent state)
        {
            var cliArgs = "-s " + (int)state + " -p " + (int)state;
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = _cliInterpreter.Parse(argsArray);

            Assert.That(args.StateOfTorrent, Is.EqualTo(state));
            Assert.That(args.PreviousStateOfTorrent, Is.EqualTo(state));
        }

        [Test]
        public void GetHelp_ReturnsPopulatedString()
        {
            var help = _cliInterpreter.GetHelp();

            Assert.That(help.Length, Is.GreaterThan(0));
        }
    }
}
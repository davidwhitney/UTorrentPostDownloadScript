using NUnit.Framework;

namespace UTorrentPostDownloadScript.Test.Unit
{
    /*

Add this script to Preferences/Advances/Run Program:Run this program when a torrent finishes

scriptcs PostDownload.csx -- -f %F -d %D -n %N -p %P -l %L -t %T -m %m -i %I -s %S -k %K

UTorrent docs...

You can use the following parameters:

%F - Name of downloaded file (for single file torrents)
%D - Directory where files are saved
%N - Title of torrent
%P - Previous state of torrent
%L - Label
%T - Tracker
%M - Status message string (same as status column)
%I - hex encoded info-hash
%S - State of torrent
%K - kind of torrent (single|multi)

Where State is one of:

Error - 1
Checked - 2
Paused - 3
Super seeding - 4
Seeding - 5
Downloading - 6
Super seed [F] - 7
Seeding [F] - 8
Downloading [F] - 9
Queued seed - 10
Finished - 11
Queued - 12
Stopped - 13*/


    [TestFixture]
    public class CommandLineArgsInterpreterTests
    {
        [Test]
        public void ParseArgs_NoArgs_ReturnsEmptyDto()
        {
            var args = CommandLineArgsInterpreter.Parse(new string[] {});

            Assert.That(args, Is.Not.Null);
        }

        [Test]
        public void ParseArgs_CompleteArgsProvided_ReturnsDto()
        {
            const string cliArgs = "-f %F -d %D -n %N -p %P -l %L -t %T -m %M -i %I -s %S -k single";
            var argsArray = cliArgs.Split(new[] { ' ' });

            var args = CommandLineArgsInterpreter.Parse(argsArray);

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

            var args = CommandLineArgsInterpreter.Parse(argsArray);

            Assert.That(args.KindOfTorrent, Is.EqualTo(kind));
        }
    }

}

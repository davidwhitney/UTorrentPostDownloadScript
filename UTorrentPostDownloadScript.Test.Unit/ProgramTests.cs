using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UTorrentPostDownloadScript.Features.ArgumentParsing;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Test.Unit
{
    [TestFixture]
    public class ProgramTests
    {
        private string[] _args;
        private Mock<IParsableArguments<UtorrentCommandLineParameters>> _parameters;
        private IList<IActOnCompletedTorrents> _handlers;
        private UtorrentCommandLineParameters _parsedParameters;

        [SetUp]
        public void SetUp()
        {
            _args = new[] {"-a", "%A"};
            _parameters = new Mock<IParsableArguments<UtorrentCommandLineParameters>>();
            _handlers = new List<IActOnCompletedTorrents>();

            _parsedParameters = new UtorrentCommandLineParameters();
            _parameters.Setup(x => x.Parse(_args)).Returns(_parsedParameters);
        }

        [Test]
        public void WhenProgramExecutes_ArgsIsEmpty_DisplaysHelp()
        {
            Program.Main(new string[0], _parameters.Object, null);

            _parameters.Verify(x=>x.GetHelp());
        }

        [Test]
        public void WhenProgramExecutes_ArgsIsEmpty_ParseNotCalled()
        {
            Program.Main(new string[0], _parameters.Object, null);

            _parameters.Verify(x=>x.Parse(It.IsAny<string[]>()), Times.Never);
        }

        [Test]
        public void WhenProgramExecutes_AllHandlersAreCalled()
        {
            var handler = new FakeHandler();
            _handlers.Add(handler);

            Program.Main(_args, _parameters.Object, _handlers);

            Assert.That(handler.Called, Is.True);
        }

        public class FakeHandler : IActOnCompletedTorrents
        {
            public bool Called { get; set; }

            public void Handle(UtorrentCommandLineParameters parameters)
            {
                Called = true;
            }
        }
    }
}
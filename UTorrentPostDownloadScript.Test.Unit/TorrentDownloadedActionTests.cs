using System;
using System.Collections.Generic;
using log4net;
using Moq;
using NUnit.Framework;
using UTorrentPostDownloadScript.Features.ArgumentParsing;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Test.Unit
{
    [TestFixture]
    public class TorrentDownloadedActionTests
    {
        private string[] _args;
        private Mock<IParsableArguments<UtorrentCommandLineParameters>> _parameters;
        private IList<IActOnCompletedTorrents> _handlers;
        private UtorrentCommandLineParameters _parsedParameters;
        private Mock<ILog> _logger;

        private TorrentDownloadedAction _action;

        [SetUp]
        public void SetUp()
        {
            _args = new[] {"-a", "%A"};
            _parameters = new Mock<IParsableArguments<UtorrentCommandLineParameters>>();
            _handlers = new List<IActOnCompletedTorrents>();
            _logger = new Mock<ILog>();

            _parsedParameters = new UtorrentCommandLineParameters();
            _parameters.Setup(x => x.Parse(_args)).Returns(_parsedParameters);

            _action = new TorrentDownloadedAction(_parameters.Object, _handlers, _logger.Object);
        }

        [Test]
        public void Execute_ArgsIsEmpty_DisplaysHelp()
        {
            _action.Execute(new string[0]);

            _parameters.Verify(x=>x.GetHelp());
        }

        [Test]
        public void Execute_ArgsIsEmpty_ParseNotCalled()
        {
            _action.Execute(new string[0]);

            _parameters.Verify(x=>x.Parse(It.IsAny<string[]>()), Times.Never);
        }

        [Test]
        public void Execute_AllHandlersAreCalled()
        {
            var handler = new FakeHandler();
            _handlers.Add(handler);

            _action.Execute(_args);

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
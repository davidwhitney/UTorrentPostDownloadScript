﻿using System.Collections.Generic;
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
            _args = new string[] {};
            _parameters = new Mock<IParsableArguments<UtorrentCommandLineParameters>>();
            _handlers = new List<IActOnCompletedTorrents>();

            _parsedParameters = new UtorrentCommandLineParameters();
            _parameters.Setup(x => x.Parse(_args)).Returns(_parsedParameters);
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

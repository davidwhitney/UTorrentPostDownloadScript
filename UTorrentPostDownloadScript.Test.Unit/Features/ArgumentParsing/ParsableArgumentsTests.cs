using NUnit.Framework;
using UTorrentPostDownloadScript.Features.ArgumentParsing;

namespace UTorrentPostDownloadScript.Test.Unit.Features.ArgumentParsing
{
    [TestFixture]
    public class ParsableArgumentsTests
    {
        [Test]
        public void WhenParserMappingAdded_AndValueOnQueryString_ValueMappedAccordingToParser()
        {
            var parser = new ParsableArguments<SomeClass> {{"a", (@class, s) => @class.SomeProperty = s}};

            var parsed = parser.Parse(new[] {"-a", "value"});

            Assert.That(parsed.SomeProperty, Is.EqualTo("value"));
        }
        
        [Test]
        public void ParseArgs_SingleQuotedNullArgProvided_ReturnsDtoWithEmptyString()
        {
            var parser = new ParsableArguments<SomeClass>
            {
                { "a", (@class, s) => @class.SomeProperty = s }
            };

            var parsed = parser.Parse(new[] { "-a", "''" });

            Assert.That(parsed.SomeProperty, Is.EqualTo(string.Empty));
        }

        public class SomeClass
        {   
            public string SomeProperty { get; set; }
        }
    }
}

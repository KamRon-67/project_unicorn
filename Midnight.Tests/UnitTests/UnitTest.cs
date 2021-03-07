using System;
using Midnight.Core.Extensions;
using Xunit;
using FluentAssertions;
using System.CommandLine;
using Moq;
using System.IO.Abstractions;

namespace Midnight.Tests.UnitTests
{
    public class UnitTest
    {
        [Fact]
        public void Parse_multiple_fields_in_header()
        {
            const string header = @"---
                        layout: post
                        title: This is a test jekyll document
                        description: Test YamlHeader method
                        date: 2021-01-30
                        tags :
                        - test
                        - alsotest
                        - lasttest
                        ---
            
                        ##Test

                        This is a test of YAML parsing";

            var result = header.YamlHeader();

            Assert.Equal("post", result["layout"].ToString());
            Assert.Equal("This is a test jekyll document", result["title"].ToString());
            Assert.Equal("2021-01-30", result["date"].ToString());
            Assert.Equal("[ test, alsotest, lasttest ]", result["tags"].ToString());
        }

        [Fact]
        public void When_multiple_options_are_configured_then_they_must_differ_by_name()
        {
            var command = new Command("the-command")
            {
                new Option("--same")
            };

            command
                .Invoking(c => c.Add(new Option("--same")))
                .Should()
                .Throw<ArgumentException>()
                .And
                .Message
                .Should()
                .Be("Alias '--same' is already in use.");
        }

        [Fact]
        public void Process()
        {
            var unicorn_engine_test_mock = new Mock<UnicornEngine>();

            unicorn_engine_test_mock.Setup(mr => mr.Process(It.IsAny<IFileSystem>(), It.IsAny<string>()))
                   .Verifiable();

        }
    }
}

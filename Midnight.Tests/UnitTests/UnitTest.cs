using System;
using Midnight.Core.Extensions;
using Xunit;

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
    }
}

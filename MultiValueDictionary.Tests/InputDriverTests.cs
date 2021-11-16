using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System;
using MultiValueDictionary.Runner;

namespace MultiValueDictionary.Tests
{
    public class InputDriverTests
    {
        [Fact]
        public void InputDriverSendsAppTerminationSignalOnQuit()
        {
            InputDriver id = new InputDriver();
            bool isAppRunning = id.HandleInput("Q");

            Assert.False(isAppRunning);
        }

        [Theory]
        [InlineData("KEYS")]
        [InlineData("MEMBERS foo bar")]
        [InlineData("ADD foo bar")]
        [InlineData("REMOVE foo bar")]
        [InlineData("REMOVEALL foo")]
        [InlineData("CLEAR")]
        [InlineData("KEYEXISTS foo")]
        [InlineData("MEMBEREXISTS foo bar")]
        [InlineData("ALLMEMBERS")]
        [InlineData("ITEMS")]
        public void InputDriverSendsRunAppSignalOnValidCmds(string cmd)
        {
            InputDriver id = new InputDriver();
            bool isAppRunning = id.HandleInput(cmd);

            Assert.True(isAppRunning);
        }
    }
}
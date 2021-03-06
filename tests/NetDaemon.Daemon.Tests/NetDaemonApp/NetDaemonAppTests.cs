using JoySoftware.HomeAssistant.NetDaemon.Common;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace NetDaemon.Daemon.Tests.NetDaemonApp
{
    public class NetDaemonApptests
    {
        private Mock<INetDaemon> _netDaemonMock;
        private JoySoftware.HomeAssistant.NetDaemon.Common.NetDaemonApp _app;

        public NetDaemonApptests()
        {
            _netDaemonMock = new Mock<INetDaemon>();
            _app = new JoySoftware.HomeAssistant.NetDaemon.Common.NetDaemonApp();
            _app.StartUpAsync(_netDaemonMock.Object);
        }

        [Fact]
        public void EnityShouldCallCorrectDaemonEntity()
        {
            // ARRANGE and  ACT
            _app.Entity("light.somelight");
            // ASSERT
            _netDaemonMock.Verify(n => n.Entity("light.somelight"));
        }

        [Fact]
        public void EnitiesShouldCallCorrectDaemonEntity()
        {
            // ARRANGE and  ACT
            _app.Entities(new string[] { "light.somelight" });
            // ASSERT
            _netDaemonMock.Verify(n => n.Entities(new string[] { "light.somelight" }));
        }

        [Fact]
        public void EnitiesFuncShouldCallCorrectDaemonEntity()
        {
            // ARRANGE and  ACT
            _app.Entities(n => n.EntityId == "light.somelight");
            // ASSERT
            _netDaemonMock.Verify(n => n.Entities(It.IsAny<Func<IEntityProperties, bool>>()));
        }

        [Fact]
        public void EventShouldCallCorrectDaemonEvent()
        {
            // ARRANGE
            _netDaemonMock.Setup(n => n.Event(It.IsAny<string[]>())).Returns(new Mock<IFluentEvent>().Object);

            // ACT
            _app.Event("event");
            // ASSERT
            _netDaemonMock.Verify(n => n.Event("event"));
        }

        [Fact]
        public void EventsShouldCallCorrectDaemonEvent()
        {
            // ARRANGE
            _netDaemonMock.Setup(n => n.Events(It.IsAny<IEnumerable<string>>())).Returns(new Mock<IFluentEvent>().Object);
            //ACT
            _app.Events(new string[] { "event" });
            // ASSERT
            _netDaemonMock.Verify(n => n.Events(new string[] { "event" }));
        }

        [Fact]
        public void EventesFuncShouldCallCorrectDaemonEvent()
        {
            // ARRANGE
            _netDaemonMock.Setup(n => n.Events(It.IsAny<Func<FluentEventProperty, bool>>())).Returns(new Mock<IFluentEvent>().Object);
            // ACT
            _app.Events(n => n.EventId == "event");
            // ASSERT
            _netDaemonMock.Verify(n => n.Events(It.IsAny<Func<FluentEventProperty, bool>>()));
        }

        [Fact]
        public void CallServiceShouldCallCorrectDaemonCallService()
        {
            // ARRANGE and  ACT
            var expandoData = new FluentExpandoObject();
            dynamic data = expandoData;
            data.AnyData = "data";

            // ACT
            _app.CallService("domain", "service", data, false);

            // ASSERT
            _netDaemonMock.Verify(n => n.CallService("domain", "service", expandoData, false));
        }

        [Fact]
        public void GetStateShouldCallCorrectDaemonGetState()
        {
            // ARRANGE and  ACT
            _app.GetState("entityid");

            // ASSERT
            _netDaemonMock.Verify(n => n.GetState("entityid"));
        }
    }
}
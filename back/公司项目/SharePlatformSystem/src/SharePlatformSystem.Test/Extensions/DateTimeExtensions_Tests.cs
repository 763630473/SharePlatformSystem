﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePlatformSystem.Core.Exceptions;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Tests.Extensions
{
    public class DateTimeExtensions_Tests
    {
        [Test]
        public void ToUnixTimestamp_Test()
        {
            var timestamp = new DateTime(1980, 11, 20).ToUnixTimestamp();
            timestamp.ShouldBe(343526400);
        }

        [Test]
        public void FromUnixTimestamp_Test()
        {
            var date = 343526400d.FromUnixTimestamp();
            date.ShouldBe(new DateTime(1980, 11, 20));
        }

        [Test]
        public void ToDayEnd_Test()
        {
            var now = Clock.Now;

            var dateEnd = now.ToDayEnd();

            dateEnd.ShouldBe(now.Date.AddDays(1).AddMilliseconds(-1));
        }

        [Test]
        public void StartOfWeek_Test()
        {
            var startOfWeekSunday = new DateTime(1980, 11, 20).StartOfWeek(DayOfWeek.Sunday);

            startOfWeekSunday.ShouldBe(new DateTime(1980, 11, 16));

            var startOfWeekMonday = new DateTime(1980, 11, 20).StartOfWeek(DayOfWeek.Monday);

            startOfWeekMonday.ShouldBe(new DateTime(1980, 11, 17));
        }

        [Test]
        public void DaysOfMonth_Test()
        {
            var days = DateTimeExtensions.DaysOfMonth(2018, 1);

            days.ShouldNotBeNull();

            days.Count().ShouldBe(31);

        }

        [Test]
        public void WeekDayInstanceOfMonth_Test()
        {
            var instance = new DateTime(2011, 11, 29).WeekDayInstanceOfMonth();

            instance.ShouldBe(5);
        }

        [Test]
        public void TotalDaysInMonth_Test()
        {
            var totalDays = new DateTime(2018, 1, 15).TotalDaysInMonth();
            totalDays.ShouldBe(31);
        }

        [Test]
        public void ToDateTimeUnspecified_Test()
        {
            var localTime = Clock.Now;

            var unspecified = localTime.ToDateTimeUnspecified();

            unspecified.Kind.ShouldBe(DateTimeKind.Unspecified);

        }

        [Test]
        public void TrimMilliseconds_Test()
        {
            var now = Clock.Now;

            var trimmed = now.TrimMilliseconds();

            trimmed.Millisecond.ShouldBe(0);
        }
    }
}

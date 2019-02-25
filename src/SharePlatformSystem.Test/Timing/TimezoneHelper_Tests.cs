using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core.Timing.Timezone;
using SharePlatformSystem.Collections.Extensions;

namespace SharePlatformSystem.Tests.Timing
{
    public class TimezoneHelper_Tests
    {
        [Theory]    
        public void Windows_Timezone_Id_To_Iana_Tests(string windowsTimezoneId, string ianaTimezoneId)
        {
            TimezoneHelper.WindowsToIana(windowsTimezoneId).ShouldBe(ianaTimezoneId);
        }

        [Theory]     
        public void Iana_Timezone_Id_To_Windows_Tests(string ianaTimezoneId, string windowsTimezoneId)
        {
            TimezoneHelper.IanaToWindows(ianaTimezoneId).ShouldBe(windowsTimezoneId);
        }

        [Test]
        public void All_Windows_Timezones_Should_Be_Convertable_To_Iana()
        {
            var allTimezones = TimezoneHelper.GetWindowsTimeZoneIds();

            Should.NotThrow(() =>
            {
                var exceptions = new List<string>();

                foreach (var timezone in allTimezones)
                {
                    try
                    {
                        TimezoneHelper.WindowsToIana(timezone);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex.Message);
                    }
                }

                if (exceptions.Any())
                {
                    throw new Exception(exceptions.JoinAsString(Environment.NewLine));
                }
            });
        }

        [Test]
        public void Should_Throw_Exception_For_Unknown_Windows_Timezone_Id()
        {
            Should.Throw<Exception>(() =>
            {
                TimezoneHelper.WindowsToIana("abc");
            });
        }

        [Test]
        public void Should_Throw_Exception_For_Unknown_Iana_Timezone_Id()
        {
            Should.Throw<Exception>(() =>
            {
                TimezoneHelper.IanaToWindows("cba");
            });
        }

        [Test]
        public void Convert_By_Iana_Timezone_Should_Be_Convert_By_Windows_Timezone()
        {
            var now = DateTime.UtcNow;
            TimezoneHelper.ConvertTimeFromUtcByIanaTimeZoneId(now, "Asia/Shanghai")
                .ShouldBe(TimezoneHelper.ConvertFromUtc(now, "China Standard Time"));
        }

        [Test]
        public void ConvertToDateTimeOffset_Date_With_America_NewYork_TimeZone_Should_Return_Correct_DateTimeOffset()
        {
            var testDate = new DateTime(1980,11,20);
            var timeSpan = new TimeSpan(-5,0,0);

            var dateTimeOffset = TimezoneHelper.ConvertToDateTimeOffset(testDate, "America/New_York");

            dateTimeOffset.ShouldNotBeNull();
            dateTimeOffset.Offset.ShouldBe(timeSpan);
            dateTimeOffset.DateTime.ShouldBe(testDate);
        }

        [Test]
        public void ConvertToDateTimeOffset_Date_With_America_NewYork_TimeZone_Should_Return_Correct_DateTimeOffset_With_DaylightSavings()
        {
            var testDate = new DateTime(1980, 5, 20);
            var timeSpan = new TimeSpan(-4, 0, 0);

            var dateTimeOffset = TimezoneHelper.ConvertToDateTimeOffset(testDate, "America/New_York");

            dateTimeOffset.ShouldNotBeNull();
            dateTimeOffset.Offset.ShouldBe(timeSpan);
            dateTimeOffset.DateTime.ShouldBe(testDate);
        }

        [Test]
        public void ConvertToDateTimeOffset_Dates_With_America_Phoenix_TimeZone_Should_Return_Correct_DateTimeOffsests_With_No_DaylightSavings()
        {
            var testDate = new DateTime(1980, 5, 20);
            var timeSpan = new TimeSpan(-7, 0, 0);

            var dateTimeOffset = TimezoneHelper.ConvertToDateTimeOffset(testDate, "America/Phoenix");

            dateTimeOffset.ShouldNotBeNull();
            dateTimeOffset.Offset.ShouldBe(timeSpan);
            dateTimeOffset.DateTime.ShouldBe(testDate);

            var testDate2 = new DateTime(1980, 11, 20);

            var dateTimeOffset2 = TimezoneHelper.ConvertToDateTimeOffset(testDate2, "America/Phoenix");

            dateTimeOffset2.ShouldNotBeNull();
            dateTimeOffset2.Offset.ShouldBe(timeSpan); // should be the same timespan as previous date
            dateTimeOffset2.DateTime.ShouldBe(testDate2);
        }
    }
}

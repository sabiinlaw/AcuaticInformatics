using RiverFlow.Service;
using RiverFlow.Helper;
using System.Collections.Generic;
using Xunit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RiverFlowTests.ServiceTests
{
    public class StreamServiceTest
    {
        private readonly IStreamService _streamService;

        public StreamServiceTest()
        {
            var services = new ServiceCollection();

            services.AddScoped<IStreamService, StreamService>();
            services.AddLogging(logging =>
            {
                logging.AddFilter(nameof(StreamService), LogLevel.Information);
            });

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IStreamService>();
            _streamService = service;
        }

        [Fact]
        public void TestCalculateVolume()
        {
            var stream = new Stream
            {
                Width = 7,
                Sections = new List<StreamSection>()
                {
                    new StreamSection() { Depth = 2, Velocity = 2 },
                    new StreamSection() { Depth = 8, Velocity = 3 },
                    new StreamSection() { Depth = 6, Velocity = 4 },
                    new StreamSection() { Depth = 4, Velocity = 5 },
                    new StreamSection() { Depth = 3, Velocity = 6 },
                    new StreamSection() { Depth = 2, Velocity = 7 },
                    new StreamSection() { Depth = 1, Velocity = 8 }
                }
            };

            double expectedVolume = 112.00;
            double actualVolume = Math.Round(_streamService.CalculateVolume(stream, false), 2);
            
            Assert.Equal(expectedVolume, actualVolume, 2);
        }

        [Fact]
        public void TestCalculateVolume_WithCorrectionFactor()
        {
            var stream = new Stream
            {
                Width = 7,
                Sections = new List<StreamSection>()
                {
                    new StreamSection() { Depth = 2, Velocity = 2 },
                    new StreamSection() { Depth = 8, Velocity = 3 },
                    new StreamSection() { Depth = 6, Velocity = 4 },
                    new StreamSection() { Depth = 4, Velocity = 5 },
                    new StreamSection() { Depth = 3, Velocity = 6 },
                    new StreamSection() { Depth = 2, Velocity = 7 },
                    new StreamSection() { Depth = 1, Velocity = 8 }
                }
            };

            double expectedVolume = 86.67;
            double actualVolume = Math.Round(_streamService.CalculateVolume(stream, true), 2);

            Assert.Equal(expectedVolume, actualVolume, 2);
        }

        [Fact]
        public void TestCalculateVolume_InvalidInput()
        {
            var stream = new Stream
            {
                Width = 7,
                Sections = new List<StreamSection>()
            };

            Assert.Throws<ArgumentException>(() => {
                _streamService.CalculateVolume(stream, false);
            });
        }

        [Fact]
        public void TestCalculateVolume_ZeroInput()
        {
            var stream = new Stream
            {
                Width = 7,
                Sections = new List<StreamSection>()
                {
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 },
                    new StreamSection() { Depth = 0, Velocity = 0 }
                }
            };

            double expectedVolume = 0.00;
            double actualVolume = Math.Round(_streamService.CalculateVolume(stream, false), 2);

            Assert.Equal(expectedVolume, actualVolume, 2);
        }

        [Fact]
        public void TestCalculateVolume_LargeInput()
        {
            var stream = new Stream
            {
                Width = 7,
                Sections = new List<StreamSection>()
                {
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 },
                    new StreamSection() { Depth = 1000000000, Velocity = 1000000000 }
                }
            };

            double expectedVolume = 7e+18;
            double actualVolume = _streamService.CalculateVolume(stream, false);

            Assert.Equal(expectedVolume, actualVolume, 2);
        }
    }
}
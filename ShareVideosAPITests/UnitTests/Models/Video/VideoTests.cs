using Bogus;
using FluentAssertions;
using ShareVideosAPI.Models.Category;
using ShareVideosAPI.Models.Video;
using ShareVideosAPITests.UnitTests.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareVideosAPITests.UnitTests.Models.Video
{
    public class VideoTests
    {
        [Theory(DisplayName = "CreateVideoInput_Invalid_ShouldReturn_InvalidModelState")]
        [Trait("CreateVideoInput", "Tests requests behaviors")]
        [MemberData(nameof(VideoTestData.GetInvalidCreateVideoInput),
            MemberType = typeof(VideoTestData))]
        public void CreateVideoInput_Invalid_ShouldReturn_InvalidModelState(CreateVideoInput input)
        {
            // Arrnge
            var context = new ValidationContext(input, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            // Act
            Validator.TryValidateObject(input, context, results, true);

            // Assert
            results.Count.Should().BeGreaterThan(0);
        }

        [Theory(DisplayName = "UpdateVideoInput_Invalid_ShouldReturn_InvalidModelState")]
        [Trait("UpdateVideoInput", "Tests requests behaviors")]
        [MemberData(nameof(VideoTestData.GetInvalidUpdateVideoInput),
             MemberType = typeof(VideoTestData))]
        public void UpdateVideoInput_Invalid_ShouldReturn_InvalidModelState(UpdateVideoInput input)
        {
            // Arrnge
            var context = new ValidationContext(input, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            // Act
            Validator.TryValidateObject(input, context, results, true);

            // Assert
            results.Count.Should().BeGreaterThan(0);
        }
    }

}

public class VideoTestData
{
    public static IEnumerable<object[]> GetInvalidCreateVideoInput()
    {
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = null;
                    }).Generate()
        };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = string.Empty;
                    }).Generate()
        };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(1);
                    }).Generate()
         };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(51);
                    }).Generate()
         };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Description = null;
                    }).Generate()
        };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Description = string.Empty;
                    }).Generate()
        };
        yield return new object[]
        {
                new Faker<CreateVideoInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Description = faker.Random.String(4);
                    }).Generate()
        };
    }

    public static IEnumerable<object[]> GetInvalidUpdateVideoInput()
    {
        yield return new object[]
        {
            new Faker<UpdateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Random.String(1);
                }).Generate()
         };
        yield return new object[]
        {
            new Faker<UpdateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Random.String(51);
                }).Generate()
         };
        yield return new object[]
        {
            new Faker<UpdateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Random.String(45);
                    target.Description = faker.Random.String(4);
                }).Generate()
        };
        yield return new object[]
       {
            new Faker<UpdateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Random.String(45);
                    target.Description = faker.Random.String(4);
                    target.Url = "invalid.url";
                }).Generate()
       };
    }
}

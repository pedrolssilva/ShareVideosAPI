using Bogus;
using FluentAssertions;
using ShareVideosAPI.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace ShareVideosAPITests.UnitTests.Models.Category
{
    public class CategoryTests
    {
        [Theory(DisplayName = "CreateCategoryInput_Invalid_ShouldReturn_InvalidModelState")]
        [Trait("CreateCategoryInput", "Tests requests behaviors")]
        [MemberData(nameof(CategoryTestData.GetInvalidCreateCategoryInput),
             MemberType = typeof(CategoryTestData))]
        public void CreateCategoryInput_Invalid_ShouldReturn_InvalidModelState(CreateCategoryInput input)
        {
            // Arrnge
            var context = new ValidationContext(input, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            // Act
            Validator.TryValidateObject(input, context, results, true);

            // Assert
            results.Count.Should().BeGreaterThan(0);
        }

        [Theory(DisplayName = "UpdateCategoryInput_Invalid_ShouldReturn_InvalidModelState")]
        [Trait("UpdateCategoryInput", "Tests requests behaviors")]
        [MemberData(nameof(CategoryTestData.GetInvalidUpdateCategoryInput),
             MemberType = typeof(CategoryTestData))]
        public void UpdateCategoryInput_Invalid_ShouldReturn_InvalidModelState(UpdateCategoryInput input)
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

    public class CategoryTestData
    {
        public static IEnumerable<object[]> GetInvalidCreateCategoryInput()
        {
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = null;
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = string.Empty;
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(1);
                    }).Generate()
             };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(51);
                    }).Generate()
             };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = null;
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = string.Empty;
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Random.String(8);
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Random.String(2);
                    }).Generate()
            };
            yield return new object[]
           {
                new Faker<CreateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Commerce.Color();
                    }).Generate()
           };
        }

        public static IEnumerable<object[]> GetInvalidUpdateCategoryInput()
        {
            yield return new object[]
            {
                new Faker<UpdateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(1);
                    }).Generate()
             };
            yield return new object[]
            {
                new Faker<UpdateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(51);
                    }).Generate()
             };
            yield return new object[]
            {
                new Faker<UpdateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Random.String(8);
                    }).Generate()
            };
            yield return new object[]
            {
                new Faker<UpdateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Random.String(2);
                    }).Generate()
            };
            yield return new object[]
           {
                new Faker<UpdateCategoryInput>()
                    .Rules((faker, target) =>
                    {
                        target.Title = faker.Random.String(45);
                        target.Color = faker.Commerce.Color();
                    }).Generate()
           };
        }
    }
}

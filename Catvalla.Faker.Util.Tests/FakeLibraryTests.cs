namespace Catvalla.Faker.Util.Tests
{
    public class FakeLibraryTests
    {
        [Fact]
        public void TypeTest()
        {
            string nameSpace = "Fake.Lib.Test";
            string objectTypeName = "FakeNameThing";
            List<string> propertyName = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten"
            };

            var retType = FakeLibrary.BuildFakeType(nameSpace, objectTypeName, propertyName.ToArray());

            Assert.NotNull(retType);
        }

        [Fact]
        public void TypeTestWithCorrections()
        {
            string nameSpace = "Fake.Lib.Test";
            string objectTypeName = "FakeNameThing";
            List<string> propertyName = new List<string>()
            {
                "COLUMN_ONE",
                "COLUMN_TWO",
                "COLUMN_THREE",
                "COLUMN_FOUR",
                "COLUMN_FIVE",
                "COLUMN_SIX",
                "COLUMN_SEVEN",
                "COLUMN_EIGHT",
                "COLUMN_NINE",
                "COLUMN_TEN"
            };

            var retType = FakeLibrary.BuildFakeTypeWithCorrections(nameSpace, objectTypeName, propertyName.ToArray());

            Assert.NotNull(retType);

            var properties = retType.GetProperties();

            bool propertyExists = false;

            for (int i = 1; i < propertyName.Count + 1; i++)
            {
                switch(i)
                {
                    case 1:
                        propertyExists = properties.Any(x => x.Name == "ColumnOne");
                        Assert.True(propertyExists);
                        break;
                    case 2:
                        propertyExists = properties.Any(x => x.Name == "ColumnTwo");
                        Assert.True(propertyExists);
                        break;
                    case 3:
                        propertyExists = properties.Any(x => x.Name == "ColumnThree");
                        Assert.True(propertyExists);
                        break;
                    case 4:
                        propertyExists = properties.Any(x => x.Name == "ColumnFour");
                        Assert.True(propertyExists);
                        break;
                    case 5:
                        propertyExists = properties.Any(x => x.Name == "ColumnFive");
                        Assert.True(propertyExists);
                        break;
                    case 6:
                        propertyExists = properties.Any(x => x.Name == "ColumnSix");
                        Assert.True(propertyExists);
                        break;
                    case 7:
                        propertyExists = properties.Any(x => x.Name == "ColumnSeven");
                        Assert.True(propertyExists);
                        break;
                    case 8:
                        propertyExists = properties.Any(x => x.Name == "ColumnEight");
                        Assert.True(propertyExists);
                        break;
                    case 9:
                        propertyExists = properties.Any(x => x.Name == "ColumnNine");
                        Assert.True(propertyExists);
                        break;
                    case 10:
                        propertyExists = properties.Any(x => x.Name == "ColumnTen");
                        Assert.True(propertyExists);
                        break;
                }
            }
        }

        [Fact]
        public void TypeTestWithValues()
        {
            string nameSpace = "Fake.Lib.Test";
            string objectTypeName = "FakeNameThing";
            List<string> propertyName = new List<string>()
            {
                "ColumnOne",
                "ColumnTwo",
                "ColumnThree",
                "ColumnFour",
                "ColumnFive",
                "ColumnSix",
                "ColumnSeven",
                "ColumnEight",
                "ColumnNine",
                "ColumnTen"
            };

            List<string> values = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten"
            };

            var retType = FakeLibrary.BuildFakeType(nameSpace, objectTypeName, propertyName.ToArray(), values.ToArray());

            Assert.NotNull(retType);

            var properties = retType.Item1.GetProperties();

            bool propertyExists = false;
            bool valueExists = false;

            for (int i = 1; i < propertyName.Count + 1; i++)
            {
                switch (i)
                {
                    case 1:
                        propertyExists = properties.Any(x => x.Name == "ColumnOne");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnOne").GetValue(retType.Item2) == "One";
                        Assert.True(valueExists);
                        break;
                    case 2:
                        propertyExists = properties.Any(x => x.Name == "ColumnTwo");
                        Assert.True(propertyExists);

                        valueExists = properties.First(x => x.Name == "ColumnTwo").GetValue(retType.Item2) == "Two";
                        Assert.True(valueExists);
                        break;
                    case 3:
                        propertyExists = properties.Any(x => x.Name == "ColumnThree");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnThree").GetValue(retType.Item2) == "Three";
                        Assert.True(valueExists);
                        break;
                    case 4:
                        propertyExists = properties.Any(x => x.Name == "ColumnFour");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnFour").GetValue(retType.Item2) == "Four";
                        Assert.True(valueExists);
                        break;
                    case 5:
                        propertyExists = properties.Any(x => x.Name == "ColumnFive");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnFive").GetValue(retType.Item2) == "Five";
                        Assert.True(valueExists);
                        break;
                    case 6:
                        propertyExists = properties.Any(x => x.Name == "ColumnSix");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnSix").GetValue(retType.Item2) == "Six";
                        Assert.True(valueExists);
                        break;
                    case 7:
                        propertyExists = properties.Any(x => x.Name == "ColumnSeven");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnSeven").GetValue(retType.Item2) == "Seven";
                        Assert.True(valueExists);
                        break;
                    case 8:
                        propertyExists = properties.Any(x => x.Name == "ColumnEight");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnEight").GetValue(retType.Item2) == "Eight";
                        Assert.True(valueExists);
                        break;
                    case 9:
                        propertyExists = properties.Any(x => x.Name == "ColumnNine");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnNine").GetValue(retType.Item2) == "Nine";
                        Assert.True(valueExists);
                        break;
                    case 10:
                        propertyExists = properties.Any(x => x.Name == "ColumnTen");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnTen").GetValue(retType.Item2) == "Ten";
                        Assert.True(valueExists);
                        break;
                }
            }
        }

        [Fact]
        public void TypeTestWithCorrectionAndValues()
        {
            string nameSpace = "Fake.Lib.Test";
            string objectTypeName = "FakeNameThing";
            List<string> propertyName = new List<string>()
            {
                "COLUMN_ONE",
                "COLUMN_TWO",
                "COLUMN_THREE",
                "COLUMN_FOUR",
                "COLUMN_FIVE",
                "COLUMN_SIX",
                "COLUMN_SEVEN",
                "COLUMN_EIGHT",
                "COLUMN_NINE",
                "COLUMN_TEN"
            };

            List<string> values = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten"
            };

            var retType = FakeLibrary.BuildFakeTypeWithCorrections(nameSpace, objectTypeName, propertyName.ToArray(), values.ToArray());

            Assert.NotNull(retType);

            var properties = retType.Item1.GetProperties();

            bool propertyExists = false;
            bool valueExists = false;

            for (int i = 1; i < propertyName.Count + 1; i++)
            {
                switch (i)
                {
                    case 1:
                        propertyExists = properties.Any(x => x.Name == "ColumnOne");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnOne").GetValue(retType.Item2) == "One";
                        Assert.True(valueExists);
                        break;
                    case 2:
                        propertyExists = properties.Any(x => x.Name == "ColumnTwo");
                        Assert.True(propertyExists);

                        valueExists = properties.First(x => x.Name == "ColumnTwo").GetValue(retType.Item2) == "Two";
                        Assert.True(valueExists);
                        break;
                    case 3:
                        propertyExists = properties.Any(x => x.Name == "ColumnThree");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnThree").GetValue(retType.Item2) == "Three";
                        Assert.True(valueExists);
                        break;
                    case 4:
                        propertyExists = properties.Any(x => x.Name == "ColumnFour");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnFour").GetValue(retType.Item2) == "Four";
                        Assert.True(valueExists);
                        break;
                    case 5:
                        propertyExists = properties.Any(x => x.Name == "ColumnFive");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnFive").GetValue(retType.Item2) == "Five";
                        Assert.True(valueExists);
                        break;
                    case 6:
                        propertyExists = properties.Any(x => x.Name == "ColumnSix");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnSix").GetValue(retType.Item2) == "Six";
                        Assert.True(valueExists);
                        break;
                    case 7:
                        propertyExists = properties.Any(x => x.Name == "ColumnSeven");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnSeven").GetValue(retType.Item2) == "Seven";
                        Assert.True(valueExists);
                        break;
                    case 8:
                        propertyExists = properties.Any(x => x.Name == "ColumnEight");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnEight").GetValue(retType.Item2) == "Eight";
                        Assert.True(valueExists);
                        break;
                    case 9:
                        propertyExists = properties.Any(x => x.Name == "ColumnNine");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnNine").GetValue(retType.Item2) == "Nine";
                        Assert.True(valueExists);
                        break;
                    case 10:
                        propertyExists = properties.Any(x => x.Name == "ColumnTen");
                        Assert.True(propertyExists);
                        valueExists = properties.First(x => x.Name == "ColumnTen").GetValue(retType.Item2) == "Ten";
                        Assert.True(valueExists);
                        break;
                }
            }
        }
    }
}

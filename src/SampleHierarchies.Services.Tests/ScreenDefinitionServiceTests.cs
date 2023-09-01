using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Services.Tests;
[TestClass]
public class ScreenDefinitionServiceTests
{
    [TestMethod]
    public void Load_ValidJsonFile_ReturnsScreenDefinition()
    {
        // Arrange
        IScreenDefinitionService service = new ScreenDefinitionService();
        string jsonFileName = "validScreenDefinition.json"; // Provide a valid JSON file path

        // Act
        ScreenDefinition result = service.Load(jsonFileName);

        // Assert
        Assert.IsNotNull(result);
        // Add more assertions as needed
    }

    [TestMethod]
    public void Save_ValidScreenDefinition_ReturnsTrue()
    {
        // Arrange
        IScreenDefinitionService service = new ScreenDefinitionService();
        ScreenDefinition screenDefinition = new ScreenDefinition();
        string jsonFileName = "validScreenDefinition.json"; // Provide a valid JSON file path

        // Act
        bool result = service.Save(screenDefinition, jsonFileName);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Load_InvalidJsonPath_ReturnsNull()
    {
        // Arrange
        IScreenDefinitionService service = new ScreenDefinitionService();
        string jsonFileName = "nonExistentFile.json"; // Provide a non-existent JSON file path

        // Act
        ScreenDefinition result = service.Load(jsonFileName);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void Save_InvalidScreenDefinition_ReturnsFalse()
    {
        // Arrange
        IScreenDefinitionService service = new ScreenDefinitionService();
        // Create an invalid screen definition (for example, with null properties)
        ScreenDefinition screenDefinition = new ScreenDefinition();
        string jsonFileName = ""; // Provide a valid JSON file path

        // Act
        bool result = service.Save(screenDefinition, jsonFileName);

        // Assert
        Assert.IsFalse(result);
    }
}

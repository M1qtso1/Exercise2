//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SampleHierarchies.Data;
//using SampleHierarchies.Interfaces.Services;
//using SampleHierarchies.Services;

//[TestClass]
//public class ScreenDefinitionServiceTests
//{
//    [TestMethod]
//    public void Load_ValidJsonFile_ReturnsScreenDefinition()
//    {
//        // Arrange
//        IScreenDefinitionService service = new ScreenDefinitionService();
//        string jsonFileName = "validScreenDefinition.json"; // Provide a valid JSON file path

//        // Act
//        ScreenDefinition result = service.Load(jsonFileName);

//        // Assert
//        Assert.IsNotNull(result);
//        // Add more assertions as needed
//    }

//    [TestMethod]
//    public void Save_ValidScreenDefinition_ReturnsTrue()
//    {
//        // Arrange
//        IScreenDefinitionService service = new ScreenDefinitionService();
//        ScreenDefinition screenDefinition = new ScreenDefinition();
//        string jsonFileName = "validScreenDefinition.json"; // Provide a valid JSON file path

//        // Act
//        bool result = service.Save(screenDefinition, jsonFileName);

//        // Assert
//        Assert.IsTrue(result);
//    }
//}

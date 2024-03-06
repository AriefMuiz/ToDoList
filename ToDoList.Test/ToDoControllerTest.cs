using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using ToDoList.Controllers;
using ToDoList.Interface;
using ToDoList.Request;
using ToDoList.Response;

namespace ToDoList.Tests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public async Task AddNewTaskAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var controller = new ToDoController(mockToDoService.Object);
            var request = new AddTaskRequest();
            mockToDoService.Setup(x => x.AddNewTaskAsync(It.IsAny<AddTaskRequest>()))
               .ReturnsAsync(new AddToDoResponse { Success = true });
            // Act
            var result = await controller.AddNewTaskAsync(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllToDoListAsync_ReturnsOk()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var controller = new ToDoController(mockToDoService.Object);

            mockToDoService.Setup(x => x.GetAllToDoListAsync())
                       .ReturnsAsync(new GetListToDoResponse { Success = true });

            // Act
            var result = await controller.GetAllToDoListAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetTaskByIdAsync_ValidId_ReturnsOk()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var controller = new ToDoController(mockToDoService.Object);
            var taskId = 1;

            mockToDoService.Setup(x => x.GetTaskByIdAsync(taskId))
                      .ReturnsAsync(new GetByIDToDoResponse { Success = true });

            // Act
            var result = await controller.GetTaskByIdAsync(taskId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateTaskByIdAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var controller = new ToDoController(mockToDoService.Object);
            var request = new UpdateTaskRequest();

            mockToDoService.Setup(x => x.UpdateTaskByIdAsync(request))
                      .ReturnsAsync(new UpdateByIDToDoResponse { Success = true });

            // Act
            var result = await controller.UpdateTaskByIdAsync(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteTaskByIdAsync_ValidId_ReturnsOk()
        {
            // Arrange
            var mockToDoService = new Mock<IToDoService>();
            var controller = new ToDoController(mockToDoService.Object);
            var taskId = 1;

            mockToDoService.Setup(x => x.DeleteTaskByIdAsync(taskId))
                       .ReturnsAsync(new DeleteByIDToDoResponse { Success = true });

            // Act
            var result = await controller.DeleteTaskByIdAsync(taskId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
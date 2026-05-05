using Xunit;
using MediaTracker.UI.Models;
using System.Linq;

namespace MediaTracker.Tests
{
    public class MediaManagerTests
    {
        private MediaManager manager;

        public MediaManagerTests()
        {
            manager = MediaManager.Instance;
            manager.MediaByType.Clear(); //Clear data before each test
        }

        [Fact]
        public void AddItem_AddsItemToDictionary()
        {
            // Arrange
            var item = new MediaItem { Name = "Test Movie", Type = "Movie" };
            //Act
            manager.AddItem(item);
            // Assert
            Assert.True(manager.MediaByType.ContainsKey("Movie"));
            Assert.Single(manager.MediaByType["Movie"]);
        }

        [Fact]
        public void RemoveItem_RemovesItem()
        {
            // Arrange
            var item = new MediaItem { Name = "Test", Type = "Movie" };
            manager.AddItem(item);
            //Act
            manager.RemoveItem(item);
            //Assert
            Assert.Empty(manager.MediaByType["Movie"]);
        }

        [Fact]
        public void ToggleCompleted_ChangesStatus()
        {
            // Arrange
            var item = new MediaItem { Name = "Test", Type = "Movie", IsCompleted = false };
            manager.AddItem(item);
            // Act
            manager.ToggleCompleted(item);
            // Assert
            Assert.True(item.IsCompleted);
        }

        [Fact]

        public void GetItemsByType_ReturnsCorrectItems()
        {
            // Arrange
            manager.AddItem(new MediaItem { Name = "A", Type = "Movie" });
            manager.AddItem(new MediaItem { Name = "B", Type = "Show" });
            //Act
            var movies = manager.GetItemsByType("Movie");
            //Result
            Assert.Single(movies);
            Assert.Equal("A", movies[0].Name);
        }

        [Fact]
        public void GetItemsByType_MultipleItems_ReturnsCorrectCount()
        {
            // Arrange

            manager.MediaByType.Clear();

            manager.AddItem(new MediaItem { Name = "A", Type = "Movie" });
            manager.AddItem(new MediaItem { Name = "B", Type = "Movie" });
            // Act
            var movies = manager.GetItemsByType("Movie");
            // Assert
            Assert.Equal(2, movies.Count);
        }

        [Fact]
        public void AddItem_DoesNothingIfTypeEmpty()
        {
            // Arrange
            var item = new MediaItem { Name = "Bad", Type = "" };
            // Act
            manager.AddItem(item);
            // Assert
            Assert.Empty(manager.MediaByType);
        }

        [Fact]

        public void RemoveItem_DoesNotCrashIfTypeMissing()
        {
            // Arrange
            var item = new MediaItem { Name = "Test", Type = "Movie" };
            // Act 
            manager.RemoveItem(item);
            // Assert
            Assert.True(true);
        }

        [Fact]

        public void ToggleCompleted_Twice_GoesBackToFalse()
        {
            // Arrange
            var item = new MediaItem { Name = "Test", Type = "Movie" };
            manager.AddItem(item);
            // Act
            manager.ToggleCompleted(item);
            manager.ToggleCompleted(item);
            // Assert
            Assert.False(item.IsCompleted);
        }

        [Fact]
        public void GetItemsByType_Unknown_ReturnsEmpty()
        {
            // Arrange - no setup needed

            // Act 
            var result = manager.GetItemsByType("Unknown");
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void AddMultipleItems_SameType_Works()
        {
           // Arrange
            var item1 = new MediaItem { Name = "A", Type = "Movie" };
            var item2 = new MediaItem { Name = "B", Type = "Movie" };

            // Act
            manager.AddItem(item1);
            manager.AddItem(item2);
            // Assert
            Assert.Equal(2, manager.MediaByType["Movie"].Count);
        }
    }
}



# Media Tracker
This is a simple media tracker app made with Blazor. It allows you to add movies, shows, or songs, mark them as completed, and delete them.
The data is saved to a json file so it stays the same when the app is closed and reopened. 

---

## Features
-- Add media items (movie, show, or song)
-- Mark items as completed or not completed
-- Delete items
-- Data is saved using a json file
-- Items are grouped by type using a dictionary

## How it works
-- The app uses two main classes:

- MediaItem: stores the name, type, and completion status
- MediaManager: handles the adding, removing, saving, and loading of items

## Data structure
A Dictionary<string, List<MediaItem> is used to store the items 

## File storage
The app saves data to a file called data.json

## Exception handling 
Try/catch is used when saving and loading the json file to prevent the app from crashing if anythign goes wrong.

## Running the App
1. open the project in Visual Studio
2. Run the app
3. Use the home page to add items
4. Go to library to view and manage items



# Task Management System
A Task Management System can be used to track tasks, their details, associated tags and assigned users. The system allows users to create, update, and manage tasks, including due dates and task statuses.

## Features
- **Task Creation & Management:** Users can create tasks with a name, status and description.
- **Task Details:** Each task can have additional details like due dates and descriptions.
- **Tags:** Tasks can be tagged with multiple tags to categorize them.
- **Users:** Tasks can be assigned to users who are responsible for completing them.
- **Sorting and filtering:** Tasks can be sorted by name or due date and filtered by name.

## Technologies Used
- **ASP.NET Core MVC:** Framework for building the web application.
- **SQLite:** Database to store task-related data.
- **Entity Framework Core:** ORM for database management.
- **HTML/CSS/JavaScript:** Front-end technologies to structure and style the web pages.

## Database Structure & Relationships
- **Task & TaskDetail (One-to-One):** Each task has one task detail containing additional information.
- **Task & User (Many-to-One):** Each task is assigned to one user, but a user can have multiple tasks.
- **Task & Tag (Many-to-Many):** Task can have multiple tags and a tag can be associated with multiple tasks.
<details closed>
<summary>Database diagram</summary>
<br>

![Database diagram](Documentation/databaseDiagram.png)
</details>

## Screenshots

### Task List View
<details closed>
<summary>Shows all tasks with sorting and filtering options.</summary>
<br>

![Database diagram](Documentation/taskList.png)
</details>

### Task Creation Form
<details closed>
<summary>Create a new task by filling out the form.</summary>
<br>

![Database diagram](Documentation/taskCreate.png)
</details>

### Task View/Edit Form
<details closed>
<summary>View and edit task details, including due dates, descriptions and assigned tags.</summary>
<br>

![Database diagram](Documentation/taskViewEdit.png)
</details>

### Users Page
<details closed>
<summary>Displays a list of all users with options to add, edit, or delete them.</summary>
<br>

![Database diagram](Documentation/users.png)
</details>

### Tags Page
<details closed>
<summary>Displays a list of all tags with options to add, edit, or delete them.</summary>
<br>

![Database diagram](Documentation/tags.png)
</details>

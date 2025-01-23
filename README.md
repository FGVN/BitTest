# File Upload and CSV Processing Application
This is a simple ASP.NET Core application designed to upload files, validate them, and process CSV files to store their data into a database.

## Features
- File Upload: Users can upload csv files through a simple UI.
- Data manipulation: Users can view, edit and deleted saved records.
- File Validation: Uploaded files are validated to ensure they meet specific criteria before being processed.
- CSV Parsing: The application reads and processes CSV files, loading the data into the database.
- Success/Error Messages: Clear success and error messages are displayed to users based on the result of the upload process.
- Scalable and Maintainable Design: The project uses separate services and validators for modularity and scalability.

## Technologies Used
- ASP.NET Core: Backend framework for building the application.
- Razor Pages/MVC: Used for creating the UI.
- FluentValidation: For validation of requests and files.
- CsvHelper: For reading csv table contents.
- Entity Framework Core: ORM used for database operations.

# Controllers
## TableController
- Purpose: Manages operations related to viewing, updating, and deleting records stored in the database.

 - Key Actions:

     - Index: Retrieves all records from the database and displays them in a table view.
     - Update: Updates a specific field of a record based on user input. Validates the field using CsvRecordValidator before saving changes to the database.
     - Delete: Deletes a specific record from the database by ID. Returns success or error responses.
       
## UploadController
- Purpose: Handles file upload functionality, including validation and processing of CSV files.
- Key Actions:
    - Index: Displays the file upload page.
    - Upload: Processes the uploaded file. Validates the file using UploadValidator, saves it to the server, and processes its content with the CsvLoader service to store data in the database.


# Getting started
1. Clone repository
```
git clone https://github.com/FGVN/BitTest
```
2. Restore dependencies
```
dotnet restore
```
3. Set your connection string in the appsettings.json
```
...
    "CsvDatabase": "Server=SERVER_IP;Database=DATABASE_NAME;Integrated Security=True;TrustServerCertificate=True;"
...
```
4. Apply migration
```
dotnet ef database update
```  
5. Run application
```
dotnet run
```

<h1>RapidReadr</h1>
<b>RapidReadr</b> is a speed reading web app with an ASP.NET backend, 
a React frontend, and PostgreSQL as the database. The app allows authenticated users to add PDF files to their account, 
and set a speed at which they can read the PDF one word at a time.
<h2>Why?</h2>
When I was 14 or 15 years old, I had a teacher who introduced us to a website designed to improve reading speed, where we read short stories using this exact technique. After each story, we took a brief quiz to test our understanding of the material. The goal was to enhance reading comprehension and speed. I found this method to be incredibly effective, and over the years, I’ve often found myself returning to the concept. As a result, I decided to create a web app that would allow me to apply this technique to text from my PDFs, whether for homework or leisure reading.
<h2>Goal</h2>
The goal of the web app is to allow people to read and understand their documents at a pace that they would not otherwise be able to achieve with normal reading techniques.

<h2>Reading Method Showcase</h2>

![Preview Web App Main Feature](https://github.com/user-attachments/assets/8173ce30-2a19-47b7-a4b4-b10b07fe685a)

<h2>Technologies Used</h2>

### Backend
- **ASP.NET Core (Web API)**
- **ASP.NET Identity** for authentication
- **PostgreSQL** as the database
- **Entity Framework Core** for ORM
- **NUnit** for unit testing
- **PDFPig** for PDF handling

### Frontend
- **React.js** with functional components
- **TypeScript** for static typing
- **React Router DOM** for navigation
- **Vite** for fast development and bundling
- **Bootstrap** for easy CSS styling

### Database
- **PostgreSQL** (local setup)

<h2>Setup Instructions</h2>

1. **Clone the repository:**

   First, clone the repository to your local machine:
   ```bash
   git clone https://github.com/thoni21/RapidReadr.git
   ```

2. **Navigate to the backend folder:**

   Go to the `RapidReadr.Server` directory:
   ```bash
   cd RapidReadr/RapidReadr.Server
   ```
   
3. **Update appsettings.json BasePath:**

   Update the `appsettings.json` file with the path to your file storage:
   ```
   "FileStorage": {
    "BasePath": "your/path/here"
     }
   ```
   
4. **Create .env file, and database connection string:**

   Create the `.env` file and create the connection string to match your local PostgreSQL database setup:
   ```
   DB_CONNECTION_STRING=Host=localhost; Database=rapid-readr; Username=your_username; Password=your_password
   ```

5. **Apply migrations to the database:**

   Run the following command to apply any pending migrations to your database:
   ```bash
   dotnet ef database update
   ```

6. **Run the project:**

   Finally, start the project by using the following command (still in the `RapidReadr.Server` directory):
   ```bash
   dotnet run --launch-profile https
   ```

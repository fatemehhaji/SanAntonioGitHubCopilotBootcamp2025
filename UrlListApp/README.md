# URL List Manager

A web application built with ASP.NET Core that helps you organize, manage, and share collections of URLs.

## Overview

URL List Manager allows users to create personal collections of web links that can be categorized, tagged, and shared with others. Whether you're researching a topic, planning a trip, or just keeping track of useful resources, this app makes organizing your web links simple and efficient.

## Key Features

- **Create URL Collections**: Organize related links into custom collections
- **Easy Organization**: Add notes, tags, and categories to each URL
- **Quick Access**: Search and filter your saved URLs
- **Share Your Lists**: Generate shareable links for your collections
- **Responsive Design**: Works on desktop and mobile devices

## Development

### Technologies Used
- ASP.NET Core 9.0
- Razor Pages
- Entity Framework Core
- Bootstrap 5
- SQLite (development) / SQL Server (production)

### Local Setup
1. Clone the repository
2. Navigate to the UrlListApp directory
3. Run `dotnet restore` to restore dependencies
4. Run `dotnet run` to start the application
5. Access the app at `https://localhost:5001`

### Database Migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Deployment

The application can be deployed to:
- Azure App Service
- Docker containers
- IIS on Windows Server

## Roadmap

- User authentication and personal URL collections
- Browser extension for easy URL saving
- API access for third-party integration
- Mobile app version

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

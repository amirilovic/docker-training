# Dockerfile

# Use the official .NET runtime image as a base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY ./out .

# Expose the port the app runs on
EXPOSE 8080

# Start the app
CMD ["dotnet", "Todos.Api.dll"]
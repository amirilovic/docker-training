# Exercise 2: Create a Dockerfile for the Todos.Api project

In this exercise, you will create a Dockerfile for the Todos.Api project. The Dockerfile will contain instructions to set up the environment for the Todos.Api project.

## Pre-requisites

- [Docker](https://docs.docker.com/get-docker/)
- [Git](https://git-scm.com/downloads)
- [Dotnet SDK](https://dotnet.microsoft.com/download)

## Getting Started

Run the project locally:

```bash
MY_NAME="{{YOUR_NAME}}" dotnet run
```

Open [http://localhost:5019/api/ping](http://localhost:5019/api/ping) in your browser to see the result. If you see `pong unknown` then you didn't set the `MY_NAME` environment variable correctly.

## Instructions

1. Build you app using the following command:

```bash
dotnet publish -c Release -o out
```

2. Create a new file called `Dockerfile` in the `Todos.Api` project directory.
3. Open the `Dockerfile` in a text editor and add the following content:

```Dockerfile
# Dockerfile

# Use the official .NET runtime image as a base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY ./out .

# Expose the port the app runs on
EXPOSE 8080

# Declare environment variables needed for your project to run
ENV MY_NAME="{{YOUR_NAME}}"

# Start the app
CMD ["dotnet", "Todos.Api.dll"]
```

4. Save the file and close the text editor.
5. Build the Docker image using the following command:

```bash
docker build -t todos-api .
```

6. List the Docker images using the following command:

```bash
docker images
```

> [!NOTE]
> You should see the `todos-api` image in the list and the size of the image, it's size should be around 255MB. You can also run `docker inspect todos-api` to see the image details.

7. Run the build image again to see the cached layers:

```bash
docker build -t todos-api .
```

> [!NOTE]
> You should see that the `COPY ./out .` layer is cached, this is because the `out` directory hasn't changed since the last build.

8. Run the Docker container using the following command:

```bash
docker run -d -p 8080:8080 todos-api
```

9. Open [http://localhost:8080/api/ping](http://localhost:8080/api/ping) in your browser to see the result.

10. Find the container ID using the following command:

```bash
docker ps
```

11. View the logs of the running container using the following command:

```bash
docker logs {{CONTAINER_ID}}
```

12. Open terminal in the running container using the following command:

```bash
docker exec -it {{CONTAINER_ID}} /bin/bash
```

13. List the files in the container using the following command:

```bash
ls
```

> [!NOTE]
> You should see the `Todos.Api.dll` file in the container.
>
> Type `exit` to exit the container terminal.

14. Stop the container using the following command:

```bash
docker stop {{CONTAINER_ID}}
```

15. Remove the container using the following command:

```bash
docker rm {{CONTAINER_ID}}
```

Optional:

- Add any endpoint to the `Todos.Api` you like and build a new docker image with `v2` tag. Run the new version of the container and test the new endpoint.
- Name the Dockerfile as `Dockerfile.v2` and try to build the image. See docker build help.
- Can I change `EXPOSE` port to something else? What happens if I change it to `8081`?
- How can I open terminal in the container?

## Summary

In this exercise, you learned how to create a Dockerfile for the Todos.Api project. You also learned how to build a Docker image, run a Docker container, view the logs of the running container, stop the container, and remove the container.

For more information on `Dockerfile` instructions, refer to the [official documentation](https://docs.docker.com/reference/dockerfile/).

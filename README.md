# Car Rental Application

## Overview

The Car Rental Application is a .NET 8-based project designed to manage car rentals. It includes functionalities for starting, ending, and managing car rentals, as well as calculating rental prices based on predefined pricing definitions.

## Features

- Start a rental
- End a rental
- Calculate rental prices
- Manage car and rental data
- Validation and error handling

## Technologies Used

- .NET 8
- C# 12.0
- Entity Framework Core in-memory database
- MediatR
- FluentResults
- NSubstitute (for unit testing)

## Project Structure

- **CarRental.Api**: Contains the API controllers and DTOs.
- **CarRental.Application**: Contains the application logic, use cases, and command/response models.
- **CarRental.Domain**: Contains the domain entities and interfaces.
- **CarRental.Infrastructure**: Contains the data access layer, including repositories and the DbContext.
- **CarRental.Application.UnitTests**: Contains unit tests for the application layer.

## Getting Started

### Prerequisites

    
# SIMA

SIMA is a backend-focused .NET application built to demonstrate
domain-driven design (DDD) and CQRS patterns in a real-world scenario.

## Tech Stack
- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- MediatR (CQRS)
- FluentValidation
- Redis Caching
- ImemoryCache
- Dapper

## Architecture
- Domain-Driven Design (Aggregates, Entities, Value Objects)
- CQRS (Command and Query separation)
- Clean Architecture
- Explicit application and domain boundaries

## Key Concepts
- Rich domain model
- Business rules enforced inside the domain layer
- Command handlers for state changes
- Query handlers optimized for reads

## Features
- RESTful API
- Validation and domain rules
- Centralized error handling
- Database migrations

## How to Run
1. Clone the repository
2. Configure database connection
3. Apply migrations
4. Run the API

## Purpose
This project was created to showcase advanced backend architecture
patterns and production-oriented design using .NET

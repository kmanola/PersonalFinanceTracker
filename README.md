# PersonalFinanceTracker

A modular personal finance tracking application built with .NET 10 and deployed to Azure.

## Overview
This application helps users track their personal finances with a modern, cloud-native architecture. It uses a modular monolith design pattern with separate bounded contexts for different domains.

## Architecture
The application follows a vertical slice architecture organized into modules:
*	Finance Module - Core financial tracking features including transactions and CSV import capabilities
*	Users Module - User management and authentication
*	Reporting Module - Financial reporting and analytics
*	Common Module - Shared functionality across modules

## Technology Stack
*	.NET 10 - Latest .NET framework
*	C# 14.0 - Modern C# language features
*	PostgreSQL - Relational database (Azure PostgreSQL Flexible Server)
*	MediatR - CQRS and mediator pattern implementation
*	FluentValidation - Request validation
*	CsvHelper - CSV import/export functionality
*	.NET Aspire - Cloud-native orchestration and deployment
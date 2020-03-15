# Academy Management System API

Author: Yeongjun Im  
Email: yeongjundev@gmail.com  
LinkedIn: https://www.linkedin.com/in/yeongjun-im-764256196/

# API Overview

1. 5 Domain models are implemented. (Student, Lesson, Enrolment, AttendanceSheet, Attendance)
2. Search, Orderby and Pagination query.
3. Student CRUD.
4. Lesson CRUD.
5. AttendanceSheet CRUD.
6. Attendance CRUD.

### Framework and Libraries

1. .Net Core 3.1
2. EntityFramework
3. AutoMapper

### Application Structure

1. Data Access Layer (DAL): UnitOfWork and repositories
2. Core Layer: Entity models, Data Transfer Objects (DTO), Pagination, Sorting, Searching helpers
3. Application Layer: Controllers
4. Service Layer: AttendanceManager, InvoiceExcelBuilder (To be added)

### Endpoints

Postman API document: https://documenter.getpostman.com/view/9426565/SzS2xo8Z?version=latest

###### Retrieve Enrolments

[GET] ~/api/enrolments/
Query params:

- Pagination: [CurrentPage, PageSize]
- Filtering:

<!-- # Database Schema

# Technical Decisions

1. The number of end-user is not many (the number of staff, maximum 10),
   Thus, multiple small size request is chosen rather than sole big size request.

# Business Logical Decisions -->

1. API is not aming for public API.
2. API mainly adopts RESTful convention.
3. API does not support CORS.
4. API does not support HTTPs request.
5. Currently no authorization added.

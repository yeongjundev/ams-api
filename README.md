# Academy Management System API

Author: Yeongjun Im  
Email: yeongjundev@gmail.com  
LinkedIn: https://www.linkedin.com/in/yeongjun-im-764256196/

# API Overview

1. API is not aming for public API.
2. API mainly adopts RESTful convention.
3. However, do not fully follow RESTful convention as it is private API.
4. API does not support CORS.
5. API does not support HTTPs request.

### Framework and Libraries

1. .Net Core 3.1
2. EntityFramework
3. AutoMapper

### Architect Diagram

### Endpoints

Examples: link to postman
Domain: http://localhost:5000/

###### Retrieve students

[GET] ~/api/students/
Query params:  
Pagination: [CurrentPage, PageSize]  
Filtering:

###### Retrieve Enrolments

[GET] ~/api/enrolments/
Query params:

- Pagination: [CurrentPage, PageSize]
- Filtering:

# Database Schema

# Technical Decisions

1. The number of end-user is not many (the number of staff, maximum 10),
   Thus, multiple small size request is chosen rather than sole big size request.

# Business Logical Decisions

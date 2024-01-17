# Software Engineer Technical Test

The objective of this test is to **solve the problem** whilst **demonstrating** your **technical** and **design capability**.

IRI maintains in its data warehouse a master list of all products that are sold across Australia. Each product has the fields:

| Field Name | Sample Data | DataType | Notes |
| --- | --- | --- | --- |
| ProductId | 18886 | int | Product unique identifier |
| ProductName | FISH OIL | string | Product name |

Australian retailers supply IRI with sales information that contains product data that IRI links to this master list. When stored in the data warehouse, the retailer product data has the fields:

| Field Name | Sample Data | DataType | Notes |
| --- | --- | --- | --- |
| ProductId | 18886 | int | Foreign key reference to master list product |
| RetailerName | Coles | string | Retailer name |
| RetailerProductCode | 93482745 | string | Retailers product identifier |
| RetailerProductCodeType | Barcode | string | Type of the code |
| DateReceived | 23/04/2006 | datetime | First receipt date of retailer product |

Note:

- All fields contain data and are required within the data warehouse; no NULLs.
- Multiple retailers may supply the same product.
- A single product may have multiple codes of different or same type.

| ProductId | RetailerName | RetailerProductCode | RetailerProductCodeType | DateReceived |
| --- | --- | --- | --- | --- |
| 18886 | DDS | 93482745 | Barcode | 16/05/2010 |
| 18886 | Woolworths | F8CE71964FAC90E59164FDB6AA19B10A | Woolworths Ref | 9/05/2017 |
| 18886 | Woolworths | 017E9562042C3E9F0E1D200A8C915052 | Woolworths Ref | 3/10/2017 |
| 18886 | Coles | 93482745 | Barcode | 23/04/2006 |

Functional Requirements:

Build a solution in C# that for each product from the master list, returns a distinct list of code types with their latest codes. An example of the expected output is:

| ProductId | ProductName | CodeType | Code |
| --- | --- | --- | --- |
| 18886 | FISH OIL | Barcode | 93482745 |
| 18886 | FISH OIL | Woolworths Ref | 017E9562042C3E9F0E1D200A8C915052 |

Non-functional Requirements:

- Show consideration for SOLID principals and use design patterns where appropriate.
- Show consideration for testability.
- Ideally upload to a GIT repository and check in regularly so we can see your progress.
- We would like to be able to run and test your solution without having to download any frameworks or database engines.

My Code Remarks:
- I have implemented a menu-based Console Application to demonstarte output of CSV files. 
- CSV files located in a shared folder - App.config to configure location of CSV files.
- Using Nuget packages like ChoETL to read/write CSV to JSON.
- Should consider caching objects fetched for better perf. 
- Added a few unit-test functions

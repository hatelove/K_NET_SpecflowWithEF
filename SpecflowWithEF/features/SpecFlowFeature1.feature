Feature: SpecFlowFeature1


Scenario: Query Joey's orders count
	Given Customers table exists
	| CustomerID | CompanyName |
	| Joey       | skilltree   |
	And Orders table exists
	| CustomerID | OrderDate  |
	| Joey       | 2017-03-18 |
	| Joey       | 2017-03-16 |
	| Joey       | 2017-03-14 |
	| Joey       | 2017-03-12 |
	When I want to know count of Joey's orders
	Then the result should be 4

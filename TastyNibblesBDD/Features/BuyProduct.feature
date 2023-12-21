Feature: BuyProduct

User buys a product

@End_to_End_2
Scenario Outline: User buys product
	Given User is on home page
	When User will type the '<searchtext>' in the search box
	Then Search results are loaded in the same page with '<searchtext>'
	When User clicks on '<productno>'
	Then Product page is loaded
	When User clicks on Add to Cart Button
	Then The product should be present inside the cart
	When User clicks on Checkout
	Then User will be redirected to the Checkout page
Examples: 
	| searchtext | productno |
	| Pickle     | 6         |
	| tuna     | 6         |
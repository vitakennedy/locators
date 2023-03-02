Feature: Login
As a: registered user
I want to: be able to send emails
So that: I can login to Proton email service 

Background: 
	Given an user navigates to the main page
	And an user clicks 'Sign in' button

@smoke
Scenario Outline:: Check that user is able to login 
	When an user submits '<username>' and '<password>'
	Then an user should be redirected to the 'Inbox' page 
	And un user should see 'Welcome' label
	
	Examples: 
 | login         | username              | password   |
 | correct login | vitaktoriia@proton.me | V_1234567* |

Scenario Outline:: Negative | Check that user is able to login 
	When an user submits '<username>' and '<password>'
	Then an user should not be redirected to the 'Inbox' page 
	But an user should stay on the 'Sign in' page
	
	Examples: 
 | login                              | username              | password   |
 | only special characters in login   | !@#$%^&*              | password   |
 | login in both upper and lower case | USERname              | password   |
 | 1 letter in password               | vitakennedy1          | 2          |
 | only numbers in password           | vitakennedy3          | 1234567    |
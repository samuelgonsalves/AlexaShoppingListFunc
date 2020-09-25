# AlexaShoppingListFunc
Alexa skill to manage a Trello grocery list.

## Features
  - Say "Alexa, open grocery board and add Red Baron pepperoni pizza".
    - Adds a Trello card (Red Baron Pepperoni Pizza) to grocery board.
  - Say "Alexa, get my grocery list"
    - Gets Trello cards off of your grocery board list.
    - Tells the user that there are more than 10 items and suggests to review board instead of reading them all out.
  - Say "Alexa, I'm done shopping"
    - Archives all items off of the grocery list
    - Responds excitedly "Way to go!"
  - Alexa asks if you want to be reminded to get your groceries.
    - Sets a reminder to remind you to get your groceries. 

## Usage

### Prerequisites
1. An AWS Lambda Function
2. Visual Studio with the AWS Toolkit extension installed
3. Environment variables in the AWS Lambda Function: 

      - **ApiKey** - your Trello API Key
      - **ApiToken** - your Trello API Token

4. Create a Trello board with at least 2 lists - one representing "To Buy" and another "Bought"
5. Find the ListIds and replace my ListIds with them. (I'm working on a way to fix this to avoid this step).

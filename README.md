# ExpenseTracker
Expense Tracker Application Summary
The Expense Tracker Application is a console-based tool designed to help users manage and track their finances efficiently. The project, built using C# and .NET Core, follows a Model-View-Controller (MVC) architecture to ensure clear separation of concerns and maintainable code. Key design patterns like Singleton, Factory, Bridge, and Composition are implemented to enhance the application's flexibility, scalability, and ease of use.

Key Features:

MVC Architecture: The application employs the MVC pattern, separating data structures (models), user interfaces (views), and logic (controllers) to improve maintainability and scalability.
Expense and Income Tracking: Users can add, edit, and view transactions, categorized as either income or expenses. Recurring transactions are supported, with flexible timeframes (daily, monthly, yearly).
Budget Management: The app allows users to set overall budgets and category-specific budgets, with real-time tracking of spending against these budgets.
Dynamic Menus: A stack-based menu system enables smooth navigation, with menus dynamically rendering data based on user interactions.
Data Persistence: Data is stored using JSON files, managed by a Database class following the Bridge pattern, allowing easy extension to other file formats in the future.
Testing and Reliability: The application underwent rigorous functional testing, ensuring all features work as intended, with identified bugs and issues documented for future improvement.
Design and Implementation Highlights:

Models: The application's core data structures are encapsulated in models like Budget, Transaction, and Category, designed to be loosely coupled for easier maintenance.
Controllers: Controllers manage data flow between models and views, ensuring data integrity and handling complex operations like budget updates and transaction summaries.
Views: The user interface is built around a calendar menu that allows users to interact with their financial data over different time periods, providing clear and detailed summaries.
Design Patterns: The use of patterns like Singleton (for Budget and Database management), Factory (for creating transactions), and Bridge (for flexible file handling) demonstrates a strong adherence to software design principles.
